using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Reflection;


using EFTransaction.Models;

namespace EFTransaction.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            var list = GetProductList();
            return View(list);
        }

        [HttpPost]
        public ActionResult DoTest() {

            UpdateProducts();

            return Redirect(Url.Action("Index"));
        }

        private void UpdateProducts() {

            //EFと同じ接続文字列を使ってSqlConnectionを作成。
            using (var sqlConnection = new SqlConnection(new MyEntities().Database.Connection.ConnectionString)) {

                //作成済みのSqlConnectionを使ってDbContextを作成。
                using (var context = new MyEntities(sqlConnection)) {

                    //EntityConnectionのOpenメソッドを呼ぶ。
                    IDbConnection entityConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
                    entityConnection.Open();

                    //EntityConnectionに対してトランザクションを開始。
                    using (var tr = entityConnection.BeginTransaction()) {
                        try {
                            //Reflectionを使ってEntityTransactionからSqlTransactionを取得する。
                            var sqlTran = (SqlTransaction)tr.GetType().InvokeMember("StoreTransaction",
                                                            BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.InvokeMethod
                                                            | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic, null, tr, new object[0]);

                            //1. ADO.NETで独自のSQL文を実行。
                            var sql = "UPDATE [Products] SET [Price] = [Price] * 1.1 ";
                            var cmd = sqlConnection.CreateCommand();
                            cmd.Transaction = sqlTran;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            //2. DbContextでProductsを更新。
                            var product = context.Products
                                                .FirstOrDefault();
                            product.Price /= 1.1M;
                            context.SaveChanges();

                            tr.Commit();

                        }
                        catch (Exception) {
                            tr.Rollback();
                            throw;
                        }
                    }
                }
            }


        }

        private IEnumerable<Product> GetProductList() {
            var context = new MyEntities();
            var list = context.Products
                            .OrderBy(m => m.Id)
                            .ToList();
            return list;
        }
    }
}

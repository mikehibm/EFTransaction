using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
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
            using (var context = new MyEntities()) {
                using (var cn = context.Database.Connection) {
                    cn.Open();
                    using (var tr = context.Database.Connection.BeginTransaction()) {
                        try {
                            var sql = "UPDATE [Products] SET [Price] = [Price] * 1.1 ";
                            var cmd = cn.CreateCommand();
                            cmd.Transaction = tr;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

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

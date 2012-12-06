using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Common;


namespace EFTransaction.Models {
    public class MyEntities : DbContext {

        public MyEntities() 
            : base("DefaultConnection") { 
        }

        public MyEntities(DbConnection existingConnection)  
            : base (existingConnection, false){ 
        }

        public DbSet<Product> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;


namespace EFTransaction.Models {
    public class MyEntities : DbContext {

        public MyEntities() 
            : base("DefaultConnection") { 
        }

        public DbSet<Product> Products { get; set; }
    }
}
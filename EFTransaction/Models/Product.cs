using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTransaction.Models {

    public class Product {

        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string ProductName { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
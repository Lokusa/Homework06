using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework06.Models.ViewModels
{
    public class order
    {
        public int orderid { get; set; }
        public product product { get; set; }
        public decimal total { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime orderdate { get; set; }
        public string category { get; set; }
    }
}
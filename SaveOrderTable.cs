using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradebot
{
    public class SaveOrderTable
    {
        [Key]
        public int id { get; set; }
        public string? symbol { get; set; }
        public decimal rsi { get; set; }
    }
}

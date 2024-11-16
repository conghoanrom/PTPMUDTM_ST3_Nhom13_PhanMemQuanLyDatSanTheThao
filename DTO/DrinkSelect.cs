using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DrinkSelect
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}

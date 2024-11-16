using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookingExport
    {
        public string BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Minutes { get; set; }
        public int? FieldId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string EmployeeId { get; set; }
        public int? TotalPrice { get; set; }
    }
}

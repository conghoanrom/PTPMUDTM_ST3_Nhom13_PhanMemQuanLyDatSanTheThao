using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public ChangePassword()
        {
            this.OldPassword = "";
            this.NewPassword = "";
            this.ConfirmPassword = "";
        }
    }
}

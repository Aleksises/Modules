using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityFramework
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        [StringLength(20)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(200)]
        public string CardHolderName { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

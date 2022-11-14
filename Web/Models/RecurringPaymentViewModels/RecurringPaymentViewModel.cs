using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwaWallet.Web.Models.RecurringPaymentViewModels
{
    public class RecurringPaymentViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }

        //[Required] // kategorie můze být smazána
        public string CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        //public Category Category { get; set; }

        //[Required] // typ platby může být smazán
        //public Guid PaymentTypeId { get; set; }
        public string PaymentTypeId { get; set; }
        public string PaymentTypeDescription { get; set; }
        //public PaymentType PaymentType { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public float Cost { get; set; }

        [Required]
        //public Guid IntervalId { get; set; }
        public string IntervalId { get; set; }
        public string IntervalDescription { get; set; }
        /// <summary>
        /// YYMMDD
        /// </summary>
        //public Interval Interval { get; set; }

        /// <summary>
        /// Příjem
        /// </summary>        
        public bool Earnings { get; set; } = false;
        public int Warranty { get; set; }
        public string Tag { get; set; }

        //[Required]
        //public DateTime DateCreated { get; set; } = DateTime.Now;

        //public DateTime LastUpdate { get; set; } = DateTime.Now.AddDays(-60);
        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Od kdy se budou trvalé platby generovat
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime NextDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Do kdy se budou trvalé platby generovat
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}

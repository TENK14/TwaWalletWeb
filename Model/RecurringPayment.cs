using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    public class RecurringPayment : IEntity
    {
        private const string TAG = "X:" + nameof(RecurringPayment);

        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public int IntervalId { get; set; }
        /// <summary>
        /// YYMMDD
        /// </summary>
        public Interval Interval { get; set; }

        /// <summary>
        /// Příjem
        /// </summary>        
        public bool Earnings { get; set; } = false;
        public int Warranty { get; set; }
        public string Tag { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime LastUpdate { get; set; } = DateTime.Now.AddDays(-60);
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Do kdy se budou trvalé platby generovat
        /// </summary>
        public DateTime? EndDate { get; set; }

        //public RecurringPayment IncludeObjects(IDataContext db)
        //{
        //    Log.Debug(TAG, nameof(IncludeObjects));

        //    this.Owner = db.Select<Owner, int>((o) => o.Id == this.OwnerId, (o) => o.Id).Result.FirstOrDefault();
        //    this.PaymentType = db.Select<PaymentType, int>((o) => o.Id == this.PaymentTypeId, (o) => o.Id).Result.FirstOrDefault();
        //    this.Category = db.Select<Category, int>((o) => o.Id == this.CategoryId, (o) => o.Id).Result.FirstOrDefault();
        //    this.Interval = db.Select<Interval, int>((o) => o.Id == this.IntervalId, (o) => o.Id).Result.FirstOrDefault();
        //    return this;
        //}

        public string ToString(string dateFormat)
        {
            //Log.Debug(TAG, nameof(ToString));

            return $"{nameof(Description)}: {Description}, \r"
                    + $"{nameof(Cost)}: {Cost}, \r"
                    + $"{ nameof(CategoryId)}: {CategoryId}, \r"
                    + $"{ nameof(Warranty)}: {Warranty}, \r"
                    + $"{ nameof(UserId)}: {UserId}, \r"
                    + $"{ nameof(PaymentTypeId)}: {PaymentTypeId}, \r"
                    + $"{ nameof(Earnings)}: {Earnings}, \r"
                    + $"{ nameof(DateCreated)}: {DateCreated.ToString(dateFormat)}\r"
                    + $"{ nameof(IntervalId)}: {IntervalId}, \r"
                    + $"{ nameof(EndDate)}: {EndDate?.ToString(dateFormat) ?? "infinity"}\r"
                    + $"{ nameof(LastUpdate)}: {LastUpdate.ToString(dateFormat)}\r"
                    + $"{ nameof(IsActive)}: {IsActive.ToString()}\r"
                    ;
        }

        public override string ToString()
        {
            //Log.Debug(TAG, nameof(ToString));

            return Description;
        }
    }
}

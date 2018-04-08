using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    /// <summary>
    /// Informace o typu platby (např. hotovostí nebo kartou)
    /// </summary>
    public class PaymentType : BaseEntity, IEntity
    {
        private const string TAG = "X:" + nameof(PaymentType);

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string PaymentTypeId { get; set; }
        
        [Required]
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Kadý uživatel si může definovat vlastní typy plateb.
        /// </summary>
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public override string ToString()
        {
            //Log.Debug(TAG, nameof(ToString));

            return Description;
        }
    }
}

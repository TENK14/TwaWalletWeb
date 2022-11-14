using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    public class Record : BaseEntity, IEntity
    {
        private const string TAG = "X:" + nameof(Record);

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public float Cost { get; set; }
        
        public string Description { get; set; }
                
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{dd.MM.yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;
        public int Warranty { get; set; } = 0;

        /// <summary>
        /// ForingKey
        /// </summary>
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// ForingKey
        /// </summary>
        //[Required] // typ platby může být smazán
        public string PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// Možnost filtrování (Ebay, Norsko, Francie,...)
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// true - příjem
        /// false - výdej
        /// </summary>
        public bool Earnings { get; set; } = false;

        public override string ToString()
        {
            //Log.Debug(TAG, nameof(ToString));

            return Description;
        }

        public string ToString(char delimiter, string dateFormat)
        {
            //Log.Debug(TAG, $"{nameof(ToString)} - {nameof(delimiter)}:{delimiter}, {nameof(dateFormat)}:{dateFormat}");

            //"01.10.2016"; "-170.0"; "obcerstveni"; "Vylety"; "0"; "Hotovost"; "Tom"
            return
                $"{Date.ToString(dateFormat)}{delimiter}"
                + $"{Cost}{delimiter}"
                + $"{Description}{delimiter}"
                //+ $"{CategoryId}{delimiter}"
                + $"{Category?.Description ?? String.Empty}{delimiter}"
                + $"{Warranty}{delimiter}"
                //+ $"{PaymentTypeId}{delimiter}"
                + $"{PaymentType?.Description ?? String.Empty}{delimiter}"
                //+ $"{OwnerId}{delimiter}"
                + $"{ApplicationUser.UserName}{delimiter}"
                + $"{Tag}{delimiter}"
                ;
        }
    }
}

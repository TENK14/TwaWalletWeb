using System.ComponentModel.DataAnnotations;

namespace TwaWallet.Web.Models.SummaryViewModels
{
    public class SummaryVM
    {
        public string Period { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Expenses { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Earnings { get; set; }
    }
}

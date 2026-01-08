using System.ComponentModel.DataAnnotations;

namespace TwaWallet.Web.Models.OverviewViewModels
{
    public class OverviewVM
    {
        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Expenses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Expenses { get; set; }

        [Display(Name = "Earnings")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Earnings { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Total { get; set; }
    }
}

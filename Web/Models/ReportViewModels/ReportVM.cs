using System.ComponentModel.DataAnnotations;

namespace TwaWallet.Web.Models.ReportViewModels
{
    public class ReportVM
    {
        [Display(Name = "Kategorie")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Výdaje")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Expenses { get; set; }

        [Display(Name = "Příjmy")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Earnings { get; set; }

        [Display(Name = "Celkem")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Total { get; set; }
    }
}



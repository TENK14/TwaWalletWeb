using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    /// <summary>
    /// Slouží za účelem kategorizace jednotlivých položek
    /// </summary>
    public class Category : BaseEntity, IEntity
    {
        private const string TAG = "X:" + nameof(Category);

        //public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}

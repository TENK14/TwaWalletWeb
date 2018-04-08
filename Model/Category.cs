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
    /// Slouží za účelem kategorizace jednotlivých položek
    /// </summary>
    public class Category : BaseEntity, IEntity
    {
        private const string TAG = "X:" + nameof(Category);

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string CategoryId { get; set; }

        //public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Kadý uživatel si může definovat vlastní kategorie.
        /// </summary>
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

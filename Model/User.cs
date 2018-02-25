using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    // TODO: V původní aplikaci se jednalo o Ownera
    /// <summary>
    /// V původní aplikaci se jednalo o Ownera
    /// </summary>
    public class User : IEntity
    {
        private const string TAG = "X:" + nameof(User);

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // TODO: bude pouze jeden přihlášený uživatel
        //public bool IsDefault { get; set; } = false; 

        public override string ToString()
        {
            //Log.Debug(TAG, nameof(ToString));

            return Name;
        }
    }
}

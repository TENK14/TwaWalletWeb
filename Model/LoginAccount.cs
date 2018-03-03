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
    public class LoginAccount : BaseEntity, IEntity
    {
        private const string TAG = "X:" + nameof(LoginAccount);

        //public int Id { get; set; }
        //[Required]
        //public string Name { get; set; }
        // TODO: bude pouze jeden přihlášený uživatel
        //public bool IsDefault { get; set; } = false; 

        [Required]
        public string Firstname
        {
            get;
            set;
        }

        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public string Username
        {
            get;
            set;
        }

        [Required]
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string VCode
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }
        public DateTime ModifyDate
        {
            get;
            set;
        }

        public override string ToString()
        {
            //Log.Debug(TAG, nameof(ToString));

            return Username;
        }
    }
}

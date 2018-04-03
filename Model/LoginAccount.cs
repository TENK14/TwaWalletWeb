//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    // TODO: V původní aplikaci se jednalo o Ownera
    /// <summary>
    /// V původní aplikaci se jednalo o Ownera
    /// LoginAccount je dostupný přes context.Users - DbSet<LoginAccount> Users je nastaven v IdentityDbContext >> AspNet.Identity.EntityFramework
    /// </summary>
    public class LoginAccount : IdentityUser,IEntity //BaseEntity, IEntity
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<LoginAccount> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

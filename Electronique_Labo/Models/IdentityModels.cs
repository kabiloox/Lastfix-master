using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Electronique_Labo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<profileimg> Profileimgs { get; set; }
        public DbSet<Expiriment> Expiriments { get; set; }
        public DbSet<Images> Imageses { get; set; }
        public DbSet<Outil> Outils { get; set; }
        public DbSet<Niveau> Niveaus { get; set; }
        public DbSet<Fillier> Filliers { get; set; }
        public DbSet<Secteur> Secteurs { get; set; }
        public DbSet<Conseil> Conseils { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<GoogleDriveFile> GoogleDriveFiles { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
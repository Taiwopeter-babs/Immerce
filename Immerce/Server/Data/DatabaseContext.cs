using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "The Hitchhiker's Guide to the Galaxy.",
                    Description = "The Hitchhiker's Guide to the Galaxy is a comedy science fiction franchise created by Douglas Adams.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                    Price = 9.99M
                },
                 new Product
                 {
                     Id = 2,
                     Title = "Ready Player One",
                     Description = "Ready Player One is a 2011 science fiction novel, and the debut novel of American author Ernest Cline. The story, set in a dystopia in 2045, follows protagonist Wade Watts on his search for an Easter egg in a worldwide virtual reality game, the discovery of which would lead him to inherit the game creator's fortune.",
                     ImageUrl = "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg",
                     Price = 5.99M
                 },
                 new Product
                 {
                     Id = 3,
                     Title = "Nineteen Eighty-Four",
                     Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell.",
                     ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/51/1984_first_edition_cover.jpg",
                     Price = 7.99M
                 }
                );
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}

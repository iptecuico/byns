namespace Codetecuico.Byns.Data.Migrations
{
    //internal sealed class Configuration : DbMigrationsConfiguration<BynsDbContext>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = true;
    //        AutomaticMigrationDataLossAllowed = true;
    //    }

    //    protected override void Seed(BynsDbContext context)
    //    {
    //        //Seed database
    //        if (!context.Users.Any())
    //        {
    //            GetInitialUsers().ForEach(data => context.Users.AddOrUpdate(data));
    //        }

    //        if (!context.Items.Any())
    //        {
    //            GetInitialItems().ForEach(data => context.Items.AddOrUpdate(data));
    //        }

    //        context.SaveChanges();
    //    }

    //    private static List<User> GetInitialUsers()
    //    {
    //        return new List<User>
    //        {
    //            new User {
    //                Id = 1,
    //                FirstName = "IP",
    //                LastName = "Tecuico",
    //                Username = "AmazonIP",
    //                ExternalId = "google-oauth2|114343767643441344703",
    //                DateRegistered = DateTime.Now,
    //                DateCreated = DateTime.Now,
    //                DateModified = DateTime.Now
    //            }
    //        };
    //    }
    //    private static List<Item> GetInitialItems()
    //    {
    //        return new List<Item>
    //        {
    //            new Item {  Id = 1,
    //                        Name = "Pro AngularJs",
    //                        Description = "Pro AngularJs Development",
    //                        Category = "Book - User Interface",
    //                        Currency = "Php",
    //                        Price = 800.00,
    //                        UserId = 1,
    //                        DatePosted = DateTime.Now,
    //                        DateCreated = DateTime.Now,
    //                        DateModified = DateTime.Now
    //            }
    //        };
    //    }
    //}
}
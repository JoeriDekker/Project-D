using WAMServer.Models;

namespace WAMServer.Data
{
    public class DBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WamDBContext>();
                if (context == null)
                {
                    return;
                }
                context.Database.EnsureCreated();
                
                // If there are no users
                if (!context.Users.Any())
                {
                    var address = new Address()
                    {
                        City = "Gouda",
                        Id = Guid.NewGuid(),
                        Street = "Karnemelksloot",
                        HouseNumber = "207"
                    };
                    var user = new User("Jan", "Waterpeil", "admin@email.com", "geheim");
                    user.AddressId = address.Id;
                    address.UserId = user.Id;
                    context.Addresses.Add(address);
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
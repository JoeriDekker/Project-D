using Microsoft.EntityFrameworkCore;
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
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS public.Address, public.Users, public.ControlPC");

                context.Database.EnsureCreated();

                // If there are no users
                if (!context.Users.Any())
                {
                    var address = new Address()
                    {
                        City = "Gouda",
                        Id = Guid.NewGuid(),
                        Street = "Karnemelksloot",
                        HouseNumber = "207",
                        Zip = "2806BE"
                    };

                    var user = new User("Jan", "Waterpeil", "admin@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword("geheim"));
                    var controlPC = new ControlPC(user.Id, "geheimPC", "123", "Uhhhhwaarvoorstaatdit?");
                    address.UserId = user.Id;
                    context.Users.Add(user);
                    context.SaveChanges();
                    context.Addresses.Add(address);
                    var editUser = context.Users.Where(_ => user.Id == _.Id).FirstOrDefault();
                    if (editUser != null)
                    {
                        editUser.AddressId = address.Id;
                    }
                    context.SaveChanges();
                    context.ControlPCs.Add(controlPC);
                    Console.WriteLine(context);
                    context.SaveChanges();
                }
            }
        }
    }
}
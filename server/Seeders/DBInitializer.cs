using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WAMServer.Models;

namespace WAMServer.Seeders
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

                    var user = new User("Jan", "Waterpeil", "admin@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword("geheim"))
                    {
                        IsConfirmed = true
                    };
                  
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
                    context.ControlPC.Add(controlPC);
                    Console.WriteLine(context);
                    // add a ground water log
                    var groundWaterLog = new GroundWaterLog(Guid.NewGuid().ToString(), "17/05/2024", Decimal.Parse("-1.33"));

                    context.GroundWaterLog.Add(groundWaterLog);


                    // add ground water logs
                    // By specifying a CultureInfo when parsing or formatting data, you ensure that the data is interpreted or presented according to the conventions of that specific culture. In cases where you want to ensure consistent behavior regardless of culture, you can use CultureInfo.InvariantCulture, which represents a culture-independent (invariant) format that is not tied to any particular culture's conventions.
                    var groundWaterLog = new List<GroundWaterLog>()
                    {
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("28/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.75")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("13/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.00")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("14/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.10")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("16/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.25")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("17/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.15")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("18/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.85")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("19/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.65")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("20/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.70")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("21/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.90")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("24/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.95")),
                        new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.ParseExact("25/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.05")),
                    };
                  
                    context.GroundWaterLog.AddRange(groundWaterLog);
                    context.SaveChanges();
                }
            }
        }
    }
}
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

                    var waterlevelsetting = new WaterLevelSettings()
                    {
                        Id = Guid.NewGuid(),
                        PoleHeight = decimal.Parse("-2.05"),
                        IdealHeight = decimal.Parse("-1.85")
                    };

                    context.WaterLevelSettings.Add(waterlevelsetting);
                    context.SaveChanges();


                    var user = new User("Jan", "Waterpeil", "admin@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword("geheim"))
                    {
                        IsConfirmed = true
                    };

                    waterlevelsetting.UserId = user.Id;
                    var controlPC = new ControlPC(user.Id, "geheimPC", "123", "Uhhhhwaarvoorstaatdit?");
                    address.UserId = user.Id;
                    context.Users.Add(user);
                    context.SaveChanges();
                    context.Addresses.Add(address);
                    var editUser = context.Users.Where(_ => user.Id == _.Id).FirstOrDefault();
                    if (editUser != null)
                    {
                        editUser.AddressId = address.Id;
                        editUser.WaterLevelSettingsId = waterlevelsetting.Id;
                    }
                    context.SaveChanges();
                    context.ControlPC.Add(controlPC);
                    // add a ground water log
                    // var groundWaterLog = new GroundWaterLog(Guid.NewGuid().ToString(), DateTime.Parse("17/05/2024"), Decimal.Parse("-1.33"));

                    // context.GroundWaterLog.Add(groundWaterLog);


                    context.ControlPC.Add(controlPC);
                    context.SaveChanges();

                    // add ground water logs
                    // By specifying a CultureInfo when parsing or formatting data, you ensure that the data is interpreted or presented according to the conventions of that specific culture. In cases where you want to ensure consistent behavior regardless of culture, you can use CultureInfo.InvariantCulture, which represents a culture-independent (invariant) format that is not tied to any particular culture's conventions.
                    var groundWaterLogs = new List<GroundWaterLog>()
                    {
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("13/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.00")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("14/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.10")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("16/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.25")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("17/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.15")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("18/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.85")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("19/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.65")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("20/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.70")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("21/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.90")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("24/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.95")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("25/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-2.05")),
                        new GroundWaterLog(controlPC.Id.ToString(), DateTime.ParseExact("28/05/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse("-1.75")),
                    };

                    context.GroundWaterLog.AddRange(groundWaterLogs);
                    context.SaveChanges();

                    //Add action types
                    var actionType1 = new ActionType("Pump", "Pump water to the ground");
                    var actionType2 = new ActionType("Stop Pump", "Stop pumping water to the ground");

                    context.ActionType.Add(actionType1);
                    context.ActionType.Add(actionType2);

                    context.SaveChanges();

                    //Add action logs
                    var actionlogs = new List<ActionLog>(){
                        new ActionLog(user.Id, actionType1.Id, DateTime.UtcNow),
                        new ActionLog(user.Id, actionType2.Id, DateTime.UtcNow),
                        new ActionLog(user.Id, actionType1.Id, DateTime.UtcNow),
                        new ActionLog(user.Id, actionType2.Id, DateTime.UtcNow),

                    };

                    context.ActionLog.AddRange(actionlogs);

                    context.SaveChanges();

                    // Add water storage
                    var WaterStorageList = new List<WaterStorage>(){
                        new (controlPC.Id, "Rain Barrel" , 56, "Kaden buurt", 1),
                        new (controlPC.Id, "Graywater Tank" , 243, "Kaden buurt", 2),
                        new (controlPC.Id, "Street Tank" , 556, "Kaden buurt", 3)
                    };

                    context.WaterStorage.AddRange(WaterStorageList);
                    context.SaveChanges();
                }
            }
        }
    }
}
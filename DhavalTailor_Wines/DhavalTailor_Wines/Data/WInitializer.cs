using DhavalTailor_Wines.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DhavalTailor_Wines.Data
{
    public class WInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            WinesContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<WinesContext>();

            try
            {
                //We can use this to delete the database and start fresh.
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                context.Database.Migrate();

                // Look for any winetypes.  Since we can't have Wines without wine types
                if (!context.Wine_Types.Any())
                {
                    context.Wine_Types.AddRange(
                        new Wine_Type { WineTypeName = "Red Wine" },
                        new Wine_Type { WineTypeName = "White Wine" }

                        );
                    //to save this all data
                    context.SaveChanges();
                }
                if (!context.Wines.Any())
                {
                    context.Wines.AddRange(
                        new Wine {WineName = "Cabernet Sauvignon", WineYear = "2019", WinePrice = 25.99, WineHarvest = new DateTime(2019, 10, 15), 
                            Wine_TypeID = context.Wine_Types.FirstOrDefault(d => d.WineTypeName == "Red Wine").ID },
                        // CustomerID = context.Customers.FirstOrDefault(d => d.FirstName == "Gregory" && d.LastName == "House").ID,
                        new Wine {WineName = "Chardonnay", WineYear = "2020", WinePrice = 19.99, WineHarvest = new DateTime(2020, 9, 20),
                            Wine_TypeID = context.Wine_Types.FirstOrDefault(d => d.WineTypeName == "White Wine").ID }

                        );
                    //to save this all data
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
            }
                //to call the seed data to run
                //In program.cs >
                //Seed Sata
                //CMInitializer.Seed(app);
            }
}

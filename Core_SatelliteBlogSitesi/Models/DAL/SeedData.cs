using Core_SatelliteBlogSitesi.Models.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ProjectContext context = serviceScope.ServiceProvider.GetService<ProjectContext>();

                //Proje her çalıştığında bekleyen migrationlara göre DB'yi güncelleyecek.
                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange
                        (
                            new Category() { Name= "Küp Uydular" },
                            new Category() { Name= "Küçük Uydular" },
                            new Category() { Name= "Uydular" },
                            new Category() { Name= "Haberleşme Uyduları" },
                            new Category() { Name= "Yer Gözlem Uyduları" },
                            new Category() { Name= "Roketler" },
                            new Category() { Name= "Askeri Amaçlı Roketler" },
                            new Category() { Name= "Uçaklar" },
                            new Category() { Name="Kargo Uçakları" },
                            new Category() { Name="Yolcu Uçakları" },
                            new Category() { Name= "Tek Pervaneli Uçaklar" },
                            new Category() { Name= "Çift Pervaneli Uçaklar" },
                            new Category() { Name= "Helikopter" },
                            new Category() { Name= "Kargo Helikopterleri" }
                        );
                }

                context.SaveChanges();
            }
        }
    }
}

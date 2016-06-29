using System.Data.SqlClient;
using PandyIt.VinylOrganizer.Labels;
using PandyIt.VinylOrganizer.Labels.Entities;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model;

namespace PandyIT.VinylOrganizer.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };

            using (var ctx = new VinylOrganizerDbContext(builder.ToString(), VinylOrganizerSeeder.GetSeeder()))
            using (var uow = new UnitOfWork(ctx))
            {
                var businessCtx = new VinylOrganizerBusinessContext(uow);

                //AddVinyls(businessCtx);
                PrintLabels(businessCtx);
            }
        }

        private static void PrintLabels(VinylOrganizerBusinessContext businessCtx)
        {
            var labelPage = new LabelPage(5, 2);
            var printer = new LabelPrinter(labelPage);

            foreach (var locationVinyl in businessCtx.GetAllLocationVinyl())
            {
                labelPage.AddLabel(locationVinyl);
            }

            printer.Print();
        }

        private static void AddVinyls(VinylOrganizerBusinessContext businessCtx)
        {
            businessCtx.AddVinyl(8633490);
            businessCtx.AddVinyl(8616691);
        }
    }
}

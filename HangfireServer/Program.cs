using Hangfire;

namespace HangfireServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=JINPC;Database=HangfireSample;Trusted_Connection=True;");

            using (var server = new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate(
                    "myrecurringjob",
                    () => Console.WriteLine("Recurring!"), Cron.Minutely);

                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}

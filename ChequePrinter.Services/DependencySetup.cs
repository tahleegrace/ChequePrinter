namespace ChequePrinter.Services
{
    /// <summary>
    /// Provides dependency setup for the ChequePrinter.Services library.
    /// </summary>
    public static class DependencySetup
    {
        public static void AddChequePrinterServices(this IServiceCollection services)
        {
            services.AddScoped<IChequePrintingService, ChequePrintingService>();
        }
    }
}
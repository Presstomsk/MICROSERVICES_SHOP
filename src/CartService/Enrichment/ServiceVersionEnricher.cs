namespace CartService.Enrichment
{
    using Serilog.Core;
    using Serilog.Events;

    public class ServiceVersionEnricher : ILogEventEnricher
    {
        private static readonly string _version;

        static ServiceVersionEnricher()
        {
            _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {            
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ServiceVersion", _version));
        }
    }
}
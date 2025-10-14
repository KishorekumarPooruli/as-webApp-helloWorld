using as_webApp_helloWorld.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace as_webApp_helloWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _telemetryClient.TrackTrace($"InstrumentationKey Registered: {_telemetryClient.TelemetryConfiguration.InstrumentationKey}", SeverityLevel.Critical);
        }

        public IActionResult Index()
        {
            _telemetryClient.TrackEvent("Index Page Viewed");
            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception("Testing");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

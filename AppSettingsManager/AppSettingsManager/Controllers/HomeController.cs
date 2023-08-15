using AppSettingsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AppSettingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private TwilioSettings _twilioSettings;
        private readonly IOptions<TwilioSettings> _twilioOptions;
        private readonly IOptions<SocialLoginSettings> _socialLoginSettings;

        public HomeController(ILogger<HomeController> logger, 
                              IConfiguration config, 
                              IOptions<TwilioSettings> twilioOptions,
                              IOptions<SocialLoginSettings> socialLoginSettings,
                              TwilioSettings twilioSettings)
        {
            _logger = logger;
            _config = config;
            _twilioOptions = twilioOptions;
            //_twilioSettings = new TwilioSettings();
            //config.GetSection("Twilio").Bind(_twilioSettings);
            _twilioSettings = twilioSettings;
            _socialLoginSettings = socialLoginSettings;
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _config.GetValue<string>("SendGridKey");
            //ViewBag.TwilioAuthToken = _config.GetSection("Twilio").GetValue<string>("AuthToken");
            //ViewBag.TwilioAccountSid = _config.GetValue<string>("Twilio:AccountSid");
            //ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;

            //IOptions
            //ViewBag.TwilioAuthToken = _twilioOptions.Value.AuthToken;
            //ViewBag.TwilioAccountSid = _twilioOptions.Value.AccountSid;
            //ViewBag.TwilioPhoneNumber = _twilioOptions.Value.PhoneNumber;

            //TwilioSettings
            ViewBag.TwilioAuthToken = _twilioSettings.AuthToken;
            ViewBag.TwilioAccountSid = _twilioSettings.AccountSid;
            ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;

            //ViewBag.ThirdLevelSetting = _config.GetValue<string>("FirstLeveSetting:SecondLevelSetting:ThirdLevelSetting");
            //ViewBag.ThirdLevelSetting = _config.GetSection("FirstLeveSetting")
            //                            .GetSection("SecondLevelSetting")
            //                            .GetValue<string>("ThirdLevelSetting");
            ViewBag.ThirdLevelSetting = _config.GetSection("FirstLeveSetting")
                                        .GetSection("SecondLevelSetting")
                                        .GetSection("ThirdLevelSetting").Value;

            ViewBag.FacebookKey = _socialLoginSettings.Value.FacebookSettings.Key;
            ViewBag.GoogleKey = _socialLoginSettings.Value.GoogleSettings.Key;

            ViewBag.ConnectionString = _config.GetConnectionString("AppSettingsManagerDb");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
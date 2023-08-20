using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TCKimlikValidationAttribute.Attributes.Models;
using TCKimlikValidationAttribute.Models;

namespace TCKimlikValidationAttribute.Attributes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new PersonModel());
        }

        [HttpPost]
        public IActionResult CheckTCKimlik(PersonModel model)
        {
            bool isValid = IsValidTCKimlik(model.TCKimlik);
            ViewBag.IsValid = isValid;

            return View("Index", model);
        }

        private bool IsValidTCKimlik(string tckn)
        {
            if (string.IsNullOrWhiteSpace(tckn) || tckn.Length != 11 || !tckn.All(char.IsDigit))
            {
                return false;
            }

            int[] digits = tckn.Select(c => int.Parse(c.ToString())).ToArray();

            if (digits[0] == 0)
            {
                return false;
            }

            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int tenthDigit = (oddSum * 7 - evenSum) % 10;
            int eleventhDigit = (digits[0] + digits[1] + digits[2] + digits[3] + digits[4] + digits[5] + digits[6] + digits[7] + digits[8] + digits[9]) % 10;

            return digits[9] == tenthDigit && digits[10] == eleventhDigit;
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

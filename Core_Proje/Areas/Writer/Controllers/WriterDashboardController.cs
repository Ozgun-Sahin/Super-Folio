using DataAccessLayer.Concrete;
using EntitiyLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    public class WriterDashboardController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public WriterDashboardController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.v = value.Name + " "+ value.Surname;

            //Weather API

            string apikey = "da0f36b2a77fbd727ca163a09d1c00fd";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=ankara&mode=xml&units=metric&appid=" + apikey;
            XDocument document = XDocument.Load(connection);
            ViewBag.v5 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            //burası çok gereksiz
            Context c = new Context();

            ViewBag.v1 = c.WriterMessages.Where(x=>x.Reciever==value.Email).Count();
            ViewBag.v2 = c.Announcements.Count();
            ViewBag.v3 = c.Users.Count();
            ViewBag.v4 = c.Skills.Count();

            return View();
        }
    }
}

/*
 https://api.openweathermap.org/data/2.5/weather?q=ankara&mode=xml&units=metric&appid=da0f36b2a77fbd727ca163a09d1c00fd
 */

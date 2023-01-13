using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PulsePLC_2._2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PulsePLC_2._2.Controllers
{
    public class HomeController : Controller
    {
        StorePulsePLCContext db = new StorePulsePLCContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]

        public string DeviceInfo()
        {
            //var device = db.Devices.Find(id);
            //if (device != null)
            //    return JsonConvert.SerializeObject(new { device.Id, device.Serial, device.ContractId });
            //else
            return "{ }";
        }

        public List<ContactListView> ContactList = new List<ContactListView>();
        public List<ContactListView> DevicesList = new List<ContactListView>();

        public IActionResult Index()
        {
            var item = db.Users.Count();
            ContactList = (from a in db.Contracts
                           select new ContactListView()
                           {
                               contactId = a.Id,
                               name = a.Name,
                               count = db.Devices.Count(s => s.ContractId == a.Id)

                           }).ToList();
            return View(ContactList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Devices(int Id)
        {

            DevicesList = (from a in db.Devices
                           where a.ContractId == Id
                           select new ContactListView()
                           {
                               devicesId = a.Id,
                               serialNumber = a.Serial,
                               contactId = Id
                           }).ToList();


            return View(DevicesList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PulsePLC_2._2;
using PulsePLC_2._2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PulsePLC.Controllers
{
    public class DeviceController : Controller
    {
        public StorePulsePLCContext db = new StorePulsePLCContext();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult GetContact(int id)
        {
            var device = db.Devices.Find(id);
            return View();
        }

        public string AddDevice(string serialNumber)
        {
            try
            {
                Devices device = new Devices();
                device.ContractId = 1;
                device.Serial = serialNumber;
                db.Devices.Add(device);
                db.SaveChanges();
                return JsonConvert.SerializeObject("Добавлен!");
            }
            catch
            {
                return JsonConvert.SerializeObject("Не удалось добавить!");
            }
        }

        public string DeleteDevice(int id)
        {
            try
            {
                Devices device = db.Devices.Find(id);
                db.Devices.Remove(device);
                db.SaveChanges();
                return JsonConvert.SerializeObject("Устройство успешно удалено!");
            }
            catch
            {
                return JsonConvert.SerializeObject("Не удалось удалить!");
            }
        }

        public string MoveDevice(int idDevice, int idContact)
        {
            try
            {
                Devices device = db.Devices.Find(idDevice);
                device.ContractId = idContact;
                db.Devices.Update(device);
                db.SaveChanges();

                return JsonConvert.SerializeObject("Устройство перемещено!");
            }
            catch
            {
                return JsonConvert.SerializeObject("Не удалось переместить!");
            }
        }

        public List<ContactListView> ContactList = new List<ContactListView>();
        public string GetListDevice(int id)
        {
            string html = "<select id='moveDevice' class='custom-select' style='width: 300px; margin: 3px;' onchange='MoveDevice()'><option>Переместить</option>";
            if (id == 1)
            {
                ContactList = (from a in db.Contracts
                               where a.Id != 1
                               select new ContactListView()
                               {
                                   contactId = a.Id,
                                   name = a.Name

                               }).ToList();
            }
            else if (id >= 2 || id <= 9)
            {
                ContactList = (from a in db.Contracts
                               where a.Id == 1
                               select new ContactListView()
                               {
                                   contactId = a.Id,
                                   name = a.Name

                               }).ToList();
            }
            foreach(var item in ContactList)
            {
                html += "<option value='" + item.contactId + "'>Переместить в " + item.name + "</option>";
            }
            html += "</select>";
            if(id == 9)
            {
                html += "<button id='deleteDevice' class='btn btn-danger' style='width: 300px; margin: 3px;' onclick='DeleteDevice()'>Удалить</button>";
            }
            //html += "<option value=" + key + ">" + data[key] + "</option>"
            return JsonConvert.SerializeObject(html);
        }
    }
}

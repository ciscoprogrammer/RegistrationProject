using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RegistrationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Internal;
using SystemMonitor;
using static System.Net.WebRequestMethods;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using System.Web.Mvc;
//using System.Web.Mvc;

namespace RegistrationProject.Controllers
{
    public class RegistrationController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly RegistrationDBContext _context;
        public RegistrationController(RegistrationDBContext context, IWebHostEnvironment hostEnvironment)
        {

            this._hostEnvironment = hostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            var registrations = _context.RegistTables.ToList();
            //foreach (var registration in registrations)
            //{
            //    registration.Gender = new SelectList(db.RegisTables.Select(x => x.Gender).Distinct().ToList());
            //    registration.Country = new MultiSelectList(db.RegisTables.Select(x => x.Country).Distinct().ToList());
            //}
            return View(registrations);
        }

        // GET: Registration/Create
        public IActionResult Create()
        {
            //var registration = new RegistTable


            ViewBag.Gender = new System.Web.Mvc.SelectList(_context.RegistTables.Select(x => x.Gender).Distinct().ToList());

            ViewBag.Country = new System.Web.Mvc.MultiSelectList(_context.RegistTables.Select(x => x.Country).Distinct().ToList()); ;


            return View();
        }


        //POST: Registration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegistTable registration, IFormFile ProfileImage)
        {
           // registration.ProfileImage = Convert.ToBase64String(ProfileImage);
            
                string name = Request.Form["Name"];
                string gender = Request.Form["Gender"];
                string email = Request.Form["EmailId"];
                string mobileNumber = Request.Form["MobileNumber"];
                //registration.ProfileImage = imageData.ToString();
                string genderValue = Request.Form["Gender"].ToString();
                registration.Gender = genderValue;

                // Get the selected country values from the form
                string[] countryValues = Request.Form["Country"];
                var countries = string.Join(",", countryValues);


                registration.Name = name;
                registration.Gender = gender;
                registration.EmailId = email;
                registration.MobileNumber = mobileNumber;
                registration.Country = countries;


                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    //string wwwRootPath = thisWebRootPath;
                    var fileName = Path.GetFileName(ProfileImage.Name);
                    var filePath = Path.Combine(wwwRootPath + "/Content/" + fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfileImage.CopyTo(fileStream);
                    }
                    using (var mes = new MemoryStream())                    {
                        ProfileImage.CopyTo(mes);
                        registration.ProfileImage = ProfileImage.FileName;
                    }
                }
                _context.Add(registration);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = _context.RegistTables.Find(id);
            

            ViewBag.Gender = new SelectList(_context.RegistTables.Select(r => r.Gender).Distinct().ToList(), registration?.Gender);
            //ViewBag.Gender = genderList;

            //var countriesList = new SelectList(_context.RegistTables.Select(r => r.Country).Distinct().ToList());
            ViewBag.Countries = new SelectList(_context.RegistTables.Select(r => r.Country).Distinct().ToList());
            //ViewBag.Countries = _context.RegistTables.Select(r => new SelectListItem { Value = r.Country, Text = r.Country }).Distinct().ToList();


            // Fetch the list of available countries
            //var countries = _context.RegistTables.Select(r => r.Country).Distinct().ToList();
            var countries = registration?.Country.Split(",").ToList();
            // ViewBag.Countries = new MultiSelectList(_context.RegistTables.Select(r => r.Country).Distinct().ToList(), "Country", "Country", countries);
            // Create a list of SelectListItem objects to be used as checkbox items



            // Create a SelectList object for the gender dropdown
            var allCountries = _context.RegistTables
            .Select(r => r.Country)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

            var selectedCountries = registration?.Country?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);



            ViewBag.Countries = allCountries.Select(c => new SelectListItem
            {
                Value = c,
                Text = c,
                Selected = selectedCountries != null && selectedCountries.Contains(c)
            });


            registration.ProfileImage = Path.Combine("/Content", registration.ProfileImage);

            return View(registration);
        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RegistTable registration, IFormFile ProfileImage)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (id != registration.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                //if (ProfileImage != null && ProfileImage.Length > 0)
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        ProfileImage.CopyTo(ms);
                //        registration.ProfileImage = Convert.ToBase64String(ms.ToArray());
                //    }
                //    registration.ProfileImage = (Directory.GetCurrentDirectory(), "wwwRootPath/Content", registration.ProfileImage);
                //}
                string name = Request.Form["Name"];
                string gender = Request.Form["Gender"];
                string email = Request.Form["EmailId"];
                string mobileNumber = Request.Form["MobileNumber"];
                //registration.ProfileImage = imageData.ToString();
                string genderValue = Request.Form["Gender"].ToString();
                registration.Gender = genderValue;

                // Get the selected country values from the form
                string[] countryValues = Request.Form["Country"];
                var countries = string.Join(",", countryValues);


                registration.Name = name;
                registration.Gender = gender;
                registration.EmailId = email;
                registration.MobileNumber = mobileNumber;
                registration.Country = countries;
                //registration.Country = string.Join(",", Country);

                if (ProfileImage != null && ProfileImage.Length > 0)
                {


                    var fileName = Path.GetFileName(ProfileImage.FileName);
                    var filePath = Path.Combine(wwwRootPath + "/Content/" + fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfileImage.CopyTo(fileStream);
                    }
                    using (var mes = new MemoryStream())
                    {
                        ProfileImage.CopyTo(mes);
                        registration.ProfileImage = ProfileImage.FileName;
                    }
                }
            

            
                _context.Update(registration);
                _context.SaveChanges();
            
           
            return RedirectToAction(nameof(Index));
        }

      

       [HttpPost]
       
        public ActionResult Delete(int id)
        {


            var registration = _context.RegistTables.FirstOrDefault(r => r.Id == id);

            if (registration != null)
            {
                _context.RegistTables.Remove(registration);
                _context.SaveChanges();
            }


            return RedirectToAction(nameof(Index));

            //_context.Attach(reg);
            //_context.Entry(reg).State = EntityState.Deleted;
            //_context.SaveChanges();
            //return RedirectToAction("Index");
        }

    }

}

    

    


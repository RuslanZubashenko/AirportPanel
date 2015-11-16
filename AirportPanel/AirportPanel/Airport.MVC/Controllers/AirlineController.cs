using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airport.UnitOfWork;
using Airport.MVC.Models;

namespace Airport.MVC.Controllers
{
    public class AirlineController : Controller, IDisposable 
    {
        AirlineUnit airlineUnit;

        public AirlineController()
        {
            airlineUnit = new AirlineUnit();
        }

        // GET: Airline
        public ActionResult Index()
        {
             
            var items = airlineUnit.AirlineRepository.Get();
            var model = ConvertToListOfMvcModel(items);
            return View(model);
        }

        List<AirlineModel> ConvertToListOfMvcModel(IEnumerable<DAL.Model.Entities.Airline> items)
        {
            var model = new List<AirlineModel>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    AirlineModel airline = ConvertToMvcModel(item);
                    model.Add(airline);
                }
            }
            return model;
        }

        static AirlineModel ConvertToMvcModel(DAL.Model.Entities.Airline item)
        {
            var airline = new AirlineModel();
            airline.Id = item.Id;
            airline.Name = item.Name;
            return airline;
        }

        // GET: Airline/Details/5
        public ActionResult Details(int id)
        {
            var item = airlineUnit.AirlineRepository.GetByID(id);
            AirlineModel model = ConvertToMvcModel(item);
            return View(model);
        }

        // GET: Airline/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new AirlineModel();
            return View(model);
        }

        // POST: Airline/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var model = new AirlineModel { Name = collection[nameof(AirlineModel.Name)] };
            try
            {
                if (ModelState.IsValid)
                {
                    airlineUnit.AirlineRepository.Insert(
                        new DAL.Model.Entities.Airline { Name = model.Name });
                    airlineUnit.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("",
                    $"Unable to save changes. Try again and if problem persist, see your system administrator ({ex.Message})");
            }
            return View(model);

        }

        // GET: Airline/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = airlineUnit.AirlineRepository.GetByID(id);
            var model = ConvertToMvcModel(item);
            return View(model);
        }

        // POST: Airline/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name")] AirlineModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    airlineUnit.AirlineRepository.Update(
                        new DAL.Model.Entities.Airline
                        {
                            Id = model.Id,
                            Name = model.Name
                        });
                    airlineUnit.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",
                    $@"Unable to update airline model in database.
                    Try again and if an error persist see administrator. ({ex.Message})");
            }
            return View(model);
        }

        // GET: Airline/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new AirlineModel();
            try
            {
                var item = airlineUnit.AirlineRepository.GetByID(id);
                model = ConvertToMvcModel(item);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("",
                    $"Unable to find item to delete. ({ex.Message})");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Airline/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id, Name")]AirlineModel model)
        {
            try
            {
                airlineUnit.AirlineRepository.Delete(model.Id);
                airlineUnit.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete item.");
                return View(model);
            }
        }

        protected bool disposed;
        protected new void Dispose(bool disposing)
        {
            if (!disposed)
            {
                airlineUnit.Dispose();
            }
        }    

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

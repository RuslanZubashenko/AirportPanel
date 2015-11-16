using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airport.DAL.Model.Entities;
using Airport.UnitOfWork;
using Airport.MVC.Models;

namespace Airport.MVC.Controllers
{
    public class PortController : Controller
    {
        PortUnit portUnit;

        public PortController()
        {
            portUnit = new PortUnit();
        }

        // GET: Port
        public ActionResult Index()
        {
            var items = portUnit.PortRepository.Get();
            var model = ConverToListMvcModels(items);
            return View(model);
        }

        List<PortModel> ConverToListMvcModels(IEnumerable<Port> items)
        {
            var model = new List<PortModel>();
            foreach (var item in items)
            {
                PortModel port = ConvertToMvcModel(item);
                model.Add(port);
            }
            return model;
        }

        static PortModel ConvertToMvcModel(Port item)
        {
            var port = new PortModel();
            port.Id = item.Id;
            port.Name = item.Name;
            return port;
        }

        // GET: Port/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var item = portUnit.PortRepository.GetByID(id);
                var model = ConvertToMvcModel(item);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",
                    $"Unable to find item to see details. ({ex.Message})");
                return RedirectToAction("Index");

            }
        }

        // GET: Port/Create
        public ActionResult Create()
        {
            var model = new PortModel();
            return View(model);
        }

        // POST: Port/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name")] PortModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    portUnit.PortRepository.Insert(
                        new Port
                        {
                            Name = model.Name
                        });
                    portUnit.Save();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",
                    $"Unable to save item. Try again or see your administrator.({ex.Message})");
            }
            return View(model);
        }

        // GET: Port/Edit/5
        public ActionResult Edit(int Id)
        {
            var model = new PortModel();
            try
            {
                var item = portUnit.PortRepository.GetByID(Id);
                model = ConvertToMvcModel(item);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("",
                    $"Unable to find item to edit. ({ex.Message})");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Port/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name")] PortModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    portUnit.PortRepository.Update(new Port
                    {
                        Id = model.Id,
                        Name = model.Name
                    });
                    portUnit.Save();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("",
                    $"Unable to edit item. ({ex.Message})");
            }
            return View(model);
        }

        // GET: Port/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new PortModel();
            try
            {
                var item = portUnit.PortRepository.GetByID(id);
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

        // POST: Port/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id, Name")]PortModel model)
        {
            try
            {
                portUnit.PortRepository.Delete(model.Id);
                portUnit.Save();
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
                portUnit.Dispose();
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

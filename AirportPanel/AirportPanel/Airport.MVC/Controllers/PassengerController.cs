using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airport.DAL.Model.Entities;
using Airport.MVC.Models;
using Airport.UnitOfWork;

namespace Airport.MVC.Controllers
{
    public class PassengerController : Controller
    {
        PassengerUnit passengerUnit;
        public PassengerController()
        {
            passengerUnit = new PassengerUnit();
        }
        // GET: Passenger
        public ActionResult Index()
        {
            var model = new List<PassengerModel>();
            var items = passengerUnit.PassengerRepository
                .Get(includeProperties: "Flight");
            if (items != null)
            {
                foreach (var item in items)
                {
                    model.Add(ConvertToMvcModel(item));
                }
            }
            return View(model);
        }

        private PassengerModel ConvertToMvcModel(Passenger item)
        {
            return new PassengerModel
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Passport = item.Passport,
                Nationality = item.Nationality,
                FlightId = item.FlightId,
                DateOfBirthday = item.DateOfBirthday,
                Sex = (Models.Enums.Sex)item.Sex,
                PlaceClass = (Models.Enums.PlaceClass)item.PlaceClass,
                FlightNumber = item.Flight.FlightNumber
            };
        }

        // GET: Passenger/Details/5
        public ActionResult Details(int id )
        {
            return View(ConvertToMvcModel(passengerUnit
                    .PassengerRepository.GetByID(id)));
        }

        // GET: Passenger/Create
        public ActionResult Create(int flightId)
        {
            var model = new PassengerModel();
            model.FlightId = flightId;
            using (var flightUnit = new FlightUnit())
            {
                model.FlightNumber = flightUnit.FlightRepository.GetByID(flightId).FlightNumber;
            }
            model.FlightNumber = flightId.ToString();
            return View(model);
        }

        // POST: Passenger/Create
        [HttpPost]
        public ActionResult Create(
            [Bind(Include = "FlightId, FirstName, LastName, Nationality, Passport, Sex, PlaceClass, DateOfBirthday")]
            PassengerModel passenger)
        {
            try
            {
                this.passengerUnit.PassengerRepository.Insert(
                    ConvertToDALModel(passenger));
                passengerUnit.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(passenger);
            }
        }

        private static Passenger ConvertToDALModel(PassengerModel passenger)
        {
            return new Passenger
            {
                FlightId = passenger.FlightId,
                FirstName = passenger.FirstName,
                DateOfBirthday = passenger.DateOfBirthday,
                LastName = passenger.LastName,
                Nationality = passenger.Nationality,
                Passport = passenger.Passport,
                PlaceClass = (DAL.Model.Enums.PlaceClass)passenger.PlaceClass,
                Sex = (DAL.Model.Enums.Sex)passenger.Sex
            };
        }

        // GET: Passenger/Edit/5
        public ActionResult Edit(int id)
        {

            try
            {
                return View(ConvertToMvcModel(passengerUnit
                    .PassengerRepository.GetByID(id)));
            }
            catch (Exception ex)
            {            
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Passenger/Edit/5
        [HttpPost]
        public ActionResult Edit(
            [Bind(Include = "Id, FlightId, FirstName, LastName, Nationality, Passport, Sex, PlaceClass, DateOfBirthday")]
            PassengerModel passenger)
        {
            try
            {
                passengerUnit.PassengerRepository.Update(
                    ConvertToDALModel(passenger));
                passengerUnit.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passenger/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ConvertToMvcModel(passengerUnit
                    .PassengerRepository.GetByID(id)));
        }

        // POST: Passenger/Delete/5
        [HttpPost]
        public ActionResult Delete(
            [Bind(Include = "Id, FlightId, FirstName, LastName, Nationality, Passport, Sex, PlaceClass, DateOfBirthday")]
            PassengerModel passenger)
        {
            try
            {
                passengerUnit.PassengerRepository
                    .Delete(passenger.Id);
                passengerUnit.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(passenger);
            }
        }
        protected bool disposed;
        protected new void Dispose(bool disposing)
        {
            if (!disposed)
            {
                passengerUnit.Dispose();
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

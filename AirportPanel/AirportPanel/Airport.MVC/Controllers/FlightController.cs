using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Airport.UnitOfWork;
using Airport.MVC.Models;
using Airport.MVC.Models.Enums;

namespace Airport.MVC.Controllers
{
    public class FlightController : Controller
    {
        FlightUnit flightUnit;
        public FlightController()
        {
            flightUnit = new FlightUnit();
        }
        // GET: Flight
        public ActionResult Index()
        {
            var model = new List<FlightModel>();
            try
            {
                var items = flightUnit.FlightRepository.Get();
                model = ConvertToListMvcModel(items);
            }
            catch
            {
                ModelState.AddModelError("",
                    "Unable to get flights. Try again or contact to administrator.");
            }
            return View(model);
        }

        List<FlightModel> ConvertToListMvcModel(IEnumerable<DAL.Model.Entities.Flight> items)
        {
            var model = new List<FlightModel>();

            foreach (var item in items)
            {
                FlightModel flight = ConvertToMvcModel(item);
                model.Add(flight);
            }
            return model;
        }

        FlightModel ConvertToMvcModel(DAL.Model.Entities.Flight item)
        {
            var flight = new FlightModel();
            flight.AirlineId = item.AirlineId;
            flight.ArrivalDate = item.ArrivalDate;
            var airline = new AirlineModel();
            using (var airlineUnit = new AirlineUnit())
            {
                var airlineItem = airlineUnit.AirlineRepository.GetByID(item.AirlineId);
                airline.Id = airlineItem.Id;
                airline.Name = airlineItem.Name;
            }
            flight.Airline = airline;
            flight.ArrivalPortId = item.ArrivalPortId;
            var arrivalPort = new PortModel();
            var departurePort = new PortModel();
            using (var portUnit = new PortUnit())
            {
                var portItem = portUnit.PortRepository.GetByID(item.Id);
                arrivalPort.Id = portItem.Id;
                arrivalPort.Name = portItem.Name;

                portItem = portUnit.PortRepository.GetByID(item.DeparturePortId);
                departurePort.Id = portItem.Id;
                departurePort.Name = portItem.Name;
            }

            flight.ArrivalPort = arrivalPort;
            flight.DepartureDate = item.DepartureDate;
            flight.DeparturePortId = item.DeparturePortId;
            flight.DeparturePort = departurePort;
            flight.FlightNumber = item.FlightNumber;
            flight.Gate = item.Gate;
            flight.Id = item.Id;
            flight.PlaceQty = item.PlaceQty;
            flight.Status = (FlightStatus)item.Status;
            flight.Terminal = item.Terminal;
            return flight;
        }

        // GET: Flight/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var item = flightUnit.FlightRepository.GetByID(id);
                var model = ConvertToMvcModel(item);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                    $"Unable to open details to id {id}");
            }
            return RedirectToAction("Index");
        }

        // GET: Flight/Create
        public ActionResult Create()
        {
            var model = new FlightModel();
            PopulateDropDownLists(model.AirlineId, model.ArrivalPortId, model.DeparturePortId);

            return View(model);
        }

        void PopulateDropDownLists(object selectedAirline = null, 
            object selectedArrivalPort = null, 
            object selectedDeparturePort = null)
        {
            // Select list for Departure port
            var DeparturePortModels = new List<PortModel>();
            using (var portUnit = new PortUnit())
            {
                var items = portUnit.PortRepository.Get();
                foreach (var item in items)
                {
                    DeparturePortModels.Add(new PortModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });                
                }
            }
            ViewData["DeparturePortId"] = new SelectList(DeparturePortModels, "Id", "Name", selectedDeparturePort);

            // Select list for Arrival port
            var ArrivalPortModels = new List<PortModel>();
            using (var portUnit = new PortUnit())
            {
                var items = portUnit.PortRepository.Get();
                foreach (var item in items)
                {
                    ArrivalPortModels.Add(new PortModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
            ViewData["ArrivalPortId"] = new SelectList(ArrivalPortModels, "Id", "Name", selectedArrivalPort);

            // Select list for Airline
            var airlines = new List<AirlineModel>();
            using (var airlineUnit = new AirlineUnit())
            {
                var items = airlineUnit.AirlineRepository.Get();
                foreach (var item in items)
                {
                    airlines.Add(new AirlineModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
            ViewData["AirlineId"] = new SelectList(airlines, "Id", "Name", selectedAirline);
        }


        // POST: Flight/Create
        [HttpPost]
        public ActionResult Create(
            [Bind(Include = "Id, FlightNumber,AirlineId, ArrivalPortId, DeparturePortId, ArrivalDate, DepartureDate, Terminal, Gate, PlaceQty, Status")]
            FlightModel flight)
        {
            try
            {
                flightUnit.FlightRepository.Insert(
                    ConvertToFlight(flight));
                flightUnit.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateDropDownLists(flight.AirlineId, flight.ArrivalPortId, flight.DeparturePortId);
                return View(flight);
            }
        }

        private static DAL.Model.Entities.Flight ConvertToFlight(FlightModel flight)
        {
            return new DAL.Model.Entities.Flight
            {
                FlightNumber = flight.FlightNumber,
                Id = flight.Id,
                AirlineId = flight.AirlineId,
                ArrivalDate = flight.ArrivalDate,
                ArrivalPortId = flight.ArrivalPortId,
                DepartureDate = flight.DepartureDate,
                DeparturePortId = flight.DeparturePortId,
                Gate = flight.Gate,
                Terminal = flight.Terminal,
                Status = (DAL.Model.Enums.FlightStatus)flight.Status,
                PlaceQty = flight.PlaceQty

            };
        }

        // GET: Flight/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new FlightModel();
            try
            {
                var item = flightUnit.FlightRepository.GetByID(id);
                model = ConvertToMvcModel(item);
            }
            catch
            {
                ModelState.AddModelError("",
                    "Unable to edit.");
                
            }
            PopulateDropDownLists(model.AirlineId, model.ArrivalPortId, model.DeparturePortId);
            return View(model);
        }

        // POST: Flight/Edit/5
        [HttpPost]
        public ActionResult Edit(
            [Bind(Include = "Id, FlightNumber,AirlineId, ArrivalPortId, DeparturePortId, ArrivalDate, DepartureDate, Terminal, Gate, PlaceQty, Status")]
            FlightModel flight)
        {
            try
            {
                var item = ConvertToFlight(flight);
                flightUnit.FlightRepository.Update(item);
                flightUnit.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateDropDownLists(flight.AirlineId, flight.ArrivalPortId, flight.DeparturePortId);
                return View(flight);
            }
        }

        // GET: Flight/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new FlightModel();
            try
            {
                var item = flightUnit.FlightRepository.GetByID(id);
                model = ConvertToMvcModel(item);
            }
            catch
            {
                ModelState.AddModelError("",
                    "Unable to delete.");

            }
            return View(model);
        }

        // POST: Flight/Delete/5
        [HttpPost]
        public ActionResult Delete(
            [Bind(Include = "Id, FlightNumber,AirlineId, ArrivalPortId, DeparturePortId, ArrivalDate, DepartureDate, Terminal, Gate, PlaceQty, Status")]
            FlightModel flight)
        {
            try
            {                
                flightUnit.FlightRepository.Delete(flight.Id);
                flightUnit.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(flight);
            }
        }
        protected bool disposed;
        protected new void Dispose(bool disposing)
        {
            if (!disposed)
            {
                flightUnit.Dispose();
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

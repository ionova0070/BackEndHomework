using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstProject.Controllers
{
    public class CarController : ApiController
    {
        CarManager _carManager;

        public CarController()
        {
            _carManager = CarManagerSingleton.CarManager();
        }

        [HttpGet]
        [Route("get/{id}")]
        public IHttpActionResult GetCarById(int id)
        {
            return Ok(_carManager.GetCarById(id));
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddCar([FromBody] Car car)
        {
            _carManager.Add(car);
            return Ok(_carManager.Count);
        }

        [HttpPut]
        [Route("put/{id}")]
        public IHttpActionResult EditCarById([FromBody] Car car, int id)
        {
            _carManager.Edit(id, car);
            return Ok();
        }
    }

    public class Car
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Car(int id, string name, int price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
    }

    public static class CarManagerSingleton
    {
        private static CarManager _carManager;

        public static CarManager CarManager()
        {
            if (_carManager == null)
            {
                _carManager = new CarManager();
                _carManager.Add(new Car(1, "first car", 100));
                _carManager.Add(new Car(2, "second car", 200));
            }
            return _carManager;
        }
    }

    public class CarManager
    {
        private HashSet<Car> _cars;
        public int Count;

        public CarManager()
        {
            _cars = new HashSet<Car>();
            Count = _cars.Count;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
            Count++;
        }

        public void Edit(int id, Car newCar)
        {
            _cars.RemoveWhere(car => car.ID.Equals(id));
            Add(newCar);
        }

        public Car GetCarById(int id)
        {
            return _cars.SingleOrDefault(car => car.ID.Equals(id));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Homework1_Users.Controllers
{

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>()
        {
            new User(){ UserId = 1, FirstName = "Vladimir", LastName = "Planojevic", Age = 34 },
            new User(){ UserId = 2, FirstName = "Nikola", LastName = "Nikolov", Age = 24 },
            new User(){ UserId = 3, FirstName = "Marija", LastName = "Poposka", Age = 28 }
        };

        [HttpGet]
        public ActionResult<IList<User>> Get()
        {
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> CheckId(int id)
        {
            User user = users.SingleOrDefault(i => i.UserId == id);
            
            if(user == null)
            {
                return NotFound($"User with id {id} was not found");
            }
            if (user.Age<18)
            {
                Console.WriteLine($"User with id {id} is under 18 years old");
            }
            return user;
        }




        [HttpGet("{index}")]
        public ActionResult<User> GetIndex(int index)
        {
            User user = users[index];

            if (user == null)
            {
                return NotFound($"There is no user under that index in the list");
            }

            return user;
        }

        [HttpPost]
        public IActionResult Post()
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            User user = JsonConvert.DeserializeObject<User>(body);
            users.Add(user);
            return Ok($"User with id {users.Count - 1} has been added!");
        }
    }
}
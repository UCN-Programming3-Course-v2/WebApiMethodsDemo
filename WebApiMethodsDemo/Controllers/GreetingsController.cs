using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiMethodsDemo.Model;

namespace WebApiMethodsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        private static List<Person> persons = new() { new Person { Id = 1, Name = "Lars", Job = "Lærer" } };

        [HttpGet]
        [Route("{id}")]
        public IActionResult Greeting(int id)
        {
            try
            {
                Person p = persons.Single(p => p.Id == id); // uses System.Linq to find a single instance of a person
                return Ok($"Hello {p.Job} {p.Name}"); // returns status 200(Ok) and the greeting
            }
            catch 
            {
                return BadRequest(); // If anything goes wrong the api returns status 400(Bad Request)
            }
        }

        [HttpPut]
        public IActionResult UpdatePerson(Person person)
        {
            try
            {
                Person p = persons.Single(p => p.Id == person.Id); // uses System.Linq to find a single instance of a person
                p.Name = person.Name; // Setting values
                p.Job = person.Job;
                return Ok(); // Return status 200(OK)
            }
            catch
            {
                return BadRequest(); // If anything goes wrong the api returns status 400(Bad Request)            }
            }
        }

        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            persons.Add(person); // Adds the person to the list
            return Created($"{HttpContext.Request.Path}/{persons.Single(p => p.Id == person.Id).Id}", null); // return status 201(Created) with the url to the new resource
        }

        [HttpDelete]
        public IActionResult DeletePerson(int id)
        {
            var result = persons.Remove(persons.Single(p => p.Id == id)); // Removes the person with the provided id
            if (result)
            {
                return Ok(); // If the person is found and removed, a status 200(OK) is returned
            }
            return NotFound(); // If we get here, the id is not found and a status 404(Not Found) is returned
        }
    }
}

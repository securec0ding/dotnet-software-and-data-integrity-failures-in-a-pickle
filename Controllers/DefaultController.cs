using Deserialization.Models;
using Deserialization.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deserialization.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IWorkoutService service;

        public DefaultController(IWorkoutService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("api/workouts")]
        public IActionResult NewWorkout([FromBody] Workout userInput)
        {
            if (userInput == null)
                return BadRequest(new { Message = "Invalid workout data" });

            this.service.AddWorkout(userInput);

            return Ok(new { Message = "Workout saved successfully" });
        }

        [HttpGet]
        [Route("api/workouts")]
        public IActionResult GetAllWorkouts()
        {
            var workouts = this.service.GetAllWorkouts();

            return Ok(workouts);
        }
        
        [HttpGet]
        [Route("/")]
        public string Index()
        {
            return "The API is up!";
        }
    }
}
using Deserialization.Models;

namespace Deserialization.Services
{
    public interface IWorkoutService
    {
        void AddWorkout(Workout workout);
        Workout[] GetAllWorkouts();
        void ResetAndSeedDatabase();
    }
}
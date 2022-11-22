using Deserialization.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Deserialization.Services
{
    public class WorkoutService : IWorkoutService
    {
        private const string DatabaseName = "workout.db";

        public void AddWorkout(Workout workout)
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseName }.ToString();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO workouts(duration_seconds, burnt_calories) VALUES(@seconds, @calories);";
                cmd.Parameters.AddWithValue("@seconds", workout.DurationSeconds);
                cmd.Parameters.AddWithValue("@calories", workout.BurntCalories);

                cmd.ExecuteNonQuery();
            }
        }

        public Workout[] GetAllWorkouts()
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseName }.ToString();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = $"SELECT duration_seconds, burnt_calories FROM workouts;";

                var result = new List<Workout>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workout = new Workout
                        {
                            DurationSeconds = (long) reader["duration_seconds"],
                            BurntCalories = (long) reader["burnt_calories"],
                        };
                        result.Add(workout);
                    }
                }

                return result.ToArray();
            }
        }

        public void ResetAndSeedDatabase()
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseName }.ToString();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createCommand = connection.CreateCommand();
                createCommand.CommandText = "DROP TABLE IF EXISTS workouts;"
                                          + "CREATE TABLE workouts (duration_seconds INT, burnt_calories INT);";
                createCommand.ExecuteNonQuery();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO workouts(duration_seconds, burnt_calories) VALUES (1800, 240);";
                insertCommand.ExecuteNonQuery();
            }
        }

    }
}
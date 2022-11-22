using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;

namespace Deserialization
{
    public class ConsoleTraceWriter : ITraceWriter
    {
        public TraceLevel LevelFilter => TraceLevel.Verbose;

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            Console.WriteLine($"MY level: {level}");
            Console.WriteLine($"MY message: {message}");
            Console.WriteLine($"MY exception: {ex}");
        }
    }
}
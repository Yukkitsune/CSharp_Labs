using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Second
{
    public interface ILoggerRepository
    {
        void LogMessage(string message);
    }
    public class  TextFileLoggerRepository : ILoggerRepository
    {
        private readonly string _filePath;
        public TextFileLoggerRepository(string filePath)
        {
            _filePath = filePath;
        }
        public void LogMessage(string message)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(message);
            }
        }
    }
    public class JsonFileLoggerRepository : ILoggerRepository
    {
        private readonly string _filePath;
        public JsonFileLoggerRepository(string filePath)
        {
            _filePath = filePath;
        }
        public void LogMessage(string message)
        {
            var logEntry = new { TimestampAttribute = DateTime.Now, Message = message };
            string json = JsonConvert.SerializeObject(logEntry, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(json);
            }
        }
    }
    public class MyLogger
    {
        private readonly ILoggerRepository _loggerRepository;
        public MyLogger(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public void Log(string message)
        {
            _loggerRepository.LogMessage(message);
        }
    }
}

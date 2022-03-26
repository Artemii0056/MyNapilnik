using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder log1 = new Pathfinder(new FileLogWritter());
            Pathfinder log2 = new Pathfinder(new ConsoleLogWritter());
            Pathfinder log3 = new Pathfinder(new SecureConsoleLogWritter(new FileLogWritter()));
            Pathfinder log4 = new Pathfinder(new SecureConsoleLogWritter(new ConsoleLogWritter()));
            Pathfinder log5 = new Pathfinder(LoggerChain.Create(new ConsoleLogWritter(), new SecureConsoleLogWritter(new FileLogWritter())));
        }
    }

    interface ILogger
    {
        void Log(string massage);
    }

    class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Find(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log("Empty");
        }
    }

    class ConsoleLogWritter : ILogger
    {
        public void Log(string massage)
        {
            if (massage == null)
                throw new ArgumentNullException(nameof(massage));

            Console.WriteLine(massage);
        }
    }

    class FileLogWritter : ILogger
    {
        public void Log(string massage)
        {
            if (massage == null)
                throw new ArgumentNullException(nameof(massage));

            File.WriteAllText("log.txt", massage);
        }
    }

    class SecureConsoleLogWritter : ILogger
    {
        private readonly ILogger _logger;

        public SecureConsoleLogWritter(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Log(string massage)
        {
            if (massage == null)
                throw new ArgumentNullException(nameof(massage));

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Log(massage);
            }
        }
    }

    class LoggerChain : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public LoggerChain(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        public void Log(string massage)
        {
            if (massage == null)
                throw new ArgumentNullException(nameof(massage));

            foreach (var log in _loggers)
                log.Log(massage);
        }

        public static LoggerChain Create(params ILogger[] loggers)
        {
            if (loggers == null)
                throw new ArgumentNullException(nameof(loggers));

            return new LoggerChain(loggers);
        }
    }
}

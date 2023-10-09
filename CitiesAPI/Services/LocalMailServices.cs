﻿using System.Collections.Concurrent;

namespace CitiesAPI.Services
{
    public class LocalMailServices : IMailServices
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailServices(IConfiguration configuration) 
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSetting:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}" +
                $"with {nameof(LocalMailServices)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }

    }
}

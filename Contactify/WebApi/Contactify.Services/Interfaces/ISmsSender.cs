﻿using System.Threading.Tasks;

namespace Contactify.Services.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}

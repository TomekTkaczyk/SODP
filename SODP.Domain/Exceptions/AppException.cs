﻿using System;

namespace SODP.Domain.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message) { }
    }
}

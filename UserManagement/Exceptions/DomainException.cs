﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class DomainNotFoundException : Exception
    {
        public DomainNotFoundException(string message) : base(message) { }
    }

    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message) : base(message) { }
    }

}
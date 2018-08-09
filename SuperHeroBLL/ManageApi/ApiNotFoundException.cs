using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.ManageApi
{
    public class ApiNotFoundException:Exception
    {
        public ApiNotFoundException(string message) : base(message)
        {
            
        }
    }
}
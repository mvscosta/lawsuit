using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2Tech.Crosscutting.Model.ServiceClient
{
        public class ResultDto<T>
        {
            public bool Success { get; set; }

            public string Error { get; set; }

            public T Value { get; set; }
        }
    }

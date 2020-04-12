using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSoLienLac.DTO
{
    public class ValueDTO<T>
    {
        public int Error { get; set; }
        public List<T> ListT { get; set; }

        public ValueDTO()
        {
            Error = 0;
            ListT = new List<T>();
        }
    }
}

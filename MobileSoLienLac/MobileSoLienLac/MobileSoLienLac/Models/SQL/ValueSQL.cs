using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MobileSoLienLac.Models.SQL
{
    public class DataTableSQL
    {
        public int Error { get; set; }
        public DataTable Data { get; set; }

        public DataTableSQL()
        {
            Error = 0;
            Data = null;
        }
    }

    public class ObjectSQL<T>
    {
        public int Error { get; set; }
        public T Value { get; set; }
    }
}

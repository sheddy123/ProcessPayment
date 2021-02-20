using System;
using System.Collections.Generic;
using System.Text;

namespace ProPayTest
{
    public class StatCodeResult
    {
        public List<string> ContentTypes { get; set; }
        public List<string> DeclaredType { get; set; }
        public List<string> Formatters { get; set; }
        public int StatusCode { get; set; }
        public string Value { get; set; }
    }
}

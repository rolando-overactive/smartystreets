using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsTests.Service
{ 
    public class StreetDto
    { 
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street2 { get; set; }

        public string MatchStrategy { get; set; }
    }
}

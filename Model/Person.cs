using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRandomizer.Model
{
    internal class Person
    {
        public Name? name { get; set; }
        public Location? location { get; set; }
    }

    internal class Name
    {
        public string? title { get; set; }
        public string? first { get; set; }
        public string? last { get; set; }
    }

    internal class Location
    {
        public string? city { get; set; }
        public string? country { get; set; }
    }

    internal class Users
    {
        public List<Person>? results { get; set; }
    }
}

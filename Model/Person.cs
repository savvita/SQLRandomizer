using System.Collections.Generic;

namespace SQLRandomizer.Model
{
    internal class Person
    {
        public Name? name { get; set; }
        public Location? location { get; set; }

        public string? email { get; set; }

        public Login? login { get; set; }

        public string? phone { get; set; }
        public string? cell { get; set; }
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

    internal class Login
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    internal class Users
    {
        public List<Person>? results { get; set; }
    }
}

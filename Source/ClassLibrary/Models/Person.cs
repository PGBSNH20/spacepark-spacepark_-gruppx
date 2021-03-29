using System.Collections.Generic;

namespace ClassLibrary.Models
{
    public class Person
    {
        public string Count { get; set; }
        public List<Results> Results { get; set; }
    }

    public class Results
    {
        public string Name { get; set; }
        public List<string> Starships { get; set; }
    }
}

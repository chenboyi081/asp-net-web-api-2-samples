using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi
{
    public class Employee
    {
        public string Name { get; set; }
        public string Grade { get; set; }

        [RangeIf("Grade", "G7", 2000, 3000)]
        [RangeIf("Grade", "G8", 3000, 4000)]
        [RangeIf("Grade", "G9", 4000, 5000)]
        public decimal Salary { get; set; }
    }
}

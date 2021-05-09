namespace GenericCsvParser.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double CO2 { get; set; }
    }
}

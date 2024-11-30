using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SiggeMimmiLabb3.Models
{
    public class WeatherData
    {
        [Key]
        [Ignore] // Ignorera detta vid CSV-import
        public int Id { get; set; }

        [Name("Datum")]
        public DateTime Datum { get; set; }

        [Name("Plats")]
        public string? Plats { get; set; }

        [Name("Temp")]
        [TypeConverter(typeof(CustomFloatConverter))]
        public float Temp { get; set; }

        [Name("Luftfuktighet")]
        public int Luftfuktighet { get; set; }
    }
}
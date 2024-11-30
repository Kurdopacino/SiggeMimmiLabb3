using SiggeMimmiLabb3.Data;
using SiggeMimmiLabb3.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiggeMimmiLabb3.Services
{
    public class WeatherQueries
    {
        private readonly WeatherDbContext _context;

        public WeatherQueries(WeatherDbContext context)
        {
            _context = context;
        }

        public double? GetAverageTemperature(DateTime date, string location)
        {
            var temperatures = _context.WeatherData
                .Where(w => w.Datum.Date == date.Date && w.Plats == location)
                .Select(w => (double?)w.Temp) // Gör detta nullable
                .ToList();

            if (temperatures.Count == 0)
            {
                return null; // Om inga resultat, returnera null
            }

            return temperatures.Average(); // Beräkna genomsnitt
        }

        public List<WeatherData> GetSortedByTemperature(string location)
        {
            return _context.WeatherData
                .Where(w => w.Plats == location)
                .OrderByDescending(w => w.Temp)
                .ToList();
        }

        public List<WeatherData> GetSortedByHumidity(string location)
        {
            return _context.WeatherData
                .Where(w => w.Plats == location)
                .OrderBy(w => w.Luftfuktighet)
                .ToList();
        }
    }
}

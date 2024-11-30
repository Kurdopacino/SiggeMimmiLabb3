using SiggeMimmiLabb3.Data;
using System.Linq;

namespace SiggeMimmiLabb3.Services
{
    public class WeatherAnalyzer
    {
        private readonly WeatherDbContext _context;

        public WeatherAnalyzer(WeatherDbContext context)
        {
            _context = context;
        }

        public DateTime? GetMeteorologicalAutumn()
        {
            var data = _context.WeatherData
                .Where(w => w.Plats == "Ute")
                .OrderBy(w => w.Datum)
                .ToList();

            for (int i = 0; i < data.Count - 4; i++)
            {
                if (data.Skip(i).Take(5).All(d => d.Temp <= 10))
                {
                    return data[i].Datum;
                }
            }

            return null;
        }

        public DateTime? GetMeteorologicalWinter()
        {
            var data = _context.WeatherData
                .Where(w => w.Plats == "Ute")
                .OrderBy(w => w.Datum)
                .ToList();

            for (int i = 0; i < data.Count - 4; i++)
            {
                if (data.Skip(i).Take(5).All(d => d.Temp <= 0) && data[i].Datum >= new DateTime(data[i].Datum.Year, 10, 14))
                {
                    return data[i].Datum;
                }
            }

            return null;
        }
    }
}

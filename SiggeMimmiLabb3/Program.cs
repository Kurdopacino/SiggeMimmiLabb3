using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using SiggeMimmiLabb3.Data;
using SiggeMimmiLabb3.Models;
using SiggeMimmiLabb3.Services; // Lägg till detta för att använda analyser och frågor
using CsvHelper.TypeConversion;

class Program
{
    static void Main(string[] args)
    {
        // Kontrollera att databasen existerar
        using (var context = new WeatherDbContext())
        {
            Console.WriteLine("Försöker skapa eller ansluta till databasen...");
            context.Database.EnsureCreated();
            Console.WriteLine("Databasen är skapad!");
        }

        // Läsa in data från CSV och fylla databasen
        ReadCsvAndSaveToDatabase();

        // Initiera analyser och frågor
        AnalyzeAndQueryDatabase();
    }

    static void ReadCsvAndSaveToDatabase()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null
        };

        try
        {
            using (var reader = new StreamReader("TempFuktData.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                // Lägg till loggning för att verifiera rubriker
                Console.WriteLine("Rubriker i CSV:");
                csv.Read(); // Flytta till första raden
                csv.ReadHeader(); // Läs rubrikerna
                foreach (var header in csv.HeaderRecord ?? Array.Empty<string>())
                {
                    Console.WriteLine(header);
                }

                // Läs poster från CSV-filen
                var records = csv.GetRecords<WeatherData>().ToList();

                using (var context = new WeatherDbContext())
                {
                    Console.WriteLine("Läser in data från CSV-filen...");
                    foreach (var record in records)
                    {
                        Console.WriteLine($"Laddar: Datum={record.Datum}, Plats={record.Plats}, Temp={record.Temp}, Luftfuktighet={record.Luftfuktighet}");
                    }

                    context.WeatherData.AddRange(records); // Lägg till i databasen
                    context.SaveChanges();

                    Console.WriteLine("Data är sparad i databasen!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ett fel inträffade: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Detaljerat fel: " + ex.InnerException.Message);
            }
        }
    }

    static void AnalyzeAndQueryDatabase()
    {
        using (var context = new WeatherDbContext())
        {
            var analyzer = new WeatherAnalyzer(context);
            var queries = new WeatherQueries(context);

            // Visa meteorologisk höst och vinter
            Console.WriteLine("Meteorologisk höst:");
            var autumn = analyzer.GetMeteorologicalAutumn();
            Console.WriteLine(autumn.HasValue ? autumn.Value.ToString("yyyy-MM-dd") : "Ingen höst hittad");

            Console.WriteLine("Meteorologisk vinter:");
            var winter = analyzer.GetMeteorologicalWinter();
            Console.WriteLine(winter.HasValue ? winter.Value.ToString("yyyy-MM-dd") : "Ingen vinter hittad");

            // Visa sorterings- och sökfunktioner
            Console.WriteLine("Medeltemperatur för 2023-01-01 (Ute):");
            Console.WriteLine(queries.GetAverageTemperature(new DateTime(2023, 1, 1), "Ute"));

            Console.WriteLine("Sorterat efter temperatur (Ute):");
            foreach (var data in queries.GetSortedByTemperature("Ute"))
            {
                Console.WriteLine($"{data.Datum}: {data.Temp}°C");
            }

            Console.WriteLine("Sorterat efter luftfuktighet (Ute):");
            foreach (var data in queries.GetSortedByHumidity("Ute"))
            {
                Console.WriteLine($"{data.Datum}: {data.Luftfuktighet}%");
            }
        }
    }
}

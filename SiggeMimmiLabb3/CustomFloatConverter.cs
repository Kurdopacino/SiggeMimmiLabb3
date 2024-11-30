using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace SiggeMimmiLabb3
{
    public class CustomFloatConverter : SingleConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            try
            {
                // Replace Unicode minus sign with ASCII minus sign
                text = text.Replace('−', '-');

                // Försök att tolka texten som ett flyttal
                if (float.TryParse(text, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var result))
                {
                    return result;
                }

                throw new CsvHelperException(row.Context, $"Kunde inte konvertera '{text}' till float.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid konvertering av värde '{text}': {ex.Message}");
                throw;
            }
        }
    }
}


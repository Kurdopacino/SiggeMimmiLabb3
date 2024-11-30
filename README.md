# SiggeMimmiLabb3

SiggeMimmiLabb3 är ett projekt för att analysera väderdata från en CSV-fil och spara informationen i en databas. Projektet använder sig av C# och Entity Framework för att hantera databasen, samt CsvHelper för att läsa och importera data från CSV-filen.

## Innehåll

- [Beskrivning](#beskrivning)
- [Installation](#installation)
- [Användning](#användning)
- [Struktur](#struktur)
- [Licens](#licens)

## Beskrivning

Detta projekt importerar väderdata från en CSV-fil (`TempFuktData.csv`), lagrar informationen i en databas och erbjuder funktioner för att analysera och fråga efter väderdata. Programmet använder sig av följande teknologier:

- **C#**
- **Entity Framework Core**
- **CsvHelper**
- **SQLite** (eller annan databas om så önskas)

Projektet innehåller analyser för att hitta meteorologisk höst och vinter, samt funktioner för att fråga efter medeltemperatur, temperatur- och luftfuktighetsdata sorterat efter plats.

## Installation

För att köra detta projekt lokalt, följ dessa steg:

1. **Kloning av repot**:  
   Klona detta repository till din lokala maskin.
   ```bash
   git clone https://github.com/ditt-användarnamn/SiggeMimmiLabb3.git

2. **Installera beroenden:**:  
  Öppna lösningen i Visual Studio och se till att alla nödvändiga paket är installerade. Detta inkluderar CsvHelper och EntityFrameworkCore.
Om du använder .NET CLI, kör följande kommando för att installera nödvändiga paket:
   ```bash
   dotnet restore

3. **Ställ in databasen:**:  
  Projektet använder Entity Framework för att hantera databasen. Du kan skapa databasen genom att köra följande kommando i Paket- eller .NET CLI:
   ```bash
   dotnet ef database update

3. **CSV-fil:**:  
  Placera din CSV-fil, `TempFuktData.csv`, i rotmappen av projektet. Den ska innehålla väderdata med kolumner som exempelvis `Datum`, `Plats`, `Temp`, och `Luftfuktighet`.


  

## Användning

1. **Kör programmet**:  
   Efter installationen och konfigurationen av databasen kan du köra programmet genom att starta `Program.cs` från Visual Studio eller via .NET CLI:
   ```bash
   dotnet run

2. **Funktionalitet:**:
När programmet körs kommer följande att ske:
Databasen skapas om den inte redan existerar.
CSV-filen `TempFuktData.csv` läses in och alla väderposter sparas i databasen.
Programmet utför analyser, inklusive att beräkna meteorologisk höst och vinter.
Du kan fråga efter medeltemperatur för ett specifikt datum och sortera väderdata efter temperatur eller luftfuktighet.


3. **Analyser:**:
Programmet erbjuder olika väderanalyser:
**Meteorologisk höst** och **meteorologisk vinter** identifieras baserat på data i databasen.  
**Medeltemperatur** kan beräknas för specifika datum och platser.  
**Sortering** efter temperatur och luftfuktighet görs för att visa väderdata ordnat.

## Struktur
Projektet är organiserat enligt följande mappstruktur:
 ```bash
SiggeMimmiLabb3
│  
├── Data  
│   ├── WeatherDbContext.cs            # Hanterar databasanslutningen och tabeller  
│   └── Migrations                     # Databas-migreringar  
│  
├── Models  
│   └── WeatherData.cs                 # Modell som representerar väderdata  
│  
├── Services  
│   ├── WeatherAnalyzer.cs             # Analyserar väderdata för meteorologisk höst och vinter  
│   ├── WeatherQueries.cs              # Frågefunktioner för att hämta medeltemperatur, sortera data  
│   └── CustomFloatConverter.cs        # Konvertering av datatyper vid inläsning av CSV  
│  
├── Program.cs                         # Huvudprogram som kör hela flödet  
├── TempFuktData.csv                   # CSV-fil med väderdata  
└── WeatherData.db                     # SQLite-databas
```

**WeatherDbContext.cs**: Hanterar databasanslutningen och tabeller för väderdata.  
**WeatherData.cs**: En modell som representerar väderdata som lagras i databasen.  
**WeatherAnalyzer.cs**: Innehåller metoder för att analysera vädermönster och identifiera säsonger (höst och vinter).  
**WeatherQueries.cs**: Innehåller frågor för att hämta väderinformation, som medeltemperatur och sortering.  
**CustomFloatConverter.cs**: Konverterar data under CSV-läsning, särskilt för temperatur och luftfuktighet.


## Licens
Detta projekt är licensierat under MIT License - se LICENSE.md för detaljer.

## Bidrag
Om du vill bidra till projektet, skapa gärna en pull request. Följ dessa steg:

Forka projektet.  
Skapa din egen feature-branch.  
Gör dina ändringar och commit.  
Skicka en pull request.  












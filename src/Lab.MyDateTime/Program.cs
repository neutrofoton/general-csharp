using Lab.MyDateTime;

var timezones = TimeZoneInfo.GetSystemTimeZones().Where(x => 
{
    
    return  x.Id.Contains("jayapura", StringComparison.OrdinalIgnoreCase)
    || x.Id.Contains("makassar", StringComparison.OrdinalIgnoreCase) 
    || x.Id.Contains("jakarta", StringComparison.OrdinalIgnoreCase) 
    || x.DisplayName.Contains("japan", StringComparison.OrdinalIgnoreCase);
    ;
});

foreach(var tz in timezones)
{
    Console.WriteLine($"[{tz.Id}] - {tz.DisplayName}");
}

var tzJayapura = timezones.First(t => t.Id.Contains("jayapura", StringComparison.CurrentCultureIgnoreCase));
var tzMakassar = timezones.First(t => t.Id.Contains("makassar", StringComparison.CurrentCultureIgnoreCase));
var tzJakarta = timezones.First(t => t.Id.Contains("jakarta", StringComparison.CurrentCultureIgnoreCase));
var tzUtc = TimeZoneInfo.Utc;

Console.WriteLine("------- demo 1 --------------------");
string timeInputUtc = "2024-07-27T17:00:00.000Z";

DateTime dateTimeInputUtc;
bool isValidTime = DateTime.TryParse(timeInputUtc,out dateTimeInputUtc);

if(isValidTime)
{
    DateTime outDateTimeJakarta = TimeZoneInfo.ConvertTime(dateTimeInputUtc,tzJakarta);
    Console.WriteLine($"{outDateTimeJakarta.ToString("dd-MMM-yyyy hh:mm:ss")}");
}

Console.WriteLine("------- demo 2 --------------------");

Console.WriteLine(" Source : Makassar -> target: Jakarta");
TimeZoneInfo sourceTimeZone = tzMakassar;
TimeZoneInfo targetTimeZone = tzJakarta;

DateTime sourceTime = new(2024,12,9,7,50,27); //assume as UTC
DateTime sourceTimeInSourceZone = TimeZoneInfo.ConvertTime(sourceTime, TimeZoneInfo.Utc, sourceTimeZone);
DateTime targetTime = TimeZoneInfo.ConvertTime(sourceTimeInSourceZone, sourceTimeZone, targetTimeZone);

Console.WriteLine($"sourceTime : {sourceTime.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"sourceTimeInSourceZone: {sourceTimeInSourceZone.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"targetTime {targetTime.ToString("dd MMM yyyy hh:mm:ss")}");

Console.WriteLine("------- demo 3 ------------");
DateTime sourceTime3 = new(2024,12,9,7,50,27);

DateTimeWithZone dateTimeWithZone3 = new DateTimeWithZone(sourceTime3, tzMakassar);
Console.WriteLine($"Input as {tzMakassar.Id} {sourceTime3.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output UTC {dateTimeWithZone3.UniversalTime.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output Local {dateTimeWithZone3.LocalTime.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output Jakarta {dateTimeWithZone3.JakartaTime.ToString("dd MMM yyyy hh:mm:ss")}");


Console.WriteLine("------- demo 4 ------------");
DateTime sourceTime4 = new(2024,12,9,7,50,27);

DateTimeWithZone dateTimeWithZone4 = new DateTimeWithZone(sourceTime4, tzUtc);
Console.WriteLine($"Input as {tzUtc.Id} {sourceTime4.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output UTC {dateTimeWithZone4.UniversalTime.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output Local {dateTimeWithZone4.LocalTime.ToString("dd MMM yyyy hh:mm:ss")}");
Console.WriteLine($"Output Jakarta {dateTimeWithZone4.JakartaTime.ToString("dd MMM yyyy hh:mm:ss")}");

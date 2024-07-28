var timezones = TimeZoneInfo.GetSystemTimeZones();

// foreach(var tz in timezones)
// {
//     Console.WriteLine($"{tz.Id}");
// }

var tzJakarta = timezones.FirstOrDefault(t => t.Id.ToLower().Contains("jakarta"));

Console.WriteLine($"{tzJakarta!.Id}");

string timeInputUtc = "2024-07-27T17:00:00.000Z";

DateTime dateTimeInputUtc;
bool isValidTime = DateTime.TryParse(timeInputUtc,out dateTimeInputUtc);

if(isValidTime)
{
    DateTime outDateTimeJakarta = TimeZoneInfo.ConvertTime(dateTimeInputUtc,tzJakarta);
    Console.WriteLine($"{outDateTimeJakarta.ToString("dd-MMM-yyyy hh:mm:ss")}");
}
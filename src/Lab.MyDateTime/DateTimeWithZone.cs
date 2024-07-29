namespace Lab.MyDateTime;

//https://stackoverflow.com/questions/246498/creating-a-datetime-in-a-specific-time-zone-in-c-sharp
public readonly struct DateTimeWithZone
{
    private readonly DateTime utcDateTime;
    private readonly TimeZoneInfo timeZone;

    /// <summary>
    /// Specify the datetime with time zone
    /// </summary>
    /// <param name="dateTime">define the datetime</param>
    /// <param name="timeZone">define the zone</param>
    public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
    {
        this.timeZone = timeZone;

        var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        this.utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone); 
        
    }

    public DateTime UniversalTime { get { return utcDateTime; } }

    public TimeZoneInfo TimeZone { get { return timeZone; } }

    public DateTime LocalTime
    { 
        get 
        { 
            return TimeZoneInfo.ConvertTime(utcDateTime, timeZone); 
        }
    }

    public DateTime JakartaTime
    {
        get{
            var tzJakarta = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(t => t.Id.Contains("jakarta", StringComparison.CurrentCultureIgnoreCase));
            return TimeZoneInfo.ConvertTime(utcDateTime, tzJakarta!); 
        }
    }        
}

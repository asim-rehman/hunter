using System;

namespace Hunter.Web.Client.Models
{
    public class Location
    {
        //
        // Summary:
        //     Speed in meters per second.
        public double? Speed { get; set; }
        //
        // Summary:
        //     Gets or sets the accuracy (in meters) of the location.
        public double? Accuracy { get; set; }
        //
        // Summary:
        //     Gets the Altitude, if available in meters above sea level.
        //
        // Remarks:
        //     Returns 0 or no value if not available.
        public double? Altitude { get; set; }
        //
        // Summary:
        //     Gets or sets the longitude of location.
        public double Longitude { get; set; }
        //
        // Summary:
        //     Gets or sets the latitude of location.
        public double Latitude { get; set; }
        //
        // Summary:
        //     Inform if location is from GPS or from Mock.
        public bool IsFromMockProvider { get; set; }
        //
        // Summary:
        //     Degrees relative to true north.
        public double? Course { get; set; }
        //
        // Summary:
        //     Gets or sets the timestamp of the location.
        public DateTimeOffset Timestamp { get; set; }
    }
}

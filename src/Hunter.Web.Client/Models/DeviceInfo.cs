using System;

namespace Hunter.Web.Client.Models
{
    public class DeviceInfo
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Device { get; set; } 
        public string Display { get; set; }
        public string Serial { get; set; }
        public string Version { get; set; } 
        public string Codename { get; set; }
        public string SDK { get; set; } 
        public string SecurityPatch { get; set; } 
        public string Hardware { get; set; } 
        public string Radio { get; set; } 
        public string Bootloader { get; set; } 
        public string Board { get; set; }
        public string User { get; set; } 
        public long Time { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public NetworkInfo NetworkInfo { get; set; }

    }
}
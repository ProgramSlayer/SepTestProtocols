using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProtocolWorkLib
{
    class EventCategory
    {
        [Flags]
        public enum EventCategories
        {
           None = 0,
           Intruder = 1,
           SecurityDevices = 2,
           Operator = 4,
           Guards = 8
        }

        public static readonly Dictionary<string, EventCategories> EventsCategoriesDictionary = new()
        {
            { "VisionRangeEvent", EventCategories.None },
            { "CaptureRadiusEvent", EventCategories.None },
            { "EntranceToZoneEvent", EventCategories.Intruder },
            { "ExitFromZoneEvent", EventCategories.Intruder },
            { "ReduceDetSensWeatherEvent", EventCategories.None },
            { "ReduceDetSensPartnerEvent", EventCategories.None },
            { "SignResetEvent", EventCategories.None },
            { "MovToPointEvent", EventCategories.None },
            { "SpecZoneNotAffVehEvent", EventCategories.None },
            { "SpecZoneAffVehEvent", EventCategories.None },
        };
    }
}

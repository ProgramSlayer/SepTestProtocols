using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TestProtocolWorkLib;
using WpfEventsReader.Models;

namespace WpfEventsReader.Services
{
    public class EventModelFactory
    {
        public static EventModel[] Manufacture(XElement[] xEvents)
        {
            IEnumerable<EventModel> product = from ev in xEvents
                                              select new EventModel()
                                              {
                                                  Name = ev.Name.LocalName,
                                                  Ctime = ev.Attribute("Ctime").Value,
                                                  Category = EventCategory.EventsCategoriesDictionary[ev.Name.LocalName],
                                              };
            return product.ToArray();
        }
    }
}

using System.Xml.Linq;
using TestProtocolWorkLib;

namespace WpfEventsReader.Models
{
    public class EventModel
    {
        public string Name { get; set; }
        public float Ctime { get; set; }

        public EventCategory.EventCategories Category { get; set; }

        public int Rating { get; set; }

        public int Id { get; set; }

        public EventModel() { }

        // TODO: нужны проверки.
        public EventModel(XElement ev)
        {
            Name = ev.Name.LocalName;
            _ = float.TryParse(ev.Attribute("Ctime").Value.Replace('.', ','), out float cTime);
            Ctime = cTime;
            Category = EventCategory.EventsCategoriesDictionary[Name];
            _ = int.TryParse(ev.Attribute("EventRating").Value, out int rating);
            Rating = rating;
            _ = int.TryParse(ev.Attribute("Id").Value, out int id);
            Id = id;
        }
    }
}

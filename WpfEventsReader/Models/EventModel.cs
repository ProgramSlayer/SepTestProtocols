using TestProtocolWorkLib;

namespace WpfEventsReader.Models
{
    public class EventModel
    {
        public string Name { get; set; } = "";
        public string Ctime { get; set; } = "";

        public EventCategory.EventCategories Category { get; set; }

        public int CcIndex { get; set; } = 0;

        public string Description => $"Название события: {Name}\nВремя события: {Ctime}\nКатегория события: {Category}\nCcIndex: {CcIndex}";
    }
}

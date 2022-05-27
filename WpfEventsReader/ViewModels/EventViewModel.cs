using System.Xml.Linq;
using WpfEventsReader.Models;

namespace WpfEventsReader.ViewModels
{
    public class EventViewModel
    {
        public string ToolTip => $"Название: {_model.Name}\nВремя: {_model.Ctime}\nКатегория: {_model.Category}\nРейтинг: {_model.Rating}";

        private EventModel _model = new EventModel();

        public EventModel EventModel => _model;

        public EventViewModel() { }
        public EventViewModel(XElement xEv)
        {
            _model = new EventModel(xEv);
        }
    }
}

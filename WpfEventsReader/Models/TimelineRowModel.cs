using System.Text;
using TestProtocolWorkLib;

namespace WpfEventsReader.Models
{
    public class TimelineRowModel
    {
        public string Time { get; private set; } = "";

        public string DescriptionCategoryNone { get; private set; } = "";
        public string DescriptionCategoryIntruder { get; private set; } = "";
        public string DescriptionCategorySecurityDevices { get; private set; } = "";
        public string DescriptionCategoryOperatorCctv { get; private set; } = "";
        public string DescriptionCategoryOperator { get; private set; } = "";
        public string DescriptionCategoryGuards { get; private set; } = "";

        public TimelineRowModel(EventModel[] row)
        {
            // TODO: check if events have the same Ctime attribute.

            StringBuilder noneCat = new();
            StringBuilder intruderCat = new();
            StringBuilder securityDevicesCat = new();
            StringBuilder operatorCctvCat = new();
            StringBuilder operatorCat = new();
            StringBuilder guardsCat = new();

            foreach (EventModel ev in row)
            {
                switch (ev.Category)
                {
                    case EventCategory.EventCategories.None:
                        _ = noneCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.Intruder:
                        _ = intruderCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.SecurityDevices:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.Operator:
                        _ = operatorCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.OperatorCctv:
                        _ = operatorCctvCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.Guards:
                        _ = guardsCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.IntruderGuards:
                        _ = intruderCat.AppendLine(ev.Description);
                        _ = guardsCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.SecurityDevicesOperator:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        _ = operatorCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.OperatorGuards:
                        _ = operatorCat.AppendLine(ev.Description);
                        _ = guardsCat.AppendLine(ev.Description);
                        break;
                    case EventCategory.EventCategories.SecurityDevicesIntruder:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        _ = intruderCat.AppendLine(ev.Description);
                        break;
                    default:
                        break;
                }
            }

            Time = row[0].Ctime;
            DescriptionCategoryNone = noneCat.ToString();
            DescriptionCategoryIntruder = intruderCat.ToString();
            DescriptionCategorySecurityDevices = securityDevicesCat.ToString();
            DescriptionCategoryOperatorCctv = operatorCctvCat.ToString();
            DescriptionCategoryOperator = operatorCat.ToString();
            DescriptionCategoryGuards = guardsCat.ToString();
        }
    }
}

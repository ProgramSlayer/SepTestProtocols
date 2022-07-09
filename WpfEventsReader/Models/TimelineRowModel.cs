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

        public string CategoryNoneCcIndexes { get; set; } = "";
        public string CategoryIntruderCcIndexes { get; set; } = "";
        public string CategorySecurityDevicesCcIndexes { get; set; } = "";
        public string CategoryOperatorCctvCcIndexes { get; set; } = "";
        public string CategoryOperatorCcIndexes { get; set; } = "";
        public string CategoryGuardsCcIndexes { get; set; } = "";
        

        public TimelineRowModel(EventModel[] row)
        {
            // TODO: check if events have the same Ctime attribute.

            StringBuilder noneCat = new();
            StringBuilder intruderCat = new();
            StringBuilder securityDevicesCat = new();
            StringBuilder operatorCctvCat = new();
            StringBuilder operatorCat = new();
            StringBuilder guardsCat = new();

            StringBuilder noneCatCcInd = new();
            StringBuilder intruderCatCcInd = new();
            StringBuilder securityDevicesCatCcInd = new();
            StringBuilder operatorCctvCatCcInd = new();
            StringBuilder operatorCatCcInd = new();
            StringBuilder guardsCatCcInd = new();

            foreach (EventModel ev in row)
            {
                switch (ev.Category)
                {
                    case EventCategory.EventCategories.None:
                        _ = noneCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = noneCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.Intruder:
                        _ = intruderCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = intruderCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.SecurityDevices:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = securityDevicesCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.Operator:
                        _ = operatorCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = operatorCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.OperatorCctv:
                        _ = operatorCctvCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = operatorCctvCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.Guards:
                        _ = guardsCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = guardsCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.IntruderGuards:
                        _ = intruderCat.AppendLine(ev.Description);
                        _ = guardsCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = intruderCatCcInd.Append($"{ev.CcIndex}; ");
                            _ = guardsCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.SecurityDevicesOperator:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        _ = operatorCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = securityDevicesCatCcInd.Append($"{ev.CcIndex}; ");
                            _ = operatorCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.OperatorGuards:
                        _ = operatorCat.AppendLine(ev.Description);
                        _ = guardsCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = operatorCatCcInd.Append($"{ev.CcIndex}; ");
                            _ = guardsCatCcInd.Append($"{ev.CcIndex}; ");
                        }
                        break;
                    case EventCategory.EventCategories.SecurityDevicesIntruder:
                        _ = securityDevicesCat.AppendLine(ev.Description);
                        _ = intruderCat.AppendLine(ev.Description);
                        if (ev.CcIndex != 0)
                        {
                            _ = securityDevicesCatCcInd.Append($"{ev.CcIndex}; ");
                            _ = intruderCatCcInd.Append($"{ev.CcIndex}; ");
                        }
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

            CategoryNoneCcIndexes = noneCatCcInd.ToString();
            CategoryIntruderCcIndexes = intruderCatCcInd.ToString();
            CategorySecurityDevicesCcIndexes = securityDevicesCatCcInd.ToString();
            CategoryOperatorCctvCcIndexes = operatorCctvCatCcInd.ToString();
            CategoryOperatorCcIndexes = operatorCatCcInd.ToString();
            CategoryGuardsCcIndexes = guardsCatCcInd.ToString();
        }
    }
}

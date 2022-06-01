using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestProtocolWorkLib
{
    public static class EventsReader
    {
        public static XElement[] ReadEvents(string fileName, Encoding encoding)
        {
            using StreamReader reader = new(fileName, encoding);
            XElement test = XElement.Load(reader);
            IEnumerable<XElement> events = from el in test.Elements("Events").Elements() select el;
            return events.ToArray();
        }
    }
}

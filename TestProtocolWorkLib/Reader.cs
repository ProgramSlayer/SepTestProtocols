using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestProtocolWorkLib
{
    public static class EventsReader
    {
        public static XElement ReadTest(string filePath, Encoding encoding)
        {
            // TODO: добавить проверки.

            using StreamReader reader = new(filePath, encoding);
            XElement test = XElement.Load(reader);
            return test;
        }

        public static XElement[] GetEvents(XElement test)
        {
            // TODO: добавить проверки.

            IEnumerable<XElement> events = test.Elements("Events").Elements();
            return events.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestProtocolWorkLib
{
    /// <summary>
    /// Класс для работы с файлами протоколов испытаний.
    /// </summary>
    public static class TestProtocolWorker
    {
        /// <summary>
        /// Маска для поиска файлов протоколов.
        /// </summary>
        public const string TestProtocolSearchPattern = "*.sep";
        
        /// <summary>
        /// Расширение файла протокола.
        /// </summary>
        public const string TestProtocolFileExtension = ".sep";
        
        /// <summary>
        /// Возвращает массив файлов протоколов испытаний из папки <paramref name="dirPath"/>.
        /// </summary>
        /// <param name="dirPath">Полный путь папки.</param>
        /// <returns>Массив файлов.</returns>
        public static FileInfo[] GetTestProtocolsFilesFromDirectory(string dirPath)
        {
            DirectoryInfo dirInfo = new(dirPath);

            if (!dirInfo.Exists)
            {
                return Array.Empty<FileInfo>();
            }

            EnumerationOptions options = new() { IgnoreInaccessible = true, RecurseSubdirectories = true };
            IEnumerable<FileInfo> foundFiles = dirInfo.EnumerateFiles(TestProtocolSearchPattern, options);
            return foundFiles.ToArray();
        }

        public static string[] GetTestProtocolsFilesPathsFromDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return Array.Empty<string>();
            }

            EnumerationOptions options = new() { IgnoreInaccessible = true, RecurseSubdirectories = true };
            IEnumerable<string> foundFiles = Directory.EnumerateFiles(dirPath, TestProtocolSearchPattern, options);
            return foundFiles.ToArray();
        }

        public static XDocument LoadTestProtocol(string sepFilePath, Encoding encoding)
        {
            if (!File.Exists(sepFilePath))
            {
                throw new FileNotFoundException($"Не найден sep-файл протокола испытаний '{sepFilePath}'.");
            }

            if (encoding is null)
            {
                throw new NullReferenceException($"Не задана кодировка для чтения sep-файла протокола испытаний '{sepFilePath}'.");
            }

            try
            {
                using StreamReader reader = new(sepFilePath, encoding);
                XDocument tProt = XDocument.Load(reader);
                return tProt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static XElement[] SplitTestProtocol(XDocument protocol)
        {
            if (protocol is null)
            {
                return Array.Empty<XElement>();
            }

            XElement experimentRoot = protocol.Root;

            if (experimentRoot?.Name != "Experiment")
            {
                throw new ArgumentException($"В документе протокола испытаний отсутствует корневой элемент <Experiment>.");
            }

            XElement[] elements = experimentRoot.Elements().Append(experimentRoot).ToArray();
            experimentRoot.RemoveNodes();
            return elements;
        }

        public static void RemoveUnnecessaryTagsFromTest(XElement test)
        {
            if (test?.Name == "Test")
            {
                XElement eventsNode = test.Element("Events");
                if (eventsNode is not null)
                {
                    XElement[] necessaryEvents = (from ev in eventsNode.Elements() select ev).ToArray();
                    eventsNode.RemoveNodes();
                    test.RemoveNodes();
                    eventsNode.AddFirst(necessaryEvents);
                    test.AddFirst(eventsNode);
                }
            }
        }

        public static void SetTestComplecityMarkerTag(XElement test)
        {
            if (test?.Name == "Test")
            {
                IEnumerable<XElement> necessaryEvents = from ev in test.Elements("Events").Elements() where ComplecityMarkersEventTags.Contains(ev.Name.LocalName) select ev;
                if (necessaryEvents.Any())
                {
                    test.AddFirst(new XElement("IsComplete", "true"));
                }
                else
                {
                    test.AddFirst(new XElement("IsComplete", "false"));
                }
            }
        }

        public static void SaveSplitTestProtocol(string testProtocolName, string destinationFolderPath, XElement[] elems)
        {
            if (string.IsNullOrEmpty(testProtocolName))
            {
                throw new ArgumentException("Пустая строка или нулевая ссылка", nameof(testProtocolName));
            }

            DirectoryInfo destination = new(destinationFolderPath);
            if (!destination.Exists)
            {
                throw new DirectoryNotFoundException($"Не найден каталог '{destinationFolderPath}'");
            }

            if (elems?.Length < 1)
            {
                throw new ArgumentNullException(nameof(elems), "Нулевая ссылка или пустой массив");
            }

            DirectoryInfo protocolFolder = destination.CreateSubdirectory(testProtocolName);
            DirectoryInfo testsFolder = protocolFolder.CreateSubdirectory("Tests");

            string savePath;

            foreach (XElement el in elems)
            {
                switch (el.Name.LocalName)
                {
                    case "Test":
                        savePath = Path.Combine(testsFolder.FullName, $"Test {(string)el.Attribute("Index")}.xml");
                        break;
                    default:
                        savePath = Path.Combine(protocolFolder.FullName, $"{el.Name.LocalName}.xml");
                        break;
                }
                try
                {
                    el.Save(savePath);
                }
                catch (Exception) { }
            }
        }

        public static readonly string[] ComplecityMarkersEventTags = { "NonPenetEvent", "PenetrationEvent", "IntruderNeutralizedEvent" };
    }
}

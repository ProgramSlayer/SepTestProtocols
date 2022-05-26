using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using TestProtocolWorkLib;

namespace WpfEventsReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private class TestEvent
        {
            public string Name { get; set; }
            public float CTime { get; set; }
            public string Category { get; set; }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            using OpenFileDialog ofd = new() { CheckFileExists = true, CheckPathExists = true, Filter = "XML-файлы (*.xml)|*.xml" };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = ofd.FileName;
                XElement test = EventsReader.ReadTest(path, Encoding.UTF8);
                XElement[] events = EventsReader.GetEvents(test);
                TestEvent[] source = (from ev in events
                                      select new TestEvent
                                      {
                                          Name = ev.Name.LocalName,
                                          // TODO: избавиться от костыля.
                                          CTime = float.Parse(ev.Attribute("Ctime").Value.Replace('.', ',')),
                                          Category = EventCategory.EventsCategoriesDictionary[ev.Name.LocalName].ToString()
                                      })
                                      .ToArray();
                dgEvents.ItemsSource = source;
            }
        }
    }
}

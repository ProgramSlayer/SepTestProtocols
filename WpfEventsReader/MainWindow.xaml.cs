using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using TestProtocolWorkLib;
using WpfEventsReader.Models;
using WpfEventsReader.Services;

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
        }

        private void btnReadFile_Click(object sender, RoutedEventArgs e)
        {
            using OpenFileDialog openFile = new() { CheckFileExists = true, CheckPathExists = true, Filter = "XML-файлы (*.xml)|*.xml" };
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = openFile.FileName;
                XElement[] xEvents = EventsReader.ReadEvents(path, Encoding.UTF8);
                EventModel[] eventModels = EventModelFactory.Manufacture(xEvents);

                IEnumerable<string> timestamps = (from ev in eventModels select ev.Ctime).Distinct();

                List<TimelineRowModel> source = new();

                foreach (string ts in timestamps)
                {
                    EventModel[] tlRow = (from ev in eventModels where ev.Ctime == ts select ev).ToArray();
                    source.Add(new TimelineRowModel(tlRow));
                }

                dgTimeLine.ItemsSource = source;
            }
        }
    }
}

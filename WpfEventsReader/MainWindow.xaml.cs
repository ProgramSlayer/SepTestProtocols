using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using TestProtocolWorkLib;
using WpfEventsReader.Models;
using WpfEventsReader.ViewModels;

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

        private void btnReadTest_Click(object sender, RoutedEventArgs e)
        {
            using OpenFileDialog openFile = new() { CheckFileExists = true, CheckPathExists = true, Filter = "XML-файлы (*.xml)|*.xml" };
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = openFile.FileName;
                XElement xTest = EventsReader.ReadTest(path, Encoding.UTF8);
                XElement[] xEvents = EventsReader.GetEvents(xTest);
                //IEnumerable<EventModel> source = from xEv in xEvents select new EventModel(xEv);
                var source = from xEv in xEvents select new EventViewModel(xEv);
                dgEventsTable.ItemsSource = source;
            }
        }
    }
}

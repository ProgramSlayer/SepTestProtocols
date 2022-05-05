using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using TestProtocolWorkLib;
using WpfSepSplittingApp.Properties;

namespace WpfSepSplittingApp
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

        public string SepFilesDirectoryPath { get; set; }
        public string ResultDirectoryPath { get; set; }

        private string ProgramStatusText { get; set; }
        
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new() { Owner = this };
            switch (settingsWindow.ShowDialog())
            {
                case true:
                    SepFilesDirectoryPath = settingsWindow.SepFilesDirectoryPath;
                    ResultDirectoryPath = settingsWindow.ResultDirectoryPath;
                    Settings.Default.SepFilesDirPath = SepFilesDirectoryPath;
                    Settings.Default.ResultDirPath = ResultDirectoryPath;
                    Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SepFilesDirectoryPath = Settings.Default.SepFilesDirPath;
            ResultDirectoryPath = Settings.Default.ResultDirPath;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private async void btnLoadSepFilesPaths_ClickAsync(object sender, RoutedEventArgs e)
        {
            ProgramStatusText = $"Идёт поиск sep-файлов внутри каталога '{SepFilesDirectoryPath}'";
            txtbProgramStatus.Text = ProgramStatusText;
            //string[] filePaths = await Task.Run(() => TestProtocolWorker.GetTestProtocolsFilesPathsFromDirectory(SepFilesDirectoryPath));
            //lbSepFilesInfos.ItemsSource = filePaths;
            FileInfo[] fileInfos = await Task.Run(() => TestProtocolWorker.GetTestProtocolsFilesFromDirectory(SepFilesDirectoryPath));
            lbSepFilesInfos.ItemsSource = fileInfos;
            ProgramStatusText = $"Найдено sep-файлов внутри каталога '{SepFilesDirectoryPath}' : {fileInfos.Length}";
            txtbProgramStatus.Text = ProgramStatusText;
        }

        private void lbSepFilesInfos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSplitSelectedSepFile.IsEnabled = e?.AddedItems?.Count == 1;
        }

        private void btnSplitSelectedSepFile_Click(object sender, RoutedEventArgs e)
        {
            var selectedTestProtocolSepFile = lbSepFilesInfos.SelectedItem as FileInfo;
            if (selectedTestProtocolSepFile is null || !selectedTestProtocolSepFile.Exists)
            {
                _ = MessageBox.Show($"Не найден файл '{selectedTestProtocolSepFile?.FullName}'", "Ошибка программы", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                return;
            }

            try
            {
                XDocument tProtDoc = TestProtocolWorker.LoadTestProtocol(selectedTestProtocolSepFile.FullName, Encoding.GetEncoding(1251));
                XElement[] elems = TestProtocolWorker.SplitTestProtocol(tProtDoc);
                foreach (XElement el in elems)
                {
                    TestProtocolWorker.RemoveUnnecessaryTagsFromTest(el);
                    TestProtocolWorker.SetTestComplecityMarkerTag(el);
                }
                TestProtocolWorker.SaveSplitTestProtocol(Path.GetFileNameWithoutExtension(selectedTestProtocolSepFile.FullName), ResultDirectoryPath, elems);
                _ = MessageBox.Show($"Успешно разбит файл протокола испытаний {selectedTestProtocolSepFile.FullName}", "Успех", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
            }
            catch (Exception exc)
            {
                _ = MessageBox.Show(exc.Message, "Ошибка разбиения файла", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }
    }
}

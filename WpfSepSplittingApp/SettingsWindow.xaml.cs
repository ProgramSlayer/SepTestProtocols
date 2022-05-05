using System.Windows;
using System.Windows.Forms;

namespace WpfSepSplittingApp
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            SepFilesDirectoryPath = Properties.Settings.Default.SepFilesDirPath;
            ResultDirectoryPath = Properties.Settings.Default.ResultDirPath;
            txtbSepFilesDirPath.Text = SepFilesDirectoryPath;
            txtbResultDirPath.Text = ResultDirectoryPath;
        }

        public string SepFilesDirectoryPath { get; set; }
        public string ResultDirectoryPath { get; set; }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        enum DirOptions
        {
            SepFiles,
            Result
        }

        private void CallBrowseFolderDialog(string whatUserShouldDoDescription, DirOptions dirOptions)
        {
            using var browser = new FolderBrowserDialog() { Description = whatUserShouldDoDescription };
            if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string chosenDirPath = browser.SelectedPath;
                switch (dirOptions)
                {
                    case DirOptions.SepFiles:
                        SepFilesDirectoryPath = chosenDirPath;
                        txtbSepFilesDirPath.Text = SepFilesDirectoryPath;
                        break;
                    case DirOptions.Result:
                        ResultDirectoryPath = chosenDirPath;
                        txtbResultDirPath.Text = ResultDirectoryPath;
                        break;
                    default:
                        break;
                }
            }
        }

        private void BtnSelectDir_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnSelectSepFilesDir)
            {
                CallBrowseFolderDialog("Выберите каталог с sep-файлами для разбиения", DirOptions.SepFiles);
            }
            else if (sender == btnSelectResultDir)
            {
                CallBrowseFolderDialog("Выберите каталог для сохранения разбитых sep-файлов", DirOptions.Result);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TestProtocolWorkLib;

namespace WpfEventCategoryShow
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

        private List<string> _eventsList = new();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _eventsList = EventCategory.EventsCategoriesDictionary.Keys.ToList();
            lbEvents.ItemsSource = _eventsList;
        }

        private void lbEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbEvents.SelectedItem is string ev && EventCategory.EventsCategoriesDictionary.ContainsKey(ev))
            {
                EventCategory.EventCategories category = EventCategory.EventsCategoriesDictionary[ev];
                List<string> categoriesNames = new();
                switch (category)
                {
                    case EventCategory.EventCategories.Intruder:
                        categoriesNames.Add("Нарушитель");
                        break;
                    case EventCategory.EventCategories.SecurityDevices:
                        categoriesNames.Add("Инженерно-технические средства охраны");
                        break;
                    case EventCategory.EventCategories.Operator:
                        categoriesNames.Add("Оператор СБ");
                        break;
                    case EventCategory.EventCategories.OperatorCctv:
                        categoriesNames.Add("Оператор СВН");
                        break;
                    case EventCategory.EventCategories.Guards:
                        categoriesNames.Add("Группа реагирования");
                        break;
                    case EventCategory.EventCategories.IntruderGuards:
                        categoriesNames.AddRange(new string[] { "Нарушитель", "Группа реагирования" });
                        break;
                    case EventCategory.EventCategories.SecurityDevicesOperator:
                        categoriesNames.AddRange(new string[] { "Инженерно-технические средства охраны", "Оператор СБ", "Оператор СВН" });
                        break;
                    case EventCategory.EventCategories.OperatorGuards:
                        categoriesNames.AddRange(new string[] { "Оператор СБ", "Оператор СВН", "Группа реагирования" });
                        break;
                    case EventCategory.EventCategories.SecurityDevicesIntruder:
                        categoriesNames.AddRange(new string[] { "Инженерно-технические средства охраны", "Нарушитель" });
                        break;
                    case EventCategory.EventCategories.None:
                    default:
                        categoriesNames.Add($"{category}");
                        break;
                }
                lbCategories.ItemsSource = categoriesNames;
            }
        }
    }
}

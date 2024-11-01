using Interface;
using System.IO;
using System.Windows;
using Cherry_Math.Module;
using System.Windows.Controls;

namespace Cherry_Math
{
    public partial class MainWindow : Window
    {
        ModuleManager moduleManager = new ModuleManager();
        private List<UIElement> _originalChildren = new List<UIElement>();
        private bool MainWindowActive = true;

        public MainWindow()
        {
            InitializeComponent();
            MainWindowActive = true;
        }

        private void grMain_Loaded(object sender, RoutedEventArgs e)
        {
            string dependenciesPath = Path.Combine(Environment.CurrentDirectory, "Dependencies");
            string modulesPath = Path.Combine(Environment.CurrentDirectory, "Dependencies");

            CheckAndCreateFolder(dependenciesPath);
            ModuleManager.LoadDependencies(Path.Combine(Environment.CurrentDirectory, "Dependencies"));

            CheckAndCreateFolder(modulesPath);
            moduleManager.LoadModules(this, Path.Combine(Environment.CurrentDirectory, "Modules"));

            _originalChildren.AddRange(grMain.Children.Cast<UIElement>());
        }


        private void CheckAndCreateFolder(string directoryPath)
        {
            if (!new DirectoryInfo(directoryPath).Exists)
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private void item_selected(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                IModule selectedItem = moduleManager.plugins[((ModuleItem)listBox.SelectedItem).Index];

                grMain.Children.Clear();
                grMain.Children.Add((UIElement)selectedItem.Show());
                MainWindowActive = false;
            }
        }

        private void change_list_size(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in listModules.Items)
            {
                if (item is ModuleItem moduleItem)
                {
                    moduleItem.Width = listModules.ActualWidth - 15;
                    moduleItem.ModuleName.Width = listModules.ActualWidth - 15;
                }
            }
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!MainWindowActive)
            {
                RestoreInitialState();
                e.Cancel = true;
            }
        }

        private void RestoreInitialState()
        {
            grMain.Children.Clear();

            foreach (var item in _originalChildren)
            {
                grMain.Children.Add(item);
            }
            MainWindowActive = true;
        }
    }
}
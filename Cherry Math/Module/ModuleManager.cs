using Interface;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cherry_Math.Module
{
    public class ModuleManager
    {
        public List<IModule> plugins = new List<IModule>();
        public Dictionary<string, IModule> pluginsDictionary = new Dictionary<string, IModule>();
        public static MainWindow MainWindow {  get; private set; }

        public void LoadModules(MainWindow window, string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");
            int column = 1;
            MainWindow = window;
            foreach (string file in files)
            {
                try
                {
                    LoadDependencies(Path.Combine(Environment.CurrentDirectory, "Dependencies"));
                    window.CheckAndCreateFolder(Path.Combine(Environment.CurrentDirectory, "Dependencies"));

                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("Interface.IModule");

                        if (null != iface)
                        {
                            IModule obj = (IModule)Activator.CreateInstance(type);

                            plugins.Add(obj);
                            pluginsDictionary.Add(obj.GetModuleName(), obj);
                            AddButtonsToGrid(window, obj, column);
                            ++column;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void AddButtonsToGrid(MainWindow window, IModule module, int column)
        {
            string moduleName = module.GetModuleName();
            // Создаем кнопку
            Button button = new Button();
            button.Content = moduleName;
            button.Margin = new Thickness(5);
            button.Cursor = Cursors.Hand;

            // Устанавливаем колонку для кнопки
            Grid.SetColumn(button, column);

            button.Style = window.FindResource("DockBtnStyle") as Style;

            Bitmap icon = module.GetIcon();
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon.Width, icon.Height)); ;
            // Устанавливаем фон кнопки с изображением
            button.Background = ib;

            // Создаем Popup для кнопки
            Popup popup = new Popup();
            popup.Width = moduleName.Length * 10;
            popup.PlacementTarget = button;
            popup.HorizontalOffset = moduleName.Length * -3.2;
            popup.Style = window.FindResource("PopupStyle") as Style;
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Style = window.FindResource("ArrowPath") as Style;
            // Устанавливаем стиль для Path

            // Содержимое Popup
            Grid popupGrid = new Grid();
            Border border = new Border();
            border.Style = window.FindResource("border") as Style;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = module.GetModuleName();
            textBlock.Style = window.FindResource("PopupText") as Style;
            textBlock.TextWrapping = TextWrapping.Wrap; // Добавляем TextWrapping="Wrap" 

            border.Child = textBlock;
            popupGrid.Children.Add(border);
            popup.Child = popupGrid;
            popupGrid.Children.Add(path);


            // Устанавливаем привязку для отображения Popup
            Binding binding = new Binding("IsMouseOver");
            binding.Source = button;
            binding.Mode = BindingMode.OneWay;
            popup.SetBinding(Popup.IsOpenProperty, binding);
            button.Click += window.ButtonOpenModule_Click;

            // Добавляем кнопку и Popup в Grid
            window.gridModules.Children.Add(button);
            window.gridModules.Children.Add(popup);
        }

        private static void LoadDependencies(string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

using Cherry_Math.Module;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace Cherry_Math
{
    public partial class MainWindow : Window
    {
        public ModuleManager moduleManager = new ModuleManager();

        public MainWindow()
        {
            InitializeComponent();
            
            this.Width = 100;
            DoubleAnimation animation = new DoubleAnimation(1220, TimeSpan.FromSeconds(1.6));
            animation.From = 100;

            animation.Completed += (sender, e) =>
            {
                this.MinHeight = 720;
                this.MinWidth = 1220;
            };

            this.BeginAnimation(Window.WidthProperty, animation);
        }

        private void TextMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void grMain_Loaded(object sender, RoutedEventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));
            moduleManager.LoadModules(this, Path.Combine(Environment.CurrentDirectory, "Modules"));
            
            Bitmap icon = (Bitmap)resources.GetObject("shutdown.Image");
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon.Width, icon.Height)); ;
            BtnCloseWindow.Background = ib;
        }

        public void ButtonOpenModule_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ((Window)moduleManager.pluginsDictionary[button.Content.ToString()].Show()).ShowDialog();
        }

        public static void CheckAndCreateFolder(string directoryPath)
        {
            if (!new DirectoryInfo(directoryPath).Exists)
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void OpenThreeModule(object sender, MouseButtonEventArgs e)
        {
            ((Window)moduleManager.pluginsDictionary[NameModuleThree.Text].Show()).ShowDialog();
        }
    }
}
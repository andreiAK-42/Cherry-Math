using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cherry_Math
{
    /// <summary>
    /// Логика взаимодействия для ModuleItem.xaml
    /// </summary>
    public partial class ModuleItem : UserControl
    {
        public string Name { get; set; }
        public System.Drawing.Image Icon { get; set; }
        public int Index { get; set; }

        public ModuleItem(string name, string icon, int index)
        {
            InitializeComponent();

            ModuleName.Text = name;
            ModuleIcon.Source = new BitmapImage(new Uri(icon));
            Index = index;
        }
    }
}

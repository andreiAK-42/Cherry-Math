using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cherry_Math
{
    public partial class ModuleItem : UserControl
    {
        public string Name { get; }
        public System.Drawing.Image Icon { get; }
        public int Index { get; }

        public ModuleItem(string name, string icon, int index)
        {
            InitializeComponent();

            ModuleName.Text = name;
            ModuleIcon.Source = new BitmapImage(new Uri(icon));
            Index = index;
        }
    }
}

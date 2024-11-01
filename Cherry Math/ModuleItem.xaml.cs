using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Cherry_Math
{
    public partial class ModuleItem : UserControl
    {
        public string Name { get; }
        public Bitmap Icon { get; }
        public int Index { get; }

        public ModuleItem(string name, Bitmap icon, int index)
        {
            InitializeComponent();
            ModuleName.Text = name;
            ModuleIcon.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon.Width, icon.Height));
            Index = index;
        }
    }
}

using System.Drawing;

namespace Interface
{
    public interface IModule
    {
        object Show();
        void Close();
        string GetModuleName();
        Bitmap GetIcon();
    }
}

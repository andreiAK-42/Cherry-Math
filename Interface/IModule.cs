namespace Interface
{
    public interface IModule
    {
        object Show();
        void Close();
        string GetModuleName();
        string GetIcon(string applicationFolder);
    }
}

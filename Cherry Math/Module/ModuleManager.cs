using System.IO;
using Interface;
using System.Reflection;

namespace Cherry_Math.Module
{
    public class ModuleManager
    {
        public List<IModule> plugins = new List<IModule>();
        public List<ModuleItem> modules = new List<ModuleItem>();

        public void LoadModules(MainWindow window, string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("Interface.IModule");

                        if (null != iface)
                        {
                            IModule obj = (IModule)Activator.CreateInstance(type);

                            plugins.Add(obj);
                            modules.Add(new ModuleItem(obj.GetModuleName(), obj.GetIcon(), plugins.Count - 1));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (ModuleItem item in modules)
            {
                window.listModules.Items.Add(item);
            }
        }

        public static void LoadDependencies(string folder)
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

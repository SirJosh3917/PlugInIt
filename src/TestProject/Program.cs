using PlugInIt;
using System;
using System.Threading;
using TestProject.Core;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
			using(var pm = new PluginManager<ITestProjectPlugin>(PluginLoader.LoadPluginsFromDirectory<ITestProjectPlugin>("plugins"))) {
				pm.StartAll();
			}

			Console.ReadLine();
        }
    }
}

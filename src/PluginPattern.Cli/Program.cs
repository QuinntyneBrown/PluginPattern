using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using PluginPattern.SharedKernal;
using System.IO;
using System.Reflection;

namespace PluginPattern.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddMediatR(typeof(Program));

            serviceCollection.AddSingleton<IService, Service>();

            var pluginPath = @"PluginPattern.Plugin\bin\Debug\net5.0\PluginPattern.Plugin.dll";

            Assembly pluginAssembly = LoadPlugin(pluginPath);

            serviceCollection.AddMediatR(pluginAssembly);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            mediator.Publish(new Notification());

            Console.ReadLine();
        }

        static Assembly LoadPlugin(string relativePath)
        {
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }
    }
}

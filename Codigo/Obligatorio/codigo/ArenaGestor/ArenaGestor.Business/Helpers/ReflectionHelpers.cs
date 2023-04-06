using ArenaGestor.BusinessInterface;
using ArenaGestor.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ArenaGestor.Business.Helpers
{
    public class ReflectionHelpers : IReflectionHelpers
    {

        public List<string> GetMethods()
        {
            List<string> methods = new List<string>();
            List<Type> types = GetImportExportTypes();
            foreach (Type type in types)
            {
                IImportExportMethod method = (IImportExportMethod)Activator.CreateInstance(type);
                methods.Add(method.Name);
            }
            return methods;
        }

        public IImportExportMethod GetMethod(string name)
        {
            List<string> methods = new List<string>();
            List<Type> types = GetImportExportTypes();
            foreach (Type type in types)
            {
                IImportExportMethod method = (IImportExportMethod)Activator.CreateInstance(type);
                if (method.Name == name)
                {
                    return method;
                }
            }
            return null;
        }

        private static List<Type> GetImportExportTypes()
        {
            List<Type> types = new List<Type>();

            List<Assembly> assemblyList = GetAssemblies();

            foreach (Assembly assembly in assemblyList)
            {
                List<Type> assemblyTypes = GetTypesInAssembly<IImportExportMethod>(assembly);
                foreach (Type type in assemblyTypes)
                {
                    types.Add(type);
                }
            }

            return types;
        }
        private static List<Type> GetTypesInAssembly<Interface>(Assembly myAssembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in myAssembly.GetTypes())
            {
                if (typeof(Interface).IsAssignableFrom(type))
                    types.Add(type);
            }
            return types;
        }

        private static List<Assembly> GetAssemblies()
        {
            string basePath = GetAssembliesFolder();

            List<Assembly> assemblyList = new List<Assembly>();

            foreach (string filePath in Directory.GetFiles(basePath))
            {
                if (filePath.EndsWith(".dll"))
                {
                    Assembly myAssembly = Assembly.LoadFile(filePath);
                    assemblyList.Add(myAssembly);
                }
            }

            return assemblyList;
        }

        private static string GetAssembliesFolder()
        {
            string directory = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetSection("AppSettings").GetSection("ExtensionsFolder").Value;
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace Main {
    class Program {
        static void Main(string[] args) {

            string curentLocation = System.IO.Directory.GetCurrentDirectory();
            string dd = string.Empty;
            while (dd != "q") {
                string TypeV1 = Path.Combine(curentLocation, @"..\..\..\..\TypeV1\bin\Debug\netcoreapp3.0\TypeV1.dll");
                string TypeV2 = Path.Combine(curentLocation, @"..\..\..\..\TypeV2\bin\Debug\netcoreapp3.0\TypeV2.dll");

                LoadClass(TypeV1);
                LoadClass(TypeV2);
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Press q to Exit");
                  dd = Console.ReadLine();
            }
            return;

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadClass(string path) {




            var context = new CollectibleAssemblyLoadContext();

            Assembly assembly = null;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                  assembly = context.LoadFromStream(fs);
            }



            Type type =   assembly.GetType("TypeTest.Class1"); 
            var getVersion = type.GetMethod("getVersion");
            var instance = Activator.CreateInstance(type);
            Console.WriteLine(getVersion.Invoke(instance, new object[] { }));

     
            context.Unload();
            
             
        }
         
    }

     
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext {
        public CollectibleAssemblyLoadContext() : base(isCollectible: true) { }

        protected override Assembly Load(AssemblyName assemblyName) {
            return null;
        }
    }


}

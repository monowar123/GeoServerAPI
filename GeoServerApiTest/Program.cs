using GeoServerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServerApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string response = string.Empty;
                Workspace wpk = new Workspace();
                DataStore store = new DataStore();
                Layer layer = new Layer();
                LayerStyle style = new LayerStyle();

                var watch = System.Diagnostics.Stopwatch.StartNew();

                //response = wpk.Add("GeoTest");
                //response = store.AddStoreFromDb("GeoTest", "geoDb", "ACC1");
                //response = layer.AddLayerFromDb("GeoTest", "geoDb", "hitbgt", "hitbgt", "polygon");

                response = wpk.GetNamespaceUri("MT12");

                Console.WriteLine(response);

                watch.Stop();
                var time = watch.ElapsedMilliseconds / 1000;
                Console.WriteLine(Convert.ToString(time));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            Console.ReadKey();
        }
    }
}

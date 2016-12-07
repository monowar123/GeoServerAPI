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
            string response = string.Empty;
            Workspace wpk = new Workspace();
            DataStore store = new DataStore();
            Layer layer = new Layer();
            LayerStyle style = new LayerStyle();

            var watch = System.Diagnostics.Stopwatch.StartNew();


            //response = store.Delete("m_test", "storeFromShape");
            //response = store.AddStoreFromShape("m_test", "storeFromShape", "data/m_test/storeFromShape/bag3/BAG3");
            //response = store.UploadShapeFile("m_test", "folderName", "D:/bag3.zip");

            response = layer.GetLayerDetails("DO19", "l1");

            //response = store.GetResources("data/");

            Console.WriteLine(response);



            watch.Stop();
            var time = watch.ElapsedMilliseconds / 1000;
            Console.WriteLine(Convert.ToString(time));

            Console.ReadKey();
        }
    }
}

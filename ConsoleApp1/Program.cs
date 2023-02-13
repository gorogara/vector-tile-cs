using Mapbox.VectorTile;
using Mapbox.VectorTile.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mapbox.VectorTile.PbfReader;
using static Mapbox.VectorTile.VectorTileReader;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var fileBytes = File.ReadAllBytes("188.vector.pbf");
            var pbf = new PbfReader(fileBytes);
            int a = 0;
            
            var vtr = new VectorTileReader(fileBytes);

            var layerNames = vtr.LayerNames();

            // write layerNames to console
            foreach (var layerName in layerNames)
            {
                var layer = vtr.GetLayer(layerName);
                for (int i = 0; i < layer.FeatureCount(); i++)
                {
                    var feature = layer.GetFeature(i);

                    var properties = feature.GetProperties();
                    foreach (var prop in properties)
                    {
                        Debug.WriteLine("key:{0} value:{1}", prop.Key, prop.Value);
                    }

                    foreach (var part in feature.Geometry<int>())
                    {
                        //iterate through coordinates of the part
                        foreach (var geom in part)
                        {
                            Debug.WriteLine("geom.X:{0} geom.Y:{1}", geom.X, geom.Y);
                        }
                    }

                    var wgs84 = VectorTileFeatureExtensions.GeometryAsWgs84(feature, 9, 253, 188);
                    
                    foreach (var pts in wgs84)
                    {
                        foreach (var pt in pts)
                        {
                            Debug.WriteLine("Lat:{0} Lon:{1}", pt.Lat, pt.Lng);
                        }
                    }
                }
            }

            Console.Write("A");
        }
    }
}
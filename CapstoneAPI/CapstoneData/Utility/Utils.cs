using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Utility
{
    public static class Utils
    {
        private static double eQuatorialEarthRadius = 6371D;
        private static double d2r = (Math.PI / 180D);

        public static double HaversineInM(double lat1, double long1, double lat2, double long2)
        {
            return (1000D * HaversineInKMFast(lat1, long1, lat2, long2));
        }

        public static double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * d2r;
            double dlat = (lat2 - lat1) * d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * d2r) * Math.Cos(lat2 * d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = eQuatorialEarthRadius * c;

            return d;
        }

        public static double HaversineInKMFast(double lat1, double long1, double lat2, double long2)
        {
            double dLat = (lat2 - lat1) * Math.PI / 180; // deg2rad below
            double dLon = (long2 - long1) * Math.PI / 180;
            double a =
                    0.5 - Math.Cos(dLat) / 2 +
                            Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                                    (1 - Math.Cos(dLon)) / 2;

            return eQuatorialEarthRadius * 2 * Math.Asin(Math.Sqrt(a));
        }
    }
}

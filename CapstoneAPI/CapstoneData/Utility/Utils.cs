using CapstoneData.Models.Entities;
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

        public static float getPixel(int x, int a, int b)
        {
            if (b == 0)
            {
                return x * a;
            }
            return (x * a) + (x / b);
        }

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

        public static double PerpendicularDistance(Corner pointA, Corner pointB, double longitude, double latitude)
        {
            double bottom = HaversineInKMFast(pointA.Latitude, pointA.Longitude, pointB.Latitude, pointB.Longitude);
            double a = HaversineInKMFast(pointA.Latitude, pointA.Longitude, latitude, longitude);
            double b = HaversineInKMFast(latitude, longitude, pointB.Latitude, pointB.Longitude);
            double p = .5 * (bottom + a + b);
            double area = Math.Sqrt(p * (p - bottom) * (p - a) * (p - b));
            double height = area / bottom * 2;
            return height * 1000D;
        }

        public static double getPixelWithPer(double perpendicular, double currentDistance)
        {
            double temp = Math.Pow(currentDistance, 2);
            temp = temp - Math.Pow(perpendicular, 2);
            temp = Math.Abs(temp);
            double result = Math.Sqrt(temp);

            return result;
        }

        public static void GetPointMap(double latitude, double longitude, List<Corner> corners, float width,
            float height, out float posX, out float posY)
        {
            int corner = 1;
            double min = Utils.PerpendicularDistance(corners[0], corners[1], longitude, latitude);
            double perpendicular = Utils.PerpendicularDistance(corners[1], corners[2], longitude, latitude);
            if (perpendicular <= min)
            {
                min = perpendicular;
                corner = 2;
            }
            perpendicular = Utils.PerpendicularDistance(corners[2], corners[3], longitude, latitude);
            if (perpendicular <= min)
            {
                min = perpendicular;
                corner = 3;
            }
            perpendicular = Utils.PerpendicularDistance(corners[3], corners[0], longitude, latitude);
            if (perpendicular <= min)
            {
                min = perpendicular;
                corner = 4;
            }
            float checkX = 0;
            float checkY = 0;
            if (corner == 1)
            {
                //29  18
                Corner currentCorner1 = corners[1];
                Corner currentCorner2 = corners[0];
                double distance2 = Utils.HaversineInM(latitude, longitude, currentCorner1.Latitude, currentCorner1.Longitude);
                double distanceCorner = Utils.HaversineInM(currentCorner2.Latitude, currentCorner2.Longitude,
                        currentCorner1.Latitude, currentCorner1.Longitude);
                double temp = Utils.getPixelWithPer(min, distance2);
                checkY = (float)(height / distanceCorner * temp);
                currentCorner2 = corners[2];
                distanceCorner = Utils.HaversineInM(currentCorner1.Latitude, currentCorner1.Longitude,
                        currentCorner2.Latitude, currentCorner2.Longitude);
                checkX = (float)(width / distanceCorner * min);
            }
            else if (corner == 3)
            {
                Corner currentCorner1 = corners[2];
                Corner currentCorner2 = corners[3];
                double distance2 = (float)Utils.HaversineInM(latitude, longitude, currentCorner1.Latitude, currentCorner1.Longitude);
                double distanceCorner = Utils.HaversineInM(currentCorner2.Latitude, currentCorner2.Longitude,
                        currentCorner1.Latitude, currentCorner1.Longitude);
                double temp = Utils.getPixelWithPer(min, distance2);
                checkY = (float)(height / distanceCorner * temp);
                currentCorner2 = corners[1];
                distanceCorner = Utils.HaversineInM(currentCorner1.Latitude, currentCorner1.Longitude,
                        currentCorner2.Latitude, currentCorner2.Longitude);
                double x = distanceCorner - (min);
                checkX = (float)(width / distanceCorner * x);
            }
            else if (corner == 2)
            {
                Corner currentCorner1 = corners[2];
                Corner currentCorner2 = corners[1];
                double distance2 = Utils.HaversineInM(latitude, longitude, currentCorner2.Latitude, currentCorner2.Longitude);
                double distanceCorner = Utils.HaversineInM(currentCorner2.Latitude, currentCorner2.Longitude,
                        currentCorner1.Latitude, currentCorner1.Longitude);
                double temp = Utils.getPixelWithPer(min, distance2);
                checkX = (float)(width / distanceCorner * temp);
                currentCorner1 = corners[0];
                distanceCorner = Utils.HaversineInM(currentCorner1.Latitude, currentCorner1.Longitude,
                        currentCorner2.Latitude, currentCorner2.Longitude);
                checkY = (float)(height / distanceCorner * min);
            }
            else if (corner == 4)
            {
                Corner currentCorner1 = corners[3];
                Corner currentCorner2 = corners[0];
                double distance2 = Utils.HaversineInM(latitude, longitude, currentCorner2.Latitude, currentCorner2.Longitude);
                double distanceCorner = Utils.HaversineInM(currentCorner2.Latitude, currentCorner2.Longitude,
                        currentCorner1.Latitude, currentCorner1.Longitude);
                double temp = Utils.getPixelWithPer(min, distance2);
                checkX = (float)(width / distanceCorner * temp);
                currentCorner2 = corners[2];
                distanceCorner = Utils.HaversineInM(currentCorner1.Latitude, currentCorner1.Longitude,
                        currentCorner2.Latitude, currentCorner2.Longitude);
                double x = distanceCorner - min;
                checkY = (float)(height / distanceCorner * x);
            }

            if ((checkX <= width && checkX >= 0) && (checkY <= height && checkY >= 0))
            {
                posX = checkX;
                posY = checkY;
            }
            else
            {
                posX = -1;
                posY = -1;
            }
        }
    }
}

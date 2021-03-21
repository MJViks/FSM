using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM
{
    class SupportFunc
    {
        static public bool InRadius(Point point1, Point point2, int radius) {
            //Расстояние между точками: AB = sqrt((x1 - x2)^2 - (y1 - y2)^2)
            double A = Math.Pow(point1.X - point2.X, 2);       //Квадрат разности A
            double B = Math.Pow(point1.Y - point2.Y, 2);       //Квадрат разности B
            double Distance = Math.Sqrt(A + B);                     //Корень

            //Если расстояние между точками меньше радиуса, то лист найден
            if (Distance < radius)
                return true;
            return false;
        }

        static public Point getCentre(Point pos, int width, int height) {
            int xCentre = pos.X + (width / 2);      
            int yCentre = pos.Y + (height / 2);
            return new Point(xCentre, yCentre);
        }
        public static bool DotInSqrt(Point sqrt, int width, int height, Point dot, int range)
        {
            int x1 = sqrt.X - range;
            int x2 = sqrt.X + width + range;
            int y1 = sqrt.Y - range;
            int y2 = sqrt.Y + height + range;
            if (dot.X > x1 && dot.Y > y1 && dot.X < x2 && dot.Y < y2)
                return true;
            return false;
        }
    }
}

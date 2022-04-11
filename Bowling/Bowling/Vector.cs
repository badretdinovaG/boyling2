using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Vector
    {
        public readonly static Vector zero = new Vector(0, 0, 0);
        private double x, y, z;

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public Vector()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
        public Vector(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }
        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public bool Equal(Vector v2)
        {
            if (x == v2.X && y == v2.Y && z == v2.Z)
                return true;
            else
                return false;
        }
        public double DotProduct(Vector v2)
        {
            return (x * v2.X + y * v2.Y + z * v2.Z);
        }

        public Vector CrossProduct(Vector v2)
        {
            return new Vector(y * v2.Z - z * v2.Y, z * v2.X - x * v2.Z, x * v2.Y - y * v2.X);
        }
        public double AngleBetween(Vector v2)
        {
            double answer = 0;
            double top = this.DotProduct(v2);
            double under = this.Length() * v2.Length();
            double angle;
            if (under != 0)
                answer = top / under;
            else
                return 0;
            if (answer > 1) answer = 1;
            if (answer < -1) answer = -1;
            angle = Math.Acos(answer);
            return (angle * 180 / Math.PI);
        }
        public Vector Unit()
        {
            double length = Math.Sqrt(x * x + y * y + z * z);
            return new Vector(x / length, y / length, z / length);
        }

        public Vector ParralelComponent(Vector v2)
        {
            double lengthSquared, dotProduct, scale;
            lengthSquared = Length() * Length();
            dotProduct = DotProduct(v2);
            if (lengthSquared != 0)
                scale = dotProduct / lengthSquared;
            else
                return new Vector();
            return new Vector(this.Scale(scale));
        }

        public Vector PerpendicularComponent(Vector v2)
        {
            return new Vector(v2 - this.ParralelComponent(v2));
        }

        public Vector Scale(double scale)
        {
            return new Vector(scale * x, scale * y, scale * z);
        }

        public static Vector operator *(double k, Vector v1)
        {
            return new Vector(k * v1.x, k * v1.y, k * v1.z);
        }

        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(k * v1.x, k * v1.y, k * v1.z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector operator -(Vector v1)
        {
            return new Vector(-v1.x, -v1.y, -v1.z);
        }
    }
}

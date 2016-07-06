using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    public class Vector2
    {
        Vector3 _vector;

        public Vector2()
        {
            _vector = new Vector3(0,0,0);
        }

        public Vector2(double magnitude, double direction)
        {
            _vector = new Vector3(magnitude, direction);
        }

        public Vector2(UnitValue magnitude, UnitValue direction)
        {
            _vector = new Vector3(magnitude, direction);
        }
        public Vector2(UnitValue rho, UnitValue phi, UnitValue theta)
        { 
        
        }



        public override bool Equals(object o)
        {
            try
            {
                return (this == (Vector2)o);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _vector.GetHashCode();
        }

        //public UnitValue X
        //{
        //    get { return _vector.Rho; }
        //    set { _vector.Rho = value; }
        //}

        //public UnitValue Y
        //{
        //    get { return _vector.Rho; }
        //    set { _vector.Phi = value; }
        //}

        public UnitValue Magnitude
        {
            get { return _vector.Magnitude; }
            set { _vector.Magnitude = value; }
        }

        public UnitValue Theta
        {
            get { return _vector.Theta; }
            set { _vector.Theta = value; }
        }

        public UnitValue XComponent
        {
            get { return _vector.Rho; }
        }

        public UnitValue YComponent
        {
            get { return _vector.Phi; }
        }

        public override string ToString()
        {
            return _vector.ToString();
        }

        public static Vector2 operator +(Vector2 first, Vector2 second)
        {
            return
            (
               new Vector2
               (
                  first.Magnitude + second.Magnitude,
                  first.Theta + second.Theta
               )
            );
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return
            (
               new Vector2
               (
                   v1.Magnitude - v2.Magnitude,
                   v1.Theta - v2.Theta
               )
            );
        }
        public static Vector2 operator -(Vector2 v1)
        {
            return
            (
               new Vector2
               (
                  -v1.Magnitude.Value,
                  -v1.Theta.Value
               )
            );
        }
        public static Vector2 operator +(Vector2 v1)
        {
            return
            (
               new Vector2
               (
                  +v1.Magnitude.Value,
                  +v1.Theta.Value
               )
            );
        }
        public static bool operator <(Vector2 v1, Vector2 v2)
        {
            return v1.Magnitude < v2.Magnitude;
        }
        public static bool operator <=(Vector2 v1, Vector2 v2)
        {
            return v1.Magnitude <= v2.Magnitude;
        }
        public static bool operator >(Vector2 v1, Vector2 v2)
        {
            return !(v1 < v2);
        }
        public static bool operator >=(Vector2 v1, Vector2 v2)
        {
            return v1.Magnitude >= v2.Magnitude;
        }
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return
            (
               (v1.Magnitude == v2.Magnitude) &&
               (v1.Theta == v2.Theta)
            );
        }
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }
        public static Vector2 operator /(Vector2 v1, double s2)
        {
            return
            (
               new Vector2
               (
                  v1.Magnitude / s2,
                  v1.Theta / s2
               )
            );
        }
        public static Vector2 operator *(Vector2 v1, double s2)
        {
            return
            (
               new Vector2
               (
                  v1.Magnitude * s2,
                  v1.Theta * s2
               )
            );
        }
        public static Vector2 operator *(double s1, Vector2 v2)
        {
            return v2 * s1;
        }

        public static UnitValue DotProduct(Vector2 v1, Vector2 v2)
        {
            return
            (
               v1.Magnitude * v2.Magnitude +
               v1.Theta * v2.Theta
            );
        }

        public static Vector2 NormalVector(Vector2 v1)
        {
            if (v1.Magnitude.Value == 0d)
                throw new Exception();
            var scalar = (1d / v1.Magnitude.Value);
            return new Vector2
            (
                v1.Magnitude * scalar,
                v1.Theta * scalar
            );
        }
        public static UnitValue Distance(Vector2 v1, Vector2 v2)
        {
            return
            (
               
               (
                   (v1.Magnitude - v2.Magnitude).ToPower(2) +
                   (v1.Theta - v2.Theta).ToPower(2)
               ).ToPower(.5)
            );
        }

        //public static double Angle(Vector2 v1, Vector2 v2)
        //{
        //    var n1 = NormalVector(v1);
        //    var n2 = NormalVector(v2);
        //    var dot = DotProduct(n1, n2);

        //    return Math.Acos(dot);
        //}
        public static bool IsPerpendicular(Vector2 v1, Vector2 v2)
        {
            return DotProduct(v1, v2).Value == 0d;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    class Vector3
    {
        private UnitValue _rho; // rho
        private UnitValue _phi; // phi
        private UnitValue _theta; // theta
        private UnitValue _magnitude;

        public override bool Equals(object o)
        {
            try
            {
                return (this == (Vector3)o);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Three Dimesionial Coordinates
        /// 
        /// (x, y, z)
        /// (rho, phi, theta)
        /// rho: Distance from origin
        /// phi: similar to latitude
        /// theta: the same as xy cartesian coordinates. angle from the xz plane to the point
        /// 
        /// for physics and UnitValue rho is _value and phi/theta angles, so therefore dimensionless.
        /// all 3 be unit value (standardunit, degree/radian, degree/radian)
        /// Conversions
        /// 
        /// BigS = SquareRoot(x^2+y^2) == (rho)*sin(theta);
        /// rho = SquareRoot(BigS*2 + z^2) == SquareRoot(x^2+y^2+z^2);
        /// x = BigS*cos(theta) == rho*sin(phi) * cos(theta);
        /// y = BigS * sin(theta) == rho*sin(phi)*sin(theta)
        /// z = rho * cos(phi);
        /// 
        /// -------------------------------------------
        /// 
        /// rho = SquareRoot(x^2 + y^2 + z^2);
        /// BigS = SquareRoot(x^2 + y^2);
        /// phi = arccose (z/rho);
        /// if (x >= 0)
        ///     theta = arcsin(y / BigS);
        /// else
        ///    theta = pi - arcsin(y/BigS);
        ///    
        /// theta == angle of ascension
        /// phi == angle of declination
        /// </summary>
        /// <returns></returns>

        public Vector3(UnitValue rho, UnitValue phi, UnitValue theta)
        {
            _rho = rho;
            _phi = phi;
            _theta = theta;
            _magnitude = new UnitValue(Math.Sqrt(_rho.ToPower(2).Value + _phi.ToPower(2).Value + _theta.ToPower(2).Value), _rho.Dividend, _rho.Divisor);
        }

        public Vector3(double rho, double phi, double theta)
        {
            _rho = new UnitValue(rho);
            _phi = new UnitValue(phi);
            _theta = new UnitValue(theta);
        }

        public Vector3(double magnitude, double direction)
        {
            _magnitude = new UnitValue(magnitude);
            _rho= new UnitValue(magnitude * Math.Cos(direction));
            _phi= new UnitValue(magnitude * Math.Sin(direction));
            _theta= new UnitValue(direction);
        }

        public Vector3(UnitValue magnitude, UnitValue direction)
        {
            var directionInRadians = (direction.ConvertTo(StandardType.radian).Value);
            _magnitude = magnitude;
            _rho = new UnitValue(magnitude.Value * Math.Cos(directionInRadians), magnitude.Dividend, magnitude.Divisor);
            _phi = new UnitValue(magnitude.Value * Math.Sin(directionInRadians), magnitude.Dividend, magnitude.Divisor);
            _theta = direction;
        }

        private UnitValue GetMagnitudeFromComponents()
        {
            return (Rho.ToPower(2) + Phi.ToPower(2)).ToPower(.5);
        }

        public override int GetHashCode()
        {
            return (_rho + _phi + _theta).GetHashCode();
        }

        public double ThetaCalculated
        {
            get { return Math.Atan(_phi.Value / _rho.Value); }
        }

        public UnitValue Rho
        {
            get { return _rho; }
            set { _rho = value; }
        }
        public UnitValue Phi
        {
            get { return _phi; }
            set { _phi = value; }
        }

        public UnitValue Theta
        {
            get { return _theta; }
            set { _theta = SetTheta(value); }
        }

        private UnitValue SetTheta(UnitValue value)
        {
            var result = value;

            _rho.Value = _magnitude.Value * Math.Cos(value.Value);
            _phi.Value = _magnitude.Value * Math.Sin(value.Value);
            result.Value = 1d / Math.Tan(_phi.ConvertTo(PowersOfTenPrefix.none).Value / _rho.ConvertTo(PowersOfTenPrefix.none).Value);

            return result;
        }

        public Vector3()
        {
            _rho = new UnitValue();
            _phi = new UnitValue();
            _theta = new UnitValue();
            _magnitude = new UnitValue();
        }

        public UnitValue Magnitude
        {
            get { 
                return _magnitude;
                //return new UnitValue(Math.Sqrt(_rho.ToPower(2).Value + _phi.ToPower(2).Value + _theta.ToPower(2).Value),_rho.Dividend, _rho.Divisor); 
            }
            set
            {
                if (value.Value < 0d)
                    return;

                var scalar = value / Magnitude;
                 _rho *= (scalar);
                 _phi *= (scalar);
                 _theta *= (scalar);
            }
        }

        public static Vector3 operator +(Vector3 first, Vector3 second)
        {
            return
            (
               new Vector3
               (
                  first.Rho + second.Rho,
                  first.Phi + second.Phi,
                  first.Theta + second.Theta
               )
            );
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return
            (
               new Vector3
               (
                   v1.Rho - v2.Rho,
                   v1.Phi - v2.Phi,
                   v1.Theta - v2.Theta
               )
            );
        }
        public static Vector3 operator -(Vector3 v1)
        {
            return
            (
               new Vector3
               (
                  -v1.Rho.Value,
                  -v1.Phi.Value,
                  -v1.Theta.Value
               )
            );
        }
        public static Vector3 operator +(Vector3 v1)
        {
            return
            (
               new Vector3
               (
                  +v1.Rho.Value,
                  +v1.Phi.Value,
                  +v1.Theta.Value
               )
            );
        }
        public static bool operator <(Vector3 v1, Vector3 v2)
        {
            return v1.Magnitude < v2.Magnitude;
        }
        public static bool operator <=(Vector3 v1, Vector3 v2)
        {
            return v1.Magnitude <= v2.Magnitude;
        }
        public static bool operator >(Vector3 v1, Vector3 v2)
        {
            return !(v1 < v2);
        }
        public static bool operator >=(Vector3 v1, Vector3 v2)
        {
            return v1.Magnitude >= v2.Magnitude;
        }
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return
            (
               (v1.Rho == v2.Rho) &&
               (v1.Phi == v2.Phi) &&
               (v1.Theta == v2.Theta)
            );
        }
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }
        public static Vector3 operator /(Vector3 v1, double s2)
        {
            return
            (
               new Vector3
               (
                  v1.Rho / s2,
                  v1.Phi / s2,
                  v1.Theta / s2
               )
            );
        }
        public static Vector3 operator *(Vector3 v1, double s2)
        {
            return
            (
               new Vector3
               (
                  v1.Rho * s2,
                  v1.Phi * s2,
                  v1.Theta * s2
               )
            );
        }
        public static Vector3 operator *(double s1, Vector3 v2)
        {
            return v2 * s1;
        }
        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            return
            (
               new Vector3
               (
                  v1.Phi * v2.Theta - v1.Theta * v2.Phi,
                  v1.Theta * v2.Rho - v1.Rho * v2.Theta,
                  v1.Rho * v2.Phi - v1.Phi * v2.Rho
               )
            );
        }
        public static UnitValue DotProduct(Vector3 v1, Vector3 v2)
        {
            return
            (
               v1.Rho * v2.Rho +
               v1.Phi * v2.Phi +
               v1.Theta * v2.Theta
            );
        }

        public static Vector3 NormalVector(Vector3 v1)
        {
            if (v1.Magnitude.Value == 0d)
                throw new Exception();
            var scalar = (1d / v1.Magnitude.Value);
            return new Vector3
            (
                v1.Rho * scalar,
                v1.Phi * scalar,
                v1.Theta * scalar
            );
        }
        public static UnitValue Distance(Vector3 v1, Vector3 v2)
        {
            return
            (
               
               (
                   (v1.Rho - v2.Rho) * (v1.Rho - v2.Rho) +
                   (v1.Phi - v2.Phi) * (v1.Phi - v2.Phi) +
                   (v1.Theta - v2.Theta) * (v1.Theta - v2.Theta)
               ).ToPower(.5)
            );
        }
        public static UnitValue Angle(Vector3 v1, Vector3 v2)
        {
            var n1 = NormalVector(v1);
            var n2 = NormalVector(v2);
            var dot = DotProduct(n1, n2);

            return new UnitValue(Math.Acos(dot.Value));
        }
        public static Vector3 Pitch(Vector3 v1, double degree) // around x-axis
        {
            var x = v1.Rho;
            var y = (v1.Phi * Math.Cos(degree)) - (v1.Theta * Math.Sin(degree));
            var z = (v1.Phi * Math.Sin(degree)) + (v1.Theta * Math.Cos(degree));
            return new Vector3(x, y, z);
        }
        public static Vector3 Yaw(Vector3 v1, double degree) // around y-axis
        {
            var x = (v1.Theta * Math.Sin(degree)) + (v1.Rho * Math.Cos(degree));
            var y = v1.Phi;
            var z = (v1.Theta * Math.Cos(degree)) - (v1.Rho * Math.Sin(degree));
            return new Vector3(x, y, z);
        }
        public static Vector3 Roll(Vector3 v1, double degree) // around z-axis
        {
            var x = (v1.Rho * Math.Cos(degree)) - (v1.Phi * Math.Sin(degree));
            var y = (v1.Rho * Math.Sin(degree)) + (v1.Phi * Math.Cos(degree));
            var z = v1.Theta;
            return new Vector3(x, y, z);
        }
        public static bool IsPerpendicular(Vector3 v1, Vector3 v2)
        {
            return DotProduct(v1, v2).Value == 0d;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{

    public static class Constants
    {
        public static UnitValue UniversalGravitationalConstant;
        static Constants()
        {
            UniversalGravitationalConstant = new UnitValue(6.67E-11, StandardType.meter, PowersOfTenPrefix.none, 3);
            UniversalGravitationalConstant.Divisor.Add(new Unit(StandardType.gram, PowersOfTenPrefix.kilo));
            UniversalGravitationalConstant.Divisor.Add(new Unit(StandardType.second, PowersOfTenPrefix.none, 2));
        }
        public static readonly UnitValue EarthGravity = new UnitValue(-9.8, StandardType.meter, new Unit(StandardType.second, PowersOfTenPrefix.none, 2));
        public static readonly UnitValue JupityGravity = new UnitValue(-25, StandardType.meter, new Unit(StandardType.second, PowersOfTenPrefix.none, 2));

        public static readonly UnitValue MassOfEarth = new UnitValue(5.98E24, StandardType.gram, PowersOfTenPrefix.kilo);
        public static readonly UnitValue MassOfMoon = new UnitValue(7.36E22, StandardType.gram, PowersOfTenPrefix.kilo);
        public static readonly UnitValue MassOfSun = new UnitValue(1.99E30, StandardType.gram, PowersOfTenPrefix.kilo);
        public static readonly UnitValue StandardAtmosphericPressure = new UnitValue(1.013E5, StandardType.pascal, PowersOfTenPrefix.none);
        public static readonly UnitValue ElectronMass = new UnitValue(9.11E-31, StandardType.gram, PowersOfTenPrefix.kilo);
        
    }

    public enum StandardType
    {
        constant,
        gram,
        second,
        meter,
        liter,
        ampere,
        atomicMassUnit,
        atmosphere,
        britishThermalUnit,
        coulomb,
        degree,
        degreeCelsius,
        degreeFahrenheit,
        degreeKelvin,
        calorie,
        day,
        electronVolt,
        farad,
        foot,
        gauss,
        henry,
        hour,
        horsepower,
        hertz,
        inch,
        joule,
        mole,
        pound,
        lightyear,
        minute,
        newton,
        pascal,
        radian,
        revolution,
        tesla,
        volt,
        watt,
        weber,
        year,
        ohm,
        mile,
        yard
    }

    public enum PowersOfTenPrefix
    {
        yocto = -24,
        zepto = -21,
        atto = -18,
        femto = -15,
        pico = -12,
        nano = -9,
        micro = -6,
        milli = -3,
        centi = -2,
        deci = -1,
        none = 0,
        deka = 1,
        hecto = 2,
        kilo = 3,
        mega = 6,
        giga = 9,
        tera = 12,
        peta = 15,
        exa = 18,
        zetta = 21,
        yotta = 24
    }

    public struct Unit
    {
        private StandardType _type;
        private PowersOfTenPrefix _prefix;
        private double _exponent;

        public Unit(StandardType type)
        {
            _type = type;
            _prefix = PowersOfTenPrefix.none;
            _exponent = 1;
        }

        public Unit(StandardType type, PowersOfTenPrefix prefix)
        {
            _type = type;
            _prefix = prefix;
            _exponent = 1;
        }

        public Unit(StandardType type, PowersOfTenPrefix prefix, double exponent)
        {
            _type = type;
            _prefix = prefix;
            _exponent = exponent;
        }

        public StandardType UnitType
        {
            get { return _type; }
            set { _type = value; }
        }

        public PowersOfTenPrefix Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        public double Exponent
        {
            get { return _exponent; }
            set { _exponent = value; }
        }

        public override string ToString()
        {
            string result = string.Empty;
            if (_type == StandardType.constant)
                return result;

            result = _prefix.Abbreviation() + _type.Abbreviation();
            if (_exponent == 0 || _exponent == 1)
                return result;
            result += "^" + _exponent.ToString();
            return result;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return (this == (Unit)obj);
            }
            catch
            {
                return false;
            }
        }

        public static bool operator==(Unit first, Unit second)
        {
            if (System.Object.ReferenceEquals(first, second))
                return true;
            if (((object)first == null) || ((object)second == null))
                return false;
            return (first.Exponent == second.Exponent && first.Prefix == second.Prefix && first.UnitType == second.UnitType);
        }

        public static bool operator !=(Unit first, Unit second)
        {
            return !(first == second);
        }

    }
}

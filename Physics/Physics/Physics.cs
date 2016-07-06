using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    public struct PolarCoordiante
    {
        private double _distance;
        private double _thetaDegrees;
        private double _x;
        private double _y;

        public PolarCoordiante(double distance, double thetaDegrees)
        {
            _distance = distance;
            _thetaDegrees = thetaDegrees;

            _x = _distance * Math.Cos(_thetaDegrees);
            _y = _distance * Math.Sin(_thetaDegrees);
        }
        public UnitValue ToDegrees()
        {
            return new UnitValue();
        }
    }
    public struct UnitValue
    {
        private int _expontential;
        private List<Unit> _dividend;
        private List<Unit> _divisor;
        private double _value;

        public UnitValue(double value)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
        
        }

        public UnitValue(double value, StandardType type, PowersOfTenPrefix prefix)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(new Unit(type, prefix, 1));
        }

        public UnitValue(double value, StandardType type, PowersOfTenPrefix prefix, double unitExponent)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(new Unit(type, prefix, unitExponent));
        }

        public UnitValue(double value, StandardType dividendType, PowersOfTenPrefix dividendPower, StandardType divisorType, PowersOfTenPrefix divisorPower)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(new Unit(dividendType, dividendPower));
            _divisor.Add(new Unit(divisorType, divisorPower));
        }

        public UnitValue(double value, StandardType dividendType, PowersOfTenPrefix dividendPower, double dividendExponent, StandardType divisorType, PowersOfTenPrefix divisorPower, double divisorExponent)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(new Unit(dividendType, dividendPower, dividendExponent));
            _divisor.Add(new Unit(divisorType, divisorPower, divisorExponent));
        }

        public UnitValue(double value, StandardType dividend, StandardType divisor)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();

            _dividend.Add(new Unit(dividend));
            _divisor.Add(new Unit(divisor));
        }
        public UnitValue(double value, Unit unit)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(unit);
        }

        public UnitValue(double value, Unit dividend, Unit divisor)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(dividend);
            _divisor.Add(divisor);
        }

        public UnitValue(double value, StandardType type)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.Add(new Unit(type));
        }

        public UnitValue(double value, StandardType dividend, Unit divisor)
        {
            _value = value;
            _expontential = 0;
            _dividend = new List<Unit>();
            _dividend.Add(new Unit(dividend));
            _divisor = new List<Unit>();
            _divisor.Add(divisor);
        }

        public UnitValue(double value, List<Unit> dividend)
        {
            _value = value;
            _expontential = 0;
            _dividend = dividend;
            _divisor = new List<Unit>();
        }

        public UnitValue(double value, List<Unit> dividend, List<Unit> divisor)
        {
            _value = value;
            _expontential = 0;
            _dividend = dividend;
            _divisor = divisor;
        }

        public override bool Equals(object o)
        {
            try
            {
                return (this == (UnitValue)o);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public UnitValue(UnitValue unitValue)
        {
            _value = unitValue.Value;
            _expontential = unitValue.Exponential;
            _dividend = new List<Unit>();
            _divisor = new List<Unit>();
            _dividend.AddRange(unitValue.Dividend);
            _divisor.AddRange(unitValue.Divisor);
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }


        public int Exponential
        {
            get { return _expontential; }
            set { _expontential = value; }
        }

        public List<Unit> Divisor
        {
            get { return _divisor; }
            set { _divisor = value; }
        }

        public List<Unit> Dividend
        {
            get { return _dividend; }
            set { _dividend = value; }
        }

        public static bool TryParse(string s, out UnitValue unitValue)
        {
            var result = false;
                unitValue = new UnitValue();

                var stringArray = s.Split(' ');
                if (stringArray.Length != 2)
                    return result;

                if (!double.TryParse(stringArray[0], out unitValue._value))
                    return result;

                var abbrev = stringArray[1];
                if (abbrev.Length == 0)
                    return result;

                var unit = abbrev.Substring(abbrev.Length - 1, 1); 
            return true;
        }

        public override string ToString()
        {
            string result = _value.ToString() + " ";
            result += GetUnitString();
            return result;
        }

        public string GetUnitString()
        {
            var result = string.Empty;
            foreach (var dividend in _dividend)
                result += dividend.ToString();

            string divisors = string.Empty;

            foreach (var divisor in _divisor)
                divisors += divisor.ToString();

            if (!string.IsNullOrEmpty(divisors))
                result += "/" + divisors;

            return result;
        }

        public static UnitValue Subtract(UnitValue first, UnitValue second)
        {
            if (first.Value == 0)
                return second;
            if (second.Value == 0)
                return first;

            if (!first.CanConvertTo(second))
                throw new Exception();

            var converted = second.ConvertTo(first);
            var result = converted;

            result.Value = first.Value - converted.Value;

            return result;
        }


        public static bool LessThan(UnitValue first, UnitValue second)
        {
            if (!first.CanConvertTo(second))
                throw new Exception();

            var converted = second.ConvertTo(first);
            var result = converted;

            return(first.Value < second.Value);         
        }

        public static UnitValue operator -(UnitValue v1)
        {
            var result = new UnitValue(v1);
            result.Value = -v1.Value;
            return result;
        }

        public static bool operator <(UnitValue v1, UnitValue v2)
        {
            return LessThan(v1, v2);
        }
        public static bool operator <=(UnitValue first, UnitValue second)
        {
            if (!first.CanConvertTo(second))
                throw new Exception();

            var converted = second.ConvertTo(first);
            var result = converted;

            return (first.Value <= second.Value);    
        }
        public static bool operator >(UnitValue first, UnitValue second)
        {
            return !(first < second);
        }
        public static bool operator >=(UnitValue first, UnitValue second)
        {
            if (!first.CanConvertTo(second))
                throw new Exception();

            var converted = second.ConvertTo(first);
            var result = converted;

            return (first.Value >= second.Value);    
        }

        public static UnitValue Multiply(UnitValue first, UnitValue second)
        {
            var result = new UnitValue(1);

            result.Dividend.AddRange(first.Dividend);
            result.Dividend.AddRange(second.Dividend);

            result.Divisor.AddRange(first.Divisor);
            result.Divisor.AddRange(second.Divisor);

            result.Value = first.Value * second.Value;

            if (result.Value == 0)
                return new UnitValue(0);

            var newDividend = new List<Unit>();
            var newDivisor = new List<Unit>();

            foreach (var item in result.Dividend)
            {
                var temp = (from div in result.Dividend
                                where item.UnitType == div.UnitType
                                select div).ToList();
                var exp = temp.Sum(x => x.Exponent);
                var pows = temp.Sum(x => (int)x.Prefix);
                if (exp != 0)
                {
                   if (!newDividend.Exists(x => (x.UnitType == temp[0].UnitType && x.Prefix == temp[0].Prefix)))
                        newDividend.Add(new Unit(temp[0].UnitType, (PowersOfTenPrefix)pows, (double)exp));
                }
            }

            foreach (var item in result.Divisor)
            {
                var temp = (from div in result.Divisor
                            where item.UnitType == div.UnitType
                            select div).ToList();
                var exp = temp.Sum(x => x.Exponent);
                var pows = temp.Sum(x => (int)x.Prefix);
                if (exp != 0)
                {
                    if (!newDivisor.Exists(x => (x.UnitType == temp[0].UnitType && x.Prefix == temp[0].Prefix)))
                        newDivisor.Add(new Unit(temp[0].UnitType, (PowersOfTenPrefix)pows, (double)exp));
                }
            }

            var unitTypes = (from unit in newDividend
                                            select unit.UnitType).ToList();
            var itemsToRemove = new List<Unit>();
            foreach(var item in unitTypes)
            {
                var inDividend = newDividend.SingleOrDefault(x => x.UnitType == item);
                var inDivisor = newDivisor.SingleOrDefault(x => x.UnitType == item);

                var newExponent = inDividend.Exponent - inDivisor.Exponent;
                newDividend.RemoveAll(x => x.UnitType == item);
                newDivisor.RemoveAll(x => x.UnitType == item);
                if (newExponent != 0)
                    newDividend.Add(new Unit(item, PowersOfTenPrefix.none, newExponent));
            }
            result.Dividend = newDividend;
            result.Divisor = newDivisor;            

            var flipListDividend = (from row in newDividend
                                   where row.Exponent < 0
                                   select new Unit(row.UnitType, row.Prefix, row.Exponent * -1)).ToList();
            var flipListDivisor = (from row in newDivisor
                                   where row.Exponent < 0
                                   select new Unit(row.UnitType, row.Prefix, row.Exponent * -1)).ToList();

            foreach (var item in flipListDividend)
                newDividend.RemoveAll(x => x.UnitType == item.UnitType);
            foreach (var item in flipListDivisor)
                newDivisor.RemoveAll(x => x.UnitType == item.UnitType);

            result.Dividend.AddRange(flipListDivisor);
            result.Divisor.AddRange(flipListDividend);
            return result;
        }

       

        public static UnitValue Divide(UnitValue first, UnitValue second)
        {
            var result = new UnitValue(1);

            result.Value = 1 / second.Value;
            foreach (var item in second.Dividend)
            {
                var newitem = item;
                result.Divisor.Add(newitem);
            }
            foreach (var item in second.Divisor)
            {
                var newitem = item;

                result.Dividend.Add(newitem);
            }
            return (first * result);
        }

        public static UnitValue Add(UnitValue first, UnitValue second)
        {
            if (first.Value == 0)
                return second;
            if (second.Value == 0)
                return first;

            if (!first.CanConvertTo(second))
                throw new Exception();

            var converted = second.ConvertTo(first);
            var result = converted;

            result.Value = first.Value + converted.Value;

            return result;
        }

        public static UnitValue operator +(UnitValue first, UnitValue second)
        {
            return Add(first, second);
        }

        public static UnitValue operator -(UnitValue first, UnitValue second)
        {
            return Subtract(first, second);
        }

        public static UnitValue operator *(int scalar, UnitValue unitValue)
        {
            var result = new UnitValue(unitValue);
            result.Value = scalar * unitValue.Value;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }

        public static UnitValue operator *(UnitValue unitValue, int scalar)
        {
            var result = new UnitValue(unitValue);
            result.Value = scalar * unitValue.Value;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }

        public static UnitValue operator *(double scalar, UnitValue unitValue)
        {
            var result = new UnitValue(unitValue);
            result.Value = scalar * unitValue.Value;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }

        public static UnitValue operator *(UnitValue unitValue, double scalar)
        {
            var result = new UnitValue(unitValue);
            result.Value = scalar * unitValue.Value;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }
        public static UnitValue operator *(UnitValue first, UnitValue second)
        {
            return Multiply(first, second);
        }

        public static UnitValue operator /(UnitValue first, UnitValue second)
        {
            return Divide(first, second);
        }

        public static UnitValue operator /(double scalar, UnitValue unitValue)
        {
            if (unitValue.Value == 0)
                throw new Exception("Divide by zero error");
            var result = new UnitValue(unitValue);
            result.Value = scalar / unitValue.Value;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }

        public static bool operator <(int compare, UnitValue unitValue)
        {
            if (compare < unitValue.ConvertTo(PowersOfTenPrefix.none).Value)
                return true;
            return false;
        }

        public static bool operator >(int compare, UnitValue unitValue)
        {
            if (compare > unitValue.ConvertTo(PowersOfTenPrefix.none).Value)
                return true;
            return false;
        }

        public static bool operator <(UnitValue unitValue, int compare)
        {
            if (unitValue.ConvertTo(PowersOfTenPrefix.none).Value < compare)
                return true;
            return false;
        }

        public static bool operator >(UnitValue unitValue, int compare)
        {
            if (unitValue.ConvertTo(PowersOfTenPrefix.none).Value > compare)
                return true;
            return false;
        }


        public static bool operator <(double compare, UnitValue unitValue)
        {
            if (compare < unitValue.ConvertTo(PowersOfTenPrefix.none).Value)
                return true;
            return false;
        }

        public static bool operator >(double compare, UnitValue unitValue)
        {
            if (compare > unitValue.ConvertTo(PowersOfTenPrefix.none).Value)
                return true;
            return false;
        }

        public static bool operator <(UnitValue unitValue, double compare)
        {
            if (unitValue.ConvertTo(PowersOfTenPrefix.none).Value < compare)
                return true;
            return false;
        }

        public static bool operator >(UnitValue unitValue, double compare)
        {
            if (unitValue.ConvertTo(PowersOfTenPrefix.none).Value > compare)
                return true;
            return false;
        }

        public static UnitValue operator /(UnitValue unitValue, double scalar)
        {
            if (scalar == 0)
                throw new Exception("Divide by zero error");
            var result = new UnitValue(unitValue);
            result.Value = unitValue.Value / scalar;
            if (result.Value == 0)
                result = new UnitValue(0);
            return result;
        }

        public static bool operator ==(UnitValue first, UnitValue second)
        {
            if (System.Object.ReferenceEquals(first, second))
                return true;
            if (((object)first == null) || ((object)second == null))
                return false;
            return ((first - second).Value == 0);
        }

        public static bool operator !=(UnitValue first, UnitValue second)
        {
            return !(first == second);
        }
    }

    public static class ExtensionMethods
    {
        private static bool CanConvertTo(StandardType type, StandardType newType)
        {
            switch (type)
            { 
                case StandardType.constant:
                    return false;
                case StandardType.day:
                    switch (newType)
                    { 
                        case StandardType.hour:
                        case StandardType.minute:
                        case StandardType.second:
                        case StandardType.day:
                        case StandardType.year:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.hour:
                    switch (newType)
                    { 
                        case StandardType.hour:
                        case StandardType.minute:
                        case StandardType.second:
                        case StandardType.day:
                        case StandardType.year:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.minute:
                    switch (newType)
                    { 
                        case StandardType.hour:
                        case StandardType.minute:
                        case StandardType.second:
                        case StandardType.day:
                        case StandardType.year:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.second:
                    switch (newType)
                    { 
                        case StandardType.hour:
                        case StandardType.minute:
                        case StandardType.second:
                        case StandardType.day:
                        case StandardType.year:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.year:
                    switch (newType)
                    { 
                        case StandardType.hour:
                        case StandardType.minute:
                        case StandardType.second:
                        case StandardType.day:
                        case StandardType.year:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.degree:
                    switch (newType)
                    { 
                        case StandardType.radian:
                        case StandardType.degree:
                            return true;
                        default:
                            return false;
                    }

                case StandardType.degreeCelsius:
                    switch (newType)
                    { 
                        case StandardType.degreeCelsius:
                        case StandardType.degreeFahrenheit:
                        case StandardType.degreeKelvin:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.degreeFahrenheit:
                    switch (newType)
                    {
                        case StandardType.degreeCelsius:
                        case StandardType.degreeFahrenheit:
                        case StandardType.degreeKelvin:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.degreeKelvin:
                    switch (newType)
                    {
                        case StandardType.degreeCelsius:
                        case StandardType.degreeFahrenheit:
                        case StandardType.degreeKelvin:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.mile:
                    switch (newType)
                    {
                        case StandardType.mile:
                        case StandardType.meter:
                        case StandardType.inch:
                        case StandardType.foot:
                        case StandardType.lightyear:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.meter:
                    switch (newType)
                    {
                        case StandardType.mile:
                        case StandardType.meter:
                        case StandardType.inch:
                        case StandardType.foot:
                        case StandardType.lightyear:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.inch:
                    switch (newType)
                    {
                        case StandardType.mile:
                        case StandardType.meter:
                        case StandardType.inch:
                        case StandardType.foot:
                        case StandardType.lightyear:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.foot:
                    switch (newType)
                    {
                        case StandardType.mile:
                        case StandardType.meter:
                        case StandardType.inch:
                        case StandardType.foot:
                        case StandardType.lightyear:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.lightyear:
                    switch (newType)
                    {
                        case StandardType.mile:
                        case StandardType.meter:
                        case StandardType.inch:
                        case StandardType.foot:
                        case StandardType.lightyear:
                            return true;
                        default:
                            return false;
                    }
                case StandardType.atomicMassUnit:
                    switch (newType)
                    {
                        case StandardType.gram:
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

        public static bool CanConvertTo(this UnitValue sourceUnit, UnitValue resultValue)
        {
            return CanConvertTo(sourceUnit.Dividend, sourceUnit.Divisor, resultValue.Dividend, resultValue.Divisor, false);
        }

        public static bool CanConvertTo(this UnitValue sourceUnit, UnitValue resultValue, bool multOrDiv)
        {

            return CanConvertTo(sourceUnit.Dividend, sourceUnit.Divisor, resultValue.Dividend, resultValue.Divisor, multOrDiv);
        }

        private static bool CanConvertTo(List<Unit> dividend, List<Unit> divisor, List<Unit> newDividend, List<Unit> newDivisor, bool val)
        {
            if (val) return true;
            foreach (var unit in dividend)
            {
                var canConvert = false;
                foreach (var newUnit in newDividend)
                {
                    canConvert = CanConvertTo(unit.UnitType, newUnit.UnitType);
                    if (canConvert)
                        continue;
                }
                if (!canConvert)
                    return false;
            }
            return true;
        }

        private static bool CanConvertTo(List<Unit> dividend, List<Unit> divisor, List<Unit> newDividend, List<Unit> newDivisor)
        {
            foreach (var unit in dividend)
            {
                var canConvert = false;
                foreach (var newUnit in newDividend)
                { 
                    canConvert = CanConvertTo(unit.UnitType, newUnit.UnitType);
                    if (canConvert)
                        continue;
                }
                if (!canConvert)
                    return false;
            }
            return true;
        }

        private static bool CanConvertTo(List<Unit> dividend, List<Unit> newDividend)
        { 
            return CanConvertTo(dividend, new List<Unit>(), newDividend, new List<Unit>());
        }

        private static bool CanConvertTo(UnitValue unitValue, List<Unit> dividend)
        {
            return CanConvertTo(unitValue.Dividend, unitValue.Divisor, dividend, new List<Unit>());
        }

        private static bool CanConvertTo(UnitValue unitValue, Unit unit)
        {
            if (unitValue.Divisor.Count == 0)
                return false;
            var newType = new List<Unit>();
            newType.Add(unit);
            return CanConvertTo(unitValue.Dividend, newType);
        }

        private static bool CanConvertTo(UnitValue unitValue, StandardType type)
        {
            var unit = new Unit(type);
            return CanConvertTo(unitValue, unit);
        }

        public static UnitValue ToPower(this UnitValue item, double exponent)
        {
            var result = new UnitValue(item);

            result.Value = Math.Pow(item.Value, exponent);
            if (result.Value == 0)
                return new UnitValue(0);
            for (int i = 0; i < result.Dividend.Count; i++)
            {
                var newItem = result.Dividend[i];
                newItem.Exponent *= exponent;
                result.Dividend[i] = newItem;
            }

            for (int i = 0; i < result.Divisor.Count; i++)
            {
                var newItem = result.Divisor[i];
                newItem.Exponent *= exponent;
                result.Divisor[i] = newItem;
            }
            return result;
        }

        private static double GetConvertedValue(double basicValue, StandardType sourceType, StandardType resultType)
        {
            switch (sourceType)
            {
                case StandardType.gram:
                    if (!(resultType == StandardType.gram || resultType == StandardType.atomicMassUnit)) 
                    switch (resultType)
                    { 
                        case StandardType.atomicMassUnit:
                            return basicValue * 6.0221415E23;
                        case StandardType.pound:
                            return basicValue / 453.59237d;
                        default:
                            return basicValue;
                    }
                    break;
                case StandardType.inch:
                    if (!(resultType == StandardType.inch || resultType == StandardType.foot || resultType == StandardType.lightyear || resultType == StandardType.meter || resultType == StandardType.mile))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.foot:
                            return (basicValue / 12d);
                        case StandardType.meter:
                            return basicValue * 0.0254d;
                        case StandardType.lightyear:
                            return basicValue * (2.68483946 * (Math.Pow(10, -18)));
                        case StandardType.mile:
                            return basicValue / (5280d * 12d);
                    }
                    break;
                case StandardType.foot:
                    if (!(resultType == StandardType.inch || resultType == StandardType.foot || resultType == StandardType.lightyear || resultType == StandardType.meter || resultType == StandardType.mile))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.mile:
                            return basicValue / 5280d;
                        case StandardType.inch:
                            return basicValue * 12d;
                        case StandardType.meter:
                            return basicValue * 0.3048d;
                        case StandardType.lightyear:
                            return basicValue;

                    }
                    break;

                case StandardType.meter:
                    if (!(resultType == StandardType.inch || resultType == StandardType.foot || resultType == StandardType.lightyear || resultType == StandardType.meter || resultType == StandardType.mile))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.mile:
                            return basicValue / 1609.344d;
                        case StandardType.inch:
                            return basicValue / .0254d;
                        case StandardType.foot:
                            return basicValue / 0.3048d;
                        case StandardType.lightyear:
                            return basicValue;
                        default:
                            return basicValue;
                    }
                case StandardType.mile:
                    if (!(resultType == StandardType.inch || resultType == StandardType.foot || resultType == StandardType.lightyear || resultType == StandardType.meter || resultType == StandardType.mile))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.meter:
                            return basicValue * 1609.344d;
                        case StandardType.inch:
                            return basicValue * (5280d * 12d);
                        case StandardType.foot:
                            return basicValue * 5280d;
                        case StandardType.lightyear:
                            return basicValue / (5.87849981d * Math.Pow(10,12));
                        default:
                            return basicValue;
                    }
                    break;
                case StandardType.lightyear:
                    if (!(resultType == StandardType.inch || resultType == StandardType.foot || resultType == StandardType.lightyear || resultType == StandardType.meter || resultType == StandardType.mile))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.meter:
                            return basicValue * 299892458;
                        case StandardType.inch:
                            return basicValue * 11806789685.0112;
                        case StandardType.foot:
                            return basicValue * 983899140.4176;
                        case StandardType.mile:
                            return basicValue * 186344.53417d;
                        default:
                            return basicValue;
                    }
                    break;
                case StandardType.radian:
                    if (!(resultType == StandardType.degree || resultType == StandardType.radian))
                        throw new ArgumentException();

                    switch (resultType)
                    {
                        case StandardType.second:
                            return (Math.PI / 180d) * basicValue;
                        default:
                            return basicValue;
                    }
                    break;
                case StandardType.degree:
                    if (!(resultType == StandardType.degree || resultType == StandardType.radian))
                        throw new ArgumentException();

                    switch (resultType)
                    {
                        case StandardType.radian:
                            return Math.PI * (basicValue / 180d);
                        default:
                            return basicValue;
                    }
                    break;

                default:
                    return basicValue;
                case StandardType.degreeCelsius:
                    if (!(resultType == StandardType.degreeFahrenheit || resultType == StandardType.degreeKelvin))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.degreeFahrenheit:
                            return (9d / 5d) * basicValue + 32;
                        case StandardType.degreeKelvin:
                            return basicValue + 273.15;
                    }
                    break;

                case StandardType.degreeFahrenheit:
                    if (!(resultType == StandardType.degreeCelsius || resultType == StandardType.degreeKelvin))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.degreeCelsius:
                            return (5d / 9d) * (basicValue - 32);
                        case StandardType.degreeKelvin:
                            return (basicValue + 459.67) * (5d / 9d);
                    }
                    break;
                case StandardType.degreeKelvin:
                    if (!(resultType == StandardType.degreeCelsius || resultType == StandardType.degreeKelvin))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.degreeCelsius:
                            return basicValue - 273.15;
                        case StandardType.degreeFahrenheit:
                            return basicValue * (9d / 5d) - 459.67;
                    }
                    break;
                case StandardType.second:
                    if (!(resultType == StandardType.second || resultType == StandardType.minute || resultType == StandardType.day || resultType == StandardType.year || resultType == StandardType.hour))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.hour:
                            return basicValue / (60d * 60d);
                        case StandardType.minute:
                            return basicValue / 60d;
                        case StandardType.day:
                            return basicValue / (60d * 60d * 24d);
                        case StandardType.year:
                            return basicValue / (60d * 60d * 24d * 365.25);
                    }
                    break;
                case StandardType.hour:
                    if (!(resultType == StandardType.second || resultType == StandardType.minute || resultType == StandardType.day || resultType == StandardType.year))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.second:
                            return basicValue * 3600d;
                        case StandardType.minute:
                            return basicValue * 60d;
                        case StandardType.day:
                            return basicValue / 24d;
                        case StandardType.year:
                            return basicValue / 8766d;
                    }
                    break;
                case StandardType.minute:
                    if (!(resultType == StandardType.second || resultType == StandardType.hour || resultType == StandardType.day || resultType == StandardType.year))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.second:
                            return basicValue * 60d;
                        case StandardType.hour:
                            return basicValue / 60d;
                        case StandardType.day:
                            return basicValue / 1440d;
                        case StandardType.year:
                            return basicValue / 525960d;
                    }
                    break;
                case StandardType.day:
                    if (!(resultType == StandardType.second || resultType == StandardType.minute || resultType == StandardType.hour || resultType == StandardType.year))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.hour:
                            return basicValue * 24d;
                        case StandardType.minute:
                            return basicValue * 1440d;
                        case StandardType.second:
                            return basicValue * 86400d;
                        case StandardType.year:
                            return basicValue / 365.25d;
                    }
                    break;
                case StandardType.year:
                    if (!(resultType == StandardType.second || resultType == StandardType.minute || resultType == StandardType.hour || resultType == StandardType.hour))
                        throw new ArgumentException();
                    
                    switch (resultType)
                    {
                        case StandardType.hour:
                            return basicValue * 8766d;
                        case StandardType.minute:
                            return basicValue * 525960d;
                        case StandardType.second:
                            return basicValue * 31557600d;
                        case StandardType.day:
                            return basicValue * 365.25d;
                    }
                    break;
               case StandardType.atomicMassUnit:
                    if (!(resultType == StandardType.atomicMassUnit || resultType == StandardType.gram))
                        throw new ArgumentException();

                    switch (resultType)
                    {
                        case StandardType.gram:
                            return basicValue / 1.66053886E-24;
                        default:
                            return basicValue;
                    }
                    break;               
            }
            return basicValue;
        }


        private static double GetBaseValue(double value, Unit unit)
        { 
            int oldPower = (int)unit.Prefix;
            int newPower = (int)PowersOfTenPrefix.none;

            int powerDifference = oldPower - newPower;

            return value * Math.Pow(10d, powerDifference);
        }

        public static UnitValue ConvertTo(this UnitValue unitValue, StandardType type)
        {
            if (unitValue.Dividend.Count != 1)
                throw new Exception();
            var newUnitValue = new UnitValue(0, type);


            return unitValue.ConvertTo(newUnitValue);
        }

        public static UnitValue ConvertTo(this UnitValue unitValue, StandardType dividendType, PowersOfTenPrefix dividendPower, StandardType divisorType, PowersOfTenPrefix divisorPower)
        {
            if (unitValue.Dividend.Count != 1)
                throw new Exception();
            var divisor = new Unit(divisorType, divisorPower, unitValue.Divisor[0].Exponent);
            var dividend = new Unit(dividendType, dividendPower, unitValue.Dividend[0].Exponent);

            var newUnitValue = new UnitValue(0, dividend, divisor);

            return unitValue.ConvertTo(newUnitValue);
        }

        public static UnitValue ConvertTo(this UnitValue unitValue, PowersOfTenPrefix prefix)
        {
            if (unitValue.Dividend.Count != 1)
                throw new Exception();
            if (unitValue.Divisor.Count != 0)
                throw new Exception();

            var unit = unitValue.Dividend[0];
            var newUnit = unit;
                newUnit.Prefix = prefix;

            var value = ConvertTo(unitValue.Value,unit, newUnit);

            return new UnitValue(value, newUnit);
        }

        public static double ConvertTo(double value, Unit unit, Unit newUnit)
        {

            // Convert to base value

            var baseValue = GetBaseValue(value, unit);

            var convertedValue = GetConvertedValue(baseValue, unit.UnitType, newUnit.UnitType);

            var newValue = ConvertTo(convertedValue, newUnit.Prefix);

            return newValue;
        }

        public static double ConvertTo(double value, PowersOfTenPrefix prefix)
        { 
            int newPower = (int)prefix;
            int oldPower = (int)PowersOfTenPrefix.none;

            int powerDifference = oldPower - newPower;

            return value * Math.Pow(10d, powerDifference);
        }

        public static UnitValue ConvertTo(double value, List<Unit> dividend, List<Unit> divisor, List<Unit> newDividend, List<Unit> newDivisor)
        {
            if (!(CanConvertTo(dividend, divisor, newDividend, newDivisor)))
                throw new Exception("Cannot convert that, silly.");

            var unitValue = new UnitValue(1);
            unitValue.Dividend.AddRange(newDividend);
            unitValue.Divisor.AddRange(newDivisor);
            foreach (var unit in dividend)
            {
                foreach (var newUnit in newDividend)
                {
                    if (!(CanConvertTo(unit.UnitType, newUnit.UnitType)))
                        continue;
                    value = ConvertTo(value, unit, newUnit);
                }
            }
            foreach (var unit in divisor)
            {
                foreach (var newUnit in newDivisor)
                {
                    if (!(CanConvertTo(unit.UnitType, newUnit.UnitType)))
                        continue;
                    value = ConvertTo(value, newUnit, unit); // flip flop so that the division/multiplication is inverted cause of being on bottom
                }
            }
            unitValue.Value = value;
            return unitValue;
        }

        public static UnitValue ConvertTo(this UnitValue unitValue, UnitValue newUnitType)
        {
            return ConvertTo(unitValue.Value, unitValue.Dividend, unitValue.Divisor, newUnitType.Dividend, newUnitType.Divisor);
        }

        public static string Abbreviation(this PowersOfTenPrefix type)
        {
            switch (type)
            {
                case PowersOfTenPrefix.atto:
                    return "a";
                case PowersOfTenPrefix.centi:
                    return "c";
                case PowersOfTenPrefix.deci:
                    return "d";
                case PowersOfTenPrefix.deka:
                    return "da";
                case PowersOfTenPrefix.exa:
                    return "E";
                case PowersOfTenPrefix.femto:
                    return "f";
                case PowersOfTenPrefix.giga:
                    return "G";
                case PowersOfTenPrefix.hecto:
                    return "h";
                case PowersOfTenPrefix.kilo:
                    return "k";
                case PowersOfTenPrefix.mega:
                    return "M";
                case PowersOfTenPrefix.micro:
                    return "μ";
                case PowersOfTenPrefix.milli:
                    return "m";
                case PowersOfTenPrefix.nano:
                    return "n";
                case PowersOfTenPrefix.peta:
                    return "P";
                case PowersOfTenPrefix.pico:
                    return "P";
                case PowersOfTenPrefix.tera:
                    return "T";
                case PowersOfTenPrefix.yocto:
                    return "y";
                case PowersOfTenPrefix.yotta:
                    return "Y";
                case PowersOfTenPrefix.zepto:
                    return "z";
                case PowersOfTenPrefix.zetta:
                    return "Z";               
                default:
                    return string.Empty;
            }
        }
        public static string Abbreviation(this StandardType type)
        {
            switch (type)
            { 
                case StandardType.gram:
                    return "g";
                case StandardType.meter:
                    return "m";
                case StandardType.second:
                    return "s";
                case StandardType.liter:
                    return "L";
                case StandardType.ohm:
                    return "Ω";
                case StandardType.ampere:
                    return "A";
                case StandardType.atmosphere:
                    return "atm";
                case StandardType.atomicMassUnit:
                    return "u";
                case StandardType.britishThermalUnit:
                    return "Btm";
                case StandardType.calorie:
                    return "cal";
                case StandardType.coulomb:
                    return "C";
                case StandardType.day:
                    return "d";
                case StandardType.degree:
                    return "°";
                case StandardType.degreeCelsius:
                    return "°C";
                case StandardType.degreeFahrenheit:
                    return "°F";
                case StandardType.degreeKelvin:
                    return "°K";
                case StandardType.electronVolt:
                    return "eV";
                case StandardType.farad:
                    return "F";
                case StandardType.foot:
                    return "ft.";
                case StandardType.gauss:
                    return "G";
                case StandardType.henry:
                    return "H";
                case StandardType.hertz:
                    return "Hz";
                case StandardType.hour:
                    return "h";
                case StandardType.horsepower:
                    return "hp";
                case StandardType.inch:
                    return "in.";
                case StandardType.joule:
                    return "J";
                case StandardType.lightyear:
                    return "ly";
                case StandardType.mile:
                    return "mi.";
                case StandardType.minute:
                    return "min";
                case StandardType.mole:
                    return "mol";
                case StandardType.newton:
                    return "N";
                case StandardType.pascal:
                    return "Pa";
                case StandardType.pound:
                    return "lb";
                case StandardType.radian:
                    return "rad";
                case StandardType.revolution:
                    return "rev";
                case StandardType.tesla:
                    return "T";
                case StandardType.volt:
                    return "V";
                case StandardType.watt:
                    return "W";
                case StandardType.weber:
                    return "Wb";
                case StandardType.yard:
                    return "yd";
                case StandardType.year:
                    return "yr";
                default:
                    return string.Empty;
            }
        }
    }
    
}

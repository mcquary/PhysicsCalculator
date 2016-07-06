using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWeight();
            GravitationalForce();
            //Console.WriteLine(Constants.MassOfEarth);
            //TestConvertSimple();
            //TestConvertComplex();
            //TestSubtraction();
            //FootballProblem();
            //LongJumperProblem();
            //GetPeriod();
            //GetVelocityFromCircularMotion();
            //GetCentripitalAcceleration();
            //GetRadiusByVelocityAndAcceleration();
            //TestAddition();
            //TestSqRoot();
            //TestEquality();
            //TestTryParse();
            //TestConversion();
            //TestMultiply();
            //TestVelocity();
            //TestAcceleration();
            //TestTimeVelocity();
        }

        static void GravitationalForce()
        {
            Console.WriteLine(Constants.UniversalGravitationalConstant.ToString());
        }

        static void FootballProblem()
        {
            var initVelocity = new UnitValue(20, StandardType.meter, StandardType.second);
            var theta = new UnitValue(45, StandardType.degree);
            var vector = new Vector2(initVelocity, theta);
            var y_accel = Constants.EarthGravity;
            var timeInAir = Motion.TwoDimensionMotion.GetTime(-vector.YComponent, vector.YComponent, y_accel);
            for (double i = 0; i < timeInAir; i += .3)
            {
                var currentTime = new UnitValue(timeInAir);
                currentTime.Value = i;
                var x_dist = Motion.TwoDimensionMotion.GetFinalPosition(vector.XComponent, currentTime, new UnitValue(0));
                Console.WriteLine(x_dist.ToString());
            }

            Console.WriteLine("Max Height: " + Motion.TwoDimensionMotion.GetMaxHeight(vector, y_accel).ToString());
        }

        static void TestWeight()
        { 
            // Weight = Mass * force of gravity;
            var weight = new UnitValue(225, StandardType.pound);
            var acc = Constants.EarthGravity;
            var mass = weight / acc;

            Console.WriteLine(mass);
        }

        static void LongJumperProblem()
        {
            //var theta = new UnitValue(30, StandardType.degree);
            //var vector = new Vector2(initialVelocity, theta);
            var x_velocity = new UnitValue(2.4, StandardType.meter, StandardType.second);
            var y_dist = new UnitValue(-.6, StandardType.meter);
            var x_accel = new UnitValue(0);
            var y_accel = Constants.EarthGravity;

            var time = Motion.TwoDimensionMotion.GetTimeNoInitialVelocity(y_dist, y_accel);
            var x_dist = Motion.TwoDimensionMotion.GetFinalPosition(x_velocity, time, x_accel);

            Console.WriteLine(x_dist.ToString());
        }

        static void GetPeriod()
        {
            var radius = new UnitValue(1, StandardType.meter);
            var velocity = new UnitValue(2, StandardType.meter, StandardType.second);
            var period = ((2 * Math.PI) * radius / velocity).ConvertTo(StandardType.day);

            Console.WriteLine(period.ToString());
        }
        static void GetCentripitalAcceleration()
        {
            var velocity = new UnitValue(10, StandardType.meter, StandardType.second);
            var radius = new UnitValue(2, StandardType.meter);

            velocity = velocity.ToPower(2);

            var acceleration = velocity/ radius;

            var milePerSeconds = acceleration.ConvertTo(StandardType.mile, PowersOfTenPrefix.none, StandardType.second, PowersOfTenPrefix.none);
            Console.WriteLine(acceleration.ToString());
            Console.WriteLine(milePerSeconds.ToString());
        }

        static void GetVelocityFromCircularMotion()
        {
            var acceleration = new UnitValue(10, StandardType.meter, PowersOfTenPrefix.none, 1, StandardType.second, PowersOfTenPrefix.none, 2);
            var radius = new UnitValue(2, StandardType.meter);

            var velocity = new UnitValue(acceleration * radius).ToPower(.5);
            Console.WriteLine("Velocity from Circular Motion: " + velocity.ToString());
        }

        static void GetRadiusByVelocityAndAcceleration()
        {
            var acceleration = new UnitValue(10, StandardType.meter, PowersOfTenPrefix.none, 1, StandardType.second, PowersOfTenPrefix.none, 2);
            var velocity = new UnitValue(2, StandardType.meter, StandardType.second);

            var radius = velocity.ToPower(2) / acceleration; 
            Console.WriteLine("Radius from Circular Motion: " + radius.ToString());
        }

        static void TestSqRoot()
        { 

            //var acceleration 
        }
        static void TestSubtraction()
        {
            var val1 = new UnitValue(38, StandardType.meter, StandardType.second);
            var val2 = new UnitValue(85, StandardType.mile, StandardType.hour);

            var result = val1 - val2;
            Console.WriteLine(val1.ToString() + " - " + val2.ToString() + " = " + result.ToString());
        }

        static void TestVelocity()
        {
            var acceleration = Constants.EarthGravity;
            var deltaTime = new UnitValue(2, StandardType.second);
            var initialVelocity = new UnitValue(10, StandardType.meter, StandardType.second);
            //initialVelocity = initialVelocity.ConvertTo(StandardType.meter, PowersOfTenPrefix.centi, StandardType.hour, PowersOfTenPrefix.none);
            var velocity = initialVelocity + (acceleration * deltaTime);
            Console.WriteLine(velocity.ToString());
        }

        static void TestAcceleration()
        {
            var velocity = new UnitValue(-9.6, StandardType.meter, StandardType.second);
            var deltaTime = new UnitValue(2, StandardType.second);
            var initialVelocity = new UnitValue(10, StandardType.meter, StandardType.second);

            var acceleration = (velocity - initialVelocity) / deltaTime;
            Console.WriteLine(acceleration.ToString());
        }

        static void TestTimeVelocity()
        {
            var initialVelocity = new UnitValue(10, StandardType.meter, StandardType.second);
            var velocity = new UnitValue(-9.6, StandardType.meter, StandardType.second);
            var acceleration = Constants.EarthGravity;
            var deltaTime = (velocity - initialVelocity) / acceleration;
            Console.WriteLine(deltaTime.ToString());
        }

        static void TestConvertSimple()
        {
            var test1type = new Unit(StandardType.meter, PowersOfTenPrefix.kilo);
            var test2type = new Unit(StandardType.foot);
            var baseUnit = new UnitValue(1, test1type);


            var test1 = baseUnit.ConvertTo(PowersOfTenPrefix.none);
            var test2Unit = new UnitValue(1, test2type);

            var test2 = baseUnit.ConvertTo(test2Unit);

            Console.WriteLine("Writing simple self-conversion: Test 1 km to m: " + test1.ToString());
            Console.WriteLine("Writing simple self-conversion: Test 1 km to ft: " + test2.ToString());
        }

        static void TestConvertComplex()
        {
            var meterUnit = new Unit(StandardType.meter);
            var secondUnit = new Unit(StandardType.second);
            var ftUnit = new Unit(StandardType.mile);
            var hourUnit = new Unit(StandardType.hour);

            var baseUnit = new UnitValue(1);
            var convertUnit = new UnitValue(85);

            baseUnit.Dividend.Add(meterUnit);
            baseUnit.Divisor.Add(secondUnit);

            convertUnit.Divisor.Add(hourUnit);
            convertUnit.Dividend.Add(ftUnit);

            var result = convertUnit.ConvertTo(baseUnit);

            Console.WriteLine("Writing complex self-conversion: Test 38 m/s: " + result.ToString());
        }

        static void TestMultiply()
        {
            var val1 = new UnitValue(5, StandardType.meter);
            var val2 = new UnitValue(5, StandardType.meter);

            var result = val1 * val2;
            Console.WriteLine(result.ToString());
        }


        static void TestAddition()
        {
            var val1 = new UnitValue(0, StandardType.meter, StandardType.second);
            var val2 = new UnitValue(85, StandardType.mile, StandardType.hour);

            var result = val1 + val2;
            Console.WriteLine(val1.ToString() + " + " + val2.ToString() + " = " + result.ToString());

            val1 = new UnitValue(3, StandardType.meter, PowersOfTenPrefix.kilo, StandardType.second, PowersOfTenPrefix.none);
            val2 = new UnitValue(3, StandardType.meter, PowersOfTenPrefix.centi, StandardType.second, PowersOfTenPrefix.none);

            result = val1 + val2;
            Console.WriteLine(result.ToString());
        }

        static void TestEquality()
        {
        }
    }
}

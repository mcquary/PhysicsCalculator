using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics.Motion
{
    class Velocity
    {
        public UnitValue GetVelocity(UnitValue initialVelocity, UnitValue acceleration, UnitValue time)
        {
            return initialVelocity + (acceleration * time);
        }

        public UnitValue GetAcceleration(UnitValue initialVelocity, UnitValue finalVelocity, UnitValue time)
        {
            return (finalVelocity - initialVelocity) / time;
        }

        public UnitValue GetTime(UnitValue initialVelocity, UnitValue finalVelocity, UnitValue acceleration)
        {
            return (finalVelocity - initialVelocity) / acceleration;
        }
    }   

    public class TwoDimensionMotion
    {
        // Generic equations. Should be used component specific or all together
        public static UnitValue GetFinalPosition(UnitValue initialVelocity, UnitValue time, UnitValue acceleration)
        {
            return initialVelocity * time + (.5) * (acceleration) * (time.ToPower(2));
        }

        public static UnitValue GetTime(UnitValue finalVelocity, UnitValue initialVelocity, UnitValue acceleration)
        {
            return (finalVelocity - initialVelocity) / acceleration;
        }
        public static UnitValue GetTimeNoInitialVelocity(UnitValue finalPosition, UnitValue acceleration)
        {
            return (finalPosition / (acceleration * (.5))).ToPower(.5);
        }
        public static UnitValue GetMaxHeight(Vector2 vector, UnitValue acceleration)
        {
            var one = vector.Magnitude.ToPower(2);
            var two = Math.Pow(Math.Sin(vector.Theta.ConvertTo(StandardType.radian).Value), 2) ; 
            var result = one * two / (2 * acceleration);
            if (result < 0)
                return  result * -1;
            return result;
        }
    }

    public class OneDimensionMotion
    {
        public UnitValue VelocityFromPosition(UnitValue velocity, UnitValue acceleration, UnitValue finalPosition, UnitValue initialPosition)
        {
            return (velocity.ToPower(2) + (2 * (finalPosition - initialPosition))).ToPower(.5);
        }

        public UnitValue PositionFromTime(UnitValue initialPosition, UnitValue velocity, UnitValue time, UnitValue acceleration)
        {
            return initialPosition + (velocity * time) + (acceleration * time.ToPower(2)) * .5;
        }

        public UnitValue PositionFromVelocityAndTime(UnitValue initialPostion, UnitValue initialVelocity, UnitValue finalVelocity, UnitValue time)
        {
            return initialPostion + .5 * (initialVelocity + finalVelocity) * time;
        }

        public UnitValue VelocityFromTime(UnitValue initialVelocity, UnitValue acceleration, UnitValue time)
        {
            return initialVelocity + (acceleration * time);
        }

        public UnitValue TimeFromPosition(UnitValue initialPosition, UnitValue finalPosition, UnitValue initialVelocity, UnitValue finalVelocity)
        {
            return 2 * (finalPosition - initialPosition) / (initialVelocity + finalVelocity);
        }
    }
}

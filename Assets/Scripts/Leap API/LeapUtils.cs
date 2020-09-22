using Leap;
using System;


namespace LeapAPI
{
    public static class LeapUtils
    {
        static float Rad2Deg = 180f / (float)Math.PI;
        //Get angle of two vectors
        public static double getAngleJoints(Vector pointOne, Vector pointCenter, Vector pointTwo, bool x = true, bool y = true, bool z = true)
        {
            Vector positionOne = TruncateVector(pointOne, x, y, z);
            Vector positionCenter = TruncateVector(pointCenter, x, y, z);
            Vector positionTwo = TruncateVector(pointTwo, x, y, z);

            //Debug.Log("" + positionOne + positionCenter + positionTwo);

            double[] vecAB = { positionOne.x - positionCenter.x, positionOne.y - positionCenter.y, positionOne.z - positionCenter.z };
            double[] vecBC = { positionTwo.x - positionCenter.x, positionTwo.y - positionCenter.y, positionTwo.z - positionCenter.z };

            double magAB = Math.Sqrt(vecAB[0] * vecAB[0] + vecAB[1] * vecAB[1] + vecAB[2] * vecAB[2]);
            double magBC = Math.Sqrt(vecBC[0] * vecBC[0] + vecBC[1] * vecBC[1] + vecBC[2] * vecBC[2]);

            double[] vecNormAB = { vecAB[0] / magAB, vecAB[1] / magAB, vecAB[2] / magAB };
            double[] vecNormBC = { vecBC[0] / magBC, vecBC[1] / magBC, vecBC[2] / magBC };

            double producto = vecNormAB[0] * vecNormBC[0] + vecNormAB[1] * vecNormBC[1] + vecNormAB[2] * vecNormBC[2];
            double angulo = Math.Acos(producto) * 180.0f / (Math.PI);

            //Debug.Log("" + positionOne + positionCenter + positionTwo + angulo);
            return angulo;
        }

        public static Vector TruncateVector(Vector vector, bool x, bool y, bool z)
        {
            Vector truncated = new Vector();
            if (x)
                truncated.x = vector.x;

            if (y)
                truncated.y = vector.y;

            if (z)
                truncated.z = vector.z;

            return truncated;
        }

        public static Vector ToEulerianAngle(LeapQuaternion q1)
        {
            float sqw = q1.w * q1.w;
            float sqx = q1.x * q1.x;
            float sqy = q1.y * q1.y;
            float sqz = q1.z * q1.z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.x * q1.w - q1.y * q1.z;
            
            Vector v;

            if (test > 0.4995f * unit)
            { // singularity at north pole
                //v.y = 2f * Mathf.Atan2(q1.y, q1.x);
                v.y = 2f * (float)Math.Atan2(q1.y, q1.x);
                v.x = (float)Math.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * Rad2Deg);
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v.y = -2f * (float)Math.Atan2(q1.y, q1.x);
                v.x = -(float)Math.PI / 2f;
                v.z = 0;
                return NormalizeAngles(v * Rad2Deg);
            }

            LeapQuaternion q = new LeapQuaternion(q1.w, q1.z, q1.x, q1.y);
            

            v.y = (float)Math.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
            v.x = (float)Math.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
            v.z = (float)Math.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
            return NormalizeAngles(v * Rad2Deg);
        }

        static Vector NormalizeAngles(Vector angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            return angles;
        }

        static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }
    }
}
﻿using System;

namespace Codingriver
{
    public static class RandomHelper
    {
        private static readonly Random random = new Random();
        static RandomHelper()
        {
            
        }
        public static UInt64 RandUInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static Int64 RandInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 获取lower与Upper之间的随机数
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int RandInt(int lower, int upper)
        {
            int value = random.Next(lower, upper);
            return value;
        }
        public static bool RandBool(){
            return RandInt(0,10000)>5000;
        }
        /// <summary>
        /// 随机浮点，保留三位小数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandFloat(float min, float max)
        {
            int minTemp = (int)(min * 1000);
            int maxTemp = (int)(max * 1000);
            int value = random.Next(minTemp, maxTemp);
            return value / 1000f;
        }
    }
}
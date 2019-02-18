using System;
using System.Collections.Generic;
using System.Text;

namespace CoreHelper
{
    public static class RandomHelper
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// 获得随机数字
        /// </summary>
        /// <param name="length">数字位数</param>
        /// <returns></returns>
        public static int GetNumber(int length)
        {
            int min = (int)Math.Pow(10, 4);
            int max = (int)Math.Pow(10, length + 1) - 1;
            return Random.Next(min, max);
        }

        /// <summary>
        /// 获得随机数字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int GetNumber(int min, int max)
        {
            return Random.Next(min, max);
        }

        /// <summary>
        /// 获得随机字母
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns></returns>
        public static string GetLetter(int length)
        {
            char[] letterNumber = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += letterNumber[Random.Next(0, letterNumber.Length)];
            }
            return result;
        }

        /// <summary>
        /// 获得随机英文和字母
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLetterNumber(int length)
        {
            char[] letterNumber = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += letterNumber[Random.Next(0, letterNumber.Length)];
            }
            return result;
        }
    }
}

using System;

namespace U22Game.Utilities
{
    public static class RandomUtil
    {

        /// <summary>
        /// ランダムな真偽値を取得します。(50%)
        /// </summary>
        public static bool GetRandomBool()
        {
            return GetRandomBool(0.5);
        }


        /// <summary>
        /// trueの確率を指定してランダムな真偽値を取得します。
        /// </summary>
        /// <param name="trueProbability">trueが出る確率(0-1)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool GetRandomBool(double trueProbability) 
        {
            if (trueProbability < 0 || trueProbability > 1)
            {
                throw new ArgumentOutOfRangeException("trueProbability", "確率は0-1の範囲で指定してください。");
            }
            
            Random r = new Random();
            return r.NextDouble() < trueProbability; 
        }
    }
}

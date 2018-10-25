using System;
using System.Collections;
using EnemyClass;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Global
{
    public class Utils : MonoBehaviour
    {

        /// <summary>
        ///     Returns a random enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandomEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var enumType = values.GetValue(Random.Range(0, values.Length));
            return (T)enumType;
        }

        /// <summary>
        ///     Repeat any action an amount of times
        /// </summary>
        /// <param name="func"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static IEnumerator RepeatActionForeverWithDelay(Action func, float delay)
        {
            while (true)
            {
                func();
                yield return new WaitForSeconds(delay);
            }
        }

        public static IEnumerator RepeatActionNumberOfTimes(Action func, int numOfTimes)
        {
            int i = 0;
            while (i < numOfTimes)
            {
                i++;
                func();
                yield return null;
            }
        }
        public static IEnumerator RepeatActionNumberOfTimesWithDelay(Action func, int numOfTimes, float delay)
        {
            int i = 0;
            while (i < numOfTimes)
            {
                i++;
                func();
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
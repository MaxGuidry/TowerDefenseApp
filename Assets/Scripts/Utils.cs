using System;
using System.Collections;
using EnemyClass;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GlobalMethods
{
    public class Utils : MonoBehaviour
    {
        public static Stats BasicMelee, BasicRanged, BasicTank;

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


        /// <summary>
        ///     Set each enemy stats here
        /// </summary>
        /// <param name="type"></param>
        public static void SetEnemyStats(EnemyTypes type)
        {
            switch (type)
            {
                case EnemyTypes.BasicMelee:
                    {
                        BasicMelee.MaxHealth = Random.Range(100, 150);
                        BasicMelee.EnemyDamage = Random.Range(10, 20);
                        BasicMelee.EnemySpeed = Random.Range(5, 8);
                        BasicMelee.StoppingDistance = 1f;
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        BasicRanged.MaxHealth = Random.Range(75, 100);
                        BasicRanged.EnemyDamage = Random.Range(25, 30);
                        BasicRanged.EnemySpeed = Random.Range(5, 8);
                        BasicRanged.StoppingDistance = 7f;
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        BasicTank.MaxHealth = Random.Range(200, 250);
                        BasicTank.EnemyDamage = Random.Range(5, 20);
                        BasicTank.EnemySpeed = Random.Range(2, 3);
                        BasicTank.StoppingDistance = 1f;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public struct Stats
    {
        public float MaxHealth, EnemyDamage, EnemySpeed, StoppingDistance;
    }
}
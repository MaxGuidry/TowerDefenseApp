using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
    public static T GetRandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        var enumType = values.GetValue(Random.Range(0, values.Length));
        return (T)enumType;
    }

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
    public static IEnumerator RepeatActionNumberOfTimesWityDelay(Action func, int numOfTimes,float delay)
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

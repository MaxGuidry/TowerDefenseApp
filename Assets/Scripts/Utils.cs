using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : MonoBehaviour
{
    public static T GetRandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        var enumType = values.GetValue(Random.Range(0, values.Length));
        return (T)enumType;
    }

    public static float UpdateHealth(float newHealth)
    {
        return newHealth;
    }

    public static IEnumerator RepeatAction(Action func, int delay)
    {
        while (true)
        {
            func();
            yield return new WaitForSeconds(delay);
        }
    }
}

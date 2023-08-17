using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    // random enum
    private static System.Random random = new System.Random();
    public static T RandomEnumValue<T>()
    {
        var v = System.Enum.GetValues(typeof(T));
        return (T)v.GetValue(random.Next(v.Length));
    }

    // lay ke theo ty le xac xuat
    public static bool Chance(int rand, int max = 100)
    {
        return Random.Range(0, max) < rand;
    }
}

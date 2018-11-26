using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class ThreadSafeRandom
{
    [ThreadStatic] private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

public static class FloatExtensions
{
    public static float SqrDistanceTo(this Vector3 source, Vector3 destination)
    {
        return source.DirectionTo(destination).sqrMagnitude;
    }
}

public static class Vector3Extensions
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector3 Flat(this Vector3 original)
    {
        return new Vector3(original.x, 0, original.y);
    }

    public static Vector3 DirectionTo(this Vector3 source, Vector3 destination, bool normalize = false)
    {
        return normalize ? Vector3.Normalize(destination - source) : destination - source;
    }

    public static Vector3 DirectionTo(this Transform source, Transform destination, bool normalize = false)
    {
        return source.position.DirectionTo(destination.position, normalize);
    }
}

public static class ListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public static class MonoBehaviourExtensions
{
    public static void Invoke(this MonoBehaviour behaviour, Action function, float delay)
    {
        behaviour.StartCoroutine(ExecuteAfterTime(function, delay));
    }

    public static void InvokeRepeating(this MonoBehaviour behaviour, Action function, float delay, float repeatDelay)
    {
        behaviour.StartCoroutine(ExecuteAfterTimeRepeating(behaviour, function, delay, repeatDelay));
    }

    private static IEnumerator ExecuteAfterTime(Action function, float delay)
    {
        yield return new WaitForSeconds(delay);
        function();
    }

    private static IEnumerator ExecuteAfterTimeRepeating(MonoBehaviour behaviour, Action function, float delay, float repeatDelay)
    {
        yield return new WaitForSeconds(delay);

        function();

        yield return new WaitForSeconds(repeatDelay);

        behaviour.StartCoroutine(ExecuteAfterTimeRepeating(behaviour, function, delay, repeatDelay));
    }
}
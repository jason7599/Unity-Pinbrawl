using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Layers
{
    Default = 0,
    PlayerZone = 6,
    Wall = 7,
    Entity = 8,
    Ball = 9,
    Powerup = 11,
}

public static class Utils
{
    public static List<T> Shuffle<T>(List<T> list) 
    {
        for (int i = 0; i < list.Count - 1; i++) // fisher yates
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = temp;
        }
        return list;
    }

    public static void PlayEffect(this Transform tr, ParticleSystem effect)
    {
        Object.Destroy(Object.Instantiate(effect, tr.position, effect.transform.rotation).gameObject, effect.main.duration);
    }

    public static IEnumerator SmoothMoveTo(this Transform transform, Vector3 destPoint, float time)
    {
        Vector3 start = transform.position;

        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(start, destPoint, (elapsed / time));
            yield return null;
        }

        transform.position = destPoint;
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Asteroid Library", menuName = "Asteroids/Asteroid Library")]
public class AsteroidLibrary : ScriptableObject
{
    [SerializeField]
    private List<AsteroidInfo> asteroids = new List<AsteroidInfo>();

    public AsteroidInfo GetAsteroid(int level)
    {
        var found = asteroids.Find(e => e.Level == level);
        return found;
    }
}

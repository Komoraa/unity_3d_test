using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Then asteroid enters this trigger it's destroyed.
/// </summary>
public class AsteroidKillTrigger : MonoBehaviour
{
    public UnityEvent<AsteroidInfo> OnAsteroidHitTrigger = new UnityEvent<AsteroidInfo>();

    private void OnTriggerEnter(Collider other)
    {
        var asteroid = other.GetComponent<AsteroidInfo>();
        if (!asteroid) return;

        OnAsteroidHitTrigger?.Invoke(asteroid);
        Destroy(asteroid.gameObject);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class AsteroidInfo : MonoBehaviour
{
    [SerializeField]
    private int level = 5;
    public int Level => level;

    [SerializeField]
    private int health = 100;
    public int Health => health;

    [SerializeField]
    private float fallDownVelocity = 20f;

    public UnityEvent<AsteroidInfo> OnAsteroidDestroyed = new UnityEvent<AsteroidInfo>();

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize()
    {
        // Set const velocity in downward direction.
        rb.AddForce(Vector3.down * fallDownVelocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Called to deal damage to asteroid.
    /// </summary>
    /// <param name="dmg"></param>
    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            DestroyAsteroid();
            GameController.instance.AddHp();
            ScoreManager.instance.AddPoint();
        }
        
    }

    /// <summary>
    /// Called to destroy asteroid.
    /// </summary>
    private void DestroyAsteroid()
    {
        OnAsteroidDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}

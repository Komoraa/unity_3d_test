using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 50;

    [SerializeField]
    private float speed = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize()
    {
        // adds velocity for the bullet
        rb.AddRelativeForce(Vector3.up * speed, ForceMode.VelocityChange);
    }

    public void Update()
    {
        gameObject.transform.Rotate(1.0f, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var asteroid = other.GetComponent<AsteroidInfo>();
        if (!asteroid) return; // ignore if object is not asteroid

        asteroid.ReceiveDamage(damage); // deal damage to the asteroid

        Destroy(gameObject); // destroy itself
    }
}

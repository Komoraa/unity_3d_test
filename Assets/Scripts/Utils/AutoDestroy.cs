using UnityEngine;

/// <summary>
/// This component autodestroys game object to which is attached after time runs out.
/// </summary>
public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float timer = 4f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}

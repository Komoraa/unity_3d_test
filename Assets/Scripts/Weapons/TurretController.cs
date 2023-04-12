using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private Transform turret;

    [Header("Firing")]
    [SerializeField]
    private List<Transform> bulletSpawnPositions = new List<Transform>();

    [SerializeField, Tooltip("Bullets per second")]
    private float fireRate = 1f;

    [SerializeField]
    private Bullet bulletPrefab;

    [Header("Input")]
    [SerializeField]
    private float mouseSensivity = 6;

    [SerializeField]
    private float moveRange = 60f;

    private int currentSpawnIndex = 0;
    private float lastFireShoot = -1;

    private Vector2 camRot;

    public void Initialize()
    {
        camRot = new Vector2(turret.eulerAngles.x, turret.eulerAngles.y);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var mouseDelta = GetMouseMove();
        UpdateRotation(mouseDelta);

        if (HoldsTrigger())
        {   
            // checks if enough time have passed from the last shoot
            var timeOff = Time.time - lastFireShoot;
            if (timeOff > 1 / fireRate)
            {
                lastFireShoot = Time.time;
                ShootBullets();
                //Invoke("ShootBullets", 1);
            }
        }
    }

    /// <summary>
    /// Returns mouse move since last frame.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMouseMove()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    /// <summary>
    /// Updates turret rotation.
    /// </summary>
    /// <param name="deltaRotation"></param>
    private void UpdateRotation(Vector2 deltaRotation)
    {
        deltaRotation *= mouseSensivity;

        // clamping camera rotation vector.
        camRot.x = Mathf.Clamp(camRot.x - deltaRotation.y, -90, moveRange);
        camRot.y = (camRot.y + deltaRotation.x) % 360;

        turret.eulerAngles = new Vector3(camRot.x, camRot.y, 0);
    }

    /// <summary>
    /// Returns if player holds fire button.
    /// </summary>
    /// <returns></returns>
    private bool HoldsTrigger()
    {
        return Input.GetButton("Fire1");
    }

    /// <summary>
    /// Shoots single bullet from its spawn points.
    /// </summary>
    private void ShootBullets()
    {
        // each shoot is made from the next spawn point
        currentSpawnIndex = (currentSpawnIndex) % bulletSpawnPositions.Count;
        var spawn = bulletSpawnPositions[currentSpawnIndex];

        var bullet = Instantiate(bulletPrefab, spawn.position, spawn.rotation * Quaternion.Euler(90, 0, 0)); // because we're using capsule we have to rotate it by 90 deg on x so bullet faces correct direction.
        bullet.Initialize();
    }
}
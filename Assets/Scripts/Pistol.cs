using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f;

    public AudioClip clip;
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        
        source.PlayOneShot(clip);

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }
}

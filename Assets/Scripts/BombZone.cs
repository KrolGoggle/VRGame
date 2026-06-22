using UnityEngine;

public class BombZone : MonoBehaviour
{

    public Collider zone;
    public string targetTag = "RottenEgg";
    public AudioClip clipBOOM;

    [Header("VFX")]
    public GameObject explosionParticlesPrefab;
    public GameObject explosionSpritePrefab;

    [Header("Vignette")]
    public Transform playerTransform;
    public float vignetteDistance = 5f;
    public VignetteController vignetteController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (zone == null)
            zone = GetComponent<BoxCollider>();

        if (zone != null)
            zone.isTrigger = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == zone.gameObject) return;

        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            Vector3 explosionPosition = other.transform.position;

            LifeManager.Instance?.LoseLife();

            if (clipBOOM != null)
            {
                AudioSource.PlayClipAtPoint(clipBOOM, explosionPosition, 0.15f);
            }

            if (explosionParticlesPrefab != null)
            {
                Instantiate(explosionParticlesPrefab, explosionPosition, Quaternion.identity);
            }

            if (explosionSpritePrefab != null)
            {
                Instantiate(explosionSpritePrefab, explosionPosition, Quaternion.identity);
            }

            if (playerTransform != null && vignetteController != null)
            {
                float distanceToPlayer = Vector3.Distance(playerTransform.position, explosionPosition);
                if (distanceToPlayer <= vignetteDistance)
                {
                    vignetteController.TriggerVignette();
                }
            }

            Destroy(other.gameObject);
        }
    }
}

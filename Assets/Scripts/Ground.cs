using UnityEngine;

public class Ground : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";
    public AudioClip clipCrack;
    public AudioClip clipBOOM;
    public GameObject smallChicken;
    public GameObject explosionParticlesPrefab;
    public GameObject explosionSpritePrefab;
    public GameObject eggParticlesPrefab;

    public ScoreManager score;

    void Start()
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
            LifeManager.Instance?.LoseLife();
            AudioSource.PlayClipAtPoint(clipCrack, transform.position, 0.15f);
            Destroy(other.gameObject);
            if (eggParticlesPrefab != null)
            {
                Instantiate(eggParticlesPrefab, other.transform.position, Quaternion.identity);
            }
            Vector3 chickPosition = new Vector3(other.transform.position.x,0,other.transform.position.z);
            GameObject chick = Instantiate(smallChicken, chickPosition, Quaternion.identity);
            Destroy(chick, 3f);
        }

        if (other.CompareTag("RottenEgg"))
        {
            AudioSource.PlayClipAtPoint(clipBOOM, other.transform.position, 0.15f);
            score.AddPoint();
            if (explosionParticlesPrefab != null)
            {
                Instantiate(explosionParticlesPrefab, other.transform.position, Quaternion.identity);
            }
            if (explosionSpritePrefab != null)
            {
                Instantiate(explosionSpritePrefab, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
        }
    }
}

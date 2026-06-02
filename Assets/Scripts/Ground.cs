using UnityEngine;

public class Ground : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";
    public AudioClip clipCrack;
    public AudioClip clipBOOM;
    public GameObject smallChicken;

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
            Vector3 chickPosition = new Vector3(other.transform.position.x,0,other.transform.position.z);
            GameObject chick = Instantiate(smallChicken, chickPosition, Quaternion.identity);
            Destroy(chick, 3f);
        }

        if (other.CompareTag("RottenEgg"))
        {
            AudioSource.PlayClipAtPoint(clipBOOM, transform.position, 0.15f);
            Destroy(other.gameObject);
        }
    }
}

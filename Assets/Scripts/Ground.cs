using UnityEngine;

public class Ground : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";

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
            Destroy(other.gameObject);
        }
    }
}

using UnityEngine;

public class BasketZone : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == zone.gameObject) return;

        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            ScoreManager.Instance?.AddPoint(1);
            Destroy(other.gameObject);
        }
    }
}

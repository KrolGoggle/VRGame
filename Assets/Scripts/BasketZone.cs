using UnityEngine;

public class BasketZone : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";
    public AudioClip clipGood;
    public AudioClip clipBad;

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
            AudioSource.PlayClipAtPoint(clipGood, transform.position, 0.15f);
            Destroy(other.gameObject);
        }
        if(other.CompareTag("RottenEgg"))
        {
            ScoreManager.Instance?.AddPoint(-1);
            LifeManager.Instance?.LoseLife();
            AudioSource.PlayClipAtPoint(clipBad, transform.position, 0.15f);
            Destroy(other.gameObject);
        }
    }
}

using UnityEngine;

public class TestGround : MonoBehaviour
{
    public BoxCollider zone;
    public string targetTag = "Egg";
    public AudioClip clipCrack;
    public AudioClip clipBOOM;

    [Header("Basket Zones")]
    public TestBasketZone[] basketZones;

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
            TestLifeManager.Instance?.LoseLife();
            TestScoreManager.Instance?.ResetCombo();
            AudioSource.PlayClipAtPoint(clipCrack, transform.position, 0.15f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("RottenEgg"))
        {
            DestroyEggsInAllBaskets();

            AudioSource.PlayClipAtPoint(clipBOOM, transform.position, 0.15f);
            Destroy(other.gameObject);
        }
    }

    private void DestroyEggsInAllBaskets()
    {
        if (basketZones == null || basketZones.Length == 0)
        {
            Debug.LogWarning("Brak przypisanych basket zones!");
            return;
        }

        foreach (TestBasketZone basket in basketZones)
        {
            if (basket != null)
                basket.DestroyEggs();
        }
    }
}
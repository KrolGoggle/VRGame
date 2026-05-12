using UnityEngine;

public class TestBombHole : MonoBehaviour
{
    [Header("Collider")]
    public Collider zone;

    public AudioClip clipBombDestroyed;

    void Awake()
    {
        if (zone == null)
            zone = GetComponent<BoxCollider>();
        if (zone != null)
            zone.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RottenEgg"))
        {
            Debug.Log("Bomba zniszczona w dziurze!");

            if (TestScoreManager.Instance != null)
            {
                TestScoreManager.Instance.AddPoint(1);
            }
            else
            {
                Debug.LogWarning("Brak TestScoreManager na scenie!");
            }

            if (clipBombDestroyed != null)
            {
                AudioSource.PlayClipAtPoint(clipBombDestroyed, transform.position, 0.15f);
            }

            Destroy(other.gameObject);
        }
    }
}
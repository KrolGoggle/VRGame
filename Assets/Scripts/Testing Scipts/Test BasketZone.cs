using UnityEngine;
using System.Collections.Generic;

public class TestBasketZone : MonoBehaviour
{
    private List<GameObject> eggsInBasket = new List<GameObject>();

    public AudioClip clipGood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Egg"))
        {
            if (!eggsInBasket.Contains(other.gameObject))
            {
                eggsInBasket.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Egg"))
        {
            eggsInBasket.Remove(other.gameObject);
        }
    }

    public void ScoreEggs()
    {
        int processedEggs = 0;

        foreach (GameObject egg in eggsInBasket)
        {
            if (TestScoreManager.Instance != null)
            {
                TestScoreManager.Instance.AddPoint(1);
            }
            else
            {
                Debug.LogWarning("Brak TestScoreManager na scenie!");
            }

            processedEggs++;
            Destroy(egg);
        }

        eggsInBasket.Clear();
        AudioSource.PlayClipAtPoint(clipGood, transform.position, 0.15f);
        Debug.Log("Do koszyka wrzucono i podliczono jajek: " + processedEggs);
    }

    public void DestroyEggs()
    {
        int destroyedEggs = 0;

        foreach (GameObject egg in eggsInBasket)
        {
            Destroy(egg);
            destroyedEggs++;
        }

        eggsInBasket.Clear();
        Debug.Log("Zniszczono jajek w koszyku: " + destroyedEggs);
    }
}

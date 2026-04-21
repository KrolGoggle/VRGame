using UnityEngine;

public class PlatformShowUI : MonoBehaviour
{
    public GameObject menuUI;

    void Start()
    {
        menuUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(SpawnerLogic._gameRunning) return;
        if (other.CompareTag("Player"))
        {
            menuUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            menuUI.SetActive(false);
        }
    }
}
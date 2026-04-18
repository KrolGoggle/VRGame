using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Toggles")]
    public Toggle toggleH;
    public Toggle toggleB;

    [Header("Mode GameObj")]
    public GameObject HM;
    public GameObject BM;

    [Header("Referencje")]
    public SpawnerLogic spawner;
    public GameObject menuPanel;

    public void OnStartPressed()
    {
        if (!toggleH.isOn && !toggleB.isOn)
        {
            Debug.LogWarning("Wybierz tryb przed startem");
            return;
        }

        if (toggleH.isOn)
            Debug.Log("Tryb Hand wybrany");
        else if (toggleB.isOn)
            Debug.Log("Tryb Basket wybrany");

        spawner.StartSpawning();
        menuPanel.SetActive(false);
    }

    public void Show() { 
        menuPanel.SetActive(true);
    }

    public void Hide()
    {
        menuPanel.SetActive(false);
    }
}
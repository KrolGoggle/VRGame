using UnityEngine;
using UnityEngine.UI;

public class TestMenuManager : MonoBehaviour
{
    [Header("Toggles")]
    public Toggle toggleH;
    public Toggle toggleB;

    [Header("Mode GameObj")]
    public GameObject HM;
    public GameObject BM;

    [Header("Referencje")]
    public TestSpawnerLogic testspawner;
    public GameObject menuPanel;

    void Start()
    {
        toggleH.onValueChanged.AddListener(isOn => { if (isOn) testspawner.SetPrefab("HM"); });
        toggleB.onValueChanged.AddListener(isOn => { if (isOn) testspawner.SetPrefab("BM"); });

        if (toggleH.isOn) testspawner.SetPrefab("HM");
        else if (toggleB.isOn) testspawner.SetPrefab("BM");
    }

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

        testspawner.StartSpawning();
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
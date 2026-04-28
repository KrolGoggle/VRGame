using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using TMPro;

public class SpawnerLogic : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefabToSpawnHM;
    public GameObject prefabToSpawnBM;
    public GameObject rottenEggPrefab;
    public GameObject _currentPrefab;

    [Header("Strefy")]
    public BoxCollider[] zones;
    public Transform rottenEggSpawnPos;

    [Header("XR")]
    public XRInteractionManager xrInteractionManager;

    [Header("Interwal")]
    public float startInterval = 2f;
    public float minInterval = 0.3f;
    public float accelerationRate = 5f;
    public float accelerationStep = 0.1f;

    [Header("Odliczanie")]
    public float countdownDuration = 3f;
    public TMP_Text countdownText;

    private float _currentInterval;
    private float _spawnTimer;
    private float _accelerationTimer;

    public static bool _gameRunning = false;

    void Start()
    {
        if (xrInteractionManager == null)
            xrInteractionManager = FindAnyObjectByType<XRInteractionManager>();

        _currentInterval = startInterval;
        _spawnTimer = startInterval;
        _accelerationTimer = accelerationRate;
    }

    public void StartGame()
    {
        // StartCoroutine(AccelerationRoutine());
        StartCoroutine(SpawningRoutine());
    }

    // IEnumerator AccelerationRoutine()
    // {
    //     while (_gameRunning)
    //     {
    //         if (_currentInterval > minInterval)
    //         {
    //             _currentInterval = Mathf.Max(minInterval, _currentInterval - accelerationStep);
    //         }

    //         yield return new WaitForSeconds(accelerationRate);
    //     }
    // }
    void ApplyAcceleration()
    {
        _currentInterval = Mathf.Max(minInterval, _currentInterval - accelerationStep);
    }
    IEnumerator SpawningRoutine()
    {
        while (_gameRunning)
        {
            if (_currentPrefab != null && zones.Length > 0)
            {
                SpawnInRandomZone();

                ApplyAcceleration(); 
            }

            yield return new WaitForSeconds(_currentInterval);
        }
    }
    void SpawnInRandomZone()
    {
        int spawnCount = Random.Range(0f, 1f) < 0.1f ? 2 : 1;

        for (int i = 0; i < spawnCount; i++)
        {
            BoxCollider zone = zones[Random.Range(0, zones.Length)];
            Vector3 center = zone.transform.position + zone.center;
            Vector3 size = Vector3.Scale(zone.size, zone.transform.lossyScale);
            Vector3 pos = new Vector3(
                center.x + Random.Range(-size.x / 2f, size.x / 2f),
                center.y + Random.Range(-size.y / 2f, size.y / 2f),
                center.z + Random.Range(-size.z / 2f, size.z / 2f)
            );

            GameObject spawned = Random.Range(0f, 1f) < 0.25f
                ? Instantiate(rottenEggPrefab, rottenEggSpawnPos.position, Quaternion.identity)
                : Instantiate(_currentPrefab, pos, Quaternion.identity);

            foreach (var interactable in spawned.GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>())
                interactable.interactionManager = xrInteractionManager;
        }
    }

    public void StartSpawning()
    {
        //StartCoroutine(CountdownThenSpawn());
        _gameRunning = true;
        StartGame();
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        DestroyAllEggs();
        _gameRunning = false;
        _currentInterval = startInterval;
        _spawnTimer = startInterval;
        _accelerationTimer = accelerationRate;
    }

    IEnumerator CountdownThenSpawn()
    {
        float timer = countdownDuration;

        if (countdownText != null) { 
            Debug.Log("CountdownText Object not found");
            yield break;
        }

        countdownText.gameObject.SetActive(true);

        while (timer > 0f)
        {
            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(timer).ToString();

            yield return new WaitForSeconds(1f);
            Debug.Log("Countdown: " + Mathf.CeilToInt(timer));
            timer -= 1f;
        }

        countdownText.text = "Start!";
        yield return new WaitForSeconds(0.8f);
        countdownText.gameObject.SetActive(false);

        _gameRunning = true;
        _currentInterval = startInterval;
        _spawnTimer = startInterval;
        _accelerationTimer = accelerationRate;
    }

    public void DestroyAllEggs()
    {
        foreach (var egg in GameObject.FindGameObjectsWithTag("Egg"))
            Destroy(egg);
        foreach (var egg in GameObject.FindGameObjectsWithTag("RottenEgg"))
            Destroy(egg);
    }

    public void SetPrefab(string prefab) {
        if (prefab == "HM")
            _currentPrefab = prefabToSpawnHM;
        if (prefab == "BM")
            _currentPrefab = prefabToSpawnBM;
    }
}
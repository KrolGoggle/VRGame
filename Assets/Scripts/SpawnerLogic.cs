using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnerLogic : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefabToSpawn;

    [Header("Strefy (GameObjecty z BoxCollider)")]
    public BoxCollider[] zones;

    [Header("XR")]
    public XRInteractionManager xrInteractionManager;

    [Header("Interwa�")]
    public float startInterval = 2f;
    public float minInterval = 0.3f;
    public float accelerationRate = 5f;
    public float accelerationStep = 0.1f;

    private float _currentInterval;
    private float _spawnTimer;
    private float _accelerationTimer;

    void Start()
    {
        if (xrInteractionManager == null)
            xrInteractionManager = FindAnyObjectByType<XRInteractionManager>();

        _currentInterval = startInterval;
        _spawnTimer = startInterval;
        _accelerationTimer = accelerationRate;
    }

    void Update()
    {
        HandleAcceleration();
        HandleSpawning();
    }

    void HandleAcceleration()
    {
        if (_currentInterval <= minInterval) return;

        _accelerationTimer -= Time.deltaTime;
        if (_accelerationTimer <= 0f)
        {
            _currentInterval = Mathf.Max(minInterval, _currentInterval - accelerationStep);
            _accelerationTimer = accelerationRate;
        }
    }

    void HandleSpawning()
    {
        if (prefabToSpawn == null || zones.Length == 0) return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            SpawnInRandomZone();
            _spawnTimer = _currentInterval;
        }
    }

    void SpawnInRandomZone()
    {
        BoxCollider zone = zones[Random.Range(0, zones.Length)];
        Vector3 center = zone.transform.position + zone.center;
        Vector3 size = Vector3.Scale(zone.size, zone.transform.lossyScale);

        Vector3 pos = new Vector3(
            center.x + Random.Range(-size.x / 2f, size.x / 2f),
            center.y + Random.Range(-size.y / 2f, size.y / 2f),
            center.z + Random.Range(-size.z / 2f, size.z / 2f)
        );

        GameObject spawned = Instantiate(prefabToSpawn, pos, Quaternion.identity);

        foreach (var interactable in spawned.GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>())
            interactable.interactionManager = xrInteractionManager;
    }
}
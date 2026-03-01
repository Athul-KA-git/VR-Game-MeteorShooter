using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Meteor Prefabs (Multiple Types)")]
    public GameObject[] meteorPrefabs;   // <-- Changed to array

    public Transform ship;

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(30, 10, 30);

    [Header("Starting Difficulty")]
    public float startSpawnInterval = 2f;
    public float startMeteorSpeed = 5f;

    [Header("Difficulty Scaling")]
    [Range(0f, 0.2f)]
    public float spawnAcceleration = 0.005f;

    [Range(0f, 1f)]
    public float speedIncreaseRate = 0.05f;

    public float minSpawnInterval = 1.2f;
    public float maxMeteorSpeed = 8f;

    [Header("Meteor Limits")]
    public int maxMeteorsAlive = 15;

    [Header("Meteor Size")]
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    private float spawnTimer;
    private float survivalTime;

    private List<GameObject> activeMeteors = new List<GameObject>();

    void Update()
    {
        survivalTime += Time.deltaTime;

        activeMeteors.RemoveAll(m => m == null);

        if (activeMeteors.Count >= maxMeteorsAlive)
            return;

        float currentInterval = Mathf.Max(
            minSpawnInterval,
            startSpawnInterval - survivalTime * spawnAcceleration
        );

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentInterval)
        {
            SpawnMeteor();
            spawnTimer = 0f;
        }
    }

    void SpawnMeteor()
    {
        if (meteorPrefabs.Length == 0) return;

        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
            Random.Range(-spawnAreaSize.y, spawnAreaSize.y),
            Random.Range(-spawnAreaSize.z, spawnAreaSize.z)
        );

        // 🔥 Pick random meteor type
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        GameObject selectedPrefab = meteorPrefabs[randomIndex];

        GameObject meteor = Instantiate(selectedPrefab, randomPos, Random.rotation);

        float size = Random.Range(minSize, maxSize);

        Meteor meteorScript = meteor.GetComponent<Meteor>();

        if (meteorScript != null)
        {
            meteorScript.speed = Mathf.Min(
                startMeteorSpeed + survivalTime * speedIncreaseRate,
                maxMeteorSpeed
            );

            meteorScript.Initialize(ship, size);
        }

        activeMeteors.Add(meteor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize * 2);
    }
}
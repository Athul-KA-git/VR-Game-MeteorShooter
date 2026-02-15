using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform ship;

    public Vector3 spawnAreaSize = new Vector3(30, 10, 30);
    public float spawnInterval = 2f;

    public float minSize = 0.5f;
    public float maxSize = 2.5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMeteor), 1f, spawnInterval);
    }

    private void SpawnMeteor()
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
            Random.Range(-spawnAreaSize.y, spawnAreaSize.y),
            Random.Range(-spawnAreaSize.z, spawnAreaSize.z)
        );

        GameObject meteor = Instantiate(meteorPrefab, randomPos, Random.rotation);

        float size = Random.Range(minSize, maxSize);

        meteor.GetComponent<Meteor>().Initialize(ship, size);
    }

    //  Visual Spawn Area In Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize * 2);
    }
}

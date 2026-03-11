using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("Tracer")]
    [SerializeField] private LineRenderer tracerPrefab;
    [SerializeField] private float tracerDuration = 0.05f;
    [SerializeField] private float tracerDistance = 50f;

    [Header("VFX")]
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Audio")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
    }

    public void FireBullet()
    {
        // Spawn bullet
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = spawnPoint.forward * bulletSpeed;

        Destroy(spawnedBullet, 5f);

        // Play muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Spawn tracer
        SpawnTracer();

        // Play sound
        if (fireSound != null)
            audioSource.PlayOneShot(fireSound, volume);
    }

    void SpawnTracer()
    {
        if (tracerPrefab == null) return;

        LineRenderer tracer = Instantiate(tracerPrefab, spawnPoint.position, Quaternion.identity);

        Vector3 startPos = spawnPoint.position;
        Vector3 endPos = startPos + spawnPoint.forward * tracerDistance;

        RaycastHit hit;

        if (Physics.Raycast(startPos, spawnPoint.forward, out hit, tracerDistance))
        {
            endPos = hit.point;
        }

        tracer.SetPosition(0, startPos);
        tracer.SetPosition(1, endPos);

        Destroy(tracer.gameObject, tracerDuration);
    }
}
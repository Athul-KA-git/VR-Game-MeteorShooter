using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;   // 3D sound
        audioSource.playOnAwake = false;
    }

    public void FireBullet()
    {
        //  Spawn bullet
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnedBullet.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnedBullet, 5f);

        //  Play fire sound
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound, volume);
        }
    }
}
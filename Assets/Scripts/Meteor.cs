using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 2;

    [Header("Rotation Settings")]
    public float rotationSpeed = 60f;

    [Header("Explosion")]
    public AudioClip explosionSound;
    public GameObject explosionVFX; // optional particle prefab

    private Transform target;
    private Rigidbody rb;
    private Vector3 rotationAxis;

    private bool isDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotationAxis = Random.onUnitSphere;
    }

    public void Initialize(Transform shipTarget, float sizeMultiplier)
    {
        target = shipTarget;

        transform.localScale *= sizeMultiplier;

        damage = Mathf.RoundToInt(damage * sizeMultiplier);
        rotationSpeed /= sizeMultiplier;
    }

    private void FixedUpdate()
    {
        if (target == null || isDestroyed) return;

        Vector3 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        rb.MoveRotation(rb.rotation *
            Quaternion.AngleAxis(rotationSpeed * Time.fixedDeltaTime, rotationAxis));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDestroyed) return;

        // Hit Ship
        ShipHealth ship = other.gameObject.GetComponentInParent<ShipHealth>();
        if (ship != null)
        {
            ship.TakeDamage(damage);
            DestroyMeteor();
            return;
        }

        // Hit Bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }

            DestroyMeteor();
        }
    }

    private void DestroyMeteor()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        //  Play Explosion Sound (3D)
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
        }

        //  Spawn Explosion VFX
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
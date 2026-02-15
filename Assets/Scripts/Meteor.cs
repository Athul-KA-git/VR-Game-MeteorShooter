using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 2;

    [Header("Rotation Settings")]
    public float rotationSpeed = 60f;

    private Transform target;
    private Rigidbody rb;
    private Vector3 rotationAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Random rotation direction
        rotationAxis = Random.onUnitSphere;
    }

    public void Initialize(Transform shipTarget, float sizeMultiplier)
    {
        target = shipTarget;

        // Scale meteor
        transform.localScale *= sizeMultiplier;

        // Bigger meteor = more damage
        damage = Mathf.RoundToInt(damage * sizeMultiplier);

        // OPTIONAL: Bigger meteors spin slower (realistic)
        rotationSpeed /= sizeMultiplier;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        // Move toward ship
        Vector3 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // Rotate meteor
        transform.Rotate(rotationAxis * rotationSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        ShipHealth ship = other.gameObject.GetComponentInParent<ShipHealth>();

        if (ship != null)
        {
            ship.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

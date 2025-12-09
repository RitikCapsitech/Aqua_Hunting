using UnityEngine;

public class AnimatedBoatController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [Header("Horizontal Movement")]
    public float scrollSpeed = 1.5f;
    public float resetXPosition = -15f;

    [Header("Bobbing (Vertical)")]
    public float bobSpeed = 1.0f;
    public float bobHeight = 0.15f;

    [Header("Tilting (Z-Rotation)")]
    public float tiltSpeed = 1.5f;
    public float maxTiltAngle = 1.5f;

    private Vector3 startPosition;

    // Reference to spawner
    private BoatSpawner spawner;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(BoatSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void Start()
    {

        startPosition = transform.position;




    }

    void Update()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

        if (transform.position.x > 10f)
        {
            if (spawner != null)
                spawner.OnBoatFinished();

            Destroy(gameObject);
            return;
        }

        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(
            transform.position.x,
            startPosition.y + bobOffset,
            startPosition.z
        );

        float tiltAngle = Mathf.Cos(Time.time * tiltSpeed) * maxTiltAngle;
        transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShipBoundary"))
        {

            _particleSystem.Play();
        }

    }
}
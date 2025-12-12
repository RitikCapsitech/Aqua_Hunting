using TMPro;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public static FishController instance;
    public Transform fishPrefab;
    private Transform fish;
    private Rigidbody2D rb;
    private TextMeshProUGUI fishScoreText;


    public float speed = 3f;


    public Vector3 scoreOffset = new Vector3(0, 1f, 0);
    private Vector2 moveTarget;


    private Vector2 targetPos;
    private bool isTouching = false;
    private bool isAlive = true;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fishSpawn();


    }
    public void fishSpawn()
    {
        // Destroy existing fish (if any) to avoid duplicates when respawning
        if (fish != null)
        {
            Destroy(fish.gameObject);
        }

        fish = Instantiate(fishPrefab);
        rb = fish.GetComponent<Rigidbody2D>();
        fishScoreText = fish.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        // Reset controller state so the newly spawned fish can move
        isAlive = true;
        isTouching = false;


    }

    void Update()
    {
        if (!isAlive || fish == null || rb == null)
            return;

        HandleMovement();
        HandleRotation();
        UpdateFishScore();
    }


    private void HandleMovement()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
#else
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        moveTarget = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }
#endif

        Vector2 direction = (moveTarget - rb.position);

        if (direction.magnitude > 0.1f)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }



    private void HandleRotation()
    {
        if (rb.linearVelocity.x > 0.05f)
        {
            fish.rotation = Quaternion.Euler(0, 180, 0);
            fishScoreText.transform.localRotation = Quaternion.Euler(0, 180, -30);
        }
        else if (rb.linearVelocity.x < -0.05f)
        {
            fish.rotation = Quaternion.Euler(0, 0, 0);
            fishScoreText.transform.localRotation = Quaternion.Euler(0, 0, 30);
        }
    }



    private void UpdateFishScore()
    {
        if (fishScoreText != null)
        {
            fishScoreText.text = GameManager.instance.GetScore().ToString();
        }
    }
    public void OnFishDead()
    {
        isAlive = false;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }





}

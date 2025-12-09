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

    private int score = 0;

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
        fish = Instantiate(fishPrefab);
        rb = fish.GetComponent<Rigidbody2D>();
        fishScoreText = fish.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        UpdateFishScore();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y).normalized;

        if (rb != null)
        {
            rb.linearVelocity = dir * speed;
        }
    }

    private void HandleRotation()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0.01f)  // moving RIGHT
        {
            fish.rotation = Quaternion.Euler(0, 180, 0);

            fishScoreText.transform.localRotation = Quaternion.Euler(0, 180, -30);
        }
        else if (x < -0.01f)  // moving LEFT
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


}

using TMPro;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public Transform fishLeftPrefab;
    public Transform fishRightPrefab;
    private Transform leftFish;
    private Transform rightFish;
    private Transform activeFish;
    public float speed = 3f;
    public TextMeshProUGUI scoreText;
    public Vector3 scoreOffset = new Vector3(0, 1f, 0);
    private int score = 0;
    private Rigidbody2D activeRB;

    void Start()
    {

        leftFish = Instantiate(fishLeftPrefab);
        rightFish = Instantiate(fishRightPrefab);

        //TextMeshProUGUI txt = rightFish.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //txt.text = "";

        leftFish.gameObject.SetActive(false);
        rightFish.gameObject.SetActive(true);

        activeFish = rightFish;
        activeRB = activeFish.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleSwitch();
        HandleMovement();
        UpdateScorePosition();
    }

    private void HandleSwitch()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SwitchFish(true);

        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SwitchFish(false);
        }
    }

    private void SwitchFish(bool left)
    {

        if (left && activeFish == leftFish) return;
        if (!left && activeFish == rightFish) return;

        Transform next = left ? leftFish : rightFish;

        next.position = activeFish.position;
        Rigidbody2D nextRB = next.GetComponent<Rigidbody2D>();
        if (nextRB != null && activeRB != null)
        {
            nextRB.linearVelocity = activeRB.linearVelocity;
        }


        activeFish.gameObject.SetActive(false);


        next.gameObject.SetActive(true);
        activeFish = next;
        activeRB = nextRB;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y).normalized;


        if (activeRB != null)
        {
            activeRB.linearVelocity = dir * speed;
        }
    }

    private void UpdateScorePosition()
    {
        if (scoreText != null && activeFish != null)
        {
            Vector3 worldPos = activeFish.position + scoreOffset;
            scoreText.transform.position = Camera.main.WorldToScreenPoint(worldPos);
            scoreText.text = score.ToString();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
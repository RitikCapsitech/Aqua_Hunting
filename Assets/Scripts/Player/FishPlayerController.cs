using UnityEngine;

public class FishMouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSpeed = 7f;
    public Rigidbody2D rb;

    private Vector2 targetPosition;
    private bool isDragging = false;

    void Update()
    {
        HandleMouseInput();
    }

    void FixedUpdate()
    {
        MoveFish();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.linearVelocity = Vector2.zero;
        }

        if (isDragging)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mouse;
        }
    }

    void MoveFish()
    {
        if (!isDragging) return;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.AddForce(direction * moveSpeed);


        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;


        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

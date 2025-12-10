using System.Collections;
using UnityEngine;

public class BoneSink : MonoBehaviour
{
    private Rigidbody2D rb;
    private float sinkSpeed = 0.3f;
    private float lifeTime = 2.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(SinkAndDisable());
    }

    IEnumerator SinkAndDisable()
    {
        yield return new WaitForSeconds(2.0f);

        float t = 0;
        while (t < lifeTime)
        {
            rb.linearVelocity = new Vector2(0, -sinkSpeed);
            t += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}

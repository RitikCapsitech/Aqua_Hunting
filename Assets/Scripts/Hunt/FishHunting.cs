using System.Collections;
using UnityEngine;

public class FishHunting : MonoBehaviour
{
    private float fadeDuration = 0.5f;
    private SpriteRenderer sr;
    private bool isFading = false;
    private bool boneSpawaned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading && !boneSpawaned)
        {
            boneSpawaned = true;

            SoundManager.Instance?.Prey();
            SpawnBone(transform.position);

            FishHunger hunger = collision.GetComponent<FishHunger>();
            if (hunger != null)
            {
                float newHunger = hunger.Eat(12);

            }

            GameManager.instance.AddScore(2);
            GameManager.instance.ShowScorePopup("+2", collision.transform.position);

            StartCoroutine(FadeAndDestroy());
        }
    }

    IEnumerator FadeAndDestroy()
    {
        isFading = true;

        float t = 0;
        Color original = sr.color;

        // fade to transparent
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            sr.color = new Color(original.r, original.g, original.b, alpha);

            yield return null;
        }

        // destroy  (fish)
        Destroy(gameObject);

    }
    void SpawnBone(Vector3 position)
    {
        GameObject bone = BonePool.Instance.GetBone(position);

        Rigidbody2D rb = bone.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Vector2 randomForce = new Vector2(
            Random.Range(-0.5f, 0.5f),
            Random.Range(-1.5f, -0.8f)
        );

        rb.AddForce(randomForce, ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-30f, 30f));
    }




}

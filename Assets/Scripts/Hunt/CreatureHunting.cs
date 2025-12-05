using System.Collections;
using UnityEngine;

public class CreatureHunting : MonoBehaviour
{
    public float fadeDuration = 0.5f; // how fast fish fades

    private SpriteRenderer sr;
    private bool isFading = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if collided with player
        if (collision.CompareTag("Player") && !isFading)
        {
            GameManager.instance.AddScore(1);
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

        // destroy creature (fish)
        Destroy(gameObject);
    }
}

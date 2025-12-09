using System.Collections;
using UnityEngine;

public class FishHunting : MonoBehaviour
{
    private float fadeDuration = 0.5f;
    private SpriteRenderer sr;
    private bool isFading = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading)
        {
            SoundManager.Instance.Prey();
            GameManager.instance.AddScore(2);
            GameManager.instance.ShowScorePopup("+2", collision.gameObject.transform.position);
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




}

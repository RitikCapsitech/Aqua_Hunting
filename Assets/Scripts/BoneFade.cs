using System.Collections;
using UnityEngine;

public class BoneFade : MonoBehaviour
{
    private SpriteRenderer sr;
    private float delay = 0.6f;
    private float fadeDuration = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(delay);

        float t = 0;
        Color original = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            sr.color = new Color(original.r, original.g, original.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}

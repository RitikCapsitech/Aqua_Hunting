using System.Collections;
using UnityEngine;

public class PlayerHunt : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFading = false;
    private float fadeDuration = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Prediator"))
        {
            GameManager.instance.ShowScorePopup("No... It was larger than you!", collision.transform.position);
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
        //GameManager.instance.ShowMessage("No... It was larger than You!", transform.position);
        GameManager.instance.ShowGameOver();

        Destroy(gameObject);
    }


}

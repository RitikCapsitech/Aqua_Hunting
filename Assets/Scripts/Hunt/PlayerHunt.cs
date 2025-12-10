using System.Collections;
using UnityEngine;

public class PlayerHunt : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFading = false;
    private float fadeDuration = 1f;
    [SerializeField] private GameObject bonePrefab;
    private bool boneSpawned = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Prediator") && !isFading && !boneSpawned)
        {
            boneSpawned = true;
            isFading = true;

            SoundManager.Instance?.PlayerDieSound();

            SpawnBone(transform.position);

            GameManager.instance.ShowScorePopup(
                "No... It was larger than you!",
                collision.transform.position
            );

            StartCoroutine(FadeAndDestroy());
        }
    }

    IEnumerator FadeAndDestroy()
    {
        float t = 0;
        Color original = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            sr.color = new Color(original.r, original.g, original.b, alpha);
            yield return null;
        }

        FishController.instance?.OnFishDead();
        GameManager.instance.ShowGameOver();

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

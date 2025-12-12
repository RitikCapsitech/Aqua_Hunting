using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public float speed = 3f;
    public float targetX = -9f;
    public float fadeTime = 0.5f;
    private float fadeDuration = 1f;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        sr.enabled = true;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= targetX)
        {
            gameObject.SetActive(false);
            FindObjectOfType<BombSpawner>().SpawnNextBomb();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(Explode());

    }

    IEnumerator Explode()
    {
        sr.enabled = false;

        // Get explosion from pool
        GameObject blast = ExplosionPool.Instance.GetExplosion();
        blast.transform.position = transform.position;

        SpriteRenderer br = blast.GetComponent<SpriteRenderer>();

        float t = 0;
        Color c = br.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, t / fadeTime);
            br.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        blast.SetActive(false);
        br.color = new Color(c.r, c.g, c.b, 1);

        gameObject.SetActive(false);
        FishController.instance?.OnFishDead();
        GameManager.instance.ShowGameOver();
        //FindObjectOfType<BombSpawner>().SpawnNextBomb();
    }


}

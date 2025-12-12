using System.Collections;
using UnityEngine;
public class BombSpawner : MonoBehaviour
{
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    public float spawnX = 9f;
    public float minY = -3f;
    public float maxY = 3f;

    void Start()
    {
        SpawnNextBomb();
    }

    public void SpawnNextBomb()
    {
        StartCoroutine(SpawnNextAfterDelay());
    }
    private IEnumerator SpawnNextAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f)); // 1–2 sec delay

        float y = Random.Range(minY, maxY);

        GameObject bomb = BombPool.Instance.GetBomb();
        bomb.transform.position = new Vector3(spawnX, y, 0);
    }
}


using System.Collections;
using UnityEngine;

public class FishSpawner_LTR : MonoBehaviour
{
    [System.Serializable]
    public class Fish
    {
        public Transform prefab;
        public float speed = 1f;
    }

    public Fish[] fishPrefabs;

    public float startX = -9f;    // spawn on the left
    public float endX = 9f;     // travel to the right

    public float minY = -2f;
    public float maxY = 2f;

    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // pick a random fish type
            int index = Random.Range(0, fishPrefabs.Length);
            Fish selected = fishPrefabs[index];

            // create it
            Transform fish = Instantiate(selected.prefab);

            // place at random Y position on left side
            float randomY = Random.Range(minY, maxY);
            fish.position = new Vector3(startX, randomY, 0);

            // start moving
            StartCoroutine(MoveFish(fish, selected.speed));

            // wait random interval before spawning next fish
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    IEnumerator MoveFish(Transform fish, float speed)
    {
        while (fish != null)
        {
            fish.position += Vector3.right * speed * Time.deltaTime;

            // reached left side → destroy
            if (fish.position.x >= endX)
                break;

            yield return null;
        }

        if (fish != null)
            Destroy(fish.gameObject);
    }
}

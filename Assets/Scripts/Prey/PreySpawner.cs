using System.Collections;

using UnityEngine;

public class PreySpawner : MonoBehaviour
{
    public static PreySpawner Instance;
    [System.Serializable]
    public class Prey
    {
        public Transform prefab;
        public float fallSpeed = 2f;
    }

    public Prey[] preyPrefabs;        // assign prey prefabs
    public float spawnY = 6f;         // height of spawn
    public float minX = -3f;
    public float maxX = 3f;

    public float destroyY = -6f;      // where the prey gets destroyed

    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 1.5f;

    private int currentIndex = 0;
    private bool spawning = false;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        spawning = true;

        while (true)
        {
            // reset when all have spawned
            if (currentIndex >= preyPrefabs.Length)
            {
                currentIndex = 0;
                yield return new WaitForSeconds(1f);   // optional pause before repeating
            }

            // pick next prey
            Prey preyData = preyPrefabs[currentIndex];
            Transform prey = Instantiate(preyData.prefab);

            // random X start position
            float randomX = Random.Range(minX, maxX);
            prey.position = new Vector3(randomX, spawnY, 0);

            // start falling
            StartCoroutine(FallAndDestroy(prey, preyData.fallSpeed));

            currentIndex++;

            // wait random time before spawning next
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    IEnumerator FallAndDestroy(Transform prey, float speed)
    {
        while (prey != null && prey.position.y > destroyY)
        {
            prey.position += Vector3.down * speed * Time.deltaTime;
            yield return null;
        }

        if (prey != null)
            Destroy(prey.gameObject);
    }
}

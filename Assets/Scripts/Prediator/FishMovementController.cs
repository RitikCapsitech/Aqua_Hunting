using System.Collections;
using UnityEngine;

public class FishMovementController : MonoBehaviour
{
    [System.Serializable]
    public class Fish
    {
        public Transform fishTransform;
        public float speed = 2f;
    }

    public Fish[] fishPrefabs;   // array of prefabs
    private Fish[] fishes;
    public float minY = -2f;
    public float maxY = 2f;

    public float startX = -6f;
    public float maxX = 6f;
    public float triggerX = 3f;

    public float restartDelay = 2f;
    private int currentFishIndex = 0;
    private bool restarting = false;

    void Start()
    {
        fishes = new Fish[fishPrefabs.Length];
        for (int i = 0; i < fishPrefabs.Length; i++)
        {
            Fish newFish = new Fish();
            newFish.fishTransform = Instantiate(fishPrefabs[i].fishTransform);
            newFish.speed = fishPrefabs[i].speed;

            fishes[i] = newFish;
        }
        ResetAllFishes();
    }

    void Update()
    {
        if (restarting || currentFishIndex >= fishes.Length)
            return;

        Fish currentFish = fishes[currentFishIndex];
        MoveFish(currentFish);


        if (currentFish.fishTransform.position.x >= triggerX)
        {
            int next = currentFishIndex + 1;

            if (next < fishes.Length)
            {
                fishes[next].fishTransform.gameObject.SetActive(true);
                SetFishStartPosition(fishes[next].fishTransform);
            }

            currentFishIndex++;


            if (currentFishIndex >= fishes.Length)
            {
                StartCoroutine(RestartSequence());
            }
        }
    }

    void MoveFish(Fish fish)
    {
        fish.fishTransform.position += Vector3.right * fish.speed * Time.deltaTime;


        if (fish.fishTransform.position.x > maxX)
        {
            SetFishStartPosition(fish.fishTransform);
        }
    }

    IEnumerator RestartSequence()
    {
        restarting = true;
        yield return new WaitForSeconds(restartDelay);

        ResetAllFishes();
        restarting = false;
    }

    void ResetAllFishes()
    {

        for (int i = 0; i < fishes.Length; i++)
        {
            fishes[i].fishTransform.gameObject.SetActive(i == 0);
            SetFishStartPosition(fishes[i].fishTransform);
        }

        currentFishIndex = 0;
    }

    void SetFishStartPosition(Transform fish)
    {
        float randomY = Random.Range(minY, maxY);
        fish.position = new Vector3(startX, randomY, fish.position.z);
    }
}

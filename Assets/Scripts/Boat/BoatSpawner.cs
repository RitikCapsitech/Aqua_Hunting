using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    [Header("Boat Prefabs")]
    public List<GameObject> boatPrefabs;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    private int currentIndex = 0;

    void Start()
    {
        SpawnBoat(); // Start with the first boat
    }

    public void SpawnBoat()
    {
        if (boatPrefabs.Count == 0) return;

        GameObject prefab = boatPrefabs[currentIndex];
        currentIndex = (currentIndex + 1) % boatPrefabs.Count;

        GameObject boat = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        boat.GetComponent<AnimatedBoatController>().Init(this);
    }

    public void OnBoatFinished()
    {
        SpawnBoat();
    }
}
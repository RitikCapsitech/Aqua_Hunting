using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    public static BombPool Instance;

    public GameObject bombPrefab;
    public int poolSize = 10;

    private List<GameObject> bombPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.SetActive(false);
            bombPool.Add(bomb);
        }
    }

    public GameObject GetBomb()
    {
        foreach (var bomb in bombPool)
        {
            if (!bomb.activeInHierarchy)
            {
                bomb.SetActive(true);
                return bomb;
            }
        }

        // optional: expand pool
        GameObject extra = Instantiate(bombPrefab);
        extra.SetActive(false);
        bombPool.Add(extra);

        extra.SetActive(true);
        return extra;
    }
}

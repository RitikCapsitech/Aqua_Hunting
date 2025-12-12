using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance;

    public GameObject explosionPrefab;
    public int poolSize = 10;

    private List<GameObject> explosionPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject exp = Instantiate(explosionPrefab);
            exp.SetActive(false);
            explosionPool.Add(exp);
        }
    }

    public GameObject GetExplosion()
    {
        foreach (var exp in explosionPool)
        {
            if (!exp.activeInHierarchy)
            {
                exp.SetActive(true);
                return exp;
            }
        }

        // optional expansion
        GameObject extra = Instantiate(explosionPrefab);
        extra.SetActive(false);
        explosionPool.Add(extra);

        extra.SetActive(true);
        return extra;
    }
}

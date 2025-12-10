using System.Collections.Generic;
using UnityEngine;

public class BonePool : MonoBehaviour
{
    public static BonePool Instance;

    [SerializeField] private GameObject bonePrefab;
    [SerializeField] private int poolSize = 15;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bone = Instantiate(bonePrefab, transform);
            bone.SetActive(false);
            pool.Enqueue(bone);
        }
    }

    public GameObject GetBone(Vector3 position)
    {
        GameObject bone = pool.Dequeue();
        bone.transform.position = position;
        bone.SetActive(true);
        pool.Enqueue(bone);
        return bone;
    }
}

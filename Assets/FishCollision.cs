using UnityEngine;

public class FishCollision : MonoBehaviour
{
    private ParticleSystem fishexpolsion;

    void Start()
    {
        fishexpolsion = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Prediator") || collision.CompareTag("Prey"))
        {

            fishexpolsion.Play();
        }
    }
}

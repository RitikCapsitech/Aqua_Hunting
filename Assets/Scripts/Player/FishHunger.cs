using UnityEngine;
using UnityEngine.UI;

public class FishHunger : MonoBehaviour
{
    public float maxHunger = 100f;
    public float currentHunger;

    public float drainPerSecond = 3f;
    public float eatFillAmount = 25f;

    private Slider hungerSlider;

    void Start()
    {
        currentHunger = maxHunger;

        hungerSlider = GameObject
            .Find("HungerSlider")
            .GetComponent<Slider>();

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
    }

    void Update()
    {
        DrainHunger();
    }

    void DrainHunger()
    {
        currentHunger -= drainPerSecond * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        hungerSlider.value = currentHunger;

        if (currentHunger <= 0)
        {
            GameManager.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    public void Eat()
    {
        currentHunger += eatFillAmount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        hungerSlider.value = currentHunger;
    }
}

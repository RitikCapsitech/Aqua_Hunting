using UnityEngine;
using UnityEngine.UI;

public class FishHunger : MonoBehaviour
{
    public float maxHunger = 100f;
    public float currentHunger;

    public float drainPerSecond = 3f;


    private Slider hungerSlider;
    private Image fillImage;


    void Start()
    {
        currentHunger = maxHunger;

        hungerSlider = GameObject
            .Find("HungerSlider")
            .GetComponent<Slider>();
        fillImage = hungerSlider
      .fillRect
      .GetComponent<Image>();

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
        UpdateColor();
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
        UpdateColor();

        if (currentHunger <= 0)
        {
            GameManager.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    public float Eat(int eatFillAmount)
    {
        currentHunger += eatFillAmount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        hungerSlider.value = currentHunger;
        UpdateColor();
        return currentHunger;
    }
    void UpdateColor()
    {
        float percent = currentHunger / maxHunger;

        if (percent > 0.6f)
        {
            fillImage.color = Color.green;
        }
        else if (percent > 0.3f)
        {
            fillImage.color = new Color(1f, 0.65f, 0f); // Orange
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

}

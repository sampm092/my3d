using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float health;
    public int MaxHealth = 100;
    public float ChipSpeed = 2f;
    public float lerpTimer; //bar speed
    public Image backHB;
    public Image FrontHB;
    public TextMeshProUGUI HealthText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        health = MaxHealth; // health full
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth); //declare health range
        if (Input.GetKeyDown(KeyCode.R)) // for testing
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.Q)) // for testing
        {
            fillHP(Random.Range(5, 10));
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = FrontHB.fillAmount; // take panel value
        float fillB = backHB.fillAmount; // take panel value
        float hFraction = health / MaxHealth;

        if (fillB > hFraction) // Taking damage, health bar animation
        {
            FrontHB.fillAmount = hFraction;
            backHB.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / ChipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHB.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction) // health bar animation if healed
        {
            backHB.color = Color.green;
            backHB.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / ChipSpeed;
            percentComplete = percentComplete * percentComplete;
            FrontHB.fillAmount = Mathf.Lerp(fillF, backHB.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void fillHP(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}

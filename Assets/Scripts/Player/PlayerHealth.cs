using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float health;

    [Header("Health Bar")]
    public int MaxHealth = 100;
    public float ChipSpeed = 2f;
    public float lerpTimer; //bar speed
    public Image backHB;
    public Image FrontHB;
    public TextMeshProUGUI HealthText;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadespeed;

    private float durationTimer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        health = MaxHealth; // health full
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth); //declare health range
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health < 30) //keep overlay if health low
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(
                    overlay.color.r,
                    overlay.color.g,
                    overlay.color.b,
                    tempAlpha
                );
            }
        }
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
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void fillHP(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}

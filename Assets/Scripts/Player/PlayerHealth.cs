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

    [Header("Overlay")]
    public Image damageOverlay;
    public Image healOverlay;
    public float duration;
    public float fadespeed;
    private float damageTimer;
    private float healTimer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        health = MaxHealth; // health full
        Color colorD = damageOverlay.color;
        Color colorH = healOverlay.color;
        damageOverlay.color = new Color(colorD.r, colorD.g, colorD.b, 0);
        healOverlay.color = new Color(colorH.r, colorH.g, colorH.b, 0);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth); //declare health range
        UpdateHealthUI();
        // Damage overlay
        FadeOverlay(damageOverlay, ref damageTimer, health < 30);

        // Heal overlay
        FadeOverlay(healOverlay, ref healTimer);
    }

    void FadeOverlay(Image overlay, ref float timer, bool keepVisible = false)
    {
        if (overlay.color.a <= 0f)
            return;

        if (keepVisible)
            return;

        timer += Time.deltaTime;

        if (timer > duration)
        {
            Color c = overlay.color;
            c.a -= Time.deltaTime * fadespeed;
            c.a = Mathf.Clamp01(c.a); // prevents negative alpha
            overlay.color = c;
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
        damageTimer = 0;
        damageOverlay.color = new Color(
            damageOverlay.color.r,
            damageOverlay.color.g,
            damageOverlay.color.b,
            1
        );
    }

    public void FillHP(float heal)
    {
        if (health == MaxHealth)
            return;
        health += heal;
        lerpTimer = 0f;
        healTimer = 0;
        healOverlay.color = new Color(
            healOverlay.color.r,
            healOverlay.color.g,
            healOverlay.color.b,
            1
        );
    }
}

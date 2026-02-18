using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float health;

    [Header("Health Bar")]
    public int MaxHealth = 100;
    public float ChipSpeed = 2f; //animation speed
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
        SetAlpha(damageOverlay, 0f); //set overlay to invicible at start
        SetAlpha(healOverlay, 0f); //set overlay to invicible at start
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth); //declare health range
        UpdateHealthUI();
        // Damage overlay
        FadeOverlay(damageOverlay, ref damageTimer, health < 30); //keep overlay with condition

        // Heal overlay
        FadeOverlay(healOverlay, ref healTimer);
    }

    void FadeOverlay(Image overlay, ref float timer, bool keepVisible = false)
    {
        // if (overlay.color.a <= 0f) //checks if opacity is zero (or below)
        if (overlay.color.a <= 0.01f) // fading over time
            return; // then do nothing

        if (keepVisible) // situation based
            return; // then do nothing

        timer += Time.deltaTime; //increment

        if (timer > duration) //overlay stays fully visible until duration reached
        {
            Color c = overlay.color; //get overlay image color
            c.a -= Time.deltaTime * fadespeed; //reduce transparancy little by little
            c.a = Mathf.Clamp01(c.a); // prevents negative alpha
            overlay.color = c; //update overlay image color
        }
    }

    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color; //get image color
        c.a = alpha; //set transparancy based on alpha value
        img.color = c; //update image color
    }

    public void UpdateHealthUI() //health bar animation
    {
        float fillF = FrontHB.fillAmount; // take panel value
        float fillB = backHB.fillAmount; // take panel value
        float hFraction = health / MaxHealth; //actual health percentage value

        if (fillB > hFraction)
        // Taking damage, health bar animation
        {
            FrontHB.fillAmount = hFraction; //instantly shorten front bar based on hfraction
            backHB.color = Color.red; //change back bar color to red
            lerpTimer += Time.deltaTime; //increase overtime
            float percentComplete = lerpTimer / ChipSpeed;
            percentComplete = percentComplete * percentComplete; //squaring for ease-in effect

            //Lerp = smoothly transition between a starting value (a) and an ending value (b) over time
            backHB.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); //back bar animation
        }
        if (fillF < hFraction) // health bar animation if healed
        {
            backHB.color = Color.green; //change back bar color to green
            backHB.fillAmount = hFraction; //instantly lengthen back bar based on hfraction
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / ChipSpeed;
            percentComplete = percentComplete * percentComplete;
            FrontHB.fillAmount = Mathf.Lerp(fillF, backHB.fillAmount, percentComplete); //front bar animation
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //reset to zero every time this happens
        lerpTimer = 0f;
        damageTimer = 0;

        damageOverlay.color = new Color(
            damageOverlay.color.r,
            damageOverlay.color.g,
            damageOverlay.color.b,
            1
        ); //set opacity to 1
    }

    public void FillHP(float heal)
    {
        if (health == MaxHealth) //make sure no effect if full health
            return;
        health += heal;

        //reset to zero every time this happens
        lerpTimer = 0f;
        damageTimer = 0;

        healOverlay.color = new Color(
            healOverlay.color.r,
            healOverlay.color.g,
            healOverlay.color.b,
            1
        ); //set opacity to 1
    }
}

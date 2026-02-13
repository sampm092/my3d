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
    public float lerpTimer;
    public Image BgHB;
    public Image FrontHB;
    public TextMeshProUGUI HealthText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        health = MaxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth);
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(Random.Range(5, 10));
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = FrontHB.fillAmount;
        float fillB = BgHB.fillAmount;
        float hFraction = health / MaxHealth;

        if (fillB > hFraction)
        {
            FrontHB.fillAmount = hFraction;
            BgHB.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / ChipSpeed;
            BgHB.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
}

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
    public Image backHB;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fillHP(Random.Range(5, 10));
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = FrontHB.fillAmount;
        float fillB = backHB.fillAmount;
        float hFraction = health / MaxHealth;

        if (fillB > hFraction)
        {
            FrontHB.fillAmount = hFraction;
            backHB.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / ChipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHB.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
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

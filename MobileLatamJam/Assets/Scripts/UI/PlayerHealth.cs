using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthGFX;
    public Sprite fullHealthGFX;
    public Sprite damagedHealthGFX;
    public Sprite criticalHealthGFX;
    public Sprite noHealthGFX;

    public int MaxHealth = 3;
    public int currentHealth = 3;

    public void Damage(int amountDamaged)
    {
        currentHealth -= amountDamaged;
        UpdateVisualHealth();
    }

    public void Heal(int amountHealed)
    {
        currentHealth += amountHealed;
        if(currentHealth>MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        UpdateVisualHealth();
    }

    void UpdateVisualHealth()
    {     
        switch(currentHealth)
        {
            case 3:
                healthGFX.sprite = fullHealthGFX;
                break;
            case 2:
                healthGFX.sprite = damagedHealthGFX;
                break;
            case 1:
                healthGFX.sprite = criticalHealthGFX;
                break;
            case 0:
                healthGFX.sprite = noHealthGFX;
                break;
        }   
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
  
public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    public int MaxHealth = 3;
    public int currentHealth = 3;

    public void Damage(int amountDamaged)
    {
        currentHealth -= amountDamaged;
        if (currentHealth<= 0)
        {
            //die
            Destroy(enemy);
        }
    }

    public void Heal(int amountHealed)
    {
        currentHealth += amountHealed;
        if(currentHealth>MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }
}

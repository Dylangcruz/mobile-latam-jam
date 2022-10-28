using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int bombDaamge;

    [SerializeField] private HealthController _healthController;

    private void OnTriggerEnter2d(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Damage();
        }
    }

    public void Damage()
    {
        _healthController.playerHealth = _healthController.playerHealth - bombDaamge;
        _healthController.UpdateHealth();
        gameObject.SetActive(false);
    }

}

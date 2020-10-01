using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Projectile"))
        {
            GameObject ballToReturn = col.transform.parent.gameObject;
            TakeDamage(ballToReturn.GetComponent<Ball>().baseDamage);
            BallPooler.Instance.ReturnToPool(ballToReturn);
        }
    }

    public void TestDmg(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TakeDamage(10);
        }        
    }
}

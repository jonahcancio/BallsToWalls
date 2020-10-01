using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    public UnityEvent<int> PlayerDie;
    public int playerIndex;

    [SerializeField]
    private bool isDying;

    void Start()
    {
        isDying = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !isDying)
        {
            isDying = true;
            PlayerDie?.Invoke(playerIndex);
        }
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


}

using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float CurrentHealth { get; private set; }
    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    void Awake() { CurrentHealth = maxHealth; }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Max(0, CurrentHealth - Mathf.Abs(amount));
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        if (CurrentHealth <= 0)
        {
            OnDied?.Invoke();
            Destroy(gameObject, 1.5f);
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + Mathf.Abs(amount));
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}

using UnityEngine;

public class LootPickup : MonoBehaviour
{
    public float healAmount = 25f;

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            health.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}

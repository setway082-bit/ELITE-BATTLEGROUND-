using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotAI : MonoBehaviour
{
    public Transform target;
    public float detectRange = 35f;
    public float moveSpeed = 3.2f;
    public float attackRange = 18f;
    public float damage = 8f;
    public float attackInterval = 1.2f;
    CharacterController controller;
    float nextAttack;

    void Awake() { controller = GetComponent<CharacterController>(); }

    void Update()
    {
        if (!target) return;
        Vector3 delta = target.position - transform.position;
        float distance = delta.magnitude;
        if (distance > detectRange) return;

        Vector3 flat = new Vector3(delta.x, 0, delta.z);
        if (flat.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(flat), 6f * Time.deltaTime);

        if (distance > attackRange)
            controller.Move(flat.normalized * moveSpeed * Time.deltaTime);
        else if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackInterval;
            Health playerHealth = target.GetComponent<Health>();
            if (playerHealth) playerHealth.TakeDamage(damage);
        }
    }
}

using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public float startRadius = 100f;
    public float endRadius = 8f;
    public float shrinkDuration = 300f;
    public float damageOutsidePerSecond = 5f;
    public Transform player;
    float elapsed;
    float damageTimer;

    public float CurrentRadius { get; private set; }

    void Start() { CurrentRadius = startRadius; }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / Mathf.Max(1f, shrinkDuration));
        CurrentRadius = Mathf.Lerp(startRadius, endRadius, t);

        if (player)
        {
            Vector3 p = player.position; p.y = transform.position.y;
            if (Vector3.Distance(p, transform.position) > CurrentRadius)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= 1f)
                {
                    Health h = player.GetComponent<Health>();
                    if (h) h.TakeDamage(damageOutsidePerSecond);
                    damageTimer = 0;
                }
            }
            else damageTimer = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Application.isPlaying ? CurrentRadius : startRadius);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BattleRoyaleGameManager : MonoBehaviour
{
    public Text statusText;
    public Health playerHealth;
    public int aliveBots = 0;

    void Update()
    {
        if (!statusText) return;
        string hp = playerHealth ? Mathf.CeilToInt(playerHealth.CurrentHealth).ToString() : "--";
        statusText.text = "HEALTH  " + hp + "     ENEMIES  " + aliveBots;
    }

    public void SetAliveBots(int count) { aliveBots = Mathf.Max(0, count); }
}

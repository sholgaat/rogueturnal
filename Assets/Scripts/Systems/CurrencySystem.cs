using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public int Aetherion = 0;        // normal currency
    public int ElysiumShards = 0;    // premium currency

    public void AddAetherion(int amount)
    {
        Aetherion += amount;
        Debug.Log("Aetherion: " + Aetherion);
    }

    public void AddElysiumShards(int amount)
    {
        ElysiumShards += amount;
        Debug.Log("Elysium Shards: " + ElysiumShards);
    }
}

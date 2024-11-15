using UnityEngine;


public class EnemyDeathTracker : IEnemyDeathTracker
{
    private int deaths = 0;

    public int GetDeathCount() => deaths;

    public void IncrementDeaths()
    {
        deaths++;
    }
}
using UnityEngine;

public interface IEnemyDeathTracker
{
    int GetDeathCount();
    void IncrementDeaths();
}
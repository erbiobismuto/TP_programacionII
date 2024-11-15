using UnityEngine;


public class ProgressionChecker : IProgressionChecker
{
    private readonly Progresion progresion;

    public ProgressionChecker(Progresion progresion)
    {
        this.progresion = progresion;
    }

    public void CheckProgress(int deathCount, int enemyCount)
    {
        if (enemyCount <= deathCount)
        {
            progresion.SubirNivel();
            Debug.Log("You win ");
        }
    }
}
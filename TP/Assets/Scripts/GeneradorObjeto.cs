using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneradorObjeto : MonoBehaviour
{
    [SerializeField] private GameObject[] objetoPrefabs;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private List<string> toDelete = new List<string> { "0", "1", "Spike","Platform", "PS", "Finish" };
    [SerializeField] private float bossSpawnDuration = 15f;
    [SerializeField] private float bossSpawnInterval = 1f;

    private int bossCount = 0;

    private Vector2 spawnPosition1 = new Vector2(-19.9899998f, -1.01999998f);
    private Vector2 spawnPosition2 = new Vector2(14.0600004f, -1.04999995f);

    public void RespawnEnemyAt(Vector3 position, string index)
    {
        StartCoroutine(RespawnCoroutine(position, System.Convert.ToInt32(index)));
    }

    private IEnumerator RespawnCoroutine(Vector3 position, int index)
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy(position, index);
    }

    private void SpawnEnemy(Vector3 position, int index)
    {
        if (objetoPrefabs.Length > 0 && index < objetoPrefabs.Length)
        {
            Instantiate(objetoPrefabs[index], position, Quaternion.identity);
        }
    }

    public void SpawnBoss()
    {
        // Destroy all game objects except the player and essential objects
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (ShouldDeleteObject(obj))
            {
                ParticleSystem ps = obj.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    Destroy(obj, ps.main.duration + ps.main.startLifetime.constantMax);
                }
                else
                {
                    Destroy(obj);
                }
            }
        }

        StartCoroutine(SpawnBosses());
    }

    private IEnumerator SpawnBosses()
    {
        float elapsedTime = 0f;
        bool useFirstPosition = true;

        while (elapsedTime < bossSpawnDuration)
        {
            Vector2 spawnPosition = useFirstPosition ? spawnPosition1 : spawnPosition2;

            if (bossPrefab != null)
            {
                GameObject bossInstance = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
                BossMovement bossMovement = bossInstance.GetComponent<BossMovement>();
                bossMovement.Initialize();
                bossCount++;
            }
            else
            {
                Debug.LogError("Boss prefab is not assigned in the GeneradorObjeto script!");
            }

            useFirstPosition = !useFirstPosition;
            elapsedTime += bossSpawnInterval;
            yield return new WaitForSeconds(bossSpawnInterval);
        }
    }

    private bool ShouldDeleteObject(GameObject obj)
    {
        return toDelete.Contains(obj.tag);
    }

    public int GetBossesCount()
    {
        return bossCount;
    }
}
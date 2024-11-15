using UnityEngine;
using UnityEngine.Tilemaps;

public class CloudSpawner : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public float spawnInterval = 2f; 
    public float speed = 2f; 
    public float fadeSpeed = 1f; // Speed of transparency fade
    public float despawnOffset = 2f; 
    private Bounds baseBounds; 

    private void Start()
    {
        GameObject baseObject = GameObject.FindGameObjectWithTag("base");
        if (baseObject != null)
        {
            Tilemap tilemap = baseObject.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                baseBounds = tilemap.localBounds;
                baseBounds.center = baseObject.transform.position + baseBounds.center;
            }
        }

        InvokeRepeating(nameof(SpawnCloud), 0f, spawnInterval);
    }

    private void SpawnCloud()
    {
        GameObject cloud = objectPooler.GetPooledObject();
        if (cloud != null)
        {
            float randomY = Random.Range(baseBounds.min.y, baseBounds.max.y);
            float startX = baseBounds.max.x + despawnOffset; 
            cloud.transform.position = new Vector3(startX, randomY, 0f);

            SpriteRenderer spriteRenderer = cloud.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 1f; 
                spriteRenderer.color = color;
            }

            cloud.SetActive(true);
        }
    }

    private void Update()
    {
        foreach (GameObject cloud in objectPooler.objectPool)
        {
            if (cloud.activeInHierarchy)
            {
                cloud.transform.position += Vector3.left * speed * Time.deltaTime;

                SpriteRenderer spriteRenderer = cloud.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    float fadeFactor = Mathf.Clamp01((cloud.transform.position.x - baseBounds.min.x) / despawnOffset);
                    Color color = spriteRenderer.color;
                    color.a = fadeFactor; 
                    spriteRenderer.color = color;

                    if (fadeFactor <= 0f)
                    {
                        cloud.SetActive(false);
                    }
                }
            }
        }
    }
}

using UnityEngine;

public class BHerir : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float damagePoints = 5f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private Progresion progresion;

    private GeneradorObjeto generadorObjeto;
    private IEnemyDeathTracker deathTracker;
    private ICollisionHandler collisionHandler;
    private IProgressionChecker progressionChecker;
    private int count;

    private void Start()
    {
        progresion = FindObjectOfType<Progresion>();
        generadorObjeto = FindObjectOfType<GeneradorObjeto>();
        
        // Initialize helper classes
        deathTracker = new EnemyDeathTracker();
        collisionHandler = new CollisionHandler(damagePoints, jumpForce, deathTracker);
        progressionChecker = new ProgressionChecker(progresion);
    }

    private void Update()
    {
        count = generadorObjeto.GetBossesCount();
        progressionChecker.CheckProgress(deathTracker.GetDeathCount(), count);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionHandler.HandleCollision(collision, gameObject);
    }
}
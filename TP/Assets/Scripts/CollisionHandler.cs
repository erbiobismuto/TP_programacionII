using UnityEngine;


public class CollisionHandler : ICollisionHandler
{
    private readonly float damagePoints;
    private readonly float jumpForce;
    private readonly IEnemyDeathTracker deathTracker;

    public CollisionHandler(float damagePoints, float jumpForce, IEnemyDeathTracker deathTracker)
    {
        this.damagePoints = damagePoints;
        this.jumpForce = jumpForce;
        this.deathTracker = deathTracker;
    }

    public void HandleCollision(Collision2D collision, GameObject enemyObject)
    {
        Jugador jugador = collision.gameObject.GetComponent<Jugador>();
        if (jugador == null) return;

        Rigidbody2D playerRb = jugador.GetComponent<Rigidbody2D>();
        
        if (IsCollidingFromAbove(collision))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Object.Destroy(enemyObject);
            deathTracker.IncrementDeaths();
        }
        else
        {
            jugador.ModificarVida(-damagePoints);
        }
    }

    private bool IsCollidingFromAbove(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                return true;
            }
        }
        return false;
    }
}
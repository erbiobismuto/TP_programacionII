using UnityEngine;

public class Herir : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] float damagePoints = 5f;
    [SerializeField] float jumpForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jugador jugador = collision.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            Rigidbody2D playerRb = jugador.GetComponent<Rigidbody2D>();
            if (IsCollidingFromAbove(collision))
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                NotifySpawnerAndDestroy();
            }
            else
            {
                jugador.ModificarVida(-damagePoints);
            }

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

    private void NotifySpawnerAndDestroy()
    {
        GeneradorObjeto spawner = FindObjectOfType<GeneradorObjeto>();
        if (spawner != null)
        {
            spawner.RespawnEnemyAt(transform.position, gameObject.tag);
        }
        Destroy(gameObject);
    }
}
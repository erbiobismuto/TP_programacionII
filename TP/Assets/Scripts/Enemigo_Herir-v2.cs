using UnityEngine;

public class Herir : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] float damagePoints = 5f;
    [SerializeField] float jumpForce = 10f; // New variable for jump force
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            Rigidbody2D playerRb = jugador.GetComponent<Rigidbody2D>();

            // Check if the collision is from above
            if (IsCollidingFromAbove(collision))
            {
                // Apply upward force to the player (jump)
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Player jumped on enemy!");
            }
            else
            {
                // If not from above, apply damage as before
                jugador.ModificarVida(-damagePoints);
                Debug.Log("Damage dealt to player: " + damagePoints);
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
}
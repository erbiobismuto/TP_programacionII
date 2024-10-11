using UnityEngine;

public class BHerir : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] float damagePoints = 5f;
    [SerializeField] float jumpForce = 1.0f;

    private GeneradorObjeto generadorObjeto;
    private int count;
    private int deaths = 0;

    private void Start()
    {
        generadorObjeto = FindObjectOfType<GeneradorObjeto>();
    }
    
    private void Update() 
    {
        count = generadorObjeto.GetBossesCount();
        if(count <= deaths)
        {
             Debug.Log(" You win ");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jugador jugador = collision.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            Rigidbody2D playerRb = jugador.GetComponent<Rigidbody2D>();
            if (IsCollidingFromAbove(collision))
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Destroy(gameObject);
                deaths++;
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

}
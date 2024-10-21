using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> colleccionables;
	[SerializeField] private Jugador player; 
    [SerializeField] private Progresion progresion;
	private KeyCode eatButton = KeyCode.E;
    private int itemsToCollect;
    private int collected;

    void Start()
    {
        progresion = FindObjectOfType<Progresion>();
        colleccionables = new Queue<GameObject>();
        if(progresion.GetLevel() == 1)
        {
            itemsToCollect = 3;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(eatButton) && colleccionables.Count > 0)
        {
            EatCollectable();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            GameObject nuevoColeccionable = collision.gameObject;
            nuevoColeccionable.SetActive(false);
            colleccionables.Enqueue(nuevoColeccionable);
            collected++;
             // Check if all colleccionables have been collected
            if (collected >= itemsToCollect)
            {
                // Add experience
                int experienciaGanada = 5;  
                progresion.GanarExperiencia(experienciaGanada);
                Debug.Log("Ganaste Experiencia.");
            }
        }
    }

    private void EatCollectable()
    {
        if (colleccionables.Count > 0)
        {
            GameObject collectable = colleccionables.Dequeue();
            Destroy(collectable);

            // Add one life to the player
            player.ModificarVida(1f);
		    Debug.Log("Comiste un coleccionable, una vida ganada");
        }
    }
}

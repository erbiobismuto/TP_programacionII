using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 5f;
    private float maxLives;
    // Reference to the PerfilJugador ScriptableObject
    [SerializeField] private Progresion progresion;

    void Start()
    {
        progresion = GetComponent<Progresion>();
        maxLives = vida + progresion.GetExperiencia();
    }

    public void ModificarVida(float puntos)
    {
        vida += puntos;
        if (vida > maxLives)
        {
            vida = maxLives;  // Cap vida at maxLives
        }
        Debug.Log(EstasVivo());
    }

    private bool EstasVivo()
    {
        return vida > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            NotifySpawner();
        }
    }

    private void NotifySpawner()
    {
        GeneradorObjeto spawner = FindObjectOfType<GeneradorObjeto>();
        if (spawner != null)
        {
            spawner.SpawnBoss();
        }
    }
}

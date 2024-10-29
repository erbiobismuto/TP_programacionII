using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 3f;
    private float maxLives;
    // Reference to the PerfilJugador ScriptableObject
    [SerializeField] private Progresion progresion;

    
    [Header("Events")]
    [SerializeField] private UnityEvent<float> onVidasCambiadas = new UnityEvent<float>();
   
    private HUDController hudController;

    public UnityEvent<float> OnVidasCambiadas => onVidasCambiadas;

    void Start()
    {
        progresion = GetComponent<Progresion>();
        maxLives = vida + progresion.GetExperiencia();
        
        hudController = FindObjectOfType<HUDController>();
        
        if (hudController != null)
        {
            onVidasCambiadas.AddListener(hudController.ActualizarVidasHUD);
        }

        onVidasCambiadas?.Invoke(vida);
    }

    public void ModificarVida(float puntos)
    {
        vida += puntos;
        if (vida > maxLives)
        {
            vida = maxLives;  // Cap vida at maxLives
        }

        onVidasCambiadas?.Invoke(vida);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltar : MonoBehaviour
{

    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] private float fuerzaSalto = 5f;

    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip collisionSFX;

    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    private bool saltando = false;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private Animator miAnimator;
    private AudioSource miAudioSource;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miAudioSource = GetComponent<AudioSource>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            puedoSaltar = false;
            if (miAudioSource.isPlaying) { return; }
            miAudioSource.PlayOneShot(jumpSFX);
        }
        miAnimator.SetBool("EnAire", saltando);
    }

    private void FixedUpdate()
    {
        if (!puedoSaltar && !saltando)
        {
            miRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltando = true;
        }
    }

    // Codigo ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        puedoSaltar = true;
        saltando = false;
        if(miAudioSource.isPlaying) { return; }
        miAudioSource.PlayOneShot(collisionSFX);
    }

}
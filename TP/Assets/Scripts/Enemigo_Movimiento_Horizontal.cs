using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 2f;  // Adjusted speed for subtle movement
    [SerializeField] float distanciaMovimiento = 1f;  // Reduced movement distance for subtle effect

    private Vector2 posicionInicial;
    private Vector2 posicionDestino;
    private bool moviendoseDerecha = true;
    private bool regresando = false;

    private Rigidbody2D miRigidbody2D;

    private void Awake()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
        CalcularSiguienteDestino();
    }

    private void FixedUpdate()
    {
        Vector2 direccion = (posicionDestino - miRigidbody2D.position).normalized;
        miRigidbody2D.MovePosition(miRigidbody2D.position + direccion * (velocidad * Time.fixedDeltaTime));

        if (Vector2.Distance(miRigidbody2D.position, posicionDestino) < 0.1f)
        {
            CalcularSiguienteDestino();
        }
    }

    private void CalcularSiguienteDestino()
    {
        if (regresando)
        {
            posicionDestino = posicionInicial;
            regresando = false;
            moviendoseDerecha = !moviendoseDerecha;
        }
        else
        {
            if (moviendoseDerecha)
            {
                posicionDestino = posicionInicial + Vector2.right * distanciaMovimiento;
            }
            else
            {
                posicionDestino = posicionInicial + Vector2.left * distanciaMovimiento;
            }
            regresando = true;
        }
    }
}

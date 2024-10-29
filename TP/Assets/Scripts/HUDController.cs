using UnityEngine;
using UnityEngine.UI;  
public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject iconoVida;    // The heart icon prefab
    [SerializeField] private Transform contenedorIconosVida; // The container for heart icons
    
    [Header("Heart Icon Settings")]
    [SerializeField] private Vector2 heartSpacing = new Vector2(30f, 0f); // Space between hearts
    [SerializeField] private Vector2 firstHeartPosition = new Vector2(15f, 0f); // Position of first heart

    private void Awake()
    {
        // Ensure we have necessary components
        if (iconoVida == null)
            Debug.LogError("Heart icon prefab not assigned to HUDController!");
        if (contenedorIconosVida == null)
            Debug.LogError("Heart container not assigned to HUDController!");
    }

    public void ActualizarVidasHUD(float vidas)
    {
        if(vidas < 0) return;
        
        if(EstaVacioContenedor())
        {
            CargarContenedor(vidas);
            return;
        }
        
        if(CantidadIconosVidas() > vidas)
        {
            EliminarUltimoIcono();
        }
        else if(CantidadIconosVidas() < vidas)
        {
            CrearIcono();
        }

        // Update positions of all hearts
        ActualizarPosicionesCorazones();
    }

    private void ActualizarPosicionesCorazones()
    {
        for (int i = 0; i < contenedorIconosVida.childCount; i++)
        {
            RectTransform heartRT = contenedorIconosVida.GetChild(i).GetComponent<RectTransform>();
            if (heartRT != null)
            {
                // Calculate position based on index
                Vector2 position = firstHeartPosition + (heartSpacing * i);
                heartRT.anchoredPosition = position;
            }
        }
    }
    private bool EstaVacioContenedor()
    {
        return contenedorIconosVida.transform.childCount == 0;
    }
    
    private int CantidadIconosVidas()
    {
        return contenedorIconosVida.transform.childCount;
    }
    
    private void EliminarUltimoIcono()
    {
        Transform contenedor = contenedorIconosVida.transform;
        GameObject.Destroy(contenedor.GetChild(contenedor.childCount - 1).gameObject);
    }
    
    private void CargarContenedor(float cantidadIconos)
    {
        int cantidad = Mathf.RoundToInt(cantidadIconos);  
        for(int i = 0; i < cantidad; i++)
        {
            CrearIcono();
        }
    }
    
    private void CrearIcono()
    {
        GameObject.Instantiate(iconoVida, contenedorIconosVida.transform);  
        
    }

}
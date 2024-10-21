using System.Collections.Generic;
using UnityEngine;

public class Progresion : MonoBehaviour
{
	[SerializeField] private PerfilJugador perfilJugador;
	
	public void SubirNivel()
    {
        perfilJugador.Nivel++;
    }
	
	public void GanarExperiencia(int nuevaExperiencia)
    {
        perfilJugador.Experiencia+= nuevaExperiencia;
    }

    public int GetLevel()
    {
        return perfilJugador.Nivel;
    }

    public int GetExperiencia()
    {
        return perfilJugador.Experiencia;
    }

}

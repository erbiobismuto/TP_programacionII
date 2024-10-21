using UnityEngine;

[CreateAssetMenu(fileName = "PerfilJugador", menuName = "SO/PerfilJugador")]
public class PerfilJugador : ScriptableObject
{
    [SerializeField] private int nivel = 1;
    public int Nivel { get => nivel; set => nivel = value; }

    [SerializeField] private int experiencia = 1;
    public int Experiencia { get => experiencia; set => experiencia = value; }
}
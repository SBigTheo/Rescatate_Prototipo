using System;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int VidaActual { get; private set; }

    public event Action<int, int> OnVidaCambiada;

    void Start()
    {
        VidaActual = vidaMaxima;
        OnVidaCambiada?.Invoke(VidaActual, vidaMaxima);
    }

    public void RecibirDano(int cantidad)
    {
        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
        Debug.Log("Vida: " + VidaActual);

        OnVidaCambiada?.Invoke(VidaActual, vidaMaxima);

        if (VidaActual <= 0)
            Morir();
    }

    void Morir()
    {
        FindFirstObjectByType<Cronometro>().Perder();
    }
}
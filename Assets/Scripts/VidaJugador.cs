using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int VidaActual { get; private set; }

    void Start()
    {
        VidaActual = vidaMaxima;
    }

    public void RecibirDano(int cantidad)
    {
        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
        Debug.Log("Vida: " + VidaActual);

        if (VidaActual <= 0)
            Morir();
    }

    void Morir()
    {
        FindFirstObjectByType<Cronometro>().Perder();
    }
}
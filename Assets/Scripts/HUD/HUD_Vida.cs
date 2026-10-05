using UnityEngine;
using UnityEngine.UI;

public class HUD_Vida : MonoBehaviour
{
    [Header("Referencias")]
    public VidaJugador jugador;
    public Image barraRelleno;

    void Start()
    {
        if (jugador != null)
            jugador.OnVidaCambiada += ActualizarBarra;

        if (jugador != null)
            ActualizarBarra(jugador.VidaActual, jugador.vidaMaxima);
    }

    void OnDestroy()
    {
        if (jugador != null)
            jugador.OnVidaCambiada -= ActualizarBarra;
    }

    void ActualizarBarra(int vidaActual, int vidaMaxima)
    {
        float porcentaje = (float)vidaActual / vidaMaxima;
        barraRelleno.fillAmount = porcentaje;
    }
}
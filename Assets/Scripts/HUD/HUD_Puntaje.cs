using UnityEngine;
using TMPro;

public class HUD_Puntaje : MonoBehaviour
{
    [Header("Referencias a los textos")]
    public TextMeshProUGUI textoPuntajeActual;
    public TextMeshProUGUI textoPuntajeMaximo;

    void OnEnable()
    {
        EventosPuntaje.OnPuntajeCambiado += ActualizarTextos;
    }

    void OnDisable()
    {
        EventosPuntaje.OnPuntajeCambiado -= ActualizarTextos;
    }

    private void ActualizarTextos(int puntajeActual, int puntajeMaximo)
    {
        if (textoPuntajeActual != null)
            textoPuntajeActual.text = puntajeActual.ToString();

        if (textoPuntajeMaximo != null)
            textoPuntajeMaximo.text = puntajeMaximo.ToString();
    }
}
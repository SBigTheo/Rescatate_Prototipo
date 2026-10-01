using UnityEngine;

public class SistemaPuntaje : MonoBehaviour
{
    private const string CLAVE_HIGHSCORE = "HighScore";

    private int puntajeActual = 0;
    private int puntajeMaximo = 0;

    void OnEnable()
    {
        EventosObjetos.OnObjetoRecogido += AlRecogerObjeto;
    }

    void OnDisable()
    {
        EventosObjetos.OnObjetoRecogido -= AlRecogerObjeto;
    }

    void Start()
    {
        // high score guardado en disco
        puntajeMaximo = PlayerPrefs.GetInt(CLAVE_HIGHSCORE, 0);

        puntajeActual = 0;

        EventosPuntaje.OnPuntajeCambiado?.Invoke(puntajeActual, puntajeMaximo);
    }

    private void AlRecogerObjeto(TipoObjeto tipo, int puntos)
    {
        puntajeActual += puntos;
        
        // solo guardamos si el puntaje es mayor
        if (puntajeActual > puntajeMaximo)
        {
            puntajeMaximo = puntajeActual;
            PlayerPrefs.SetInt(CLAVE_HIGHSCORE, puntajeMaximo);
            PlayerPrefs.Save();
        }

        EventosPuntaje.OnPuntajeCambiado?.Invoke(puntajeActual, puntajeMaximo);

        Debug.Log($"Recogido {tipo} ({puntos}). Actual: {puntajeActual}, Máximo: {puntajeMaximo}");
    }

    // metodos publicos por si otra rama necesita leer
    public int GetPuntajeActual() => puntajeActual;
    public int GetPuntajeMaximo() => puntajeMaximo;
}
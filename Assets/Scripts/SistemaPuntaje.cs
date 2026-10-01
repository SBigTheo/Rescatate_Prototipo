using UnityEngine;

public class SistemaPuntaje : MonoBehaviour
{
    private const string CLAVE_HIGHSCORE = "HighScore";

    //  true para habilitar el reinicio del high score con la tecla Borrar.
    //  false para deshabilitarlo (build final).
    private const bool RESET_HIGHSCORE_HABILITADO = true;

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

    void Update()
    {
        if (RESET_HIGHSCORE_HABILITADO && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarHighScore();
        }
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

        //Debug.Log($"Recogido {tipo} ({puntos}). Actual: {puntajeActual}, Máximo: {puntajeMaximo}");
    }

    private void ReiniciarHighScore()
    {
        puntajeMaximo = 0;
        PlayerPrefs.SetInt(CLAVE_HIGHSCORE, 0);
        PlayerPrefs.Save();

        EventosPuntaje.OnPuntajeCambiado?.Invoke(puntajeActual, puntajeMaximo);

        Debug.Log("High score reiniciado a 0.");
    }

    // metodos publicos por si otra rama necesita leer
    public int GetPuntajeActual() => puntajeActual;
    public int GetPuntajeMaximo() => puntajeMaximo;
}
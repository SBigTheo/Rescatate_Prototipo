using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaGameOver : MonoBehaviour
{
    [Header("Referencias")]
    public VidaJugador jugador;
    public GameObject panelGameOver;

    void Start()
    {
        panelGameOver.SetActive(false);

        if (jugador != null)
            jugador.OnVidaCambiada += ChequearGameOver;
    }

    void OnDestroy()
    {
        if (jugador != null)
            jugador.OnVidaCambiada -= ChequearGameOver;
    }

    void ChequearGameOver(int vidaActual, int vidaMaxima)
    {
        if (vidaActual <= 0)
            MostrarGameOver();
    }

    void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
    }

    void Update()
    {
        if (panelGameOver.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
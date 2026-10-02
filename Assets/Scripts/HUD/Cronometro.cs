using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public float duracionPartida = 60f;
    public float TiempoRestante { get; private set; }
    public bool Terminada { get; private set; }

    // en 0 empieza y es 1 al llegar a los 60 seg
    public float Progreso => 1f - (TiempoRestante / duracionPartida);

    void Start()
    {
        TiempoRestante = duracionPartida;
    }

    void Update()
    {
        if (Terminada) return;

        TiempoRestante -= Time.deltaTime;

        if (textoTiempo != null)
            textoTiempo.text = Mathf.CeilToInt(Mathf.Max(TiempoRestante, 0)).ToString();

        if (TiempoRestante <= 0f)
            Ganar();
    }

    void Ganar()
    {
        Terminada = true;
        Debug.Log("¡Ganaste!");
    }

    public void Perder()
    {
        if (Terminada) return;
        Terminada = true;
        Debug.Log("Perdiste");
    }
}
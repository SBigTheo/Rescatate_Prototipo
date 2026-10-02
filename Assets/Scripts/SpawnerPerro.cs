using UnityEngine;

public class SpawnerPerro : MonoBehaviour
{
    public Cronometro cronometro;
    public GameObject alertaPrefab;       // la alerta (ella suelta al perro)
    public Transform[] puntos;            // lugares posibles de aparición

    public float intervaloInicial = 5f;   // al principio aparece cada 5 seg
    public float intervaloFinal = 1.5f;   // al final aparece cada 1.5 seg
    public int maxPerros = 3;             // máximo de perros por tanda al final

    private float proximo;

    void Start()
    {
        proximo = intervaloInicial;
    }

    void Update()
    {
        if (cronometro.Terminada) return;

        proximo -= Time.deltaTime;
        if (proximo > 0f) return;

        // Cuántos perros salen en esta tanda: de 1 a maxPerros según el avance
        int cantidad = Mathf.Min(1 + Mathf.FloorToInt(cronometro.Progreso * maxPerros), maxPerros);

        for (int i = 0; i < cantidad; i++)
        {
            Transform punto = puntos[Random.Range(0, puntos.Length)];
            Instantiate(alertaPrefab, punto.position, Quaternion.identity);
        }

        // El intervalo se acorta a medida que avanza el tiempo
        proximo = Mathf.Lerp(intervaloInicial, intervaloFinal, cronometro.Progreso);
    }
}
using UnityEngine;

public class SpawnerPerro : MonoBehaviour
{
    public Cronometro cronometro;
    public GameObject alertaPrefab;
    public Transform[] puntos;

    public float intervaloInicial = 5f; 
    public float intervaloFinal = 2f;
    [HideInInspector] public GameObject perroActual;

    private GameObject alertaActual;
    private float proximo;

    void Start()
    {
        proximo = intervaloInicial;

        if (puntos == null || puntos.Length == 0)
        {
            Debug.LogError("SpawnerPerro: no hay puntos asignados", this);
            enabled = false;
        }
    }

    void Update()
    {
        if (cronometro.Terminada) return;

       
        if (alertaActual != null || perroActual != null) return;

        proximo -= Time.deltaTime;
        if (proximo > 0f) return;

       
        Transform punto = puntos[Random.Range(0, puntos.Length)];
        alertaActual = Instantiate(alertaPrefab, punto.position, Quaternion.identity);
        alertaActual.GetComponent<AlertaPerro>().spawner = this;

        proximo = Mathf.Lerp(intervaloInicial, intervaloFinal, cronometro.Progreso);
    }
}
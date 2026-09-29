using System.Collections;
using UnityEngine;

public class ObjetoSpawner : MonoBehaviour
{
    [Header("Prefabs (3 buenos, 2 malos)")]
    public GameObject[] prefabsBuenos;
    public GameObject[] prefabsMalos;

    [Header("Probabilidades")]
    [Range(0f, 1f)] public float probBueno = 0.45f;
    [Range(0f, 1f)] public float probMaloInicial = 0.25f;
    [Range(0f, 1f)] public float probMaloMax = 0.55f;
    public float incrementoMalo = 0.0375f;
    public float intervaloProgresoMalo = 8f;

    [Header("Timing de spawn")]
    public float intervaloSpawnInicial = 2.5f;
    public float intervaloSpawnMinimo = 1.3f;
    public float reduccionIntervalo = 0.1f;
    public float intervaloReduccion = 15f;

    [Header("Zona de aparición")]
    public float xMin = -9f;
    public float xMax = 9f;
    public float yFijo = 7f;

    [Header("Ráfaga")]
    public int rafagaMin = 2;
    public int rafagaMax = 5;

    [Header("Activación")]
    public float retardoInicial = 3f;

    private float probMaloActual;
    private float intervaloActual;

    void Start()
    {
        probMaloActual = probMaloInicial;
        intervaloActual = intervaloSpawnInicial;
        StartCoroutine(RutinaPrincipal());
    }

    IEnumerator RutinaPrincipal()
    {
        // retardo inicial
        yield return new WaitForSeconds(retardoInicial);

        // temporizadores de progresión
        StartCoroutine(ProgresionMalo());
        StartCoroutine(ProgresionIntervalo());

        while (true)
        {
            yield return StartCoroutine(SpawnearRafaga());
        }
    }

    IEnumerator ProgresionMalo()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloProgresoMalo);
            probMaloActual = Mathf.Min(probMaloActual + incrementoMalo, probMaloMax);
        }
    }

    IEnumerator ProgresionIntervalo()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloReduccion);
            intervaloActual = Mathf.Max(intervaloActual - reduccionIntervalo, intervaloSpawnMinimo);
        }
    }

    IEnumerator SpawnearRafaga()
    {
        int cantidad = Random.Range(rafagaMin, rafagaMax + 1);
        float desfase = intervaloActual / cantidad;

        for (int i = 0; i < cantidad; i++)
        {
            SpawnearUnObjeto();
            yield return new WaitForSeconds(desfase);
        }
    }

    void SpawnearUnObjeto()
    {
        float r = Random.value;
        GameObject[] lista;

        if (r < probMaloActual)
        {
            lista = prefabsMalos;
        }
        else
        {
            // 45% "bueno" y el "nada" reemplazados por bueno hasta que "malo" llegue a su 55%
            lista = prefabsBuenos;
        }

        if (lista == null || lista.Length == 0) return;

        GameObject prefab = lista[Random.Range(0, lista.Length)];
        float x = Random.Range(xMin, xMax);
        Vector3 posicion = new Vector3(x, yFijo, 0f);

        Instantiate(prefab, posicion, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        //línea de aparición en la escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(xMin, yFijo, 0f), new Vector3(xMax, yFijo, 0f));
        Gizmos.DrawWireSphere(new Vector3(xMin, yFijo, 0f), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(xMax, yFijo, 0f), 0.2f);
    }
}
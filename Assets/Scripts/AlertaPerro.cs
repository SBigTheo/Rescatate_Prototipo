using UnityEngine;

public class AlertaPerro : MonoBehaviour
{
    public float duracion = 2.5f;
    public GameObject perroPrefab;
    [HideInInspector] public SpawnerPerro spawner;  

    void Start()
    {
        Invoke(nameof(SoltarPerro), duracion);
    }

    void SoltarPerro()
    {
        GameObject perro = Instantiate(perroPrefab, transform.position, Quaternion.identity);

        if (spawner != null)
            spawner.perroActual = perro;

        Destroy(gameObject);
    }
}
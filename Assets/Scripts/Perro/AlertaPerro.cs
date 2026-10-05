using UnityEngine;

public class AlertaPerro : MonoBehaviour
{
    public float duracion = 2.5f;
    public AudioSource audioSource;
    public AudioClip alerta;
    public GameObject perroPrefab;
    [HideInInspector] public SpawnerPerro spawner;  

    void Start()
    {
        playAlerta();
        Invoke(nameof(SoltarPerro), duracion);
    }

    public void playAlerta()
    {
        audioSource.PlayOneShot(alerta);
    }

    void SoltarPerro()
    {
        GameObject perro = Instantiate(perroPrefab, transform.position, Quaternion.identity);

        if (spawner != null)
            spawner.perroActual = perro;

        Destroy(gameObject);
    }
}
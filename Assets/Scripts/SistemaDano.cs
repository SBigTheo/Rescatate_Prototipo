using UnityEngine;

public class SistemaDano : MonoBehaviour
{
    public int cantidadDano = 1;
    public string tagObjetivo = "Player";
    public float cooldown = 1f;   

    private float ultimoGolpe = -999f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Aplicar(col.gameObject);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Aplicar(col.gameObject);
    }

    void Aplicar(GameObject objetivo)
    {
        if (!objetivo.CompareTag(tagObjetivo)) return;
        if (Time.time < ultimoGolpe + cooldown) return;

        VidaJugador vida = objetivo.GetComponent<VidaJugador>();
        if (vida == null) return;

        vida.RecibirDano(cantidadDano);
        ultimoGolpe = Time.time;

    }
}
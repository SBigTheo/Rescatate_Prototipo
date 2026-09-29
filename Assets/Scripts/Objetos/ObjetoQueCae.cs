using UnityEngine;

public enum TipoObjeto
{
    Bueno,
    Malo
}

public class ObjetoQueCae : MonoBehaviour
{
    [Header("Configuración")]
    public TipoObjeto tipo = TipoObjeto.Bueno;

    [Header("Detección de suelo")]
    public LayerMask groundLayer;

    [Header("Movimiento")]
    public float fallSpeed = 3f;

    private bool yaDestruido = false;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (yaDestruido) return;

        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            Destruir();
            return;
        }

        if (other.CompareTag("Player"))
        {
            EventosObjetos.OnObjetoRecogido?.Invoke(tipo);
            Destruir();
        }
    }

    private void Destruir()
    {
        yaDestruido = true;
        Destroy(gameObject);
    }
}
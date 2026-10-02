using UnityEngine;

public class PerroController : MonoBehaviour
{
    public float velocidad = 2f;
    public float distancia = 20;
    private float posicionInicial;
    private int direccion = 1;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.posicionInicial = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimiento();
    }

    void Movimiento() 
    {
        if (transform.position.x >= posicionInicial + distancia)
        
            direccion = -1;

            else if (transform.position.x <= posicionInicial + direccion)
                direccion = 1;

        rb.linearVelocity = new Vector2( direccion * velocidad, rb.linearVelocity.y);

        transform.localScale = new Vector3(direccion, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Impacto");
        }
    }
}

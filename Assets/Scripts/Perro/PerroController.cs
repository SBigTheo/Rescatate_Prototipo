using UnityEngine;

public class PerroController : MonoBehaviour
{
    public float velocidad = 2f;
    public float distancia = 10f;  

    private Rigidbody2D rb;
    private float posicionInicial;
    private int direccion = 1;    
    private bool llegoDerecha = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position.x;
    }

    void FixedUpdate()
    {
        Movimiento();
    }

    void Movimiento()
    {

       
        if (!llegoDerecha && transform.position.x >= posicionInicial + distancia)
        {
            llegoDerecha = true;
            direccion = -1;
        }
     
        else if (llegoDerecha && transform.position.x <= posicionInicial)
        {
            Destroy(gameObject);
            return;
        }

        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direccion;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Impacta");
        }
    }
}
using UnityEngine;

public class AlertaPerro : MonoBehaviour
{
    public float duracion = 2f;          
    public GameObject perroPrefab;     
    //public Transform puntoAparicion;   

    void Start()
    {
        Invoke(nameof(SoltarPerro), duracion);
    }

    void SoltarPerro()
    {
        //Instantiate(perroPrefab, puntoAparicion.position, Quaternion.identity);
        Instantiate(perroPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);            
    }
}
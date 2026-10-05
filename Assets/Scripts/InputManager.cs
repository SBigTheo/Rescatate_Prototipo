using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController player;

    [Header("Control por teclado")]
    [Tooltip("Desactiva esto para que el teclado no interfiera con el hand tracking")]
    public bool tecladoActivo = true;

    void Update()
    {
        if (!tecladoActivo) return;

        if (Input.GetKey(KeyCode.A))
            player.MoveLeft();
        else if (Input.GetKey(KeyCode.D))
            player.MoveRight();
        else
            player.Stop();

        if (Input.GetKeyDown(KeyCode.Space))
            player.Stop();
    }
}
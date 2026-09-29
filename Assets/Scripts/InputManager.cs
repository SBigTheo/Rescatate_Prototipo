using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
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
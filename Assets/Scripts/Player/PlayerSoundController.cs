using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip salto;
    public AudioClip paso1;
    [Range(0f, 1f)] public float volumenPaso1 = 0.3f;
    public AudioClip paso2;
    [Range(0f, 1f)] public float volumenPaso2 = 0.3f;
    public AudioClip dano;
    public AudioClip muere;

    public void playSalto() 
    {
        audioSource.PlayOneShot(salto);
    }

    public void playPaso1()
    {
        audioSource.PlayOneShot(paso1, volumenPaso1);
    }

    public void playPaso2()
    {
        audioSource.PlayOneShot(paso2, volumenPaso2);
    }

    public void playDano()
    {
        audioSource.PlayOneShot(dano);
    }

    public void playMuere()
    {
        audioSource.PlayOneShot(muere);
    }
}

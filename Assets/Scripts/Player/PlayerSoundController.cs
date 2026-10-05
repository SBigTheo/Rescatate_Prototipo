using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip salto;
    public AudioClip paso1;
    public AudioClip paso2;

    public AudioClip dano;
    public AudioClip muere;

    public void playSalto() 
    {
        audioSource.PlayOneShot(salto);
    }

    public void playPaso1()
    {
        audioSource.PlayOneShot(paso1);
    }

    public void playPaso2()
    {
        audioSource.PlayOneShot(paso2);
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

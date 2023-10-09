using UnityEngine;

public class GunInfo : ItemInfo
{
    public float damage;
    public AudioClip gunshotSound; // Ses dosyasını buraya sürükleyip bırakabilirsiniz.

    public void PlayGunshotSound(AudioSource audioSource)
    {
        audioSource.clip = gunshotSound;
        audioSource.Play();
    }
}

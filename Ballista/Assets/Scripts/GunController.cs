using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunInfo gunInfo;
    public AudioSource audioSource;

    public void Fire()
    {
        // Silahın atış kodu burada
        // ...

        // Silah sesini çal
        gunInfo.PlayGunshotSound(audioSource);
    }
}

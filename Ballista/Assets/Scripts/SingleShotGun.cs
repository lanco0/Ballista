using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    PhotonView PV;
    AudioSource audioSource;
    private int currentAmmo = 16; // Mermi sayısı
    private int maxAmmoCapacity = 16; // Maksimum mermi kapasitesi
    public TMP_Text ammoText; // UI üzerinde mermi sayısını gösterecek TextMeshPro bileşeni

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        UpdateAmmoUI(); // Mermi sayısını başlangıçta göster
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IDamageable damageableObject = hit.collider.gameObject.GetComponent<IDamageable>();
                if (damageableObject != null)
                {
                    damageableObject.TakeDamage(((GunInfo)itemInfo).damage);
                }

                PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);

                if (audioSource != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.LogError("AudioSource bileşeni eksik veya atanmamış.");
                }

                currentAmmo--; // Mermi kullanıldı
                UpdateAmmoUI(); // Mermi sayısını güncelle
            }
        }
    }

    void Update()
    {
        // R tuşuna basıldığında mermi tazeleme işlemi
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }
    }

    void ReloadAmmo()
    {
        int missingAmmo = maxAmmoCapacity - currentAmmo;
        if (missingAmmo > 0)
        {
            // Here you can add the missing ammo or use your desired mechanism to replenish ammo.
            // For example, set the current ammo to the maximum capacity:
            currentAmmo = maxAmmoCapacity;
            UpdateAmmoUI();
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmoCapacity;
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);

        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 10f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : ShootBase
{
    public Text text;

    public AudioClip reloading;
    public AudioClip noAmmo;

    private int _currentAmmo = 10;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (_currentAmmo > 0) PlayFireAnimation();
            else Src.PlayOneShot(noAmmo);
        }

        if (Vector3.Angle(transform.up, Vector3.up) > 100 && _currentAmmo < maxAmmo) Reload();

        text.text = _currentAmmo.ToString();
    }

    void Reload()
    {
        Src.PlayOneShot(reloading);
        _currentAmmo = maxAmmo;
    }

    void Shoot()
    {
         _currentAmmo--;
         
         RaycastHit hitInfo;
         var position = barrelLocation.position;
         
         var isHit = Physics.Raycast(position, barrelLocation.forward, out hitInfo, shotMaxDistance);

         if (isHit)
         {
             if (hitInfo.transform.CompareTag("Enemy"))
             {
                 hitInfo.transform.gameObject.SetActive(false);
             }
         }
         var hitCoords = isHit ? hitInfo.point : position + barrelLocation.forward * shotMaxDistance;

         Src.PlayOneShot(fire);
         var lineGo = Instantiate(line);
         lineGo.GetComponent<LineRenderer>().SetPositions(new[]{ position, hitCoords });
         Destroy(lineGo, lineLifetime);
         
         var tempFlash = Instantiate(muzzleFlashPrefab, position, barrelLocation.rotation);
         Destroy(tempFlash, lifetime / 2);
         CasingRelease();
    }
}
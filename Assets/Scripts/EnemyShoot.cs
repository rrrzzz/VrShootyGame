using System.Collections;
using UnityEngine;

public class EnemyShoot : ShootBase
{
    public Transform PlayerTransform { get; set; }
    public float timeToTarget = 100;

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, barrelLocation);
        bullet.transform.parent = null;
        StartCoroutine(MoveBullet(bullet.transform));
        var tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
        Destroy(tempFlash, lifetime / 2);
        CasingRelease();
        Destroy(bullet, 10f);
    }

    IEnumerator MoveBullet(Transform bullet)
    {
        var vel = new Vector3();

        var dir = (PlayerTransform.position - barrelLocation.position).normalized;
        var target = barrelLocation.position + dir * shotMaxDistance;
        var dist = (target - bullet.position).magnitude;
        
        while (dist > 0.1f)
        {
            bullet.position = Vector3.SmoothDamp(bullet.position, target, ref vel, timeToTarget); 
            //yield return new WaitForSeconds(0.1f);
            yield return null;
            if (bullet == null) break;
            
            dist = (target - bullet.position).magnitude;
        }
    }
}
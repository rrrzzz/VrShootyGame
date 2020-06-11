using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int ShootAnimTrigger = Animator.StringToHash("Shoot");
    
    public Transform player;
    public Transform barrel;
    public float shotDelay = 3;
    
    
    private Animator _anim;
    private EnemyShoot _shootScript;
    private Transform _barrelPos;
    private bool _isShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _shootScript = GetComponentInChildren<EnemyShoot>();
        _barrelPos = _shootScript.barrelLocation;
        _shootScript.PlayerTransform = player;
        StartCoroutine(RotateToPlayer());
     
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootScript.maxAmmo < 1) return;
        
        RaycastHit hitInfo;
        var dirToPlayer = (player.position - barrel.position).normalized;
        var isHit = Physics.Raycast(barrel.position, dirToPlayer, out hitInfo, _shootScript.shotMaxDistance);
        if (isHit && hitInfo.collider.CompareTag("Player") && _shootScript.maxAmmo > 0 && !_isShooting)
        {
            _shootScript.maxAmmo--;
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        _isShooting = true;
        PlayHumanoidShootAnimation();
        _shootScript.PlayFireAnimation();
        yield return new WaitForSeconds(shotDelay);
        _isShooting = false;
    }

    IEnumerator RotateToPlayer()
    {
        while (true)
        {
            var playerPos = player.position;
            playerPos.y = _barrelPos.position.y;
            barrel.LookAt(playerPos);
            yield return null;
        }
    }

    void PlayHumanoidShootAnimation()
    {
        _anim.SetTrigger(ShootAnimTrigger);
    }
}

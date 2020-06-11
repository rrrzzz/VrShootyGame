using UnityEngine;

public class ShootBase : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject line;
    public Transform barrelLocation;
    public Transform casingExitLocation;
    public float shotMaxDistance = 100f;
    public float lifetime = 3f;
    public float lineLifetime = 1f;
    public AudioClip fire;
    public int maxAmmo = 10;

    private protected AudioSource Src;
    private Animator _animator;
    
    private static readonly int FireHash = Animator.StringToHash("Fire");


    void Awake()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        _animator = GetComponent<Animator>();
        Src = GetComponent<AudioSource>();
    }

    protected void CasingRelease()
    {
        var casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
        Destroy(casing, lifetime);
    }

    public void PlayFireAnimation() => _animator.SetTrigger(FireHash); 
}
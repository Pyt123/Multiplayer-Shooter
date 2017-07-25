using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
	RaycastHit shootHit;
	Ray shootRay;
	int shootableMask;
	private bool isShooting = false;
	public int damagePoints = 10;

	public bool isEnabled = true;

    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private GameObject[] projectilePrefabs;
    private GameObject currentProjectilePrefab;
    private GameObject selectedProjectilePrefab;
    FireBaseScript currentProjectilePrefabScript;



	AudioSource audioSource;


	// Use this for initialization
	void Start ()
    {
		shootableMask = LayerMask.GetMask ("Enemies");
		audioSource = GetComponent<AudioSource> ();

        InitializeProjectile();
	
	}

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer)
        {
#if !MOBILE_INPUT
            if (Input.GetButtonDown("Fire1") && isShooting == false && isEnabled == true)
            {
                CmdShoot();
            }
#else
		    if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			CmdShoot();
		    }
#endif
        }
    }

    [Command]
	public void CmdShoot(){
		isShooting = true;
        SpawnProjectile();
        Invoke("StopShooting", 1f);
	}

	void StopShooting(){
		isShooting = false;
	}

	public void DisableShooting(){
		isEnabled = false;
	}

    private void InitializeProjectile()
    {
        int selectedProjectile = Random.Range(1, 1000) % projectilePrefabs.Length;
        selectedProjectilePrefab = projectilePrefabs[selectedProjectile];
    }

    private void SpawnProjectile()
    {
        currentProjectilePrefab = Instantiate(selectedProjectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
        NetworkServer.Spawn(currentProjectilePrefab);
        currentProjectilePrefab.GetComponent<FireProjectileScript>().ownerName = transform.name;
    }
}

using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;

public class PlayerShoot : MonoBehaviour {


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
	void Update () {
		#if !MOBILE_INPUT
		if (Input.GetButtonDown ("Fire1") && isShooting == false && isEnabled == true) {
            Shoot();
		} 
		#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			Shoot();
		}
		#endif
	
	}


	public void Shoot(){
		isShooting = true;
        SpawnProjectile();
        Invoke("StopShooting", 2f);
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
    }
}

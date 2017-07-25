using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class IronManBehaviorScript : NetworkBehaviour {

	public float speed = 2.0f;
	PlayerHealth playerHealth;
	public int score;

    [SerializeField] private Material[] materials;

	Vector3 movement;
	Rigidbody playerRigidBody;
	bool isMoving = false;
	Animator anim;
	int floorMask;
	float camRayLength = 100.0f;
	public bool isEnabled = true;

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		score = 0;
		audioSource = GetComponent<AudioSource> ();

        InitializeCostume();

		AdsSetup ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.currentHealth <= 0) {
			isEnabled = false;
		}
	}

	void Awake() {
		playerRigidBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
	}

	void FixedUpdate (){
		if (isEnabled == false)
			return;

        if (isLocalPlayer)
        {
            // we need to get a hold of which keys (input) has been used by the Player
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            Move(h, v);

            if (h != 0 || v != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            Animating();
            Turning();
        }
	}


	void Move( float h, float v){
		movement.Set (-v, 0f, h);

		movement = movement.normalized * Time.fixedDeltaTime * speed;

		playerRigidBody.MovePosition (transform.position + movement);


	}


	void Animating(){
		// if the character is moving, then play the walking animation
		// if not, go to idle animation
		if (isMoving == true) {
			// we play the walking anim
			anim.SetFloat ("speed", 1);
		} else {
			anim.SetFloat ("speed", 0);
		}
	}

	public void Turning(){
		// 1) get to know where the mouse is located at
		// if it is in range, then rotate the character towards the mouse

		#if !MOBILE_INPUT
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0.0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
			
		}
		#else
		Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, CrossPlatformInputManager.GetAxisRaw("Vertical"));
		if(turnDir != Vector3.zero){
			Vector3 playerToMouse = (transform.position + turnDir) - transform.position;
			playerToMouse.y = 0;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
		
		}
		#endif
	}


	public void DisableMovement()
    {
		isEnabled = false;
	}

	public void RestartGame()
    {
		SceneManager.LoadScene ("scene-ironman");
	}

	void DisplayGameOver()
    {
		// todo: display ads
		//Advertisement.Show();
	}

	void AdsSetup()
    {
		//Advertisement.Initialize ("1041425", true);
	}

    private void InitializeCostume()
    {
        int nb = Random.Range(1, materials.Length);
        gameObject.GetComponent<Renderer>().material = materials[nb] as Material;
    }
}

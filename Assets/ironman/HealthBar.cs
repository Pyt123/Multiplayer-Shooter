using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthBar : NetworkBehaviour
{
    private PlayerHealth playerHealth;
    private float maxHealth;
    private Color minColor = Color.red;
    private Color maxColor = Color.green;
    private SpriteRenderer spriteRenderer;
    private float initialBarLength = 0.2f;

    void Start ()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        maxHealth = playerHealth.GetComponentInParent<PlayerHealth>().startingHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        float fraction = playerHealth.currentHealth / maxHealth;
        transform.localScale = new Vector3(initialBarLength * fraction, transform.localScale.y, transform.localScale.z);
        spriteRenderer.color = Color.Lerp(minColor, maxColor,
                            Mathf.Lerp(0f, 1f, playerHealth.GetComponent<PlayerHealth>().currentHealth / maxHealth));

        transform.LookAt(Camera.main.transform);
	}
}

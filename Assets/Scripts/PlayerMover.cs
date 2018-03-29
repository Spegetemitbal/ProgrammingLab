using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax; 
}

public class PlayerMover : MonoBehaviour {
	
	public float thrust;
	public Rigidbody2D rb2d;
	public Boundary boundary;
	public GameObject shot;
	public GameObject Player;
	public Transform shotSpawn;
	public float fireRate;
	public float nextFire;
	public GameObject explosion;
	public int health;
	public GameController respawner;
	public Transform spawnpoint;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			transform.position = spawnpoint.position;
			respawner.Respawn();
		}
			
		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		

		if (other.tag == "enemyShot") {

			Destroy (other.gameObject);
			health--;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "enemy") {

			Destroy (other.gameObject);
			health = 0;
		}
	}

	void FixedUpdate() {
		
		  rb2d.position = new Vector3 (
			Mathf.Clamp (rb2d.position.x, boundary.xMin, boundary.xMax),  
			Mathf.Clamp (rb2d.position.y, boundary.yMin, boundary.yMax),
			0.0f
		);

		if (Player != null)
		{
			if (Input.GetKey (KeyCode.UpArrow)) {
				rb2d.AddForce (transform.up * thrust);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				rb2d.AddTorque (1f * thrust);
				rb2d.AddForce (transform.right * -5);
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				rb2d.AddTorque (1f * -thrust);
				rb2d.AddForce (transform.right * 5);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				rb2d.AddForce (transform.up * -thrust);
			}

			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				//				GameObject clone = 
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
			}
		}
	}
}

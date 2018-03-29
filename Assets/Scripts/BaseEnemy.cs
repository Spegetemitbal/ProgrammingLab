using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

	public GameObject playerExplosion;
	public GameObject explosion;
	public int speed;
	public GameObject player;
	public float shot_reset;
	public Transform enemyShotSpawn;
	public GameObject enemyshot;
	public float shotTime;
	public GameController gameController;
	Rigidbody2D rb;
	public int scoreValue;

	void Start() {
		player = gameController.GetPlayer ();
		rb = GetComponent<Rigidbody2D> ();
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "enemyShot") {
			return;
		}

		if (other.tag == "Shot") {
			
			gameController.AddScore (scoreValue);
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}


	}

	void Update () {

		rb.velocity = transform.up * speed;

		if (player != null) 
		{
			gameController.GetPlayer();
			LookAtPlayer ();
			shotTime = shotTime + Time.deltaTime;
			if (shotTime > shot_reset) {
				//				GameObject clone = 
				Instantiate (enemyshot, enemyShotSpawn.position, enemyShotSpawn.rotation);
				shotTime = 0;
			}
		}
	}
			

	void LookAtPlayer(){
		Vector3 offset = player.transform.position - transform.position;
		offset.Normalize(); 
		float rot_z = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);
	}
}
	
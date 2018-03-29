using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject playerPrototype;
	public GameObject currentPlayer;
	public Transform Spawn;
	public CameraController camera;
	public float enemyspawnrate;
	public GameObject basenemy;
	public Transform enemySpawn;
	public PlayerMover playcontrols;
	public BaseEnemy basewavecontroller;
	public BaseEnemy enemy;

	public Text scoreText;
	public int score;

	public List<GameObject> enemies = new List<GameObject>();

	public void Respawn () 
	{
		
		GameObject newPlayer = Instantiate (playerPrototype, Spawn);
		currentPlayer = newPlayer;
		camera.player = newPlayer; 
		newPlayer.GetComponent<PlayerMover> ().spawnpoint = this.transform;
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<BaseEnemy>().player = newPlayer;
		}
			//get the script from the enemy
			//set the player element of the enemy to the player
		DestroyAllEnemiesMyDude ();

	}			

	void Update ()
	{
		enemyspawnrate = enemyspawnrate - Time.deltaTime;
		if (enemyspawnrate <= 0.0f) 
		{
			SpawnEnemy ();
			enemyspawnrate = 5;
		}
	}

	void SpawnEnemy(){
		GameObject newEnemy = Instantiate (basenemy, enemySpawn.position, enemySpawn.rotation);
		BaseEnemy enemy = newEnemy.GetComponent<BaseEnemy>();
		enemies.Add (newEnemy);
		enemy.gameController = this;
	}

	public void DestroyAllEnemiesMyDude(){
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}
	}
	void Start ()
	{
		Respawn ();
		enemyspawnrate = 7;
		playcontrols.respawner = this;
		basewavecontroller.gameController = this;
		playcontrols.spawnpoint = Spawn;
		score = 0;
		UpdateScore ();
	}
		
	public GameObject GetPlayer(){
		return currentPlayer;
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//ALLOW FOR AN OBJECT INPUT
	public GameObject projectile;
	public GameObject explosion;
	public float hitPoints = 100f;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 1;

	private ScoreKeeper scoreKeeper;

	void Start() {
		//references ScoreKeeper Script
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile laser = collider.gameObject.GetComponent<Projectile> ();
		if (laser) {
			hitPoints -= laser.GetDamage ();
			laser.Hit ();
			if (hitPoints <= 0){
				Destroy(gameObject);
				Instantiate (explosion, transform.position, Quaternion.identity);
				scoreKeeper.Score (scoreValue);
			}
		}
	}

	void Update (){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
		Fire();
		}
	}
		
	void Fire () {
		GameObject enemyLaser = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		enemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -projectileSpeed, 0);
	}
}
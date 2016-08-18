using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	public float hitPoints = 500f;

	void OnTriggerEnter2D (Collider2D collider) {
		EnemyProjectile enemyLaser = collider.gameObject.GetComponent<EnemyProjectile> ();
		Enemy enemy = collider.gameObject.GetComponent<Enemy> ();

		if (enemy) {
			Destroy (gameObject);
		}

		if (enemyLaser) {
			hitPoints -= enemyLaser.GetDamage ();
			enemyLaser.Hit ();
			if (hitPoints <= 0){
				Destroy(gameObject);
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;
	public GameObject explosion;
	public GameObject shield;
	public float speed = 5.0f;
	public float projectileSpeed = 10f;
	public float firingRate = 0.3f;
	public float hitPoints = 100f;
	public float shieldCount = 1f;

	float xmin;
	float xmax;
	float ymin;
	float ymax;
	float padding = 0.7f;

	void Start() {
		//LOCK WORLD VIEW TO CAMERA VIEW
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distance));
		Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,1,distance));
		Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		ymax = topmost.y - padding;
		ymin = bottommost.y + padding;
	}

	// CREATES "FIRE" Method for Shooting porjectiles
	void Fire ()
	{
		GameObject laser = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed,0);
	}

	void Shield(){
		GameObject playerShield = Instantiate (shield, transform.position, transform.rotation) as GameObject;
		playerShield.transform.parent = transform;
		shieldCount -= 1;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			//TWO WAYS TO ACHIEVE MOVEMENT
			//transform.position += new Vector3 (0,+speed * Time.deltaTime,0);
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			if (shieldCount > 0) {
				Shield ();
			}
		}

			// ENABLES FIRING ON COMMAND
		if (Input.GetKeyDown (KeyCode.X)) {
			//ALLOWS US TO REGULATE RATE OF FIRE 
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}

		//ALLOWS FIRING TO CEASE WHEN KEY IS RELEASED
		if (Input.GetKeyUp (KeyCode.X)) {
			CancelInvoke ("Fire");
		}
			
		//RESTRICTS MOVEMENT TO GAMESPACE
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
	}

	//ALLOWS PLAYER TO TAKE DAMAGE
	void OnTriggerEnter2D (Collider2D collider) {
		EnemyProjectile enemyLaser = collider.gameObject.GetComponent<EnemyProjectile> ();
		Enemy enemy = collider.gameObject.GetComponent<Enemy> ();

		if (enemy) {
			Destroy(gameObject);
			Instantiate (explosion, transform.position, Quaternion.identity);
			Gameover ();

		}
		if (enemyLaser) {
			hitPoints -= enemyLaser.GetDamage ();
			enemyLaser.Hit ();
			if (hitPoints <= 0){
				Destroy(gameObject);
				Instantiate (explosion, transform.position, Quaternion.identity);
				Gameover ();
			}
		}
	}

	public void Gameover (){
		Application.LoadLevel ("WinScreen");
	}
}
	
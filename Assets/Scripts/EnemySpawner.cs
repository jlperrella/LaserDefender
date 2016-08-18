using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	//CREATES A PREFAB INPUT FOR THE SPAWNER
	public GameObject enemyPrefab;

	public float width = 6f;
	public float height = 2f;
	public float enemySpeed = 5f;
	private bool movingRight = true;
	float xmin, xmax;


	// Use this for initialization
	void Start ()
	{

		//SPAWNS ENEMY AT LOCATION OF GAMEOBJECT HOLDING SCRIPT
		GameObject enemy = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
		enemy.transform.parent = transform;


	/* CODE BLOCK CAUSES UNITY TO CRASH
		//SPAWNS ENEMIES AT CHILD LOATIONS
		foreach (Transform child in transform) {

		// CREATES NEW OBJECT FROM PREFAB
		GameObject enemy = Instantiate (enemyBlackPrefab, child.transform.position, Quaternion.identity) as GameObject;

		//CREATES NEW OBJECT INSIDE EMPTY GAME OBJECT (EnemyFormation)
		enemy.transform.parent = transform;
		}
		*/


	//LOCK ENEMIES TO CAMERA VIEW

		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		float padding = 0.7f;

	
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distanceToCamera));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distanceToCamera));
		//Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,1,distanceToCamera));
		//Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distanceToCamera));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;


	}

	void Update ()
	{
		//MOVE ENEMIES LEFT AND RIGHT
		if (movingRight) {
			transform.position += Vector3.right * enemySpeed * Time.deltaTime;
		} else {
			transform.position += Vector3.left *enemySpeed * Time.deltaTime;
		}

		float rightmostOfFormation = transform.position.x + (0.5f*width);
		float leftmostOfFormation = transform.position.x - (0.5f*width);
		if (leftmostOfFormation < xmin){
			movingRight = true;
		}else if (rightmostOfFormation > xmax){
		movingRight = false;
		}
	}
}

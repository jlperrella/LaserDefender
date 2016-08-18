using UnityEngine;
using System.Collections;

public class EnemyRespawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject soundFx;
	public float spawnDelay = 0.5f;

	void Update () {
		if (AllMembersDead ()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull ();
			Instantiate (soundFx, transform.position, transform.rotation);
		}
	}

	Transform NextFreePosition (){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, transform.rotation) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
}

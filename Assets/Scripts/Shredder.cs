using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	//DESTORYS OBJECTS ON IMPACT TO KEEP SCENE CLEAN
	void OnTriggerEnter2D (Collider2D col) {
		Destroy(col.gameObject);
	}
}

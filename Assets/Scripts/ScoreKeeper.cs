using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;

	private Text myText;

	void Start () {
		myText = GetComponent<Text> ();
	}

	public void Score(int points){
		score += points;
		myText.text = "ENEMIES DESTROYED: " + score.ToString();
	}

	public static void Reset(){
		score = 0;
	}
}
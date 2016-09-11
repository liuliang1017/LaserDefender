using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	public static int score = 0;
	public Text myText;
	
	void Start(){
		myText = GetComponent<Text>();
	}	
				
	public void Score(int points){
		score += points;
		myText.text = "Score: " + score.ToString();
	}
			
	public static void Reset(){
		score = 0;
		//myText.text = "Score: " + score.ToString();
	}

	
}

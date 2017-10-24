using UnityEngine;
using System.Collections;

public class Pausa : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	public void Pausar (){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UI : MonoBehaviour {
	public Button Cambio;
	void OnTriggerEnter(Collider collider){
		Cambio.onClick.Invoke ();
	}
}

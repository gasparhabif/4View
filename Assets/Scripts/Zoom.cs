using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Zoom : MonoBehaviour {
	private Button boton;
	public Camera camara;
	public static int rangoCamera = 0;

	// Use this for initialization
	void Start () {
		boton = this.GetComponent<Button> ();
	}

	void OnTriggerEnter(Collider collider){
		boton.onClick.Invoke ();
	}
	public void ZoomIn(){
		if (rangoCamera < 6) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y, poscamara.z + 1f);
			rangoCamera++;
		}
	}
	public void ZoomOut(){
		if (rangoCamera > -6) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y, poscamara.z - 1f);
			rangoCamera--;
		}

	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArribaAbajo: MonoBehaviour {
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

	public void Subir(){
		if (rangoCamera < 6) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y + 1f, poscamara.z);
			rangoCamera++;
		}
	}

	public void Abajo(){
		if (rangoCamera > -6) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y - 1f, poscamara.z);
			rangoCamera--;
		}

	}
}

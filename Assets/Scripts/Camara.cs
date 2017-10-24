using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {

	public GameObject Jugador;
	//public Vector3 offset;
	private float offsetx, offsetz;
	// Use this for initialization
	void Start () 
	{
		//offset = transform.position - Jugador.transform.position;
		offsetx = transform.position.x-Jugador.transform.position.x;
		offsetz = transform.position.z-Jugador.transform.position.z;
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 origin = Jugador.transform.position;
		//Vector3 origin = Vista.transform.position;
		Vector3 direction = Vector3.down;
		RaycastHit hitInfo;

		if (Physics.Raycast (origin, direction, out hitInfo) == true && Jugador.GetComponent<Jugador> ().perdio == false &&
			Jugador.GetComponent<Jugador> ().bajada != true) {
			if (hitInfo.collider.gameObject.tag != "Perdio") {
				//this.transform.position = Jugador.transform.position + offset;
				this.transform.position = new Vector3(Jugador.transform.position.x+offsetx,transform.position.y,Jugador.transform.position.z+offsetz); 
			}
		}
		if (Jugador.GetComponent<Jugador> ().bajada == true) 
		{
			//this.transform.position = Jugador.transform.position + offset;
			this.transform.position = new Vector3(Jugador.transform.position.x+offsetx,transform.position.y,Jugador.transform.position.z+offsetz); 
		}
		else {
			this.transform.position = this.transform.position;
			Jugador.GetComponent<Jugador> ().movement= new Vector3 (Jugador.GetComponent<Jugador> ().hMovement, -3f, 1f);
		}
	}
	

}

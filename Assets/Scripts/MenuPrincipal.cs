using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;
using WindowsInput;

public class MenuPrincipal : MonoBehaviour {

	private string Boton = "";
	private Controller controller;

	public HandController controlador;
	public Hand firstHand;

	Frame frameActual;
	Frame frameAnt;

	int cerrarapp = 0, tiempoentregestos = 0;

	// Use this for initialization
	void Start () {
		controller = controlador.GetLeapController();
		controller.EnableGesture (Gesture.GestureType.TYPECIRCLE);
		controller.Config.SetFloat ("Gesture.Circle.MinRadius", 30.0f);
		controller.Config.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tiempoentregestos == 350) {
			cerrarapp = 0;
			tiempoentregestos = 0;
		}
		frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
		int ant = (int)frameActual.Id - 1;
		frameAnt = controller.Frame(ant);
		GestureList gestos = frameActual.Gestures ();

		if (frameActual.Hands.Count > 0)
		{
			HandList hands = frameActual.Hands;
			firstHand = hands[0];
		}

		FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
		int dedos_extendidos = 0;

		for (int i = 0; i < dedos.Extended().Count; i++)
			dedos_extendidos++;//cuento todos los dedos que estan extendidos de la FingerList dedos        
		
		if (dedos_extendidos == 5) {
			Debug.Log ("Awanteaaaa");
			if (Boton == "Body")
				Application.LoadLevel (0);
			else if (Boton == "Earth")
				Application.LoadLevel (4);
			else if (Boton == "Visual")
				Application.LoadLevel (5);
			else if (Boton == "Rolling")
				Application.LoadLevel (6);
		}
		for (int i = 0; i < gestos.Count; i++) {
			if (gestos [i].Type == Gesture.GestureType.TYPECIRCLE && dedos_extendidos == 1) {
				if(cerrarapp == 5)
					UnityEditor.EditorApplication.isPlaying = false;
				cerrarapp++;
				tiempoentregestos = 0;
			}
		}
		tiempoentregestos++;
	}

	void OnTriggerEnter(Collider collider){
		Debug.Log("holaaaaa");
		Debug.Log(this.name);
		Debug.Log(this.gameObject.name);
		if (this.name == "BodyMotion")
			Boton = "Body";
		else if (this.gameObject.name == "EarthMotion")
			Boton = "Earth";
		else if (this.gameObject.name == "VisualMotion")
			Boton = "Visual";
		else if (this.gameObject.name == "RollingRoshakazu")
			Boton = "Rolling";
		
	}
}

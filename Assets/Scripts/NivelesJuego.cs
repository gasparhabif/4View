using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;
using UnityEngine.SceneManagement;

public class NivelesJuego : MonoBehaviour {
	
	public static bool fuetocado = false;
	public Button Princi, Inter, Avan, Nave, Qui;
	public HandController controlador;
	public Hand firstHand;

	Frame frameActual;

	private Controller controller;
	private bool Listo1 = false, Listo2 = false, Listo3 = false;

	/* */
	// Use this for initialization
	void Start () {
		controller = controlador.GetLeapController ();
	}

	void HabilitarDes(Button boton, bool estado){//Funcion que muestra o esconde el gameobject
		boton.gameObject.SetActive (estado);
	}

	// Update is called once per frame
	void Update () {
		frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
		if (frameActual.Hands.Count > 0)
		{
			HandList hands = frameActual.Hands;
			firstHand = hands[0];
		}

		FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
		int dedos_extendidos = 0;

		for (int i = 0; i < dedos.Extended().Count; i++)
			dedos_extendidos++;//cuento todos los dedos que estan extendidos de la FingerList dedos        

		if (dedos_extendidos == 2) {
			if (fuetocado == true) {
				fuetocado = false;
				HabilitarDes (Princi, false);//Esconde el boton Principiante
				HabilitarDes (Inter, false);//Esconde el boton Intermedio
				HabilitarDes (Avan, false);//Esconde el boton Avanzado
				HabilitarDes (Nave, true);//Muestra El boton Navegar
				HabilitarDes (Qui, true);//Muestra El boton Quiz
				if (Listo3 == true) {
					Listo3 = false;
					if (Listo3 == false) {
						 SceneManager.LoadScene (3);
					}
				}
			}
		}
		if (dedos_extendidos == 5) {
		if (Listo1 == true) {
			Listo1 = false;
			if (Listo1 == false) {
				 SceneManager.LoadScene (1);
			}
		} else {
			if (Listo2 == true) {
				Listo2 = false;
				if (Listo2 == false) {
					 SceneManager.LoadScene (2);
				}
			} 
		}
	}
	}
		
	void OnTriggerEnter(Collider collider){
		if (this.gameObject.name == "Exit") {
			HabilitarDes (Princi, true);
			HabilitarDes (Inter, true);
			HabilitarDes (Avan, true);
			HabilitarDes (Nave, false);
			HabilitarDes (Qui, false);
			Listo3 = true;
			fuetocado = true;
		}else if (this.gameObject.name == "Principiante") {
			Seleccionado.nivel = 8;//Guarda en la variable nivel que nivel se eligio
			SelectNav.nivel = 8;//Guarda en la variable nivel que nivel se eligio
			Botones.cant = 5;//Guarda en la variable cant la cantidad de preguntas que se haran del quiz correspondiendo al nivel
			fuetocado = true;//Guarda en la variable fuetocado que ya fue tocado
		} else if (this.gameObject.name == "Intermedio") {
			Seleccionado.nivel = 9;//Guarda en la variable nivel que nivel se eligio
			SelectNav.nivel = 9;//Guarda en la variable nivel que nivel se eligio
			Botones.cant = 9;//Guarda en la variable cant la cantidad de preguntas que se haran del quiz correspondiendo al nivel
			fuetocado = true;
		} else if (this.gameObject.name == "Avanzado") {
			Seleccionado.nivel = 10;//Guarda en la variable nivel que nivel se eligio
			SelectNav.nivel = 10;//Guarda en la variable nivel que nivel se eligio
			Botones.cant = 15;//Guarda en la variable cant la cantidad de preguntas que se haran del quiz correspondiendo al nivel
			fuetocado = true;
		} else {
			if (this.gameObject.name == "Quiz") {
				Listo1 = true;
			} else if (this.gameObject.name == "Navegar") {
				Listo2 = true;
			}
		}
	}
}

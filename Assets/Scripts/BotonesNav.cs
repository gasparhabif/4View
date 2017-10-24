using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class BotonesNav : MonoBehaviour {

	//Este script es para los botones: Pasar, Proxima, Comenzar, Ok y Cancel

	public Text Parte, TextAviso, TextInfo;
	public Button Informacion, Ok, Cancel, Subir, Bajar, Mas, Menos, exit;
	public UnityEngine.UI.Image LoadingBar, Frame, ImgAviso;
	public HandController controlador;
	public Hand firstHand;

	Frame frameActual;
	Frame frameAnt;

	private Controller controller;
	private bool Listo1 = false, Listo2 = false, Listo3 = false;

	private static int Aviso = -1;
	public static bool mostrar = false;
	private bool verde = true, comen = false, chau = false;
	public static bool presionado = false, yapaso = false, Mensaje=false;
	private int dedos_extendidos = 0;


	// Use this for initialization
	void Start () {
		controller = controlador.GetLeapController ();
		if (this.gameObject.name == "Comenzar") {
			if (this.gameObject.name == "Ok" || this.gameObject.name == "Cancel") {//Este if es para que no haga lo de abajo si apreta el Ok o Cancel
			} else if (this.name == "Proxima")
				this.gameObject.SetActive (false);
			else if (this.name == "Pasar")
				this.gameObject.SetActive (false);
			else {//Si llego hasta aca, significa que es el boton comenzar
				CerrarMensaje(false);//Esconde el panel de mensaje
				EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
				todofalse (false);//esconde casi todo
				InforOcultar(false);
			}
		Mensaje = false;
		}
	}
	void Update(){
		frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
		int ant = (int)frameActual.Id - 1;
		frameAnt = controller.Frame(ant);


		if (frameActual.Hands.Count > 0)
		{
			HandList hands = frameActual.Hands;
			firstHand = hands[0];
		}

		FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
		dedos_extendidos = 0;

		for (int i = 0; i < dedos.Extended().Count; i++)
			dedos_extendidos++;//cuento todos los dedos que estan extendidos de la FingerList dedos 
		if (comen == true) {
			GameObject.Find("Comenzar").gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter (Collider collider){
		if (this.gameObject.name == "Exit") {
			CerrarMensaje (true);
			//Cancel.gameObject.SetActive (true);
			Mensaje = false;
			SelectNav.elegido = false;
			EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
			todofalse (false);//esconde casi todo
			Aviso = 2;
			TextAviso.GetComponentInChildren<Text> ().text = "¿Estas seguro que deseas salir del modo navegacion?";//Cambia el texto del panel de mensaje
		} else if (this.gameObject.name == "Ok") {//Pregunta si el objeto tocado es el boton Ok
			if (dedos_extendidos == 5) {//Pregunta si tiene 5 dedos extendidos
				if (Aviso == 1) {
					CerrarMensaje (false);//Esconde todo el panel de mensaje
					Mensaje = true;
					EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
					todofalse (true);//muestra casi todo
					Aviso = -1;//Pone avios en -1 para que no se confunda ni con 0 ni uno
				} else {
					if (Aviso == 2) {
						CerrarMensaje (false);//Esconde todo el panel de mensaje
						Mensaje = true;
						EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
						todofalse (true);//muestra casi todo
						Aviso = -1;//Pone avios en -1 para que no se confunda ni con 0 ni uno
						chau = true;
						if (chau == true) {
							chau = false;
							if (chau == false) {
								Application.LoadLevel (0);
							}
						}
						//chau = true;
					} else {
						CerrarMensaje (false);//Esconde todo el panel de mensaje
						Mensaje = true;
						EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
						todofalse (true);//muestra casi todo
						Aviso = -1;//Pone avios en -1 para que no se confunda ni con 0 ni uno
					}
				}
			}
		} else if (this.gameObject.name == "Cancel") {//Pregunta si el objeto tocado es el boton Cancel
			if (dedos_extendidos == 5) {//Pregunta si tiene 5 dedos extendidos
				CerrarMensaje (false);//Esconde todo el panel de mensaje
				Mensaje = true;
				EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
				todofalse (true);//muestra casi todo
				Aviso = -1;//Pone avios en -1 para que no se confunda ni con 0 ni uno
			}
		}else {
				string Nivel;
				Nivel = cambiarDeIntAString ();//cambia los niveles(8,9,10) a palabras(Principiante, Intermedio y Avanzado)
				if (this.gameObject.name == "Comenzar") {//Pregunta si el objeto tocado es el boton Comenzar
				if (dedos_extendidos == 2) {
					SelectNav.elegido = false;
					Mensaje = true;
					EsconderAjustes (true);//esconde los botones de zoom y de arribaAbajo
					todofalse (true);//esconde casi todo
					Informacion.GetComponent<UnityEngine.UI.Image> ().color = Color.red;
					comen = true;
				}
				} else {
					 if (this.gameObject.name == "Informacion") {
						if (verde == true) {
						Informacion.GetComponent<UnityEngine.UI.Image> ().color = Color.green;//El boton pasar se pone en verde
							mostrar = true;
							InforOcultar(true);
							verde = false;
						} else {
						Informacion.GetComponent<UnityEngine.UI.Image> ().color = Color.red;//El boton pasar se pone en verde
							mostrar = false;
						TextInfo.text = "";
							InforOcultar(false);	
							verde = true;
						}
						CerrarMensaje (true);
						Mensaje = false;
						SelectNav.elegido = false;
						todofalse (false);
						EsconderAjustes (false);
						Aviso = 1;
						SelectNav.MouseClicked = true;
					if (verde == false) {
						TextAviso.GetComponentInChildren<Text> ().text = "Si presionas OK, aparecera la informacion de la parte seleccionada.";
					} else {
						TextAviso.GetComponentInChildren<Text> ().text = "Si presionas OK, dejara de aparecer la informacion de la parte seleccionada.";
					}
					}
				}
				presionado = true;
			}
	}

	public static string cambiarDeIntAString(){
		string Nivel = "";
		if (SelectNav.nivel == 8) {
			Nivel = "Principiante";
		} else if (SelectNav.nivel == 9){
			Nivel = "Intermedio";
		}
		else if (SelectNav.nivel == 10){
			Nivel = "Avanzado";
		}
		else
			Debug.Log ("Salio mal la logica de niveles");
		return Nivel;
	}

	public void EsconderAjustes(bool estado){ //Esconde los botones de zoom y de moverse hacia arribaabajo
		Subir.gameObject.SetActive (estado);
		Bajar.gameObject.SetActive (estado);
		Mas.gameObject.SetActive (estado);
		Menos.gameObject.SetActive (estado);
		exit.gameObject.SetActive(estado);
	}

	public void CerrarMensaje(bool estado){//Esconde el panel de aviso
		ImgAviso.gameObject.SetActive (estado);
		TextAviso.gameObject.SetActive (estado);
		Ok.gameObject.SetActive (estado);
		Cancel.gameObject.SetActive (estado);
	}

	public void todofalse(bool estado){//Esconde casi todas las cosas
		Parte.gameObject.SetActive (estado);
		Informacion.gameObject.SetActive (estado);
		if (estado == false) {
			LoadingBar.gameObject.SetActive (estado);
			Frame.gameObject.SetActive (estado);
		}
	}

	public void InforOcultar(bool estado){//Esconde el panel de informacion
		TextInfo.gameObject.SetActive (estado);
	}

	private void VolverMenu(){
		CerrarMensaje(false);//Esconde el panel de mensaje
		EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
		InforOcultar(false);////Esconde el panel de informacion
		todofalse (false);//esconde casi todo
		Mensaje = false;
		//Application.LoadLevel (0);
	}

}

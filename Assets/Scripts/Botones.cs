using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour {

	//Este script es para los botones: Pasar, Proxima, Comenzar, Ok y Cancel

	public Text Pregunta, CualEs, TextAviso, punt, puntMal, puntab, puntm;
	public Button Proxim, Ok, Cancel, Subir, Bajar, Mas, Menos, Pass, exit;
	public UnityEngine.UI.Image LoadingBar, Frame, ImgAviso;

	Frame frameActual;
	 

	public static string[,] info = new string[cant,2];
	public static bool presionado = false, yapaso = false, Mensaje=false;
	public static int cant = 5;
	public HandController controlador;
	public Hand firstHand;

	private Controller controller;
	private static int Aviso = -1, p = 0, mal = 0, punta;
	private string mostrar = "";
	private bool chau = false;
	private int dedos_extendidos = 0;

	// Use this for initialization
	void Start () {
		controller = controlador.GetLeapController ();
		if (this.gameObject.name == "Ok" || this.gameObject.name == "Cancel") {//Este if es para que no haga lo de abajo si apreta el Ok o Cancel
		} else if (this.name == "Proxima")
				this.gameObject.SetActive (false);
		else if (this.name == "Pasar")
				this.gameObject.SetActive (false);
			else {//Si llego hasta aca, significa que es el boton comenzar
				CerrarMensaje(false);//Esconde el panel de mensaje
				EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
				todofalse (false);//esconde casi todo
			Pass.GetComponent<UnityEngine.UI.Image> ().color = Color.green;//El boton pasar se pone en verde
			}
		Mensaje = false;
		if (chau == true) {
			chau = false;
			if (chau == false) {
                SceneManager.LoadScene(0);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Seleccionado.elegido == true && yapaso == false)
			Proxim.GetComponent<UnityEngine.UI.Image> ().color = Color.green;
		frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
		 
		 


		if (frameActual.Hands.Count > 0)
		{
			HandList hands = frameActual.Hands;
			firstHand = hands[0];
		}

		FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
		dedos_extendidos = 0;

		for (int i = 0; i < dedos.Extended().Count; i++)
			dedos_extendidos++;//cuento todos los dedos que estan extendidos de la FingerList dedos
	}
	//this.GetComponent<Button> ().enabled = false;
	void OnTriggerEnter (Collider collider){
			if (this.gameObject.name == "Exit") {
				CerrarMensaje (true);
				Cancel.gameObject.SetActive (true);
				Mensaje = false;
				Seleccionado.elegido = false;
				EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
				ProxPass (false);//esconde el boton de pasar y proxima
				todofalse (false);//esconde casi todo
				Aviso = 3;
				TextAviso.GetComponentInChildren<Text> ().text = "¿Estas seguro que deseas salir del juego y perder todo el contenido guardado hasta ahora?";//Cambia el texto del panel de mensaje
			}else if (this.gameObject.name == "Ok") {//Pregunta si el objeto tocado es el boton Ok
			if (dedos_extendidos == 5) {
				Debug.Log("Ok");
				if (Aviso == 1) {
					if (mal + punta < (cant - 1)) {//pregunta si todavia no se respondieron todas las preguntas
						p++;//suma uno a la variable que indica que pregunta traer
						mal++;//Suma una respuesta mal
						puntMal.text = mal.ToString ();//Muestra el numero de respuestas mal
						Pregunta.text = "Encontrar " + info [0, p] + ".";//Cambia la pregunta
						CualEs.text = p + 1 + "/" + cant + ":";//Indica por que pregunta esta el usuario(sobre cuantas)
						CerrarMensaje (false);//Esconde todo el panel de mensaje
						Mensaje = true;
						EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
						ProxPass (true);//muestra el boton de pasar y proxima
						todofalse (true);//muestra casi todo
					}
				} else {
					if (Aviso == 0) {
						CerrarMensaje (false);//Esconde todo el panel de mensaje
						Mensaje = true;
						EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
						ProxPass (true);//muestra el boton de pasar y proxima
						todofalse (true);//muestra casi todo
					} else {
						if (Aviso == 3) {
							Debug.Log ("CargarNivel");
							CerrarMensaje (false);//Esconde todo el panel de mensaje
							Mensaje = true;
							chau = true;
							SceneManager.LoadScene (7);
						} else {
							if (mal > punta) {
								mostrar = "Perdiste! ";
							} else {
								mostrar = "Ganaste! ";
							}
							Debug.Log ("Perdio 0");
							Debug.Log ("viene todo Ok");
							Debug.Log (p);
							Debug.Log (mal);
							Debug.Log (punta);
							VolverMenu ();
						}
					}
				}
			}
			} else if (this.gameObject.name == "Cancel") {
			if (dedos_extendidos == 5) {//Pregunta si el objeto tocado es el boton Cancel
				Debug.Log("Cancel");
				CerrarMensaje (false);//Esconde todo el panel de mensaje
				Mensaje = true;
				EsconderAjustes (true);//muestra los botones de zoom y de arribaAbajo
				ProxPass (true);//muestra el boton de pasar y proxima
				todofalse (true);//muestra casi todo
				Aviso = 1;//Pone avios en -1 para que no se confunda ni con 0 ni uno
			} 
		} else {
				string Nivel;
				Nivel = cambiarDeIntAString ();//cambia los niveles(8,9,10) a palabras(Principiante, Intermedio y Avanzado)
				if (this.gameObject.name == "Comenzar") {
				if (dedos_extendidos == 2) {//Pregunta si el objeto tocado es el boton Comenzar
					punta = 0;
					info = Quiz.TraerInfo ("Indicar", "Parte", Nivel, cant);//Devuele un array con 5 partes random y desordenadas del cuerpo(del nivel correspondiente)
					Pregunta.text = "Encontrar " + info [0, p] + ".";
					Seleccionado.elegido = false;
					Mensaje = true;
					CualEs.text = p + 1 + "/" + cant + ":";
					this.gameObject.SetActive (false);
					ProxPass (true);
					exit.gameObject.SetActive (true);
					Debug.Log ("Deberia verse(cre0)");
					Proxim.GetComponent<UnityEngine.UI.Image> ().color = Color.red;
					Pregunta.gameObject.SetActive (true);
					CualEs.gameObject.SetActive (true);
					punt.gameObject.SetActive (true);
					puntMal.gameObject.SetActive (true);
					puntab.gameObject.SetActive (true);
					puntm.gameObject.SetActive (true);
					EsconderAjustes (true);
				}
				} else {
					if (this.gameObject.name == "Proxima") {//Pregunta si el boton tocado es proxima
						if (Seleccionado.elegido == false) {//Pregunta si ya se elegio alguna parte del cuerpo(si ya se cargo la barra al 100%)
							CerrarMensaje (true);//Muestra el panel de mensaje
							Mensaje = false;
							//Seleccionado.elegido = false;
							EsconderAjustes (false);//esconde los botones de zoom y de arribaAbajo
							ProxPass (false);//esconde el boton de pasar y proxima
							todofalse (false);//esconde casi todo
							Aviso = 0;
							TextAviso.GetComponentInChildren<Text> ().text = "Seleccione una parte del cuerpo antes de continuar o pase de pregunta";//Cambia el texto del panel de mensaje
						} else {
						Proxim.GetComponent<UnityEngine.UI.Image> ().color = Color.red;//Pone el boton proximo en rojo
							yapaso = true;
							if (presionado == false) {
								if (mal + punta < cant) {//Se fija si todavia le quedan preguntas por responder
									Debug.Log (Seleccionado.Selected);
									if (Seleccionado.Selected == info [1, p]) {//si fija si la pregunta es correcta
										punta++;//Suma un punto a los correctos
										punt.text = punta.ToString ();//Actualiza los correctos
										if (p < (cant - 1)) {
											p++;
											Pregunta.text = "Encontrar " + info [0, p] + ".";
											CualEs.text = p + 1 + "/" + cant + ":";
										} else {
											if (mal > punta) {
												mostrar = "Perdiste! ";
											} else {
												mostrar = "Ganaste! ";
											}
											VolverMenu ();
										}
									} else {//Entra al else si se respondio mal
										mal++;//Suma uno a las incorrectas
										puntMal.text = mal.ToString ();//Actualiza el numero de mal
										if (p < (cant - 1)) {
											p++;
											Pregunta.text = "Encontrar " + info [0, p] + ".";
											CualEs.text = p + 1 + "/" + cant + ":";
										} else {
											Debug.Log ("Perdio2");
											if (mal > punta) {
												mostrar = "Perdiste! ";
											} else {
												mostrar = "Ganaste! ";
											}
											VolverMenu ();
										}
									}
								} else {Debug.Log ("Perdio3");
									if (mal > punta) {
										mostrar = "Perdiste! ";
									} else {
										mostrar = "Ganaste! ";
									}
									VolverMenu ();
								}
							}
						}
					} else if (this.gameObject.name == "Pasar") {
						CerrarMensaje (true);
						Mensaje = false;
						Seleccionado.elegido = false;
						todofalse (false);
						ProxPass (false);
						EsconderAjustes (false);
						Aviso = 1;
						TextAviso.GetComponentInChildren<Text> ().text = "¿Estas seguro de que deseas pasar la pregunta?";
					}
				}
				presionado = true;
			}
	}

	void OnTriggerExit(Collider collider){
		if (this.gameObject.name == "Proxima")
			Seleccionado.elegido = false;
	}

	public static string cambiarDeIntAString(){
		string Nivel = "";
		if (Seleccionado.nivel == 8) {
			Nivel = "Principiante";
			cant = 5;
		} else if (Seleccionado.nivel == 9){
			Nivel = "Intermedio";
			cant = 9;
			Debug.Log ("Es Intermedio");
		}
		else if (Seleccionado.nivel == 10){
			Nivel = "Avanzado";
			cant = 15;
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
	}

	private void CerrarMensaje(bool estado){//Esconde el panel de aviso
		ImgAviso.gameObject.SetActive (estado);
		TextAviso.gameObject.SetActive (estado);
		Ok.gameObject.SetActive (estado);
		Cancel.gameObject.SetActive (estado);
	}

	private void todofalse(bool estado){//Esconde casi todas las cosas
		Pregunta.gameObject.SetActive (estado);
		CualEs.gameObject.SetActive (estado);
		punt.gameObject.SetActive (estado);
		puntMal.gameObject.SetActive (estado);
		puntab.gameObject.SetActive (estado);
		puntm.gameObject.SetActive (estado);
		if (estado == false) {
			LoadingBar.gameObject.SetActive (estado);
			Frame.gameObject.SetActive (estado);
		}
	}

	private void ProxPass(bool estado){//Esconde el boton Proxima y Pasar
		exit.gameObject.SetActive(estado);
		Proxim.gameObject.SetActive(estado);
		Pass.gameObject.SetActive (estado);
	}

	private void VolverMenu(){
		CerrarMensaje (true);
		Mensaje = false;
		Seleccionado.elegido = false;
		todofalse (false);
		ProxPass (false);
		EsconderAjustes (false);
		Aviso = 3;
		TextAviso.GetComponentInChildren<Text> ().text = mostrar + " Apreta Ok para salir del juego";
	}
}

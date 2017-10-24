using UnityEngine;
using System.Collections;

public class ColorAUto : MonoBehaviour {

	public GameObject auto,puerta1,puerta2;
	public Material Azul,Verde,Rojo,Celeste,Violeta,VerdeClaro,VerdeOscuro,Naranja,Amarillo;
	public Material ColorAuto;
	string colorprevio;
	void Start () {
		colorprevio = PlayerPrefs.GetString ("color", "Violeta");
		switch(colorprevio){
		case "Azul":
			auto.GetComponent<Renderer> ().material.color = Azul.color;
			puerta1.GetComponent<Renderer> ().material.color = Azul.color;
			puerta2.GetComponent<Renderer> ().material.color = Azul.color;
			break;
		case "Celeste":
			auto.GetComponent<Renderer> ().material.color = Celeste.color;
			puerta1.GetComponent<Renderer> ().material.color = Celeste.color;
			puerta2.GetComponent<Renderer> ().material.color = Celeste.color;
			break;
		case "Verde":
			auto.GetComponent<Renderer> ().material.color = Verde.color;
			puerta1.GetComponent<Renderer> ().material.color = Verde.color;
			puerta2.GetComponent<Renderer> ().material.color = Verde.color;
			break;
		case "Rojo":
			auto.GetComponent<Renderer> ().material.color = Rojo.color;
			puerta1.GetComponent<Renderer> ().material.color = Rojo.color;
			puerta2.GetComponent<Renderer> ().material.color = Rojo.color;
			break;
		case "Violeta":
			auto.GetComponent<Renderer> ().material.color = Violeta.color;
			puerta1.GetComponent<Renderer> ().material.color = Violeta.color;
			puerta2.GetComponent<Renderer> ().material.color = Violeta.color;
			break;
		case "VerdeClaro":
			auto.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puerta1.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puerta2.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			break;
		case "VerdeOscuro":
			auto.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puerta1.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puerta2.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			break;
		case "Naranja":
			auto.GetComponent<Renderer> ().material.color = Naranja.color;
			puerta1.GetComponent<Renderer> ().material.color = Naranja.color;
			puerta2.GetComponent<Renderer> ().material.color = Naranja.color;
			break;
		case "Amarillo":
			auto.GetComponent<Renderer> ().material.color = Amarillo.color;
			puerta1.GetComponent<Renderer> ().material.color = Amarillo.color;
			puerta2.GetComponent<Renderer> ().material.color = Amarillo.color;
			break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void CambiarColor(){
		switch(this.name){
		case "Azul":
			auto.GetComponent<Renderer> ().material.color = Azul.color;
			puerta1.GetComponent<Renderer> ().material.color = Azul.color;
			puerta2.GetComponent<Renderer> ().material.color = Azul.color;
			break;
		case "Celeste":
			auto.GetComponent<Renderer> ().material.color = Celeste.color;
			puerta1.GetComponent<Renderer> ().material.color = Celeste.color;
			puerta2.GetComponent<Renderer> ().material.color = Celeste.color;
			break;
		case "Verde":
			auto.GetComponent<Renderer> ().material.color = Verde.color;
			puerta1.GetComponent<Renderer> ().material.color = Verde.color;
			puerta2.GetComponent<Renderer> ().material.color = Verde.color;
			break;
		case "Rojo":
			auto.GetComponent<Renderer> ().material.color = Rojo.color;
			puerta1.GetComponent<Renderer> ().material.color = Rojo.color;
			puerta2.GetComponent<Renderer> ().material.color = Rojo.color;
			break;
		case "Violeta":
			auto.GetComponent<Renderer> ().material.color = Violeta.color;
			puerta1.GetComponent<Renderer> ().material.color = Violeta.color;
			puerta2.GetComponent<Renderer> ().material.color = Violeta.color;
			break;
		case "VerdeClaro":
			auto.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puerta1.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puerta2.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			break;
		case "VerdeOscuro":
			auto.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puerta1.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puerta2.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			break;
		case "Naranja":
			auto.GetComponent<Renderer> ().material.color = Naranja.color;
			puerta1.GetComponent<Renderer> ().material.color = Naranja.color;
			puerta2.GetComponent<Renderer> ().material.color = Naranja.color;
			break;
		case "Amarillo":
			auto.GetComponent<Renderer> ().material.color = Amarillo.color;
			puerta1.GetComponent<Renderer> ().material.color = Amarillo.color;
			puerta2.GetComponent<Renderer> ().material.color = Amarillo.color;
			break;
		}
		PlayerPrefs.SetString ("color",this.name);
		PlayerPrefs.Save ();
	}
}

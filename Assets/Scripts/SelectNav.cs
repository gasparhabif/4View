using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;


public class SelectNav : MonoBehaviour {

	Color color1 = Color.blue, color2 = Color.blue;

	public Text porcentaje, parte, Informacion;
	public Image LoadingBar, Frame;

	public static int nivel = 8;
	public static bool elegido = false,  ObjetCarga = false, terminado = false, MouseClicked = false;
	public static string Selected = "no", ObjectoCarga = "";
	public float LoadingTime;

	private bool stay = false, Charging = false;
	private string[] info = new string[3];
	private string Partee = "";

	void Start () {
		if (this.gameObject.name == "Mandíbula") {
			Debug.Log(this.GetComponent<Renderer> ().materials [1].color);
		}
		if (this.GetComponent<Renderer>().materials.Length == 2)
		{
			color1 = this.GetComponent<Renderer>().material.color;
			color2 = this.GetComponent<Renderer>().materials[1].color;
		}
		else
		{
			color1 = this.GetComponent<Renderer>().material.color;
		}
	}

	void Update () {
		if (this.name == "Mandíbula" && this.GetComponent<Renderer> ().material.color == Color.blue) {
			this.GetComponent<Renderer> ().materials [1].color = Color.blue;
		}
		if (this.name == "Mandíbula" && this.GetComponent<Renderer> ().material.color == color1) {
			this.GetComponent<Renderer> ().materials [1].color = color2;
		}
		if (stay == true)
			if(ObjectoCarga == this.transform.gameObject.name) 
				if(BotonesNav.mostrar == true)
					SumarPorcen ();
		}

	void OnTriggerEnter(Collider collider)
	{
		if (Charging == false && this.transform.parent.gameObject.layer == nivel) {
				LoadingBar.fillAmount = 0;
		}
	}

	void OnTriggerStay(Collider collider)
	{if (BotonesNav.Mensaje == true) {
			if (BotonesNav.mostrar == true) {
				LoadingBar.gameObject.SetActive (true);
				Frame.gameObject.SetActive (true);
			}
		if (ObjetCarga == false) {
			ObjetCarga = true;
			ObjectoCarga = this.transform.gameObject.name;
		}
		stay = true;
		if (this.transform.gameObject.layer == nivel) {
			CambiarColor ();
				parte.text = this.transform.gameObject.name;
				if (MouseClicked == true) {
					if (terminado == true) {
						Selected = this.transform.gameObject.name;
						terminado = false;
						Partee = this.transform.gameObject.name;
						Debug.Log (this.transform.gameObject.name);
					}
				}
		}else{
			if (this.transform.parent.gameObject.layer == nivel) {
					parte.text = this.transform.parent.gameObject.name;
					if (MouseClicked == true) {
						if (terminado == true) {
							Selected = this.transform.parent.gameObject.name;
							terminado = false;
							Partee = this.transform.parent.gameObject.name;
							Debug.Log (this.transform.parent.gameObject.name);
						}
					}
				if (BotonesNav.Mensaje == true) {
					Renderer[] childs = this.transform.parent.GetComponentsInChildren<Renderer> ();
					foreach (Renderer child in childs) {
						child.material.color = Color.blue;
					}
				}
			}else{
				if (this.transform.parent.parent.gameObject.layer == nivel) {
						parte.text = this.transform.parent.parent.gameObject.name;
						if (MouseClicked == true) {
							if (terminado == true) {
								Selected = this.transform.parent.parent.gameObject.name;
								terminado = false;
								Partee = this.transform.parent.parent.gameObject.name;
								Debug.Log (this.transform.parent.parent.gameObject.name);
							}
						}
					Transform[] childs = this.transform.parent.parent.GetComponentsInChildren<Transform> ();
					foreach (Transform child in childs) {
						if (child.childCount == 0) {
							child.GetComponent<Renderer> ().material.color = Color.blue;
						} else {
							child.GetComponentInChildren<Renderer> ().material.color = Color.blue;
						}

					}

				} else {
					if (this.transform.parent.gameObject.layer == 0) {
						if (this.transform.parent.parent.parent.gameObject.layer == nivel) {
								parte.text = this.transform.parent.parent.parent.gameObject.name;
								if (MouseClicked == true) {
									if (terminado == true) {
										Partee = this.transform.parent.parent.parent.gameObject.name;
										Selected = this.transform.parent.parent.parent.gameObject.name;
										terminado = false;
										Debug.Log (this.transform.parent.parent.parent.gameObject.name);
									}
							}
							Transform[] childs = this.transform.parent.parent.parent.GetComponentsInChildren<Transform> ();
							foreach (Transform child in childs) {
								if (child.childCount == 0) {
									child.GetComponent<Renderer> ().material.color = Color.blue;
								} else {
									Transform[] childs1 = child.transform.GetComponentsInChildren<Transform> ();
									foreach (Transform child1 in childs1) {
										if (child1.childCount == 0) {
											child1.GetComponent<Renderer> ().material.color = Color.blue;
										} else {
											child1.GetComponentInChildren<Renderer> ().material.color = Color.blue;
										}
									}
								}
							}

						} else {
							Debug.Log ("Cambiar Proyecto");
						}
					} else {
						Debug.Log ("Cambiar Proyecto");
					}
				}
			}
		}
	}
	}
	void OnTriggerExit(Collider collider)
	{
		LoadingBar.gameObject.SetActive (false);
		Frame.gameObject.SetActive (false);
		if(ObjectoCarga== this.transform.gameObject.name){
			ObjetCarga = false;
			ObjectoCarga = "";
		}
		if (this.transform.gameObject.layer == nivel) {
			ColorOriginal ();
			todofalse ();
		}else{
			if (this.transform.parent.gameObject.layer == nivel) {
				LoadingBar.fillAmount = 0;
				Renderer[] childs = this.transform.parent.GetComponentsInChildren<Renderer> ();
				foreach (Renderer child in childs) {
					child.material.color = color1;
					todofalse ();
				}
			}else{
				if (this.transform.parent.parent.gameObject.layer == nivel) {
					Transform[] childs = this.transform.parent.parent.GetComponentsInChildren<Transform> ();
					foreach (Transform child in childs) {
						if (child.childCount == 0) {
							child.GetComponent<Renderer> ().material.color = color1;
							todofalse ();
						} else {
							child.GetComponentInChildren<Renderer> ().material.color = color1;
							todofalse();
						}
					}
				} else {
					if (this.transform.parent.parent.parent.gameObject.layer == nivel) {
						Transform[] childs = this.transform.parent.parent.parent.GetComponentsInChildren<Transform> ();
						foreach (Transform child in childs) {
							if (child.childCount == 0) {
								child.GetComponent<Renderer> ().material.color = color1;
								todofalse();
							} else {
								Transform[] childs1 = child.transform.GetComponentsInChildren<Transform> ();
								foreach (Transform child1 in childs1) {
									if (child1.childCount == 0) {
										child1.GetComponent<Renderer> ().material.color = color1;
										todofalse();
									} else {
										child1.GetComponentInChildren<Renderer> ().material.color = color1;
										todofalse();
									}
								}
							}
						}
					} else {
						Debug.Log ("Cambiar Proyecto");
					}
				}
			}
		}
	}

	void CambiarColor(){
			this.GetComponent<Renderer> ().material.color = Color.blue;
	}

	void ColorOriginal(){
		this.GetComponent<Renderer>().material.color = color1;
	}

	void SumarPorcen(){
			Charging = true;
			if (LoadingBar.fillAmount <= 1) {
				LoadingBar.fillAmount += 2.0f / LoadingTime * Time.deltaTime;
				porcentaje.text = (LoadingBar.fillAmount * 100).ToString ("f0");
			}
			if (LoadingBar.fillAmount == 1.0f) {
				elegido = true;
				stay = false;
				Charging = false;
				BotonesNav.presionado = false;
				BotonesNav.yapaso = false;
				terminado = true;
				info = Quiz.TraerUno (Partee);
				InforOcultar (true);
				Informacion.text = info[0];
				Debug.Log ("DartaBase");
			}
		}

	void todofalse(){
		stay = false;
		Charging = false;
		porcentaje.text = "0";
	}

	public void InforOcultar(bool estado){
		Informacion.gameObject.SetActive (estado);
	}
}

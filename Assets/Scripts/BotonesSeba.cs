using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesSeba : MonoBehaviour
{	
	public static GameObject temp_file = null;
	private Button boton;
	public Camera camara;
	public GameObject canvasmenuprincipal;

	private bool character = false, car = false, starship = false, walle = false;
	public static bool seleccionado = false;
	public static string newfilepath;
	public static int rangoCamera = 0;


    void Start()
    {
		boton = this.GetComponent<Button>();
    }

	public void OnTriggerEnter(Collider collider)
	{
		switch (collider.name) {
		case "Character":
			Interaccion.ModificarComponentesCanvas (false, ref canvasmenuprincipal);
			character = true;
			seleccionado = true;
			break;
		case "Car":
			Interaccion.ModificarComponentesCanvas (false, ref canvasmenuprincipal);
			car = true;
			seleccionado = true;
			break;
		case "Starship":
			Interaccion.ModificarComponentesCanvas (false, ref canvasmenuprincipal);
			starship = true;
			seleccionado = true;
			break;
		case "Wall E":
			Interaccion.ModificarComponentesCanvas (false, ref canvasmenuprincipal);
			walle = true;
			seleccionado = true;
			break;
		case "Button_Exit":
			if (seleccionado == true) {
				DestruirArchivoActual (ref temp_file);
				seleccionado = false;
				Interaccion.ModificarComponentesCanvas (true, ref canvasmenuprincipal);
			} else {
				if(Interaccion.dedos_extendidos == 1)
                     SceneManager.LoadScene(7);
			}
			break;
		case "Button_ZoomIn":
			ZoomIn ();
			break;
		case "Button_ZoomOut":
			ZoomOut ();
			break;
		}
	}

	public void Update (){
		if (seleccionado == true) {
			if (character == true) {
				CrearObjeto (ref temp_file, "CharacterCatTxt/CharacterCatTxt");
				character = false;
				rangoCamera = 0;
			} else if (car == true) {
				CrearObjeto (ref temp_file, "Car/Car");
				car = false;
				rangoCamera = 0;
			} else if (starship == true) {
				CrearObjeto (ref temp_file, "Nave/Nave");
				starship = false;
				rangoCamera = 0;
			} else {
				if (walle == true) {
					CrearObjeto (ref temp_file, "Wall E/Wall E");
					walle = false;
					rangoCamera = 0;
				}
			}
		}
	}    
	public void CrearObjeto(ref GameObject archivo, string filename)
    {
		archivo = GameObject.Instantiate (Resources.Load (filename, typeof(GameObject))) as GameObject;
		archivo.GetComponent<Transform> ().position = new Vector3 (0f, 2f, 1f);
		archivo.name = "temp_file";
		BoxCollider bc = archivo.AddComponent <BoxCollider>() as BoxCollider;
		archivo.GetComponent<BoxCollider> ().isTrigger = true;
		Rigidbody rb = archivo.AddComponent<Rigidbody>() as Rigidbody;
		archivo.GetComponent<Rigidbody> ().useGravity = false;

    }
	public void DestruirArchivoActual(ref GameObject archivo)
	{
			Destroy(GameObject.Find("temp_file"));
			archivo = null;
	}
	public void Salir(){
		Application.Quit();
	}
	public void ZoomIn(){
		if (rangoCamera < 3) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y, poscamara.z + 1f);
			rangoCamera++;
		}
	}
	public void ZoomOut(){
		if (rangoCamera > -3) {
			Vector3 poscamara = camara.GetComponent<Transform> ().position;
			camara.GetComponent<Transform> ().position = new Vector3 (poscamara.x, poscamara.y, poscamara.z - 1f);
			rangoCamera--;
		}

	}

}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesSeba : MonoBehaviour
{
    #region Variables
    public static GameObject temp_file = null;
    public Camera camara;
    public GameObject canvasMenuPrincipal;

    public static bool seleccionado = false;
    public static string newfilepath;
    public static int rangoCamera = 0;

    string objetoSelec = null;
    #endregion

    void Start()
    {
        canvasMenuPrincipal = GameObject.Find("CanvasMenuPrincipal");
    }

    public void OnTriggerEnter(Collider collider)
    {
        switch (collider.name)
        {
            case "Button_Exit":
                {
                    objetoSelec = null;
                    if (seleccionado == true)
                    {
                        DestruirArchivoActual(ref temp_file);
                        seleccionado = false;
                        Interaccion.ModificarComponentesCanvas(true, ref canvasMenuPrincipal);  // Reactivo el canvas de Seleccion de Objeto
                    }
                    else
                    {
                        if (Interaccion.dedos.Extended().Count == 0)
                            SceneManager.LoadScene(7);
                    }
                }
                break;
            case "Button_ZoomIn":
                ZoomIn();
                break;
            case "Button_ZoomOut":
                ZoomOut();
                break;
            case "Character":
            case "Car":
            case "Starship":
            case "Wall E":
                {
                    if (!seleccionado)
                    {
                        Interaccion.ModificarComponentesCanvas(false, ref canvasMenuPrincipal); // Desactivo el canvas de Seleccion de Objeto
                        objetoSelec = collider.name;
                    }
                }
                break;
        }
    }

    public void Update()
    {
        if (seleccionado == false && objetoSelec != null)   // Si aun no hay un objeto en pantalla, pero ya se selecciono uno por botones
        {

            rangoCamera = 0;                            // Reestablezco el rango del zoom a 0
            seleccionado = true;

            switch (objetoSelec)
            {
                case "Character":
                    CrearObjeto(ref temp_file, "CharacterCatTxt/CharacterCatTxt");
                    break;
                case "Car":
                    CrearObjeto(ref temp_file, "Car/Car");
                    break;
                case "Starship":
                    CrearObjeto(ref temp_file, "Nave/Nave");
                    break;
                case "Wall E":
                    CrearObjeto(ref temp_file, "Wall E/Wall E");
                    break;
                default:
                    seleccionado = false;
                    break;
            }
        }
    }
    #region Funciones
    #region Manejo de Archivos
    public void CrearObjeto(ref GameObject archivo, string filename)
    {
        archivo = GameObject.Instantiate(Resources.Load(filename, typeof(GameObject))) as GameObject;
        archivo.GetComponent<Transform>().position = new Vector3(0f, 2f, 1f);
        archivo.name = "temp_file";
        BoxCollider bc = archivo.AddComponent<BoxCollider>() as BoxCollider;    // Agrego un BoxCollider para que se pueda agarrar
        archivo.GetComponent<BoxCollider>().isTrigger = true;
        Rigidbody rb = archivo.AddComponent<Rigidbody>() as Rigidbody;          // Agrego un Rigidbody por sus fisicas
        archivo.GetComponent<Rigidbody>().useGravity = false;

    }
    public void DestruirArchivoActual(ref GameObject archivo)
    {
        Destroy(GameObject.Find("temp_file"));
        archivo = null;
    }
    #endregion
    #region Zoom
    public void ZoomIn()
    {
        if (rangoCamera < 3)
        {
            Vector3 poscamara = camara.GetComponent<Transform>().position;
            camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z + 1f);
            rangoCamera++;
        }
    }
    public void ZoomOut()
    {
        if (rangoCamera > -3)
        {
            Vector3 poscamara = camara.GetComponent<Transform>().position;
            camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z - 1f);
            rangoCamera--;
        }

    }
    #endregion
    #endregion
}

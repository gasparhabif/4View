using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;
using WindowsInput;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    #region Info Escenas
    // 0- EarthMotion
    // 1- Visualizador
    // 2- Menu Rolling
    // 3- Colores Rolling
    // 4- Juego Rolling
    // 5- Gano Rolling
    // 6- Esqueleto
    // 7- MenuPrincipal
    #endregion
    #region Variables
    private string boton = "";
	private Controller controller;

	public HandController controlador;
	public Hand firstHand;

	Frame frameActual;
	Frame frameAnt;

	int cerrarapp = 0, tiempoentregestos = 0;
    #endregion

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
            switch (boton)
            {
                case "BodyMotion":
                    break;
                case "EarthMotion":
                    SceneManager.LoadScene(0);
                    break;
                case "VisualMotion":
                    SceneManager.LoadScene(1);
                    break;
                case "RollingRoshakazu":
                    SceneManager.LoadScene(4);
                    break;
            }
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
        boton = this.gameObject.name;
	}
}

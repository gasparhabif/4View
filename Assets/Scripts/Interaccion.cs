using UnityEngine;
using Leap;

public class Interaccion : MonoBehaviour {

    private Controller controller;
    public HandController controlador;
    public Hand firstHand;

    private GameObject archivo;
    private GameObject menu_mano;
    public GameObject canvas_mano = null;

    Frame frameActual;

    string pos = "Centro";
	int contador = 0, giroautomatico = 500;
	public static bool zoom = false;

	public static int dedos_extendidos = 0;
	void Start () {
        controller = controlador.GetLeapController();
	}
	
    void Update()
    {
        frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
        if (frameActual.Hands.Count > 0)
        {
            HandList hands = frameActual.Hands;
            firstHand = hands[0];
            if (giroautomatico == 0)
            {
                giroautomatico = 500;
                BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
		FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
		dedos_extendidos = 0;
        for (int i = 0; i < dedos.Extended().Count; i++)
        { 
            dedos_extendidos++;
        }

		if (BotonesSeba.temp_file != null) {
			try {
				Vector posMano = firstHand.PalmPosition;
				if (posMano.y >= 300)
					pos = "Norte";
				if (posMano.y <= 150)
					pos = "Sur";
				if (posMano.x <= -50)
					pos = "Oeste";
				if (posMano.x >= 90)
					pos = "Este";
			} catch {
				pos = "Centro";
			}

			if (dedos_extendidos == 1) {
				Mover (pos);
				contador++;
			}
            if (frameActual.Hands.Count == 0){
                if (giroautomatico > 0){
                    giroautomatico -= 1;
                }
                if (giroautomatico <= 0){
                    BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(new Vector3(0f, BotonesSeba.temp_file.transform.eulerAngles.y + 0.5f, 0f));
                }
            }
        }
		zoom = false;
		foreach (var mano in controller.Frame().Hands) {
			if (BotonesSeba.seleccionado == true) {
				if (mano.IsLeft) {
					float grados = 140f * Mathf.Deg2Rad;
					if (mano.PalmNormal.Roll > grados && canvas_mano.GetComponent<Canvas> ().enabled == false) {
						ModificarComponentesCanvas (true,ref canvas_mano);
					} else if (canvas_mano != null && mano.PalmNormal.Roll <= grados) {
						ModificarComponentesCanvas (false, ref canvas_mano);
					}
				}
				if (frameActual.Hands.Count < 2) {
					ModificarComponentesCanvas (false, ref canvas_mano);
				}
			} else {
				ModificarComponentesCanvas (false, ref canvas_mano);
			}
		}
    }

	public static void ModificarComponentesCanvas(bool valor, ref GameObject canvas)
	{
		canvas.GetComponent<Canvas> ().enabled = valor;
		int cant = canvas.GetComponentsInChildren<BoxCollider> ().Length;
		for (int i = 0; i < cant; i++) {
			canvas.GetComponentsInChildren<BoxCollider> () [i].enabled = valor;
		}
	}

    void Mover(string Pos)
    {
        float x, y, z;
		x = BotonesSeba.temp_file.transform.rotation.eulerAngles.x;
		y = BotonesSeba.temp_file.transform.rotation.eulerAngles.y;
		z = BotonesSeba.temp_file.transform.rotation.eulerAngles.z;

        if (contador == 2)
        {
            switch (Pos)
            {
                case "Centro":
                    break;
                case "Norte":
				BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(x, y, z + 1.5f);
                    break;
                case "Sur":
				BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(x, y, z - 1.5f);
                    break;
                case "Este":
				BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(x, y + 1.5f, z);
                    break;
                case "Oeste":
				BotonesSeba.temp_file.transform.rotation = Quaternion.Euler(x, y - 1.5f, z);
                    break;
            }
            contador = 0;
        }

    }
    
}
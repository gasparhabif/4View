using UnityEngine;
using Leap;
using System.Collections;

public class Rotacion : MonoBehaviour {


	private Controller controller;
	public HandController controlador;
	private HandModel modelos_manos;
	public Hand firstHand;

	public GameObject esqueleto;

	Frame frameActual;

	string pos = "Centro";
	int contador = 0;

	void Start()
	{
		controller = controlador.GetLeapController();
	}

	// Update is called once per frame
	void Update()
	{
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

		try
		{
			Vector posMano = firstHand.PalmPosition;
			if (posMano.x <= -50)
				pos = "Oeste";
			if (posMano.x >= 90)
				pos = "Este";       
		}
		catch
		{
			//Debug.Log("Coloque las manos dentro del alcance del Leap");
			pos = "Centro";
		}

		if (dedos_extendidos == 3)
		{
			Mover(pos);
			contador++;
		}

	}

	void Mover(string Pos)
	{
		float x, y, z;
		x = esqueleto.transform.rotation.eulerAngles.x;
		y = esqueleto.transform.rotation.eulerAngles.y;
		z = esqueleto.transform.rotation.eulerAngles.z;

		if (contador == 2)
		{
			switch (Pos)
			{
			case "Centro":
				break;
			case "Este":
				esqueleto.transform.rotation = Quaternion.Euler(x, y+1f, z);
				break;
			case "Oeste":
				esqueleto.transform.rotation = Quaternion.Euler(x, y - 1f, z);
				break;
			}
			contador = 0;
		}

	}
}

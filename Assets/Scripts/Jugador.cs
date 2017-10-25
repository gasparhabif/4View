using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Leap;
using WindowsInput;

public class Jugador : MonoBehaviour
{
	Frame frameActual;
	 

	private Controller controller;
	private HandModel modelos_manos;

	public HandController controlador;
	public Button Pausar;
	public Hand firstHand;
	public Vector3 movement;
	public Material Azul,Verde,Rojo,Celeste,Violeta,VerdeClaro,VerdeOscuro,Naranja,Amarillo;
	public GameObject auto, puertaizq, puertader;
	public Text Pantalla;

	public int Puntos = 0;
    public bool perdio = false, salto = false, bajada = false;
	public float speed = 0.15f, hMovement,maxSteerAngle = 15;

	string pos = "Centro",posAnt="";
	bool masrapido = false, maslento = false;//variable creada
	int contadoraire = 0, contadorrapido = 0, Puntaje=0, contadorlento = 0,contparalelo=0;//variablecreada
	float x, y, z;

	void Start(){

		controller = controlador.GetLeapController();

		string ColorFinal = PlayerPrefs.GetString ("color","Violeta");
		switch(ColorFinal){
		case "Azul":
			auto.GetComponent<Renderer> ().material.color = Azul.color;
			puertaizq.GetComponent<Renderer> ().material.color = Azul.color;
			puertader.GetComponent<Renderer> ().material.color = Azul.color;
			break;
		case "Celeste":
			auto.GetComponent<Renderer> ().material.color = Celeste.color;
			puertaizq.GetComponent<Renderer> ().material.color = Celeste.color;
			puertader.GetComponent<Renderer> ().material.color = Celeste.color;
			break;
		case "Verde":
			auto.GetComponent<Renderer> ().material.color = Verde.color;
			puertaizq.GetComponent<Renderer> ().material.color = Verde.color;
			puertader.GetComponent<Renderer> ().material.color = Verde.color;
			break;
		case "Rojo":
			auto.GetComponent<Renderer> ().material.color = Rojo.color;
			puertaizq.GetComponent<Renderer> ().material.color = Rojo.color;
			puertader.GetComponent<Renderer> ().material.color = Rojo.color;
			break;
		case "Violeta":
			auto.GetComponent<Renderer> ().material.color = Violeta.color;
			puertaizq.GetComponent<Renderer> ().material.color = Violeta.color;
			puertader.GetComponent<Renderer> ().material.color = Violeta.color;
			break;
		case "VerdeClaro":
			auto.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puertaizq.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			puertader.GetComponent<Renderer> ().material.color = VerdeClaro.color;
			break;
		case "VerdeOscuro":
			auto.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puertaizq.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			puertader.GetComponent<Renderer> ().material.color = VerdeOscuro.color;
			break;
		case "Naranja":
			auto.GetComponent<Renderer> ().material.color = Naranja.color;
			puertaizq.GetComponent<Renderer> ().material.color = Naranja.color;
			puertader.GetComponent<Renderer> ().material.color = Naranja.color;
			break;
		case "Amarillo":
			auto.GetComponent<Renderer> ().material.color = Amarillo.color;
			puertaizq.GetComponent<Renderer> ().material.color = Amarillo.color;
			puertader.GetComponent<Renderer> ().material.color = Amarillo.color;
			break;
		}
		PlayerPrefs.Save ();
	}

    void FixedUpdate()
    {
        hMovement = Input.GetAxis("Horizontal")*0.3f;

        Vector3 origin = transform.position;
		string tag;
        RaycastHit hitInfo;
		RaycastHit hitInfo2;
		RaycastHit hitInfo3;
		RaycastHit hitInfo4;

        if (contadorrapido == 90)
        {
            masrapido = false;
            contadorrapido = 0;
        }
        if (contadoraire == 25)//cambiado valor 15 por 25
        {
            salto = false;
            contadoraire = 0;
        }
		if (contadorlento == 120) //condicion agregada
		{
			maslento = false;
			contadorlento = 0;
		}
			
		if (Physics.Raycast(origin, Vector3.forward, out hitInfo2, 0.9f))
		{
			if (hitInfo2.collider.gameObject.tag == "Cono")
			{
				perdio = true;
			}

		}
		if (Physics.Raycast(origin, Vector3.right, out hitInfo3, 0.52f))
		{
			if (hitInfo3.collider.gameObject.tag == "Cono")
			{
				perdio = true;
			}

		}
		if (Physics.Raycast(origin, Vector3.left, out hitInfo4, 0.52f))
		{
			if (hitInfo4.collider.gameObject.tag == "Cono")
			{
				perdio = true;
			}

		}

        if (salto == false && bajada == false)
        {
            if (Physics.Raycast(origin, Vector3.down, out hitInfo) == false)
            {
                perdio = true;
            }
            else
            {
				tag = hitInfo.collider.gameObject.tag;
				switch (tag) {
				case "Saltos":
					salto = true;
					bajada = true;
					break;
				case "SpeedBoost":
					masrapido = true;
					break;
				case "SlowBoost":
					maslento = true;
					break;
				case "Satel":
					if(masrapido==false){
						perdio=true;
					}
					break;
				case "Gano":
					this.GetComponent<MenuRolling> ().Iniciar (6);
					break;
				}
            }

        }

        if (perdio == false)
        {
            if (salto == false)
            {
                movement = new Vector3(hMovement, -0.75f, 1f);
                if (this.GetComponent<CharacterController>().isGrounded == true)
                {
                    bajada = false;
                }
            }
            else
            {
                movement = new Vector3(hMovement, 1f, 1f);
            }
            if (masrapido == true)
            {
                movement = new Vector3(hMovement, 0f, 2f);
            }
			if (maslento == true) //codigo para el slowboost
			{
				movement = new Vector3 (hMovement, 0f, 0.75f);
			}
        }
        else
        {
            movement = new Vector3(hMovement, -3f, 1f);
            this.GetComponent<MenuRolling>().Iniciar(6);
        }
        ChequearScore(Puntaje, Puntos);
        this.GetComponent<CharacterController>().Move(movement * speed);
		float rotacion = Input.GetAxis("Horizontal") * maxSteerAngle;
		this.GetComponent<Transform>().localEulerAngles = new Vector3(0, 180+ rotacion, 0);
		        if (masrapido == true)
        {
            contadorrapido++;
        }
        if (salto == true)
        {
            contadoraire++;
        }
		if (maslento == true)//condicion agregada
		{
			contadorlento++;
		}
			
		frameActual = controller.Frame();
		 
		 

		if (frameActual.Hands.Count > 0)
		{
			HandList hands = frameActual.Hands;
			firstHand = hands[0];
		}
			
		try
		{
			//x= auto.transform.position.x;
			//y= auto.transform.position.y;
			//z= auto.transform.position.z;
			Vector posMano = firstHand.PalmPosition;
			if (posMano.x <= -50)
			{
				pos = "Oeste";
				Debug.Log("OESTE");
				InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
			}
			if (posMano.x >= 90)
			{
				pos = "Este";
				Debug.Log("ESTE");
				InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_D);
			}
		}
		catch
		{
			pos = "Centro";
		}
    }

    void OnTriggerEnter(Collider colision)
    {
        if (colision.tag == "Recolectables")
        {
            Puntos++;
            colision.gameObject.SetActive(false);
        }
    }
    void ChequearScore(int PrevScore, int Score)
    {
        if (PrevScore != Score)
        {
            PrevScore = Score;
            Pantalla = (Text)GameObject.Find("Score").GetComponent<Text>();
            Pantalla.text = "Puntos: " + Score;
        }
    }

	bool DireccionAnterior()
	{
		if (pos == posAnt)
		{
			contparalelo++;
			return true;
		}
		else
		{
			posAnt = pos;
			contparalelo = 0;
			return false;
		}
	}
}
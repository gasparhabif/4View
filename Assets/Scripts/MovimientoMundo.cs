using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Leap;
using WindowsInput;
using UnityEngine.SceneManagement;

namespace WPM
{

    public class MovimientoMundo : MonoBehaviour
    {
        #region Variables
        private Controller controller;
        public HandController controlador;
        private HandModel modelos_manos;
        public static int rangoCamera = 0;

        public GameObject world, manos, canvas_mano = null;
        public Canvas can, quiz;
        public Text polo;
        public Camera camara;
        private Button boton;

        WorldMapGlobe map;
        Frame frameActual;

        string pos = "Centro", posAnt = "", nombreBoton = "", nombreBotonAnterior = "", opcionSelect = "", funcion = "", funcionAnterior="";
        static bool nivel = false, opciones = false, respondio = false, selec = false;
        public static int dedos_extendidos = 0;
        static int opcionCorrecta = 0, paisRandom = 0, tiempoEspera = 350, puntos = 0;
        int contparalelo = 0;
        int topeN = 400,
            topeS = 200,
            topeE = 90,
            topeO = -50;
        public Hand firstHand, secondHand;
        #endregion
        #region Start
        void Start()
        {
            controller = controlador.GetLeapController();
            BotonesZoom(true);
            //ActivacionQuiz(false);
            boton = this.GetComponent<Button>();
            map = WorldMapGlobe.instance;
        }
        #endregion
        #region Trigger
        void OnTriggerEnter(Collider collider)
        {
            nombreBoton = this.tag;
			if (nombreBoton == "Zoom In" || nombreBoton == "Zoom Out")
                    nombreBoton = nombreBotonAnterior;
			if (nombreBoton == "Exit"&&dedos_extendidos==5)
				SceneManager.LoadScene (7);
            boton.onClick.Invoke();
        }
        #endregion
        void Update()
        {
            #region Datos
            manos = GameObject.Find("CleanRobotRightHand(Clone)");
            frameActual = controller.Frame(); //obtengo la informacion del leap de un frame
            HandList hands;
            FingerList dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
            dedos_extendidos = 0;
            #endregion
            #region ManosYDedos
            if (frameActual.Hands.Count > 0)
            {
                hands = frameActual.Hands;
                firstHand = hands[0];
                secondHand = hands[1];
            }
            for (int i = 0; i < dedos.Extended().Count; i++)
                dedos_extendidos++;//cuento todos los dedos que estan extendidos de la FingerList dedos                                
            #endregion
            #region Menu
            foreach (var mano in frameActual.Hands)
            {
                if (mano.IsLeft == true)
                {
                    float grados = 140f * Mathf.Deg2Rad;
                    if (mano.PalmNormal.Roll > grados && canvas_mano.GetComponent<Canvas>().enabled == false)
                    {
                        ActivacionMenu(true);
                        selec = false;
                        nombreBoton = "";
                        funcion = "";
                        funcionAnterior = "";
                        BotonesZoom(true);
                        //ActivacionQuiz(false);
                    }
                    else
                        if (canvas_mano != null && mano.PalmNormal.Roll <= grados)
                        ActivacionMenu(false);
                }
                if (frameActual.Hands.Count < 2)
                    ActivacionMenu(false);
            }
            #endregion

            switch ("Fly")//funcion
            {
                #region Fly
                case "Fly":
                    {
                        funcion="Fly";
                        funcionAnterior = "Fly";
                        nombreBoton = "Fly";
                        //ActivacionQuiz(false);
                        BotonesZoom(true);
                        ActivacionMenu(true);
                        map.showCountryNames = true;
                        DireccionDelMovimiento();
                    }
                    break;
                #endregion

            }

        }
        #region Funciones
            #region DireccionDelMovimiento
        public void DireccionDelMovimiento()
        {
            try
            {
                Vector posMano = firstHand.PalmPosition;
                if (posMano.y >= topeN)
                    pos = "Norte";
                if (posMano.y <= topeS)
                    pos = "Sur";
                if (posMano.x <= topeO)
                    pos = "Oeste";
                if (posMano.x >= topeE)
                    pos = "Este";
                if (posMano.y >= topeN && posMano.x >= topeE)
                    pos = "NorEste";
                if (posMano.y >= topeN && posMano.x <= topeO)
                    pos = "NorOeste";
                if (posMano.y <= topeS && posMano.x >= topeE)
                    pos = "SudEste";
                if (posMano.y <= topeS && posMano.x <= topeO)
                    pos = "SudOeste";
                if (posMano.y < topeN && posMano.y > topeS && posMano.x < topeE && posMano.x > topeO)
                    pos = "Centro";
            }
            catch { pos = "Centro"; }
            if (dedos_extendidos == 1)
                Mover(pos);
            else
                pos = "Centro";
        }
        #endregion
            #region MoverMundo
        void Mover(string Pos)
        {
            switch (Pos)
            {
                case "Centro":
                    break;
                case "Norte":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_W);
                    break;
                case "Sur":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_S);
                    break;
                case "Este":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_D);
                    break;
                case "Oeste":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
                    break;
                case "NorEste":
                    {
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_W);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_D);
                    }
                    break;
                case "NorOeste":
                    {
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_W);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
                    }
                    break;
                case "SudEste":
                    {
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_S);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_D);
                    }
                    break;
                case "SudOeste":
                    {
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_S);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
                    }
                    break;
            }

            ModificarTexto(Pos);

        }
        #endregion
            #region EditTexto
            void ModificarTexto(string texto)
            {
                polo.text = texto;
                if (DireccionAnterior() == true)
                {
                    if (contparalelo <= 20)
                        can.GetComponent<CanvasGroup>().alpha = 1;
                    else
                        if (contparalelo <= 50)
                        can.GetComponent<CanvasGroup>().alpha = 0.9f;
                    else
                            if (contparalelo <= 70)
                        can.GetComponent<CanvasGroup>().alpha = 0.8f;
                    else
                                if (contparalelo <= 90)
                        can.GetComponent<CanvasGroup>().alpha = 0.7f;
                    else
                                    if (contparalelo <= 110)
                        can.GetComponent<CanvasGroup>().alpha = 0.6f;
                    else
                                        if (contparalelo <= 130)
                        can.GetComponent<CanvasGroup>().alpha = 0.5f;
                    else
                                            if (contparalelo <= 150)
                        can.GetComponent<CanvasGroup>().alpha = 0.4f;
                    else
                                                if (contparalelo <= 170)
                        can.GetComponent<CanvasGroup>().alpha = 0.3f;
                    else
                                                    if (contparalelo <= 190)
                        can.GetComponent<CanvasGroup>().alpha = 0.2f;
                    else
                                                        if (contparalelo <= 210)
                        can.GetComponent<CanvasGroup>().alpha = 0.1f;
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
            #endregion
		#region ZoomMundo
            #region Zoom
            public void ZoomIn()
            {
                if (rangoCamera < 45 && dedos_extendidos == 5)
                {
                    Vector3 poscamara = camara.GetComponent<Transform>().position;
                    camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z + 0.1f);
                    rangoCamera++;
                }
            }
            public void ZoomOut()
            {
                if (rangoCamera > 0 && dedos_extendidos == 5)
                {
                    Vector3 poscamara = camara.GetComponent<Transform>().position;
                    camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z - 0.1f);
                    rangoCamera--;
                }

            }
            #endregion
            #region BotonesZoom
            public void BotonesZoom(bool Estado)
            {
                try
                {
                    if (Estado)
                    {
                        can.GetComponent<Canvas>().enabled = true;
                        can.GetComponentsInChildren<BoxCollider>()[0].enabled = true;
                        can.GetComponentsInChildren<BoxCollider>()[1].enabled = true;
                    }
                    else
                    {
                        can.GetComponent<Canvas>().enabled = false;
                        can.GetComponentsInChildren<BoxCollider>()[0].enabled = false;
                        can.GetComponentsInChildren<BoxCollider>()[1].enabled = false;
                    }
                }
                catch { }
            }
            #endregion
            #region ActivarMenu
            public void ActivacionMenu(bool Estado)
            {
                if (Estado == true)
                {
                    canvas_mano.GetComponent<Canvas>().enabled = true;
                    canvas_mano.GetComponentsInChildren<BoxCollider>()[0].enabled = true;
                }
                else
                {
                    canvas_mano.GetComponent<Canvas>().enabled = false;
                    canvas_mano.GetComponentsInChildren<BoxCollider>()[0].enabled = false;
                }
            }
			#endregion
		#endregion
		#endregion
    }
}
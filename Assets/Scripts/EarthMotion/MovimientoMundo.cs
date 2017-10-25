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

        public static int rangoCamera = 0;

        public GameObject world;
        public Canvas can, quiz;
        public static Camera camara;

        WorldMapGlobe map;
        Frame frameActual;

        GameObject polo;

        public static FingerList dedos;

        string pos = "Centro";
        int topeN = 400,
            topeS = 200,
            topeE = 90,
            topeO = -50;
        public Hand firstHand;
        #endregion
        #region Start
        void Start()
        {
            controller = controlador.GetLeapController();
            map = WorldMapGlobe.instance;
            polo = GameObject.Find("Polo");
        }
        #endregion

        void Update()
        {
            #region Manos y Dedos
            frameActual = controller.Frame(); // Obtengo la informacion del leap de un frame
            if (frameActual.Hands.Count > 0)
            {
                dedos = frameActual.Fingers; //obtengo todos los dedos detectados en un frame
                HandList hands;
                hands = frameActual.Hands;
                firstHand = hands[0];
            }
            #endregion

            switch (BotonesEarthMotion.nombreBoton)//funcion
            {
                #region Fly
                case "Fly":
                    {
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
            if (firstHand!=null)
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
                if (dedos.Extended().Count == 1)
                    Mover(pos);
                else
                    pos = " ";
                polo.GetComponent<Text>().text = pos;
            }
        }
        #endregion
        #region MoverMundo
        void Mover(string Pos)
        {
            switch (Pos)
            {
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

        }
        #endregion
        #region Zoom
        public static void ZoomIn()
        {
            if (rangoCamera < 45 && dedos.Extended().Count == 5)
            {
                Vector3 poscamara = camara.GetComponent<Transform>().position;
                camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z + 0.1f);
                rangoCamera++;
            }
        }
        public static void ZoomOut()
        {
            if (rangoCamera > 0 && dedos.Extended().Count == 5)
            {
                Vector3 poscamara = camara.GetComponent<Transform>().position;
                camara.GetComponent<Transform>().position = new Vector3(poscamara.x, poscamara.y, poscamara.z - 0.1f);
                rangoCamera--;
            }

        }
        #endregion


        #endregion
    }
}
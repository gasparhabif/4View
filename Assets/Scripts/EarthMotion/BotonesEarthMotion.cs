using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WPM
{
    public class BotonesEarthMotion : MonoBehaviour
    {

        #region Variables
        [HideInInspector]
        public static string nombreBoton = "";
        [HideInInspector]
        public bool modoSeleccionado = false;
        [HideInInspector]
        static GameObject canvasPrincipal, canvasModos, canvasQuiz;
        #endregion

        // Use this for initialization
        void Start()
        {
            canvasPrincipal = GameObject.Find("CanvasPrincipal");
            canvasModos = GameObject.Find("CanvasModos");
            canvasQuiz = GameObject.Find("CanvasQuiz");
            modoMenu(true);
        }

        #region Trigger
        public void OnTriggerEnter(Collider collider)
        {
            nombreBoton = this.name;
            if (modoSeleccionado && MovimientoMundo.dedos.Extended().Count == 5 && nombreBoton == "Exit")
            {
                modoSeleccionado = false;
                modoMenu(true);
            }
            else
            {
                if (MovimientoMundo.dedos.Extended().Count == 0)
                    SceneManager.LoadScene(7);
            }

            if (MovimientoMundo.dedos.Extended().Count == 5)
            {
                switch (nombreBoton)
                {
                    case "ZoomIn":
                        MovimientoMundo.ZoomIn();
                        break;
                    case "ZoomOut":
                        MovimientoMundo.ZoomOut();
                        break;
                    case "Fly":
                        modoSeleccionado = true;
                        canvasModos.SetActive(false);
                        canvasPrincipal.SetActive(true);
                        break;
                    case "Flag Quiz":
                    case "Name Quiz":
                        modoSeleccionado = true;
                        modoMenu(false);
                        break;
                    default:
                        modoMenu(true);
                        break;
                }
            }
        }
        #endregion
        #region Funciones
        public static void modoMenu(bool estado)
        {
            if (estado)
            {
                canvasPrincipal.SetActive(false);
                canvasQuiz.SetActive(false);
                canvasModos.SetActive(true);
            }
            else
            {
                canvasPrincipal.SetActive(true);
                canvasQuiz.SetActive(true);
                canvasModos.SetActive(false);
            }
        }
        #endregion

    }
}


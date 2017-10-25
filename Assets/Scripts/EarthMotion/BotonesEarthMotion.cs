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
        GameObject canvasPrincipal, canvasModos;
        #endregion

        // Use this for initialization
        void Start()
        {
            canvasPrincipal = GameObject.Find("CanvasPrincipal");
            canvasModos = GameObject.Find("CanvasModos");
        }

        #region Trigger
        public void OnTriggerEnter(Collider collider)
        {
            nombreBoton = this.name;
            if (modoSeleccionado && MovimientoMundo.dedos.Extended().Count == 5 && nombreBoton == "Exit")
                modoSeleccionado = false;
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
                    case "Flag Quiz":
                    case "Name Quiz":
                        modoSeleccionado = true;
                        canvasPrincipal.SetActive(true);
                        canvasModos.SetActive(false);
                        break;
                    default:
                        canvasModos.SetActive(true);
                        break;
                }
            }
        }
        #endregion
    }
}


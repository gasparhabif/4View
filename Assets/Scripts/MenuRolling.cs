
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuRolling : MonoBehaviour 
{
	public void Iniciar(int nivel)
	{
		 SceneManager.LoadScene (nivel);
	}
	public void Salir()
	{
		 SceneManager.LoadScene (3);
	}

}

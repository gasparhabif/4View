
using UnityEngine;
using System.Collections;

public class MenuRolling : MonoBehaviour 
{
	public void Iniciar(int nivel)
	{
		Application.LoadLevel (nivel);
	}
	public void Salir()
	{
		Application.LoadLevel (3);
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class Quiz : MonoBehaviour {

	public static IDbConnection cn;
	public static IDbCommand cmd;

	// Use this for initialization
	void Start () {
		ConnectDb ();
		cn.Open ();
		IsConnected ();
		cmd = cn.CreateCommand();
	}
	void Update () {
	
	}

	void ConnectDb(){
		string conn = "URI=file:" + Application.dataPath + "/ParaUnity.db";//
		cn= new SqliteConnection (conn);
	}

	void IsConnected(){
		if(cn.State.ToString() == "Open"){
			Debug.Log("open");
		}
	}

	public static void Prueba()
	{
		string SQLQuery = "Select * from Preguntas where Parte = 'Caja Toráxica'";
		cmd.CommandText = SQLQuery;
		DataTable dt = new DataTable ();
		dt.Load (cmd.ExecuteReader ());
		if (dt.Rows.Count == 0) {
			Debug.Log ("Muy Mal");
		} else {
			Debug.Log ("me recibo de ingeniero");
			foreach (DataRow row in dt.Rows) {
				Debug.Log (row [3]);
			}
		}
	}

	public static int[] randomNum(int cant){
		int[] numeros = new int[cant];
		bool comprobar = false;
		for (int j = 0; j < cant; j++) {
			numeros [j] = -1;
		}
		for (int i = 0; i < cant; i++) {
			int r = Random.Range (0, cant);
			for (int k = 0; k < i; k++) {
				if (numeros [k] == r && comprobar == false) {
					i--;
					comprobar = true;
				}
			}
			if (comprobar == false) {
				numeros [i] = r;
			} else {
				comprobar = false;
			}
		}
		return numeros;
	}

	public static string[,] TraerInfo(string Requer1, string Requer2, string nivel, int cant){
		int[] numeros = new int[cant];
		string[,] Reques = new string[2, cant];
		if (nivel == "Principiante")
			numeros = randomNum (cant);
		else if (nivel == "Intermedio")
			numeros = randomNum (cant);
		else if (nivel == "Avanzado")
			numeros = randomNum (cant);
		string SQLQuery = "Select * from Preguntas where Nivel ='" + nivel + "'";//Trae toda la info de la base de datos donde el nivel sea igual al seleccionado
		cmd.CommandText = SQLQuery;
		DataTable dt = new DataTable ();
		dt.Load (cmd.ExecuteReader ());
		if (dt.Rows.Count == 0) {
			return null;
		} else {
			Debug.Log(dt.Rows.Count);
			Debug.Log(dt.Columns.Count);
			Debug.Log (cant);
			for(int j = 0; j < cant; j++){//En lugar de cant dt.rows.count
				for(int i = 3; i < 5; i++){
					Reques [i-3, j] = dt.Rows [numeros[j]][i].ToString();
				}
			}
			return Reques;
		}

	}

	public static string[] TraerUno(string parte){
		string[] Reques = new string[3];
		string SQLQuery = "Select * from Preguntas where Parte ='" + parte + "'";
		cmd.CommandText = SQLQuery;
		DataTable dt = new DataTable ();
		dt.Load (cmd.ExecuteReader ());
		if (dt.Rows.Count == 0) {
			return null;
		} else {
			Debug.Log(dt.Rows.Count);
			Debug.Log(dt.Columns.Count);
			for (int i = 0; i < 3; i++) {
				Reques [i] = dt.Rows [0][i+2].ToString();
			}
			Debug.Log ("infoooooooooooooooooooo");
			Debug.Log (Reques[0]);
			Debug.Log (Reques[1]);
			Debug.Log (Reques[2]);
			return Reques;
		}

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mapa : MonoBehaviour {
	
	int ContadorSatel = 0;
	int ContadorActSatelites = 0;
	int ContadorSemaforo = 0;
	int ContadorActSemaforos = 0;
	public Object Camino,Vacio,PerderSatelite,ActivarSatelite,Speed,Salto,Conos,Player,Nafta,SateliteObj,Slow,Gano;
	// Use this for initialization
	void Start () {
		int x = 1; double z = 1.5;
		int Tipo;
		int [,]Mapa={//0-Vacio|1-Camino|2-Salto|3-Speed|4-Slow|5-ActivarSatelite|6-PerderSatelite/Satelite|7-PickUp|8-Cono|
			{1,1,1,1,1},//1-5
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},
			{1,1,1,1,1},//6-10
			{1,1,1,1,1},//11-15
			{0,4,1,2,1},//16-20
			{0,1,1,1,1},//21-25
			{1,7,1,0,1},//26-30
			{1,1,1,1,1},//31-35
			{1,1,0,8,1},//36-40
			{0,1,1,1,1},//41-45
			{1,1,1,1,1},//46-50
			{1,1,1,1,1},//51-55
			{1,1,8,3,1},//56-60
			{1,1,0,1,1},//61-65
			{1,1,1,7,1},//65-70
			{1,1,1,0,1},//71-75
			{1,1,2,1,1},//76-80
			{1,1,1,1,1},//81-85
			{7,1,1,1,1},//86-90
			{0,1,1,1,1},//91-95
			{1,1,1,1,1},//96-100
			{10,11,11,11,11},//101-105
			{1,1,1,1,1},//106-110
			{1,1,1,1,1},//111-115
			{1,1,1,0,6},//116-120
			{8,1,1,0,1},//121-125
			{1,1,1,1,1},//126-130
			{1,7,1,1,1},//131-135
			{1,1,1,1,0},//136-140
			{1,0,1,1,1},//141-145
			{1,1,1,8,1},//146-150
			{1,1,1,1,1},//151-155
			{1,0,0,1,1},//156-160
			{1,0,1,1,1},//161-165
			{1,1,3,1,1},//166-170
			{10,11,11,11,11},//171-175
			{1,1,1,1,1},//176-180
			{1,1,1,1,0},//181-185
			{6,1,5,1,0},//186-190
			{1,1,1,2,1},//191-195
			{1,1,2,1,1},//196-200
			{1,2,1,1,0},//201-205
			{2,1,1,0,0},//206-210
			{1,1,0,0,1},//211-215
			{1,0,0,1,1},//216-220
			{0,0,1,1,0},//221-225
			{0,1,1,0,0},//226-230
			{2,1,0,0,0},//231-235
			{1,2,1,0,0},//236-240
			{0,0,2,1,0},//241-245
			{1,0,0,2,1},//246-250
			{1,1,0,0,2},//251-255
			{1,1,1,0,0},//256-260
			{10,11,11,11,0},//261-265
			{1,1,8,1,1},//266-270
			{1,1,1,8,1},//271-275
			{6,1,1,1,5},//276-280
			{1,1,1,1,1},//281-285
			{1,3,1,1,7},//286-290
			{1,1,1,1,1},//291-295
			{1,1,1,8,1},//296-300
			{1,7,1,1,1},//301-305
			{1,1,8,1,1},//306-310
			{1,1,1,1,1},//311-315
			{1,1,1,1,1},//316-320
			{1,0,2,0,1},//321-325
			{0,1,0,1,0},//326-330
			{0,1,0,1,0},//331-335
			{1,1,1,1,1},//336-340
			{1,7,1,8,1},//341-345
			{1,1,1,1,1},//346-350
			{1,1,1,1,1},//351-355
			{1,1,8,1,1},//356-360
			{1,8,1,8,1},//361-365
			{1,1,1,1,1},//366-370
			{2,1,1,1,2},//371-375
			{8,0,1,0,8},//376-380
			{1,0,1,0,1},//381-385
			{1,0,1,0,7},//386-390
			{1,1,1,1,1},//391-395
			{1,0,0,0,1},//396-400
			{0,0,0,1,1},//401-405
			{1,1,1,1,1},//406-410
			{3,1,1,7,1},//411-415
			{1,4,1,1,1},//416-420
			{1,1,1,1,1},//421-425
			{1,1,1,1,1},//426-430
			{1,1,2,1,1},//431-435
			{0,0,0,0,0},//436-440
			{0,0,0,0,0},//441-445
			{0,0,0,0,0},//446-450
			{0,0,0,0,0},//451-455
			{0,0,0,0,0},//456-460
			{0,0,2,0,0},//461-465
			{0,0,8,0,0},//466-470
			{0,0,0,0,0},//471-475
			{0,0,0,0,0},//476-480
			{0,1,0,1,7},//481-485
			{1,1,1,1,1},//486-490
			{1,8,1,1,1},//491-495
			{1,1,1,1,8},//496-500
			{1,1,1,1,1},//501-5
			{1,0,1,1,1},//506-510
			{1,0,1,7,1},//511-515
			{1,1,2,1,1},//516-520
			{1,1,1,8,1},//521-525
			{4,1,1,1,1},//526-530
			{1,1,1,1,1},//531-535
			{1,1,3,1,1},//536-540
			{1,1,1,1,1},//541-545
			{1,0,0,0,1},//546-550
			{1,1,1,1,1},//551-555
			{1,2,1,1,1},//556-560
			{1,0,1,1,1},//561-565
			{1,1,1,1,1},//566-570
			{1,1,7,1,1},//571-575
			{1,1,1,8,8},//576-580
			{1,1,1,1,1},//581-585
			{1,1,1,1,1},//586-590
			{1,1,8,1,1},//591-595
			{1,1,1,1,1},//596-600
			{1,2,2,2,1},//601-605
			{8,0,0,0,8},//606-610
			{8,0,0,0,8},//611-615
			{8,0,0,0,8},//616-620
			{8,0,0,0,8},//621-625
			{8,0,0,0,8},//626-630
			{8,1,7,1,8},//631-635
			{8,1,1,1,8},//636-640
			{0,1,0,1,0},//641-645
			{0,1,0,1,0},//646-650
			{8,1,1,1,8},//651-655
			{8,1,7,1,8},//656-660
			{0,2,2,2,0},//661-665
			{0,0,0,0,0},//666-670
			{0,0,0,0,0},//671-675
			{0,0,0,0,0},//676-680
			{0,0,0,0,0},//681-685
			{0,2,0,2,0},//686-690
			{8,0,2,0,8},//691-695
			{0,0,0,0,0},//696-700
			{8,0,0,0,8},//701-705
			{8,0,0,0,8},//706-710
			{1,0,0,0,1},//711-715
			{1,7,1,1,1},//716-720
			{1,1,1,1,1},//721-725
			{1,1,3,1,1},//726-730
			{1,8,1,8,1},//731-735
			{1,1,8,1,1},//736-740
			{1,1,1,1,1},//741-745
			{1,1,1,1,1},//746-750
			{1,8,1,8,1},//751-755
			{1,1,1,1,1},//756-760
			{1,1,1,1,1},//761-765
			{1,1,1,1,1},//766-770
			{1,1,4,7,1},//771-775
			{1,1,1,1,1},//776-780
			{1,0,0,3,1},//781-785
			{1,0,0,8,1},//786-790
			{1,1,1,1,1},//791-795
			{1,1,1,1,1},//796-800
			{1,4,1,1,1},//801-805
			{1,1,8,1,1},//806-810
			{1,1,1,1,1},//811-815
			{1,1,1,1,1},//816-820
			{1,1,1,1,8},//821-825
			{1,1,1,8,1},//826-830
			{1,1,8,1,4},//831-835
			{1,1,1,4,1},//836-840
			{1,1,4,1,1},//841-845
			{0,1,1,1,1},//846-850
			{1,0,1,1,1},//851-855
			{1,1,7,1,1},//856-860
			{1,1,1,0,1},//861-865
			{1,1,1,1,0},//866-870
			{1,1,0,1,1},//871-875
			{1,1,1,1,1},//876-880
			{1,1,2,1,1},//881-885
			{1,1,0,1,1},//886-890
			{1,1,0,1,1},//891-895
			{8,1,1,1,8},//896-900
			{4,1,1,1,4},//901-905
			{1,8,8,8,1},//906-910
			{1,1,1,1,1},//911-915
			{1,1,7,1,1},//916-920
			{1,1,1,1,1},//921-925
			{2,1,8,1,2},//926-930
			{0,1,1,1,0},//931-935
			{0,0,1,0,0},//936-940
			{0,0,1,0,0},//941-945
			{0,1,1,1,0},//946-950
			{1,1,7,1,1},//951-955
			{8,1,1,1,8},//956-960
			{1,1,1,1,1},//961-965
			{1,1,8,1,1},//966-970
			{1,1,8,1,1},//971-975
			{1,1,8,1,1},//976-980
			{3,3,3,3,3},//981-985
			{1,1,1,1,1},//986-990
			{1,1,1,1,1},//991-995
			{9,9,9,9,9},//996-1000
		};

		for (int a = 0; a < Mapa.GetLength (0); a++)
			for (int b = 0; b < Mapa.GetLength (1); b++) {
				Tipo = Mapa [a, b];
				GenerarCubo (x * b, 0, z * a, Tipo);
			}
	}
	private void GenerarCubo(double x, double y, double z, int Type)
	{
		Vector3 Posicion = new Vector3 ((float)x, (float)y, (float)z);
		Object Cubo;
		switch (Type) {
		case 0:
			Cubo = Instantiate (Vacio, Posicion, Quaternion.identity);
			break;
		case 1:
			Cubo = Instantiate (Camino, Posicion, Quaternion.identity);
			break;
		case 2:
			Cubo = Instantiate (Salto, Posicion, Quaternion.identity);
			break;
		case 3:
			Cubo = Instantiate (Speed, Posicion, Quaternion.identity);
			break;
		case 4:
			Cubo = Instantiate (Slow, Posicion, Quaternion.identity);
			break;
		case 5:
			Cubo = Instantiate (PerderSatelite, Posicion, Quaternion.identity);
			GenerarSatelLineal(x, y, z);
			break;
		case 6:
			Cubo = Instantiate (PerderSatelite, Posicion, Quaternion.identity);
			GenerarSatelite (x, y, z);
			break;
		case 7:
			Cubo = Instantiate (Camino, Posicion, Quaternion.identity);
			GenerarPickUp (x, y, z);
			break;	
		case 8:
			Cubo = Instantiate (Camino, Posicion, Quaternion.identity);
			GenerarCono (x, y, z);
			break;
		case 9:
			Cubo = Instantiate (Gano, Posicion, Quaternion.identity);
			break;
		case 10:
			Cubo = Instantiate (ActivarSatelite, Posicion, Quaternion.identity);
			Cubo.name = "ActSatel" + ContadorActSatelites;
			ContadorActSatelites++;
			break;
		case 11:
			Cubo = Instantiate (ActivarSatelite, Posicion, Quaternion.identity);
			ContadorActSatelites--;
			Cubo.name = "ActSatel" + ContadorActSatelites;
			ContadorActSatelites++;
			break;
		}

	}
	private void GenerarSatelite(double x, double y, double z)
	{
		Vector3 Posicion = new Vector3 ((float)x, (float)y+3, (float)z);
		GameObject Satelite;
		Satelite = (GameObject)Instantiate (SateliteObj, Posicion, Quaternion.identity);
		Satelite.name = "ActSatel" + ContadorSatel;
		ContadorSatel++;
	}

	private void GenerarSatelLineal(double x, double y, double z)
	{
		Vector3 Posicion = new Vector3 ((float)x, (float)y+3, (float)z);
		GameObject Satelite;
		Satelite = (GameObject)Instantiate (SateliteObj, Posicion, Quaternion.identity);
		ContadorSatel--;
		Satelite.name = "ActSatel" + ContadorSatel;
		ContadorSatel++;
	}
		
	private void GenerarPickUp(double x, double y, double z)
	{
		Vector3 Posicion = new Vector3 ((float)x, (float)y+1, (float)z);
		Object PickUp;
		PickUp = Instantiate (Nafta, Posicion, Quaternion.identity);
	}

	private void GenerarCono(double x, double y, double z)
	{
		Vector3 Posicion = new Vector3 ((float)x, (float)y+0.65f, (float)z);
		Object Cono;
		Cono = Instantiate (Conos, Posicion, Quaternion.FromToRotation(Vector3.down,transform.forward));
	}
}

using UnityEngine;
using System.Collections;

public class Satelite : MonoBehaviour {

	RaycastHit HitInfo;
	Vector3 Origen;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Origen = GameObject.Find("autotesuper").transform.position;
		if (Physics.Raycast (Origen, Vector3.down, out HitInfo) == true) 
		{
			if (HitInfo.collider.gameObject.name == this.name) {
				Vector3 Mov = new Vector3 (0f, -15f, 0f);
				this.GetComponent<Rigidbody> ().AddForce (Mov);
			}
		}
	}
}

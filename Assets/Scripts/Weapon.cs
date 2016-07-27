using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().Play ();
		Invoke("EndAttack",0.5f);
	}


	void EndAttack() {
		Destroy(this.gameObject);
	}


	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.parent.position + new Vector3(0.63f,0.16f,0f);
	}
}

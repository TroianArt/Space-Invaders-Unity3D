using UnityEngine;
using System.Collections;

public class DeathCollider : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		Destroy (other.gameObject);
	}

}

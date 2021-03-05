using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	
	public float speed;
	public GameObject shootEffect;

	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} 
		GetComponent<Rigidbody> ().velocity = transform.forward * -speed;
	}
	
	
	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Player") {
			GameObject effect = (GameObject)Instantiate(shootEffect, transform.position, transform.rotation);
			Destroy(effect, 5f);
			Destroy (gameObject);
			gameController.SetScore(0);
			gameController.ReduceLives();
			
		}
	}
}

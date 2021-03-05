using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public float speed;
	public GameObject shootEffect;

	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}


	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Enemy"){
			GameObject effect = (GameObject)Instantiate(shootEffect, transform.position, transform.rotation);
			Destroy(effect, 5f);
			Destroy(gameObject);
			Destroy (other.gameObject);
			gameController.SetScore(1);
			gameController.SetScore (other.gameObject);
		}
	}
}

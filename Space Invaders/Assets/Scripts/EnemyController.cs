using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject shot;
	public GameObject collisionEffect;

	private GameController gameController;
	private GameObject player;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} 
		player = GameObject.FindWithTag ("Player");
	}



    void FixedUpdate()
    {
        if (GetComponent<Transform>().position.z < player.gameObject.GetComponent<Rigidbody>().position.z)
        {
			gameController.ReduceLives();
			GameObject effect = (GameObject)Instantiate(collisionEffect, transform.position, transform.rotation);
			Destroy(effect, 5f);
			Destroy(gameObject);

		}
    }
    public void Shoot()
    {
		Instantiate(shot, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
	}

    void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			GameObject effect = (GameObject)Instantiate(collisionEffect, transform.position, transform.rotation);
			Destroy(effect, 5f);
			gameController.GetComponent<GameController>().ReduceLives();
			Destroy(gameObject);
		}
	}
}

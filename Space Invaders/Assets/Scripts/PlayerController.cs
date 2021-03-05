using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;

	public float maxX;
	public float minX;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	public bool leftPointerDown;
	public bool rightPointerDown;

	void Update () {

		if (!leftPointerDown && !rightPointerDown && Input.GetKey(KeyCode.Space) && (Time.time > nextFire || !GameObject.FindGameObjectWithTag("PlayerShot"))) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate(){
		if (transform.position.x > minX)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(-speed, 0f, 0f);
			}
		}
		if (transform.position.x < maxX)
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(speed, 0f, 0f);
			}
		}
	}
}

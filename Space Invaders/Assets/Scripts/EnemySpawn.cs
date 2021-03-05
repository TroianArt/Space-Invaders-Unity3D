using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class OneEnemy
{
	public GameObject enemy;
	public int indexI;
	public int IndexJ;
	public OneEnemy(GameObject go,int i,int j)
    {
		enemy = go;
		indexI = i;
		IndexJ = j;
    }
}
public class EnemySpawn : MonoBehaviour {

	public int startX, startZ;
	public int endX, endZ;

	public float startSpeed;
	private float speed;
	public int countRandomEnemyShoot;
	public float acceleration=0.1f;
	private Vector3 startPosition;

	public int rows, cols;

	public GameObject enemy;
	
	public float period;
	private float nextFire;

	private Vector3 enemyPosition;
	private GameObject[,] enemies;
	private OneEnemy[] enemiesArr;
	private int countEnemies;
	private bool directionHor;
	public int leftBoundary, rightBoundary;
	private int initialLeftBoundary, initialRightBoundary;
	private bool directionVer;
	private GameController gameController;
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} 
		speed = startSpeed;
		directionHor = true;
		directionVer = false;

		startPosition.x = startX;
		startPosition.y = GetComponent<Rigidbody>().position.y;
		startPosition.z = startZ;
		GetComponent<Rigidbody> ().position = startPosition;

		initialLeftBoundary = leftBoundary; 
		initialRightBoundary = rightBoundary;

		endX = -startX;
		endZ = -startZ;

		enemies = new GameObject[cols,rows];
		countEnemies = cols * rows;
		enemiesArr = new OneEnemy[rows*cols];
		int index=0;
		for (int i = 0; i < cols; i++) {
			for(int j = 0; j < rows; j++){
				GameObject newEnemy;

				enemyPosition.x = startX + (i * 2);
				enemyPosition.y = 0;
				enemyPosition.z = startZ + (j * 2);

				newEnemy = Instantiate(enemy,enemyPosition ,GetComponent<Transform>().rotation) as GameObject;

				newEnemy.gameObject.transform.parent = this.gameObject.transform;
				newEnemy.gameObject.tag = "Enemy";

				enemies[i,j] = newEnemy;
				enemiesArr[index] = new OneEnemy(newEnemy, i, j);
				index++;
			}
		}
		

		GetComponent<Rigidbody>().velocity = transform.right * speed;


	}

	public void restartPosition(){
		GetComponent<Rigidbody> ().position = startPosition;
		directionHor = true;
		GetComponent<Rigidbody> ().velocity = transform.right * speed;
	}
	public void Restart()
	{
		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Destroy(enemies[i, j]);
			}
		}
		Start();
		
		
	}
	

	public void oneEnemyDown(GameObject gameObject){
		int indexi=0;
		int indexj=0;
		for(int i = 0; i < cols; i++){
			for(int j = 0; j < rows; j++){
				if(
				   enemies[i,j] != null &&
				   enemies[i,j] == gameObject){
					enemies[i,j] = null;
					indexi = i;
					indexj = j;
				}
			}
		}

		int indexfordelete = 0;
		for(int i = 0; i < countEnemies; i++)
        {
			if (enemiesArr[i].indexI==indexi && enemiesArr[i].IndexJ == indexj)
            {
				indexfordelete = i;
            }
        }
		for(int i = indexfordelete + 1; i < countEnemies; i++)
        {
			enemiesArr[i - 1] = enemiesArr[i];

		}


		int missingLeftColumns = 0;
		for(int i = 0; i < cols; i++){
			bool allRowsOfColumnAreMissing = true;
			for(int j = 0; j < rows; j++){
				if(enemies[i,j] != null){
					allRowsOfColumnAreMissing = false;
					break;
				}
			}
			if(allRowsOfColumnAreMissing){
				missingLeftColumns++;
			}
			else{
				break;
			}
		}

		int missingRightColumns = 0;
		for (int i = cols - 1; i >= 0; i--) {
			bool allRowsOfColumnAreMissing = true;
			for(int j = rows - 1; j >= 0; j--){
				if(enemies[i,j] != null){
					allRowsOfColumnAreMissing = false;
					break;
				}
			}
			if(allRowsOfColumnAreMissing){
				missingRightColumns++;
			}
			else{
				break;
			}
		}

		boundariesUpdate (missingLeftColumns, missingRightColumns);
		speed += acceleration;
		countEnemies--;
		if(countEnemies == 0){
			gameController.Victory();
		}
	}


	private void boundariesUpdate(int left, int right){
		leftBoundary = initialLeftBoundary - (left * 2);
		rightBoundary = initialRightBoundary + (right * 2);
	}
	
	void Update () {

		if (GetComponent<Transform> ().position.x > rightBoundary && directionHor == true) {
			GetComponent<Rigidbody>().position = new Vector3(rightBoundary, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z - 1f);
			directionHor = false;
			GetComponent<Rigidbody> ().velocity = transform.right * -speed;	
		} else {
			if (GetComponent<Transform> ().position.x < leftBoundary && directionHor == false) {
				GetComponent<Rigidbody>().position = new Vector3(leftBoundary, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z - 1f);
				directionHor = true;
				GetComponent<Rigidbody> ().velocity = transform.right * speed;	
			}
		}
		if(Time.time > nextFire && countEnemies>0)
        {
			nextFire = Time.time + period;
			for(int i = 0; i < countRandomEnemyShoot; i++)
            {
                int enemyIndex = Random.Range(0, countEnemies);
				enemies[enemiesArr[enemyIndex].indexI, enemiesArr[enemyIndex].IndexJ].GetComponent<EnemyController>().Shoot();
			}
		}

	}
}

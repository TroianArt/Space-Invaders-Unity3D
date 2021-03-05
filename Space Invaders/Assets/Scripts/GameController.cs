using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	private EnemySpawn enemyMatrix;
	public bool enemyMatrixEmpty;

	public float spawnWait;

	public Text scoreText;
	public Text livesText;

	public GameObject pauseUI;
	public GameObject endUI;

	private int score;
	private int lives;
	public int startingLives;
	private bool gameover;
    void Start () {
		GameObject enemyMatrixObject = GameObject.FindWithTag ("EnemyMatrix");
		if (enemyMatrixObject != null) {
			enemyMatrix = enemyMatrixObject.GetComponent<EnemySpawn> ();
		} 
		score = 0;
		UpdateScore ();
		lives = startingLives;
		UpdateLives ();
	}
	
	void Update () {
		if (enemyMatrixEmpty == true) {
			SpawnEnemies();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	IEnumerator SpawnEnemies(){
		yield return new WaitForSeconds (spawnWait);
	}
	public void SetScore(int type){
		switch(type){
		case 0:
				if ((score - 100) < 0) score = 0;
				else score -= 100;
				break;
		case 1: 
			score += 10;
			break;
		}
		UpdateScore ();
	}
	public void SetScore(GameObject gameObject){
		switch(gameObject.tag){
		case "Player" :
				if ((score - 100) < 0)score = 0;
				else score -= 100;
				break;
		case "Enemy" :
				score += 30;
				enemyMatrix.oneEnemyDown (gameObject);
				break;
		}
		UpdateScore ();
	}
	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}
	public void ReduceLives(){
		if (lives > 0) {
			lives--;
		}
		if (lives <= 0) {
			GameOver ();
		}
		UpdateLives ();
	}

	void UpdateLives(){
		livesText.text = "Lives: " + lives;
	}


	public void GameOver(){
		endUI.SetActive(true);
		Time.timeScale = 0f;
		gameover = true;
	}
	public void Victory(){
		endUI.SetActive(true);
		gameover = true;
	}
	public void Restart()
    {
		Time.timeScale = 1f;
		enemyMatrix.Restart();
		pauseUI.SetActive(false);
		endUI.SetActive(false);
		Start();
		gameover = false;

	}
	public void Pause()
	{
		if (!gameover)
		{
			pauseUI.SetActive(!pauseUI.activeSelf);
			if (pauseUI.activeSelf)
			{
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}
	public void Exit()
	{
		Application.Quit();
	}


}

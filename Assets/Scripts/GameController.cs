using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start () {
		gameOver = false; //o jogo nao pode comecar terminado
		restart = false; //nem reiniciando
		restartText.text = ""; //as mensagens devem estar vazias
		gameOverText.text = "";
		score = 0; //a pontuacao deve iniciar zerada
		UpdateScore (); //a variavel de controle da pontuacao deve estar zerada
		StartCoroutine (SpawnWaves ()); //a primeira onda de meteoros é iniciada
	}

	void Update () {
		if (restart) { //se o jogo acabou a variavel de restart foi setada como verdadeira
			if (Input.GetKeyDown (KeyCode.R)) { //espera-se pelo usuario para reiniciar com a tecla 'R'
				//A cena entao é recarregada para reiniciar o jogo
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				SceneManager.LoadScene (0);
			}
		}
	}

	IEnumerator SpawnWaves () {//rotina para disparar a criacao dos inimigos
		//ela espera o tempo estipulado para que seja executada
		yield return new WaitForSeconds (startWait); 
		while (true) {
			//para cada meteoro que se deseja criar
			for(int i = 0; i < hazardCount; i++) {
				//a posicao em que o meteoro sera criado deve estar dentro de um range,
				//ou seja, deve estar contida dentro da tela do jogo.
				//Nao adianta em nada criar meteoros fora da tela do jogo
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//A rotacao e criada para o meteoro
				Quaternion spawnRotation = Quaternion.identity;
				//e por fim o meteoro e instanciado
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			//se o jogador perdeu, entao a mensagem de restart deve ser exibida
			//alem disso, a variavel restart deve ser verdadeira
			//assim a condicao mais acima ira verificar se o jogador pressionou o 'R'
			if (gameOver) {
				restartText.text = "Press 'R' for Restart or 'ESC' to Return to Main Menu";
				restart = true;
				break;
			}
		}
	}

	//Metodo para adicionar pontuacao
	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	//Metodo para atualizar a exibicao do texto de pontuacao
	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	//variavel para exibir o texto de gameover e mudar o estado da variavel gameOver
	public void GameOver (){
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}

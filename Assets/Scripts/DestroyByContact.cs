using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//Codigo que determina as condicoes para destruir tanto o jogador quanto os meteoros
	void OnTriggerEnter (Collider other) {
		//colisao com a fronteira nao deve disparar animacoes
		if (other.tag == "Boundary") {
			return;
		}

		//O objeto que colidiu deve ser destruido (como o meteoro com o tiro)
		Instantiate (explosion, transform.position, transform.rotation);
		//se o meteoro bateu no jogador, o jogador tambem deve ser destruido
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver (); //devemos chamar o fim do jogo quando a nave e destruida
		}

		//se somente o meteoro foi destruido, entao o score deve aumentar
		gameController.AddScore (scoreValue);
		//tanto o tiro quanto o meteoro devem deixar de existir apos a colisao
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}

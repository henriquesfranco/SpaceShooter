using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed; //variavel para controlar a velocidade da nave
	public float tilt; //variavel para controlar a curva da nave
	public Boundary boundary; //variavel para representar a fronteira do cenario

	public GameObject shot; //objeto que representa o tiro
	public Transform shotSpawn; //posicao de partida do tiro
	public float fireRate; //taxa de disparo

	private float nextFire;

	void Update () {
		if(Input.GetButton("Fire1") && Time.time > nextFire) { //verifica se já decorreu tempo para proximo tiro
			nextFire = Time.time + fireRate; //determina o tempo do proximo tiro
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //instacia o novo tiro
			GetComponent<AudioSource> ().Play (); //toca o som de disparo do tiro
		}
	}

	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis ("Horizontal");	
		float moveVertical = Input.GetAxis ("Vertical");	

		//cria o vetor de movimento com base nos botoes pressionados e captados acima
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		//Aplica o vetor de movimento na nave. Multiplica pela variavel de velocidade, speed
		GetComponent<Rigidbody>().velocity = movement * speed;

		/*
		 * O trecho de codigo abaixo tem a funcao de controlar a fronteira do jogo
		 * evita que a nave saia do cenario
		 */
		
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		//A linha abaixo aplica o tilt na nave a partir de uma rotacao em seu 
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}

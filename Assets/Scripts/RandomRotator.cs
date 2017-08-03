using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	//Este codigo é aplicado quando um meteoro é instanciado
	void Start () {
		//uma velocidade angular é aplicada a partir de uma variavel aleatoria
		//assim os diferentes meteoros terao diferentes rotacoes
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
	}
}

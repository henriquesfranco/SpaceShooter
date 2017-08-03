using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	//Quando os meteoros saem do cenario é preciso que sejam destruídos
	//caso contrario, o Unity ira processa-los
	//Ou seja, o numero de elementos a serem processados ira crescer ate que o jogo acabe
	//Com a fronteira criada, o comando abaixo permite que qualquer coisa que atingi-la
	//seja destruída
	void OnTriggerExit(Collider other) {
		Destroy (other.gameObject);
	}
}

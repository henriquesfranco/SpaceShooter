using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {
	public void LoadScene(int level)
	{
		System.IO.File.WriteAllText("conf", "EASY");
		SceneManager.LoadScene (level);
	}
}

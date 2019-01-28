using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyUp("space"))
		{
			SceneManager.LoadScene("Home");
		}
	}
}

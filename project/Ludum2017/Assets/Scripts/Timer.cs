using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public int seconds = 5;
	public Text output;

	void OnEnable ()
	{
		StartCoroutine (timer (seconds));
	}
		
	IEnumerator timer(int seconds)
	{
		while (seconds >= 0) 
		{
			output.text = (":" + seconds.ToString());
			yield return new WaitForSeconds (1);
			seconds--;
		}

		gameObject.SetActive (false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager BoardScript;
	private int level = 6;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		BoardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame()
	{
		BoardScript.SetupScene(level);
	}
	// Update is called once per frame
	void Update () {
		
	}
}

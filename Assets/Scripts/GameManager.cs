using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public BoardManager BoardScript;
	private int level = 6;

	void Awake()
	{
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

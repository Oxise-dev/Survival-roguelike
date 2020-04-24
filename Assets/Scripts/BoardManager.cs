using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int maximum;
		public int minimum;

		public Count(int min, int max)
		{
			maximum = max;
			minimum = min;
		}
	}

	public int rows = 8; // int that save grid columns on which spawn walls, enemy, food
	public int columns = 8; // int that save grid columns on which spawn walls, enemy, food
	public Count wallCount = new Count(5, 9); // random count of walls, that blocking your way
	public Count foodCount = new Count(1, 5); // random count of food
	public GameObject exit; // exit
	public GameObject[] floorTiles; // floors
	public GameObject[] foodTiles;	// food
	public GameObject[] enemyTiles; // enemy
	public GameObject[] WallTiles; //Walls
	public GameObject[] OuterWallTiles; //OuterWalls
	public GameObject[] OuterWallTilesLeftUp; // diagonal walls up left
	public GameObject[] OuterWallTilesRightUp; // diagonal walls up right
	public GameObject[] OuterWallTilesLeftDown; // diagonal walls down left
	public GameObject[] OuterWallTilesRightDown; // diagonal walls down right
	public GameObject[] OuterWallTilesLeft; // border walls left
	public GameObject[] OuterWallTilesRight; // border walls right
	private Transform boardHolder; // keep all repeating game object here
	private List<Vector3> gridPositions = new List<Vector3>(); // save the positions on which will spawn walls, enemy, food

	void InitialiseList()
	{
		for (int x = 1; x < columns - 1; x++)
		{
			for (int y = 1; y < rows - 1; y++)
			{
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
	}
	void BoardSetup()
	{
		boardHolder = new GameObject("Board").transform;

		for (int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject toInstantiate = 
						floorTiles[Random.Range(0, floorTiles.Length)];

				if (x >= -1 && y == -1 || y == rows)
				{
					toInstantiate = 
						OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];
				}
				else if (x == -1  && y >= -1 )
				{
					toInstantiate =
						OuterWallTilesRight[Random.Range(0, OuterWallTilesRight.Length)];
				}
				else if (x == columns  ||  y == rows )
				{
					toInstantiate =
						OuterWallTilesLeft[Random.Range(0, OuterWallTilesLeft.Length)];
				}

				GameObject instance = Instantiate(toInstantiate,
					new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent(boardHolder); 
			}
		}
	}

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPositions = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);

		return randomPositions;
	}
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int ObjectCount = Random.Range(minimum, maximum + 1);
		for(int i = 0; i < ObjectCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoise = tileArray[Random.Range(0, tileArray.Length)];

			Instantiate(tileChoise, randomPosition, Quaternion.identity);
		}
	}
	public void SetupScene(int level)
	{
		BoardSetup();
		InitialiseList();
		LayoutObjectAtRandom(WallTiles,
					wallCount.minimum, wallCount.maximum);
		LayoutObjectAtRandom(foodTiles,
					foodCount.minimum, foodCount.maximum);

		int enemyCount = (int)Mathf.Log(level, 2f);
		LayoutObjectAtRandom(enemyTiles,
					enemyCount, enemyCount);
		Instantiate(exit,
				new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}

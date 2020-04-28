using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MovingObject {
	public CameraShake mainCamera;
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1;
	public Text foodText;

	public Weapon weapon;
	private Animator animator;
	private int food;
	


	// Use this for initialization
	protected override void Start ()
	{
		animator = GetComponent<Animator>();

		food = GameManager.instance.playerFoodPoints;
		foodText.text = "Potions " + food;

		base.Start();
	}
	
	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;

	}
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.instance.playersTurn) return;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw("Horizontal");
		vertical = (int)Input.GetAxisRaw("Vertical");

		if (horizontal != 0)
			vertical = 0;

		if (horizontal != 0 || vertical != 0)
			AttemptMove<Wall>(horizontal, vertical);

	}
	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		food--;
		foodText.text = "Potions " + food;
		base.AttemptMove<T>(xDir, yDir);
		animator.SetTrigger("KnightRun");

		RaycastHit2D hit;

		CheckIfGameOver();

		GameManager.instance.playersTurn = false;
		

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Exit")
		{
			Invoke("Restart", restartLevelDelay);
			enabled = false;
		}
		else if (other.tag == "Food")
		{
			food += pointsPerFood;
			other.gameObject.SetActive(false);
			foodText.text = "+ " + pointsPerFood + " Potions " + food;
		}
		else if (other.tag == "Soda")
		{
			food += pointsPerSoda;
			other.gameObject.SetActive(false);
			foodText.text = "+ " + pointsPerSoda + " Potions " + food;
		}
		else if (other.tag == "Spike")
		{
			LoseFood(10);
			foodText.text = "- " + 10 + " Potions " + food;
		}

	}
	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		
		weapon.Atack();
		mainCamera.CameraShakef();
		hitWall.DamageWall(wallDamage);
	
	}
	private void Restart()
	{
		SceneManager.LoadScene("Main");
	}
	public void LoseFood(int loss)
	{

		animator.SetTrigger("KnightHit");
		food -= loss;
		foodText.text = "- " + loss + " Potions " + food;
		CheckIfGameOver();
	}

	private void CheckIfGameOver()
	{
		if (food <= 0)
			GameManager.instance.GameOver();
	}
}

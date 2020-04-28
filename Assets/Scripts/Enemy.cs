using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject, IEnemy {
	public CameraShake mainCamera;
	public int playerDamage;
	public Weapon weapon;

	private Animator animator;
	private Transform target;
	private bool skipMove;

	protected override void Start ()
	{
		GameManager.instance.AddEnemiesToList(this);
		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start();
	}
	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		if (skipMove)
		{
			skipMove = false;
			return;
		}
		base.AttemptMove<T>(xDir, yDir);
		skipMove = true;	
	}
	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;

		AttemptMove<Player>(xDir, yDir);
		animator.SetTrigger("EnemyRun");
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Food" && playerDamage > 10)
		{
			other.gameObject.SetActive(false);		
		}
		if (other.tag == "Soda" && playerDamage > 10)
		{
			other.gameObject.SetActive(false);
		}
	}
	protected override void OnCantMove<T>(T component)
	{	
		Player hitPlayer = component as Player;
		weapon.Atack();
		mainCamera.CameraShakef();
		hitPlayer.LoseFood(playerDamage);
	}
}

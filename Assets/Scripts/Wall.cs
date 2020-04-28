using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public int hp = 4;

	public AudioClip wallcrashSnd;

	private Animator animator;
	

	void Awake()
	{

		animator = GetComponent<Animator>();
	}
	public void DamageWall(int loss)
	{
		
		animator.SetTrigger("Hit");

		hp -= loss;

		if (hp <= 0)
			
			gameObject.SetActive(false);
	}		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	private Animator weaponAnimator;

	void Awake()
	{
		weaponAnimator = GetComponent<Animator>();
	}
	
	public void Atack()
	{
		weaponAnimator.SetTrigger("atack");
	}
}

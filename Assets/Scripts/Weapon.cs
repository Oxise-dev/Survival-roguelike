using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	public Animator weaponAnimator;

	void Awake()
	{
		weaponAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

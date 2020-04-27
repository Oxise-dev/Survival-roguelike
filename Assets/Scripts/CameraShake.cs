using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	private static Animator animator;

	public CameraShake cameraShake;
	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void CameraShakef()
	{
		animator.SetTrigger("Shake");
	}
}
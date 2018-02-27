using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Principal;

public class GameManager : SingletonPersistant<GameManager> {

	[SerializeField]
	private float initialScrollSpeed = 5f;
	[HideInInspector]
	public float scrollSpeed;

	[SerializeField]
	private float initialDeathWallSpeed = 0f;
	[HideInInspector]
	public float deathWallSpeed;

	// Use this for initialization
	void Awake () {
		scrollSpeed = initialScrollSpeed;
		deathWallSpeed = initialScrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

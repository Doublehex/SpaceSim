using UnityEngine;
using System.Collections;

public class ShipData : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float maxHealth = 100.0f;
	public float moveSpeed = 1.0f;
	public float maxSpeed = 3.0f;
	public string image;
	public string sprite;
	
	public void loadShip(string title)
	{
		if (title == "DefaultShip") {
			moveSpeed = 2.0f;
			maxSpeed = 5.0f;
			image = "Images/Player Ship";
			sprite = "Sprites/PlayerShip";

			return;
		}

		Debug.LogError ("Trying to load unknown ship type");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

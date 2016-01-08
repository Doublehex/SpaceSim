using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipData : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	static Dictionary<string, string> ShipSprite = new Dictionary<string, string>
	{
		{"DefaultShip", "Sprites/PlayerShip"},
	};

	public static string SpriteForShip(string ship)
	{
		return ShipSprite [ship];
	}

	public float maxHealth = 100.0f;
	public float moveSpeed = 1.0f;
	public float maxSpeed = 3.0f;
	public string image;
	public string sprite;
	public Vector3[] weaponPositions;
	
	public void loadShip(string title)
	{
		if (title == "DefaultShip") {
			moveSpeed = 2.0f;
			maxSpeed = 5.0f;
			image = "Images/Player Ship";
			sprite = "Sprites/PlayerShip";

			weaponPositions = new Vector3[2];
			weaponPositions[0] = new Vector3(0.025f, 0.285f, -1.0f);
			weaponPositions[1] = new Vector3(0.025f, -0.285f, -1.0f);

			return;
		}

		Debug.LogError ("Trying to load unknown ship type");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

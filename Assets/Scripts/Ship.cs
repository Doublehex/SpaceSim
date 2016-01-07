using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	/*public static GameObject CreateShip(string shipName)
	{
		Ship newShip = new Ship ();
		ShipData data = new ShipData ();
		data.loadShip (shipName);

		GameObject go = GameObject.Instantiate(data.sprite) as GameObject;
		go.AddComponent (newShip);
		go.AddComponent (data);

		newShip.initializeShip ();

		return go;
	}*/

	public void initializeShip(string shipName)
	{
		ShipData data = gameObject.AddComponent<ShipData> ();
		data.loadShip (shipName);

		Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D> ();
		rigidBody.gravityScale = 0.0f;

		SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
		renderer.sprite = Resources.Load<Sprite>(data.image);

		//Sprite test = Resources.Load<Sprite> ("Images/Player Ship");
		//Debug.Log (test);
	}

	public void initializeMainPlayerShip(string shipName)
	{
		gameObject.name = "Player";
		initializeShip (shipName);

		gameObject.AddComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

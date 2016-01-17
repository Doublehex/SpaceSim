using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public bool isPlayer = false;
	// Use this for initialization
	void Start () {

	}

	public static GameObject CreateShip(string shipName)
	{
		GameObject go = GameObject.Instantiate(Resources.Load(ShipData.SpriteForShip(shipName))) as GameObject;
		ShipData data = go.AddComponent<ShipData> ();
		Ship newShip = go.AddComponent<Ship> ();
		data.loadShip (shipName);

		newShip.initializeShip (shipName);

		return go;
	}

	public static GameObject CreatePlayerShip(string shipName)
	{
		GameObject go = CreateShip (shipName);
		
		go.GetComponent<Ship>().initializeMainPlayerShip (shipName);
		
		return go;
	}

	public void initializeShip(string shipName)
	{
		gameObject.AddComponent<WeaponsSystem> ();
		gameObject.AddComponent<Movement> ();
	}

	public void initializeMainPlayerShip(string shipName)
	{
		gameObject.name = "Player";
		gameObject.layer = LayerMask.NameToLayer ("Player");
		isPlayer = true;

		gameObject.AddComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}

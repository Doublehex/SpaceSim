using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject[] weapons;
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
		GameObject go = GameObject.Instantiate(Resources.Load(ShipData.SpriteForShip(shipName))) as GameObject;
		ShipData data = go.AddComponent<ShipData> ();
		Ship newShip = go.AddComponent<Ship> ();
		data.loadShip (shipName);
		
		newShip.initializeMainPlayerShip (shipName);
		
		return go;
	}

	public void initializeShip(string shipName)
	{
		/*ShipData data = gameObject.AddComponent<ShipData> ();
		data.loadShip (shipName);

		Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D> ();
		rigidBody.gravityScale = 0.0f;

		SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
		renderer.sprite = Resources.Load<Sprite>(data.image);*/

		ShipData data = gameObject.GetComponent<ShipData> ();

		weapons = new GameObject[data.weaponPositions.Length];

		weapons [0] = Weapon.CreateWeapon ("Sprites/Weapon1", gameObject, data.weaponPositions[0]);
		weapons [1] = Weapon.CreateWeapon ("Sprites/Weapon1", gameObject, data.weaponPositions[1]);
	}

	public void initializeMainPlayerShip(string shipName)
	{
		gameObject.name = "Player";
		//gameObject.layer = LayerMask.NameToLayer ("Player");
		initializeShip (shipName);


		gameObject.AddComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1")) {

			foreach (GameObject weapon in weapons) {
				if (weapon != null && weapon.GetComponent<Weapon> ().activated) {
					weapon.GetComponent<Weapon> ().fire ();
				}
			}
		} else {

			foreach(GameObject weapon in weapons)
			{
				if (weapon != null && weapon.GetComponent<Weapon>().activated)
				{
					weapon.GetComponent<Weapon>().stopFiring();
				}
			}
		}
	}
}

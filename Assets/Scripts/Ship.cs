using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject[] weapons;
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

		weapons = new GameObject[data.weaponPositions.Length];

		weapons[0] = new GameObject ();
		SpriteRenderer weaponRenderer = weapons[0].AddComponent<SpriteRenderer>();
		weaponRenderer.sprite = Resources.Load<Sprite>("Images/Weapon1");
		weaponRenderer.sortingOrder = 1;

		weapons[0].transform.SetParent (gameObject.transform);
		weapons[0].transform.localPosition = data.weaponPositions[0];
		weapons[0].AddComponent<Weapon>();

		weapons[1] = GameObject.Instantiate (weapons[0]);

		weapons[1].transform.SetParent (gameObject.transform);
		weapons[1].transform.localPosition = data.weaponPositions [1];
		weapons[1].AddComponent<Weapon>();
	}

	public void initializeMainPlayerShip(string shipName)
	{
		gameObject.name = "Player";
		initializeShip (shipName);

		gameObject.AddComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Fire1"))
		{
			foreach(GameObject weapon in weapons)
			{
				if (weapon != null && weapon.GetComponent<Weapon>().activated)
				{
					weapon.GetComponent<Weapon>().fire();
				}
			}
		}
	}
}

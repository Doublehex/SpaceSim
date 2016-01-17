using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsSystem : MonoBehaviour {

	public List<GameObject> weapons;

	// Use this for initialization
	void Start () {
		weapons = new List<GameObject> ();

		ShipData data = gameObject.GetComponent<ShipData> ();
		foreach (Transform child in gameObject.transform)
		{
			weapons.Add (Weapon.CreateWeapon ("Sprites/Weapon1", gameObject, child.transform.localPosition));
		}
		//weapons.Add (Weapon.CreateWeapon ("Sprites/Weapon1", gameObject, data.weaponPositions [0]));
	}
	
	// Update is called once per frame
	void Update () {
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

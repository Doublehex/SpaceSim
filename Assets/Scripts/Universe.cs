using UnityEngine;
using System.Collections;

public class Universe : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//chunks = new UniverseChunk [SizeX, SizeY];

		//GenerateChunks ();

		Ship.CreatePlayerShip ("DefaultShip");
	}
	
	// Update is called once per frame
	void Update () {

	}
}

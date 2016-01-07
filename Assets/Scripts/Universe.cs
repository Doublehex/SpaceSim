using UnityEngine;
using System.Collections;

public class Universe : MonoBehaviour {

	public static int SizeX = 10;
	public static int SizeY = 10;

	int previousChunkX, previousChunkY;
	
	UniverseChunk[,] chunks;
	// Use this for initialization
	void Start () {
		chunks = new UniverseChunk [SizeX, SizeY];

		GenerateChunks ();

		GameObject player = new GameObject ();
		Ship playerShip = player.AddComponent<Ship> ();
		playerShip.initializeMainPlayerShip ("DefaultShip");
	}

	void GenerateChunks()
	{
		int startX = (int)(Camera.main.transform.position.x / 20f);
		int startY = (int)(Camera.main.transform.position.y / 20f);

		for (int i = 0; i < SizeX; i++) {
			for (int j = 0; j < SizeY; j++)
			{
				chunks[i, j] = UniverseChunk.Create(i, j);

				if (i > startX - 2 && i < startX + 2 && j > startY - 2 && j < startY + 2)
				{
					chunks[i, j].LoadStars();
				}
			}
		}

		previousChunkX = startX;
		previousChunkY = startY;
	}
	
	// Update is called once per frame
	void Update () {
	
		int currentX = (int)(Camera.main.transform.position.x / 20f);
		int currentY = (int)(Camera.main.transform.position.y / 20f);

		if (currentX != previousChunkX || currentY != previousChunkY) {

			for (int x = (currentX - 3 < 0 ? 0 : (currentX - 3)); x < (currentX + 3 > SizeX ? SizeX : (currentX + 3)); x++)
			{
				for (int y = (currentY - 3 < 0 ? 0 : (currentY - 3)); y < (currentY + 3 > SizeY ? SizeY : (currentY + 3)); y++)
				{
					if (x > currentX - 2 && x < currentX + 2 && y > currentY - 2 && y < currentY + 2)
					{
						if (!chunks[x, y].Loaded)
						{
							chunks[x, y].LoadStars();
						}
					}
					else if (chunks[x, y].Loaded)
					{
						chunks[x, y].UnloadStars();
					}
				}
			}

			previousChunkX = currentX;
			previousChunkY = currentY;
		}
	}
}

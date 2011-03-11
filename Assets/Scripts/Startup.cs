using UnityEngine;
using System;
using System.Collections;

using DominantSpecies;

public class Startup : MonoBehaviour {
	public GameObject EmptyHex;
	public GameObject EmptyChit;
	
	public Game g;
	
	// Use this for initialization
	void Awake() {
		g = new Game();

		for (int i = 0; i <= g.map.tiles.GetUpperBound(0); i++) {
			for (int j = 0; j <= g.map.tiles.GetUpperBound(1); j++) {
				var t = g.map.tiles[i ,j];			

				if (t.Terrain == Tile.TerrainType.Invalid)
				{
					continue;
				}

				float z = j + (i * .5f);

				GameObject newHex = (GameObject)Instantiate(EmptyHex);
				newHex.transform.position = new Vector3(i * 1.68f, 0, z * 1.9f);
				((HexController)newHex.GetComponent("HexController")).Tile = t;
			}
		}
		
		for (int i = 0; i <= g.map.chits.GetUpperBound(0); i++) {
			for (int j = 0; j <= g.map.chits.GetUpperBound(1); j++) {
				var c = g.map.chits[i,j];

				if (c.Element == Chit.ElementType.None)
				{
					continue;
				}

				float z = j + (i * .5f);
				
				GameObject newChit = (GameObject)Instantiate(EmptyChit);
				newChit.transform.position = new Vector3((i-.5f) * 1.68f, 1.5f, z * 1.9f * .5f);
				((ChitController)newChit.GetComponent("ChitController")).Chit = c;

			}
		}
	}
}
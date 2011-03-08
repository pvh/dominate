using UnityEngine;
using System;
using System.Collections;

using DominantSpecies;

public class Startup : MonoBehaviour {
	public GameObject EmptyHex;
	
	public Game g;
	
	// Use this for initialization
	void Awake() {
		g = new Game();
		
		g.map.Tiles.All.ForEach(delegate(Tile t)
		{
			if (t.Terrain == Tile.TerrainType.Invalid)
			{
				return;
			}
			
			float z = t.J;
			if (t.I % 2 == 1)
			{
				z -= .5f;
			}
			
			var newHex = Instantiate(EmptyHex, new Vector3(t.I * 1.68f, 0, z * 1.9f), Quaternion.identity);
			newHex.name = String.Format("{0} {1}", t.I, t.J);
		});
	}
}
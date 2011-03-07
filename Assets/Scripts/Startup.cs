using UnityEngine;
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
			float x = t.I;
			if (t.J % 2 == 0)
			{
				x += .5f;
			}
			
			Instantiate(EmptyHex, new Vector3(x * 1.90f, 0, t.J * 1.68f), Quaternion.identity);
		});
	}
}
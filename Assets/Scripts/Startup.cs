using UnityEngine;
using System.Collections;

using DominantSpecies;

public class Startup : MonoBehaviour {
	public GameObject EmptyHex;
	
	public Game g;
	
	// Use this for initialization
	void Awake() {
		g = new Game();
		
		g.map.Tiles.All.ForEach((t) => Instantiate(EmptyHex, new Vector3(t.I, 0, t.J), Quaternion.identity));
	}
}
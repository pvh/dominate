using UnityEngine;
using System.Collections;

using DominantSpecies;

public class HexController : MonoBehaviour {
	private Tile _tile;
	public Tile Tile
	{
		get { return _tile; }
		set
		{
			_tile = value;
			UpdateMaterial();
		}
	}
	
	void Start () {
	}
	
	void Update () {
	}
	
	private void UpdateMaterial()
	{
		switch (Tile.Terrain)
		{
			case Tile.TerrainType.Sea:
				GetComponentInChildren(typeof(MeshRenderer)).renderer.material = SeaMaterial;
				break;
		}
	}
	
	public Material SeaMaterial;
}

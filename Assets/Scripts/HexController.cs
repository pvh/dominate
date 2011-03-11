using UnityEngine;

using System;
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
			name = String.Format("Tile {0},{1}", -1, -1);
			UpdateMaterial();
		}
	}
	
	void Start () {
	}
	
	void Update () {
	}
	
	private void UpdateMaterial()
	{
		Material newMaterial = null;
		
		switch (Tile.Terrain)
		{
			case Tile.TerrainType.Sea:
				newMaterial = SeaMaterial;
				break;
			case Tile.TerrainType.Desert:
				newMaterial = DesertMaterial;
				break;
			case Tile.TerrainType.Forest:
				newMaterial = ForestMaterial;
				break;
			case Tile.TerrainType.Jungle:
				newMaterial = JungleMaterial;
				break;
			case Tile.TerrainType.Mountain:
				newMaterial = MountainMaterial;
				break;
			case Tile.TerrainType.Savannah:
				newMaterial = SavannahMaterial;
				break;
			case Tile.TerrainType.Wetlands:
				newMaterial = WetlandsMaterial;
				break;
		}
		
		if (newMaterial != null)
		{
			GetComponentInChildren(typeof(MeshRenderer)).renderer.material = newMaterial;
		}
	}
	
	public Material SeaMaterial;
	public Material DesertMaterial;
	public Material ForestMaterial;
	public Material JungleMaterial;
	public Material MountainMaterial;
	public Material SavannahMaterial;
	public Material WetlandsMaterial;
}

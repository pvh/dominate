using UnityEngine;

using System;
using System.Collections;

using DominantSpecies;

public class ChitController : MonoBehaviour {
	private Chit _chit;
	public Chit Chit
	{
		get { return _chit; }
		set
		{
			_chit = value;
			name = String.Format("Chit {0},{1}", -1, -1);
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
		
		switch (Chit.Element)
		{
			case Chit.ElementType.Water:
				newMaterial = SeaMaterial;
				break;
			case Chit.ElementType.Sun:
				newMaterial = DesertMaterial;
				break;
			case Chit.ElementType.Meat:
				newMaterial = ForestMaterial;
				break;
			case Chit.ElementType.Grub:
				newMaterial = JungleMaterial;
				break;
			case Chit.ElementType.Seed:
				newMaterial = MountainMaterial;
				break;
			case Chit.ElementType.Grass:
				newMaterial = SavannahMaterial;
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

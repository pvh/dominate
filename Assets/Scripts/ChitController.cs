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
		
		switch (Chit.element)
		{
			case Chit.Element.Water:
				newMaterial = SeaMaterial;
				break;
			case Chit.Element.Sun:
				newMaterial = DesertMaterial;
				break;
			case Chit.Element.Meat:
				newMaterial = ForestMaterial;
				break;
			case Chit.Element.Grub:
				newMaterial = JungleMaterial;
				break;
			case Chit.Element.Seed:
				newMaterial = MountainMaterial;
				break;
			case Chit.Element.Grass:
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

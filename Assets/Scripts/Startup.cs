using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
	public GameObject EmptyHex;

	// Use this for initialization
	void Awake() {
		for (var y = 0; y < 5; y++) {
			for (var x = 0; x < 5; x++) {
				Instantiate(EmptyHex, new Vector3(x * 2, 0, y * 2), Quaternion.identity);
			}
		}
	}
}
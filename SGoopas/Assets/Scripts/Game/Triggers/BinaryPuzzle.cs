using System;
using UnityEngine;

public class BinaryPuzzle : SwitchContainer
{
	private bool isSolved = false;
	private GameObject[] cubes;
	private Vector3[] originalPos;
	private float[] interpolations = {0f, -.5f, -1f};
	public int solution = 0;
	public GameObject cube1, cube2, cube3;
	public int yTranslate;

	void Start () {
		base.Start ();
		cubes = new GameObject[]{cube1, cube2, cube3};
		originalPos = new Vector3[]{
			new Vector3(cube1.transform.position.x, cube1.transform.position.y, cube1.transform.position.z),
			new Vector3(cube2.transform.position.x, cube2.transform.position.y, cube2.transform.position.z),
			new Vector3(cube3.transform.position.x, cube3.transform.position.y, cube3.transform.position.z)
		};
	}

	public override bool NotifyTriggerStateChange(int index){
		triggerStates[index] = !triggerStates[index];
		if (GetBinaryInput() == solution) {
			Debug.Log ("solved!");
			isSolved = true;
		}

		return triggerStates [index];
	}

	// Update is called once per frame
	void Update () {
		if (isSolved) {
			for(int i=0; i < interpolations.Length; i++){
				if (interpolations [i] < 1) {
					interpolations [i] += Time.deltaTime;
				}

				if (interpolations [i] > 0) {
					//Shift ytranslate
					cubes[i].transform.position = originalPos[i] + yTranslate*Vector3.up*interpolations [i];
				}
			}
		}
	}

	private int GetBinaryInput(){
		int total = 0;
		for (int i = 0; i < triggerStates.Count; i++) {
			int active = (triggerStates [i] ? 1 : 0);
			total += (int)(active*Math.Pow(2, i));
		}
		return total;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProducer: MonoBehaviour
{
	public Transform parent;
	public GameObject prefab;

	private void Start() =>
		Produce();

	public void Produce()
	{
		var mesh = GetComponent<MeshFilter>().mesh;
		int[] indis = mesh.triangles;
		Vector3[] verts = mesh.vertices;

		for (int i = 0; i < indis.Length; i += 6)
		{
			var checkpoint = Instantiate(prefab, parent);
			Vector3 avg1 = (verts[indis[i + 0]] + verts[indis[i + 1]] + verts[indis[i + 2]]) / 3;
			Vector3 avg2 = (verts[indis[i + 3]] + verts[indis[i + 4]] + verts[indis[i + 5]]) / 3;
			Vector3 avg = (avg1 + avg2) / 2;
			checkpoint.transform.localPosition = new Vector3(avg.x, -avg.y);
		}

		Debug.Log("Path Produced!");
	}
}

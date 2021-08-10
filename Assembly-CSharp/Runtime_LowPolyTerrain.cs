using System;
using UnityEngine;
using VacuumShaders.LowPolyMeshGenerator;

// Token: 0x02000011 RID: 17
[AddComponentMenu("VacuumShaders/Low Poly Mesh Generator/Example/Runtime Low Poly Terrain")]
public class Runtime_LowPolyTerrain : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x00003F38 File Offset: 0x00002138
	private void Start()
	{
		if (this.targetTerrain == null)
		{
			return;
		}
		LowPolyMeshGenerator.CONVERTION_INFO[] array2;
		string[] array3;
		Mesh[] array = LowPolyMeshGenerator.GenerateLowPolyTerrain(this.targetTerrain, out array2, out array3, this.lowPolyOptions);
		if (array3 != null)
		{
			for (int i = 0; i < array3.Length; i++)
			{
				Debug.LogWarning(array3[i]);
			}
		}
		if (array != null)
		{
			for (int j = 0; j < array.Length; j++)
			{
				GameObject gameObject = new GameObject(array[j].name);
				gameObject.AddComponent<MeshFilter>().sharedMesh = array[j];
				gameObject.AddComponent<MeshRenderer>().sharedMaterial = this.vertexColorMaterial;
				gameObject.transform.parent = base.gameObject.transform;
				gameObject.transform.localPosition = Vector3.zero;
			}
		}
	}

	// Token: 0x0400003B RID: 59
	public Terrain targetTerrain;

	// Token: 0x0400003C RID: 60
	public LowPolyTerrainOptions lowPolyOptions;

	// Token: 0x0400003D RID: 61
	public Material vertexColorMaterial;
}

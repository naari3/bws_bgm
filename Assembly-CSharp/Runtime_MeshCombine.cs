using System;
using UnityEngine;
using VacuumShaders.LowPolyMeshGenerator;

// Token: 0x02000012 RID: 18
[AddComponentMenu("VacuumShaders/Low Poly Mesh Generator/Example/Runtime Mesh Combine")]
public class Runtime_MeshCombine : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00003FF0 File Offset: 0x000021F0
	private void Start()
	{
		LowPolyMeshGenerator.COMBINE_INFO combine_INFO = LowPolyMeshGenerator.CanBeMeshesCombined(this.meshfilterCollection);
		if (combine_INFO != LowPolyMeshGenerator.COMBINE_INFO.OK)
		{
			Debug.LogError(combine_INFO.ToString());
			return;
		}
		LowPolyMeshGenerator.CONVERTION_INFO[] array;
		string[] array2;
		Mesh mesh = LowPolyMeshGenerator.GenerateLowPolyMeshesAndThenCombine(this.meshfilterCollection, out array, out array2, this.lowPolyOptions);
		if (array2 != null)
		{
			for (int i = 0; i < array2.Length; i++)
			{
				if (array[i] != LowPolyMeshGenerator.CONVERTION_INFO.Ok)
				{
					Debug.LogWarning(array2[i]);
				}
			}
		}
		if (mesh != null)
		{
			base.gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
			base.gameObject.AddComponent<MeshRenderer>().sharedMaterial = this.vertexColorMaterial;
		}
	}

	// Token: 0x0400003E RID: 62
	public Transform meshfilterCollection;

	// Token: 0x0400003F RID: 63
	public LowPolyMeshOptions lowPolyOptions;

	// Token: 0x04000040 RID: 64
	public Material vertexColorMaterial;
}

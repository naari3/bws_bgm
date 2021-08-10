using System;
using UnityEngine;
using VacuumShaders.LowPolyMeshGenerator;

// Token: 0x02000010 RID: 16
[AddComponentMenu("VacuumShaders/Low Poly Mesh Generator/Example/Runtime Low Poly Mesh")]
public class Runtime_LowPolyMesh : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00003E68 File Offset: 0x00002068
	private void Start()
	{
		Mesh x = null;
		MeshFilter component = base.GetComponent<MeshFilter>();
		SkinnedMeshRenderer component2 = base.GetComponent<SkinnedMeshRenderer>();
		if (component != null)
		{
			x = component.sharedMesh;
		}
		if (x == null && component2 != null)
		{
			x = component2.sharedMesh;
		}
		if (x == null)
		{
			return;
		}
		LowPolyMeshGenerator.CONVERTION_INFO[] array;
		string[] array2;
		Mesh mesh = LowPolyMeshGenerator.GenerateLowPolyMesh(base.GetComponent<Renderer>(), out array, out array2, this.lowPolyOptions);
		if (array2 != null)
		{
			for (int i = 0; i < array2.Length; i++)
			{
				Debug.Log(array2[i]);
			}
		}
		if (mesh != null)
		{
			if (component != null)
			{
				component.sharedMesh = mesh;
			}
			else if (component2 != null)
			{
				component2.sharedMesh = mesh;
			}
			base.GetComponent<Renderer>().sharedMaterials = new Material[]
			{
				this.vertexColorMaterial
			};
		}
	}

	// Token: 0x04000039 RID: 57
	public LowPolyMeshOptions lowPolyOptions;

	// Token: 0x0400003A RID: 58
	public Material vertexColorMaterial;
}

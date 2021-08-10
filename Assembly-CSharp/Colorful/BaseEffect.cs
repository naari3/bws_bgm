using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200002F RID: 47
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("")]
	public class BaseEffect : MonoBehaviour
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009E70 File Offset: 0x00008070
		public Shader ShaderSafe
		{
			get
			{
				if (this.Shader == null)
				{
					this.Shader = Shader.Find(this.GetShaderName());
				}
				return this.Shader;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009E97 File Offset: 0x00008097
		public Material Material
		{
			get
			{
				if (this.m_Material == null)
				{
					this.m_Material = new Material(this.ShaderSafe)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_Material;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009EC6 File Offset: 0x000080C6
		protected virtual void Start()
		{
			if (this.ShaderSafe == null || !this.Shader.isSupported)
			{
				Debug.LogWarning("The shader is null or unsupported on this device " + this.GetShaderName());
				base.enabled = false;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009EFF File Offset: 0x000080FF
		protected virtual void OnDisable()
		{
			if (this.m_Material)
			{
				Object.DestroyImmediate(this.m_Material);
			}
			this.m_Material = null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009F20 File Offset: 0x00008120
		public void Apply(Texture source, RenderTexture destination)
		{
			if (source is RenderTexture)
			{
				this.OnRenderImage(source as RenderTexture, destination);
				return;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
			Graphics.Blit(source, temporary);
			this.OnRenderImage(temporary, destination);
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00002CA8 File Offset: 0x00000EA8
		protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009F6A File Offset: 0x0000816A
		protected virtual string GetShaderName()
		{
			return "null";
		}

		// Token: 0x04000101 RID: 257
		public Shader Shader;

		// Token: 0x04000102 RID: 258
		protected Material m_Material;
	}
}

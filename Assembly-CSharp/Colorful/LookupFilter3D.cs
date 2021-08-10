using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000056 RID: 86
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/lookup-filter-3d.html")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Colorful FX/Color Correction/Lookup Filter 3D")]
	public class LookupFilter3D : MonoBehaviour
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CB9A File Offset: 0x0000AD9A
		public Shader Shader2DSafe
		{
			get
			{
				if (this.Shader2D == null)
				{
					this.Shader2D = Shader.Find("Hidden/Colorful/Lookup Filter 2D");
				}
				return this.Shader2D;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		public Shader Shader3DSafe
		{
			get
			{
				if (this.Shader3D == null)
				{
					this.Shader3D = Shader.Find("Hidden/Colorful/Lookup Filter 3D");
				}
				return this.Shader3D;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public Material Material
		{
			get
			{
				if (this.m_Use2DLut || this.ForceCompatibility)
				{
					if (this.m_Material2D == null)
					{
						this.m_Material2D = new Material(this.Shader2DSafe)
						{
							hideFlags = HideFlags.HideAndDontSave
						};
					}
					return this.m_Material2D;
				}
				if (this.m_Material3D == null)
				{
					this.m_Material3D = new Material(this.Shader3DSafe)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_Material3D;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CC60 File Offset: 0x0000AE60
		protected virtual void Start()
		{
			if (!SystemInfo.supports3DTextures)
			{
				this.m_Use2DLut = true;
			}
			if (!this.Shader2DSafe || !this.Shader2D.isSupported)
			{
				Debug.LogWarning("The shader is null or unsupported on this device");
				base.enabled = false;
				return;
			}
			if (!this.m_Use2DLut && !this.ForceCompatibility && (!this.Shader3DSafe || !this.Shader3D.isSupported))
			{
				this.m_Use2DLut = true;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		protected virtual void OnDisable()
		{
			if (this.m_Material2D)
			{
				Object.DestroyImmediate(this.m_Material2D);
			}
			if (this.m_Material3D)
			{
				Object.DestroyImmediate(this.m_Material3D);
			}
			if (this.m_Lut3D)
			{
				Object.DestroyImmediate(this.m_Lut3D);
			}
			this.m_BaseTextureName = "";
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000CD38 File Offset: 0x0000AF38
		protected virtual void Reset()
		{
			this.m_BaseTextureName = "";
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000CD48 File Offset: 0x0000AF48
		protected void SetIdentityLut()
		{
			int num = 16;
			Color[] array = new Color[num * num * num];
			float num2 = 1f / (1f * (float)num - 1f);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
					}
				}
			}
			if (this.m_Lut3D)
			{
				Object.DestroyImmediate(this.m_Lut3D);
			}
			this.m_Lut3D = new Texture3D(num, num, num, TextureFormat.ARGB32, false)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			this.m_Lut3D.SetPixels(array);
			this.m_Lut3D.Apply();
			this.m_BaseTextureName = "";
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000CE33 File Offset: 0x0000B033
		public bool ValidDimensions(Texture2D tex2D)
		{
			return !(tex2D == null) && tex2D.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2D.width));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000CE5C File Offset: 0x0000B05C
		protected void ConvertBaseTexture()
		{
			if (!this.ValidDimensions(this.LookupTexture))
			{
				Debug.LogWarning("The given 2D texture " + this.LookupTexture.name + " cannot be used as a 3D LUT. Pick another texture or adjust dimension to e.g. 256x16.");
				return;
			}
			this.m_BaseTextureName = this.LookupTexture.name;
			int height = this.LookupTexture.height;
			Color[] pixels = this.LookupTexture.GetPixels();
			Color[] array = new Color[pixels.Length];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < height; k++)
					{
						int num = height - j - 1;
						array[i + j * height + k * height * height] = pixels[k * height + i + num * height * height];
					}
				}
			}
			if (this.m_Lut3D)
			{
				Object.DestroyImmediate(this.m_Lut3D);
			}
			this.m_Lut3D = new Texture3D(height, height, height, TextureFormat.ARGB32, false)
			{
				hideFlags = HideFlags.HideAndDontSave,
				wrapMode = TextureWrapMode.Clamp
			};
			this.m_Lut3D.SetPixels(array);
			this.m_Lut3D.Apply();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CF74 File Offset: 0x0000B174
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

		// Token: 0x06000250 RID: 592 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.LookupTexture == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.m_Use2DLut || this.ForceCompatibility)
			{
				this.RenderLut2D(source, destination);
				return;
			}
			this.RenderLut3D(source, destination);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D014 File Offset: 0x0000B214
		protected virtual void RenderLut2D(RenderTexture source, RenderTexture destination)
		{
			float num = Mathf.Sqrt((float)this.LookupTexture.width);
			this.Material.SetTexture("_LookupTex", this.LookupTexture);
			this.Material.SetVector("_Params1", new Vector3(1f / (float)this.LookupTexture.width, 1f / (float)this.LookupTexture.height, num - 1f));
			this.Material.SetVector("_Params2", new Vector2(this.Amount, 0f));
			Graphics.Blit(source, destination, this.Material, CLib.IsLinearColorSpace() ? 1 : 0);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		protected virtual void RenderLut3D(RenderTexture source, RenderTexture destination)
		{
			if (this.LookupTexture.name != this.m_BaseTextureName)
			{
				this.ConvertBaseTexture();
			}
			if (this.m_Lut3D == null)
			{
				this.SetIdentityLut();
			}
			this.Material.SetTexture("_LookupTex", this.m_Lut3D);
			float num = (float)this.m_Lut3D.width;
			this.Material.SetVector("_Params", new Vector3((num - 1f) / (1f * num), 1f / (2f * num), this.Amount));
			Graphics.Blit(source, destination, this.Material, CLib.IsLinearColorSpace() ? 1 : 0);
		}

		// Token: 0x040001BB RID: 443
		[Tooltip("The lookup texture to apply. Read the documentation to learn how to create one.")]
		public Texture2D LookupTexture;

		// Token: 0x040001BC RID: 444
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x040001BD RID: 445
		[Tooltip("The effect will automatically detect the correct shader to use for the device but you can force it to only use the compatibility shader.")]
		public bool ForceCompatibility;

		// Token: 0x040001BE RID: 446
		protected Texture3D m_Lut3D;

		// Token: 0x040001BF RID: 447
		protected string m_BaseTextureName;

		// Token: 0x040001C0 RID: 448
		protected bool m_Use2DLut;

		// Token: 0x040001C1 RID: 449
		public Shader Shader2D;

		// Token: 0x040001C2 RID: 450
		public Shader Shader3D;

		// Token: 0x040001C3 RID: 451
		protected Material m_Material2D;

		// Token: 0x040001C4 RID: 452
		protected Material m_Material3D;
	}
}

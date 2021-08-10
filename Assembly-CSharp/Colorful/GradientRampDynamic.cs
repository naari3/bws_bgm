using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000048 RID: 72
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/gradient-ramp-dynamic.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Gradient Ramp (Dynamic)")]
	public class GradientRampDynamic : BaseEffect
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000BABB File Offset: 0x00009CBB
		protected override void Start()
		{
			base.Start();
			if (this.Ramp != null)
			{
				this.UpdateGradientCache();
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		protected virtual void Reset()
		{
			this.Ramp = new Gradient
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(Color.black, 0f),
					new GradientColorKey(Color.white, 1f)
				},
				alphaKeys = new GradientAlphaKey[]
				{
					new GradientAlphaKey(1f, 0f),
					new GradientAlphaKey(1f, 1f)
				}
			};
			this.UpdateGradientCache();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000BB64 File Offset: 0x00009D64
		public void UpdateGradientCache()
		{
			if (this.m_RampTexture == null)
			{
				this.m_RampTexture = new Texture2D(256, 1, TextureFormat.RGB24, false)
				{
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					hideFlags = HideFlags.HideAndDontSave
				};
			}
			Color[] array = new Color[256];
			for (int i = 0; i < 256; i++)
			{
				array[i] = this.Ramp.Evaluate((float)i / 255f);
			}
			this.m_RampTexture.SetPixels(array);
			this.m_RampTexture.Apply();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Ramp == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetTexture("_RampTex", this.m_RampTexture);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000BAA1 File Offset: 0x00009CA1
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Gradient Ramp";
		}

		// Token: 0x04000175 RID: 373
		[Tooltip("Gradient used to remap the pixels luminosity.")]
		public Gradient Ramp;

		// Token: 0x04000176 RID: 374
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x04000177 RID: 375
		protected Texture2D m_RampTexture;
	}
}

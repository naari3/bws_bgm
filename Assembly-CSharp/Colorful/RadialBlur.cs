using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005D RID: 93
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/radial-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Radial Blur")]
	public class RadialBlur : BaseEffect
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000D528 File Offset: 0x0000B728
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Strength <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			int num = (this.Quality == RadialBlur.QualityPreset.Custom) ? this.Samples : ((int)this.Quality);
			base.Material.SetVector("_Center", this.Center);
			base.Material.SetVector("_Params", new Vector4(this.Strength, (float)num, this.Sharpness * 0.01f, this.Darkness * 0.02f));
			Graphics.Blit(source, destination, base.Material, this.EnableVignette ? 1 : 0);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Radial Blur";
		}

		// Token: 0x040001D7 RID: 471
		[Range(0f, 1f)]
		[Tooltip("Blur strength.")]
		public float Strength = 0.1f;

		// Token: 0x040001D8 RID: 472
		[Range(2f, 32f)]
		[Tooltip("Sample count. Higher means better quality but slower processing.")]
		public int Samples = 10;

		// Token: 0x040001D9 RID: 473
		[Tooltip("Focus point.")]
		public Vector2 Center = new Vector2(0.5f, 0.5f);

		// Token: 0x040001DA RID: 474
		[Tooltip("Quality preset. Higher means better quality but slower processing.")]
		public RadialBlur.QualityPreset Quality = RadialBlur.QualityPreset.Medium;

		// Token: 0x040001DB RID: 475
		[Range(-100f, 100f)]
		[Tooltip("Smoothness of the vignette effect.")]
		public float Sharpness = 40f;

		// Token: 0x040001DC RID: 476
		[Range(0f, 100f)]
		[Tooltip("Amount of vignetting on screen.")]
		public float Darkness = 35f;

		// Token: 0x040001DD RID: 477
		[Tooltip("Should the effect be applied like a vignette ?")]
		public bool EnableVignette = true;

		// Token: 0x0200009D RID: 157
		public enum QualityPreset
		{
			// Token: 0x040002DC RID: 732
			Low = 4,
			// Token: 0x040002DD RID: 733
			Medium = 8,
			// Token: 0x040002DE RID: 734
			High = 12,
			// Token: 0x040002DF RID: 735
			Custom
		}
	}
}

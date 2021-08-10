using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003F RID: 63
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/directional-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Directional Blur")]
	public class DirectionalBlur : BaseEffect
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000AED8 File Offset: 0x000090D8
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			int num = (this.Quality == DirectionalBlur.QualityPreset.Custom) ? this.Samples : ((int)this.Quality);
			float x = Mathf.Sin(this.Angle) * this.Strength * 0.05f / (float)num;
			float y = Mathf.Cos(this.Angle) * this.Strength * 0.05f / (float)num;
			base.Material.SetVector("_Params", new Vector3(x, y, (float)num));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000AF60 File Offset: 0x00009160
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/DirectionalBlur";
		}

		// Token: 0x04000146 RID: 326
		[Tooltip("Quality preset. Higher means better quality but slower processing.")]
		public DirectionalBlur.QualityPreset Quality = DirectionalBlur.QualityPreset.Medium;

		// Token: 0x04000147 RID: 327
		[Range(1f, 16f)]
		[Tooltip("Sample count. Higher means better quality but slower processing.")]
		public int Samples = 5;

		// Token: 0x04000148 RID: 328
		[Range(0f, 5f)]
		[Tooltip("Blur strength (distance).")]
		public float Strength = 1f;

		// Token: 0x04000149 RID: 329
		[Tooltip("Blur direction in radians.")]
		public float Angle;

		// Token: 0x02000092 RID: 146
		public enum QualityPreset
		{
			// Token: 0x04000294 RID: 660
			Low = 2,
			// Token: 0x04000295 RID: 661
			Medium = 4,
			// Token: 0x04000296 RID: 662
			High = 6,
			// Token: 0x04000297 RID: 663
			Custom
		}
	}
}

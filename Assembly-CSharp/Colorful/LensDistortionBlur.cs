using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000051 RID: 81
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/lens-distortion-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Lens Distortion Blur")]
	public class LensDistortionBlur : BaseEffect
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000C350 File Offset: 0x0000A550
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			int num = (this.Quality == LensDistortionBlur.QualityPreset.Custom) ? this.Samples : ((int)this.Quality);
			base.Material.SetVector("_Params", new Vector4((float)num, this.Distortion / (float)num, this.CubicDistortion / (float)num, this.Scale));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C3B2 File Offset: 0x0000A5B2
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/LensDistortionBlur";
		}

		// Token: 0x040001A5 RID: 421
		[Tooltip("Quality preset. Higher means better quality but slower processing.")]
		public LensDistortionBlur.QualityPreset Quality = LensDistortionBlur.QualityPreset.Medium;

		// Token: 0x040001A6 RID: 422
		[Range(2f, 32f)]
		[Tooltip("Sample count. Higher means better quality but slower processing.")]
		public int Samples = 10;

		// Token: 0x040001A7 RID: 423
		[Range(-2f, 2f)]
		[Tooltip("Spherical distortion factor.")]
		public float Distortion = 0.2f;

		// Token: 0x040001A8 RID: 424
		[Range(-2f, 2f)]
		[Tooltip("Cubic distortion factor.")]
		public float CubicDistortion = 0.6f;

		// Token: 0x040001A9 RID: 425
		[Range(0.01f, 2f)]
		[Tooltip("Helps avoid screen streching on borders when working with heavy distortions.")]
		public float Scale = 0.8f;

		// Token: 0x02000098 RID: 152
		public enum QualityPreset
		{
			// Token: 0x040002AD RID: 685
			Low = 4,
			// Token: 0x040002AE RID: 686
			Medium = 8,
			// Token: 0x040002AF RID: 687
			High = 12,
			// Token: 0x040002B0 RID: 688
			Custom
		}
	}
}

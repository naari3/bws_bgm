using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000065 RID: 101
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/threshold.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Threshold")]
	public class Threshold : BaseEffect
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetFloat("_Threshold", this.Value / 255f);
			base.Material.SetFloat("_Range", this.NoiseRange / 255f);
			Graphics.Blit(source, destination, base.Material, this.UseNoise ? 1 : 0);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DE36 File Offset: 0x0000C036
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Threshold";
		}

		// Token: 0x040001FE RID: 510
		[Range(1f, 255f)]
		[Tooltip("Luminosity threshold.")]
		public float Value = 128f;

		// Token: 0x040001FF RID: 511
		[Range(0f, 128f)]
		[Tooltip("Aomunt of randomization.")]
		public float NoiseRange = 24f;

		// Token: 0x04000200 RID: 512
		[Tooltip("Adds some randomization to the threshold value.")]
		public bool UseNoise;
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000035 RID: 53
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/brightness-contrast-gamma.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Brightness, Contrast, Gamma")]
	public class BrightnessContrastGamma : BaseEffect
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000A4A0 File Offset: 0x000086A0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Brightness == 0f && this.Contrast == 0f && this.Gamma == 1f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_BCG", new Vector4((this.Brightness + 100f) * 0.01f, (this.Contrast + 100f) * 0.01f, 1f / this.Gamma));
			base.Material.SetVector("_Coeffs", this.ContrastCoeff);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A549 File Offset: 0x00008749
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Brightness Contrast Gamma";
		}

		// Token: 0x04000117 RID: 279
		[Range(-100f, 100f)]
		[Tooltip("Moving the slider to the right increases tonal values and expands highlights, to the left decreases values and expands shadows.")]
		public float Brightness;

		// Token: 0x04000118 RID: 280
		[Range(-100f, 100f)]
		[Tooltip("Expands or shrinks the overall range of tonal values.")]
		public float Contrast;

		// Token: 0x04000119 RID: 281
		public Vector3 ContrastCoeff = new Vector3(0.5f, 0.5f, 0.5f);

		// Token: 0x0400011A RID: 282
		[Range(0.1f, 9.9f)]
		[Tooltip("Simple power function.")]
		public float Gamma = 1f;
	}
}

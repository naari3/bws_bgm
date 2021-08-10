using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003C RID: 60
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/contrast-vignette.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Contrast Vignette")]
	public class ContrastVignette : BaseEffect
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000ABDC File Offset: 0x00008DDC
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Params", new Vector4(this.Sharpness * 0.01f, this.Darkness * 0.02f, this.Contrast * 0.01f, this.EdgeBlending * 0.01f));
			base.Material.SetVector("_Coeffs", this.ContrastCoeff);
			base.Material.SetVector("_Center", this.Center);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AC71 File Offset: 0x00008E71
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Contrast Vignette";
		}

		// Token: 0x04000137 RID: 311
		[Tooltip("Center point.")]
		public Vector2 Center = new Vector2(0.5f, 0.5f);

		// Token: 0x04000138 RID: 312
		[Range(-100f, 100f)]
		[Tooltip("Smoothness of the vignette effect.")]
		public float Sharpness = 32f;

		// Token: 0x04000139 RID: 313
		[Range(0f, 100f)]
		[Tooltip("Amount of vignetting on screen.")]
		public float Darkness = 28f;

		// Token: 0x0400013A RID: 314
		[Range(0f, 200f)]
		[Tooltip("Expands or shrinks the overall range of tonal values in the vignette area.")]
		public float Contrast = 20f;

		// Token: 0x0400013B RID: 315
		public Vector3 ContrastCoeff = new Vector3(0.5f, 0.5f, 0.5f);

		// Token: 0x0400013C RID: 316
		[Range(0f, 200f)]
		[Tooltip("Blends the contrast change toward the edges of the vignette effect.")]
		public float EdgeBlending;
	}
}

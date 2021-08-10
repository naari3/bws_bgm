using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000043 RID: 67
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/fast-vignette.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Fast Vignette")]
	public class FastVignette : BaseEffect
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000B32C File Offset: 0x0000952C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Params", new Vector4(this.Center.x, this.Center.y, this.Sharpness * 0.01f, this.Darkness * 0.02f));
			base.Material.SetColor("_Color", this.Color);
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B3A5 File Offset: 0x000095A5
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Fast Vignette";
		}

		// Token: 0x0400015C RID: 348
		[Tooltip("Vignette type.")]
		public FastVignette.ColorMode Mode;

		// Token: 0x0400015D RID: 349
		[ColorUsage(false)]
		[Tooltip("The color to use in the vignette area.")]
		public Color Color = Color.red;

		// Token: 0x0400015E RID: 350
		[Tooltip("Center point.")]
		public Vector2 Center = new Vector2(0.5f, 0.5f);

		// Token: 0x0400015F RID: 351
		[Range(-100f, 100f)]
		[Tooltip("Smoothness of the vignette effect.")]
		public float Sharpness = 10f;

		// Token: 0x04000160 RID: 352
		[Range(0f, 100f)]
		[Tooltip("Amount of vignetting on screen.")]
		public float Darkness = 30f;

		// Token: 0x02000093 RID: 147
		public enum ColorMode
		{
			// Token: 0x04000299 RID: 665
			Classic,
			// Token: 0x0400029A RID: 666
			Desaturate,
			// Token: 0x0400029B RID: 667
			Colored
		}
	}
}

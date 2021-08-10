using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000040 RID: 64
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/dithering.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Dithering")]
	public class Dithering : BaseEffect
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0000AF88 File Offset: 0x00009188
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.m_DitherPattern == null)
			{
				this.m_DitherPattern = Resources.Load<Texture2D>("Misc/DitherPattern");
			}
			base.Material.SetTexture("_Pattern", this.m_DitherPattern);
			base.Material.SetVector("_Params", new Vector4(this.RedLuminance, this.GreenLuminance, this.BlueLuminance, this.Amount));
			int num = this.ShowOriginal ? 4 : 0;
			num += (this.ConvertToGrayscale ? 2 : 0);
			num += (CLib.IsLinearColorSpace() ? 1 : 0);
			Graphics.Blit(source, destination, base.Material, num);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B043 File Offset: 0x00009243
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Dithering";
		}

		// Token: 0x0400014A RID: 330
		[Tooltip("Show the original picture under the dithering pass.")]
		public bool ShowOriginal;

		// Token: 0x0400014B RID: 331
		[Tooltip("Convert the original render to black & white.")]
		public bool ConvertToGrayscale;

		// Token: 0x0400014C RID: 332
		[Range(0f, 1f)]
		[Tooltip("Amount of red to contribute to the luminosity.")]
		public float RedLuminance = 0.299f;

		// Token: 0x0400014D RID: 333
		[Range(0f, 1f)]
		[Tooltip("Amount of green to contribute to the luminosity.")]
		public float GreenLuminance = 0.587f;

		// Token: 0x0400014E RID: 334
		[Range(0f, 1f)]
		[Tooltip("Amount of blue to contribute to the luminosity.")]
		public float BlueLuminance = 0.114f;

		// Token: 0x0400014F RID: 335
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x04000150 RID: 336
		protected Texture2D m_DitherPattern;
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200004A RID: 74
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/grayscale.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Grayscale")]
	public class Grayscale : BaseEffect
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000BCEC File Offset: 0x00009EEC
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector4(this.RedLuminance, this.GreenLuminance, this.BlueLuminance, this.Amount));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BD48 File Offset: 0x00009F48
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Grayscale";
		}

		// Token: 0x0400017A RID: 378
		[Range(0f, 1f)]
		[Tooltip("Amount of red to contribute to the luminosity.")]
		public float RedLuminance = 0.299f;

		// Token: 0x0400017B RID: 379
		[Range(0f, 1f)]
		[Tooltip("Amount of green to contribute to the luminosity.")]
		public float GreenLuminance = 0.587f;

		// Token: 0x0400017C RID: 380
		[Range(0f, 1f)]
		[Tooltip("Amount of blue to contribute to the luminosity.")]
		public float BlueLuminance = 0.114f;

		// Token: 0x0400017D RID: 381
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000047 RID: 71
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/gradient-ramp.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Gradient Ramp")]
	public class GradientRamp : BaseEffect
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000BA38 File Offset: 0x00009C38
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.RampTexture == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetTexture("_RampTex", this.RampTexture);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000BAA1 File Offset: 0x00009CA1
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Gradient Ramp";
		}

		// Token: 0x04000173 RID: 371
		[Tooltip("Texture used to remap the pixels luminosity.")]
		public Texture RampTexture;

		// Token: 0x04000174 RID: 372
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}

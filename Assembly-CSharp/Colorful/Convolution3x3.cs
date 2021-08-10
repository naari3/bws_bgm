using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003D RID: 61
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/convolution-3x3.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Convolution Matrix 3x3")]
	public class Convolution3x3 : BaseEffect
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000ACDC File Offset: 0x00008EDC
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_PSize", new Vector2(1f / (float)source.width, 1f / (float)source.height));
			base.Material.SetVector("_KernelT", this.KernelTop / this.Divisor);
			base.Material.SetVector("_KernelM", this.KernelMiddle / this.Divisor);
			base.Material.SetVector("_KernelB", this.KernelBottom / this.Divisor);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Convolution 3x3";
		}

		// Token: 0x0400013D RID: 317
		public Vector3 KernelTop = Vector3.zero;

		// Token: 0x0400013E RID: 318
		public Vector3 KernelMiddle = Vector3.up;

		// Token: 0x0400013F RID: 319
		public Vector3 KernelBottom = Vector3.zero;

		// Token: 0x04000140 RID: 320
		[Tooltip("Used to normalize the kernel.")]
		public float Divisor = 1f;

		// Token: 0x04000141 RID: 321
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}

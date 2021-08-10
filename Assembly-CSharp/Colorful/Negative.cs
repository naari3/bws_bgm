using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000057 RID: 87
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/negative.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Negative")]
	public class Negative : BaseEffect
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000D198 File Offset: 0x0000B398
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material, CLib.IsLinearColorSpace() ? 1 : 0);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Negative";
		}

		// Token: 0x040001C5 RID: 453
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}

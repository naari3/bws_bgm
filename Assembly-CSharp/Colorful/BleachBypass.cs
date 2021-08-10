using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000033 RID: 51
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/bleach-bypass.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Bleach Bypass")]
	public class BleachBypass : BaseEffect
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A3BF File Offset: 0x000085BF
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A3F9 File Offset: 0x000085F9
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Bleach Bypass";
		}

		// Token: 0x04000113 RID: 275
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}

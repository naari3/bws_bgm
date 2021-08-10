using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005C RID: 92
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/posterize.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Posterize")]
	public class Posterize : BaseEffect
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector2((float)this.Levels, this.Amount));
			Graphics.Blit(source, destination, base.Material, this.LuminosityOnly ? 1 : 0);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D506 File Offset: 0x0000B706
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Posterize";
		}

		// Token: 0x040001D4 RID: 468
		[Range(2f, 255f)]
		[Tooltip("Number of tonal levels (brightness values) for each channel.")]
		public int Levels = 16;

		// Token: 0x040001D5 RID: 469
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x040001D6 RID: 470
		[Tooltip("Only affects luminosity. Use this if you don't want any hue shifting or color changes.")]
		public bool LuminosityOnly;
	}
}

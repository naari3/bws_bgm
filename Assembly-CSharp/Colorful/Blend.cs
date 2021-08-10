using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000034 RID: 52
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/blend.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Blend")]
	public class Blend : BaseEffect
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000A414 File Offset: 0x00008614
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Texture == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetTexture("_OverlayTex", this.Texture);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A483 File Offset: 0x00008683
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Blend";
		}

		// Token: 0x04000114 RID: 276
		[Tooltip("The Texture2D, RenderTexture or MovieTexture to blend.")]
		public Texture Texture;

		// Token: 0x04000115 RID: 277
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x04000116 RID: 278
		[Tooltip("Blending mode.")]
		public Blend.BlendingMode Mode;

		// Token: 0x02000090 RID: 144
		public enum BlendingMode
		{
			// Token: 0x0400027A RID: 634
			Darken,
			// Token: 0x0400027B RID: 635
			Multiply,
			// Token: 0x0400027C RID: 636
			ColorBurn,
			// Token: 0x0400027D RID: 637
			LinearBurn,
			// Token: 0x0400027E RID: 638
			DarkerColor,
			// Token: 0x0400027F RID: 639
			Lighten = 6,
			// Token: 0x04000280 RID: 640
			Screen,
			// Token: 0x04000281 RID: 641
			ColorDodge,
			// Token: 0x04000282 RID: 642
			LinearDodge,
			// Token: 0x04000283 RID: 643
			LighterColor,
			// Token: 0x04000284 RID: 644
			Overlay = 12,
			// Token: 0x04000285 RID: 645
			SoftLight,
			// Token: 0x04000286 RID: 646
			HardLight,
			// Token: 0x04000287 RID: 647
			VividLight,
			// Token: 0x04000288 RID: 648
			LinearLight,
			// Token: 0x04000289 RID: 649
			PinLight,
			// Token: 0x0400028A RID: 650
			HardMix,
			// Token: 0x0400028B RID: 651
			Difference = 20,
			// Token: 0x0400028C RID: 652
			Exclusion,
			// Token: 0x0400028D RID: 653
			Subtract,
			// Token: 0x0400028E RID: 654
			Divide
		}
	}
}

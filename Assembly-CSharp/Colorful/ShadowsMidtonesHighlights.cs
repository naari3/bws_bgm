using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000060 RID: 96
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/shadows-midtones-highlights.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Shadows, Midtones, Highlights")]
	public class ShadowsMidtonesHighlights : BaseEffect
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000D794 File Offset: 0x0000B994
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Shadows", this.Shadows * (this.Shadows.a * 2f));
			float b = 1f + (1f - (this.Midtones.r * 0.299f + this.Midtones.g * 0.587f + this.Midtones.b * 0.114f));
			base.Material.SetVector("_Midtones", this.Midtones * b * (this.Midtones.a * 2f));
			b = 1f + (1f - (this.Highlights.r * 0.299f + this.Highlights.g * 0.587f + this.Highlights.b * 0.114f));
			base.Material.SetVector("_Highlights", this.Highlights * b * (this.Highlights.a * 2f));
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D902 File Offset: 0x0000BB02
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Shadows Midtones Highlights";
		}

		// Token: 0x040001E6 RID: 486
		[Tooltip("Color mode. The difference between these two modes is the way shadows are handled.")]
		public ShadowsMidtonesHighlights.ColorMode Mode;

		// Token: 0x040001E7 RID: 487
		[Tooltip("Adds density or darkness, raises or lowers the shadow levels with its alpha value and offset the color balance in the dark regions with the hue point.")]
		public Color Shadows = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x040001E8 RID: 488
		[Tooltip("Shifts the middle tones to be brighter or darker. For instance, to make your render more warm, just move the midtone color toward the yellow/red range. The more saturated the color is, the warmer the render becomes.")]
		public Color Midtones = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x040001E9 RID: 489
		[Tooltip("Brightens and tints the entire render but mostly affects the highlights.")]
		public Color Highlights = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x040001EA RID: 490
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;

		// Token: 0x0200009E RID: 158
		public enum ColorMode
		{
			// Token: 0x040002E1 RID: 737
			LiftGammaGain,
			// Token: 0x040002E2 RID: 738
			OffsetGammaSlope
		}
	}
}

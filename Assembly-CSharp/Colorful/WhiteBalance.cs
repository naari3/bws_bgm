using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200006B RID: 107
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/white-balance.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/White Balance")]
	public class WhiteBalance : BaseEffect
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000E12D File Offset: 0x0000C32D
		protected virtual void Reset()
		{
			this.White = (CLib.IsLinearColorSpace() ? new Color(0.72974f, 0.72974f, 0.72974f) : new Color(0.5f, 0.5f, 0.5f));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E166 File Offset: 0x0000C366
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetColor("_White", this.White);
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E191 File Offset: 0x0000C391
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/White Balance";
		}

		// Token: 0x04000210 RID: 528
		[ColorUsage(false)]
		[Tooltip("Reference white point or midtone value.")]
		public Color White = new Color(0.5f, 0.5f, 0.5f);

		// Token: 0x04000211 RID: 529
		[Tooltip("Algorithm used.")]
		public WhiteBalance.BalanceMode Mode = WhiteBalance.BalanceMode.Complex;

		// Token: 0x020000A2 RID: 162
		public enum BalanceMode
		{
			// Token: 0x0400030B RID: 779
			Simple,
			// Token: 0x0400030C RID: 780
			Complex
		}
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005F RID: 95
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/s-curve-contrast.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/S-Curve Contrast")]
	public class SCurveContrast : BaseEffect
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Red", new Vector2(this.RedSteepness, this.RedGamma));
			base.Material.SetVector("_Green", new Vector2(this.GreenSteepness, this.GreenGamma));
			base.Material.SetVector("_Blue", new Vector2(this.BlueSteepness, this.BlueGamma));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D734 File Offset: 0x0000B934
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/SCurveContrast";
		}

		// Token: 0x040001E0 RID: 480
		public float RedSteepness = 1f;

		// Token: 0x040001E1 RID: 481
		public float RedGamma = 1f;

		// Token: 0x040001E2 RID: 482
		public float GreenSteepness = 1f;

		// Token: 0x040001E3 RID: 483
		public float GreenGamma = 1f;

		// Token: 0x040001E4 RID: 484
		public float BlueSteepness = 1f;

		// Token: 0x040001E5 RID: 485
		public float BlueGamma = 1f;
	}
}

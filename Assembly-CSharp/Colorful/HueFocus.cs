using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200004D RID: 77
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/hue-focus.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Hue Focus")]
	public class HueFocus : BaseEffect
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000BF14 File Offset: 0x0000A114
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = this.Hue / 360f;
			float num2 = this.Range / 180f;
			base.Material.SetVector("_Range", new Vector2(num - num2, num + num2));
			base.Material.SetVector("_Params", new Vector3(num, this.Boost + 1f, this.Amount));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BF95 File Offset: 0x0000A195
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Hue Focus";
		}

		// Token: 0x04000184 RID: 388
		[Range(0f, 360f)]
		[Tooltip("Center hue.")]
		public float Hue;

		// Token: 0x04000185 RID: 389
		[Range(1f, 180f)]
		[Tooltip("Hue range to focus on.")]
		public float Range = 30f;

		// Token: 0x04000186 RID: 390
		[Range(0f, 1f)]
		[Tooltip("Makes the colored pixels more vibrant.")]
		public float Boost = 0.5f;

		// Token: 0x04000187 RID: 391
		[Range(0f, 1f)]
		[Tooltip("Blending Factor.")]
		public float Amount = 1f;
	}
}

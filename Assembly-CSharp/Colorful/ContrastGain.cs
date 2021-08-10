using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003B RID: 59
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/contrast-gain.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Contrast Gain")]
	public class ContrastGain : BaseEffect
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000AB9B File Offset: 0x00008D9B
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetFloat("_Gain", this.Gain);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Contrast Gain";
		}

		// Token: 0x04000136 RID: 310
		[Range(0.001f, 2f)]
		[Tooltip("Steepness of the contrast curve. 1 is linear, no contrast change.")]
		public float Gain = 1f;
	}
}

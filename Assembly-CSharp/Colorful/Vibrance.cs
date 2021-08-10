using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000067 RID: 103
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/vibrance.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Vibrance")]
	public class Vibrance : BaseEffect
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.AdvancedMode)
			{
				base.Material.SetFloat("_Amount", this.Amount * 0.01f);
				base.Material.SetVector("_Channels", new Vector3(this.RedChannel, this.GreenChannel, this.BlueChannel));
				Graphics.Blit(source, destination, base.Material, 1);
				return;
			}
			base.Material.SetFloat("_Amount", this.Amount * 0.02f);
			Graphics.Blit(source, destination, base.Material, 0);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DF83 File Offset: 0x0000C183
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Vibrance";
		}

		// Token: 0x04000203 RID: 515
		[Range(-100f, 100f)]
		[Tooltip("Adjusts the saturation so that clipping is minimized as colors approach full saturation.")]
		public float Amount;

		// Token: 0x04000204 RID: 516
		[Range(-5f, 5f)]
		public float RedChannel = 1f;

		// Token: 0x04000205 RID: 517
		[Range(-5f, 5f)]
		public float GreenChannel = 1f;

		// Token: 0x04000206 RID: 518
		[Range(-5f, 5f)]
		public float BlueChannel = 1f;

		// Token: 0x04000207 RID: 519
		public bool AdvancedMode;
	}
}

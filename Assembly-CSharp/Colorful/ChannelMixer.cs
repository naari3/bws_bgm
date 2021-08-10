using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000037 RID: 55
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/channel-mixer.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Channel Mixer")]
	public class ChannelMixer : BaseEffect
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000A648 File Offset: 0x00008848
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Red", new Vector4(this.Red.x * 0.01f, this.Green.x * 0.01f, this.Blue.x * 0.01f));
			base.Material.SetVector("_Green", new Vector4(this.Red.y * 0.01f, this.Green.y * 0.01f, this.Blue.y * 0.01f));
			base.Material.SetVector("_Blue", new Vector4(this.Red.z * 0.01f, this.Green.z * 0.01f, this.Blue.z * 0.01f));
			base.Material.SetVector("_Constant", new Vector4(this.Constant.x * 0.01f, this.Constant.y * 0.01f, this.Constant.z * 0.01f));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A782 File Offset: 0x00008982
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Channel Mixer";
		}

		// Token: 0x0400011E RID: 286
		public Vector3 Red = new Vector3(100f, 0f, 0f);

		// Token: 0x0400011F RID: 287
		public Vector3 Green = new Vector3(0f, 100f, 0f);

		// Token: 0x04000120 RID: 288
		public Vector3 Blue = new Vector3(0f, 0f, 100f);

		// Token: 0x04000121 RID: 289
		public Vector3 Constant = new Vector3(0f, 0f, 0f);
	}
}

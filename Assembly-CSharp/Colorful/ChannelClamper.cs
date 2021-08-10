using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000036 RID: 54
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/channel-clamper.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Channel Clamper")]
	public class ChannelClamper : BaseEffect
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000A580 File Offset: 0x00008780
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_RedClamp", this.Red);
			base.Material.SetVector("_GreenClamp", this.Green);
			base.Material.SetVector("_BlueClamp", this.Blue);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A5EB File Offset: 0x000087EB
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Channel Clamper";
		}

		// Token: 0x0400011B RID: 283
		public Vector2 Red = new Vector2(0f, 1f);

		// Token: 0x0400011C RID: 284
		public Vector2 Green = new Vector2(0f, 1f);

		// Token: 0x0400011D RID: 285
		public Vector2 Blue = new Vector2(0f, 1f);
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000066 RID: 102
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/tv-vignette.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/TV Vignette")]
	public class TVVignette : BaseEffect
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000DE5C File Offset: 0x0000C05C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Offset >= 1f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector2(this.Size, this.Offset));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000DEB1 File Offset: 0x0000C0B1
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/TV Vignette";
		}

		// Token: 0x04000201 RID: 513
		[Min(0f)]
		public float Size = 25f;

		// Token: 0x04000202 RID: 514
		[Range(0f, 1f)]
		public float Offset = 0.2f;
	}
}

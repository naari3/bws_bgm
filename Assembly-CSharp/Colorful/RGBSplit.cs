using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005E RID: 94
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/rgb-split.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/RGB Split")]
	public class RGBSplit : BaseEffect
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000D634 File Offset: 0x0000B834
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector3(this.Amount * 0.001f, Mathf.Sin(this.Angle), Mathf.Cos(this.Angle)));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D69F File Offset: 0x0000B89F
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/RGB Split";
		}

		// Token: 0x040001DE RID: 478
		[Tooltip("RGB shifting amount.")]
		public float Amount;

		// Token: 0x040001DF RID: 479
		[Tooltip("Shift direction in radians.")]
		public float Angle;
	}
}

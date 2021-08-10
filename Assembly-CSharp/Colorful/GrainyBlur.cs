using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000049 RID: 73
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/grainy-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Grainy Blur")]
	public class GrainyBlur : BaseEffect
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000BC6C File Offset: 0x00009E6C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (Mathf.Approximately(this.Radius, 0f))
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector2(this.Radius, (float)this.Samples));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BCC7 File Offset: 0x00009EC7
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/GrainyBlur";
		}

		// Token: 0x04000178 RID: 376
		[Min(0f)]
		[Tooltip("Blur radius.")]
		public float Radius = 32f;

		// Token: 0x04000179 RID: 377
		[Range(1f, 32f)]
		[Tooltip("Sample count. Higher means better quality but slower processing.")]
		public int Samples = 16;
	}
}

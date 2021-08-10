using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200004F RID: 79
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/kuwahara.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Kuwahara")]
	public class Kuwahara : BaseEffect
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000C214 File Offset: 0x0000A414
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			this.Radius = Mathf.Clamp(this.Radius, 1, 6);
			base.Material.SetVector("_PSize", new Vector2(1f / (float)source.width, 1f / (float)source.height));
			Graphics.Blit(source, destination, base.Material, this.Radius - 1);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C27D File Offset: 0x0000A47D
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Kuwahara";
		}

		// Token: 0x0400019E RID: 414
		[Range(1f, 6f)]
		[Tooltip("Larger radius will give a more abstract look but will lower performances.")]
		public int Radius = 3;
	}
}

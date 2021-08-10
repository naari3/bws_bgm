using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000052 RID: 82
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/letterbox.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Letterbox")]
	public class Letterbox : BaseEffect
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = (float)source.width;
			float num2 = (float)source.height;
			float num3 = num / num2;
			int pass = 0;
			base.Material.SetColor("_FillColor", this.FillColor);
			float num4;
			if (num3 < this.Aspect)
			{
				num4 = (num2 - num / this.Aspect) * 0.5f / num2;
			}
			else
			{
				if (num3 <= this.Aspect)
				{
					Graphics.Blit(source, destination);
					return;
				}
				num4 = (num - num2 * this.Aspect) * 0.5f / num;
				pass = 1;
			}
			base.Material.SetVector("_Offsets", new Vector2(num4, 1f - num4));
			Graphics.Blit(source, destination, base.Material, pass);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Letterbox";
		}

		// Token: 0x040001AA RID: 426
		[Min(0f)]
		[Tooltip("Crop the screen to the given aspect ratio.")]
		public float Aspect = 2.3333333f;

		// Token: 0x040001AB RID: 427
		[Tooltip("Letter/Pillar box color. Alpha is transparency.")]
		public Color FillColor = Color.black;
	}
}

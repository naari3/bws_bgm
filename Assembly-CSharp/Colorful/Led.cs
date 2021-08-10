using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000050 RID: 80
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/led.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/LED")]
	public class Led : BaseEffect
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000C294 File Offset: 0x0000A494
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float x = this.Scale;
			if (this.Mode == Led.SizeMode.PixelPerfect)
			{
				x = (float)source.width / this.Scale;
			}
			base.Material.SetVector("_Params", new Vector4(x, this.AutomaticRatio ? ((float)source.width / (float)source.height) : this.Ratio, this.Brightness, this.Shape));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C30E File Offset: 0x0000A50E
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Led";
		}

		// Token: 0x0400019F RID: 415
		[Range(1f, 255f)]
		[Tooltip("Scale of an individual LED. Depends on the Mode used.")]
		public float Scale = 80f;

		// Token: 0x040001A0 RID: 416
		[Range(0f, 10f)]
		[Tooltip("LED brightness booster.")]
		public float Brightness = 1f;

		// Token: 0x040001A1 RID: 417
		[Range(1f, 3f)]
		[Tooltip("LED shape, from softer to harsher.")]
		public float Shape = 1.5f;

		// Token: 0x040001A2 RID: 418
		[Tooltip("Turn this on to automatically compute the aspect ratio needed for squared LED.")]
		public bool AutomaticRatio = true;

		// Token: 0x040001A3 RID: 419
		[Tooltip("Custom aspect ratio.")]
		public float Ratio = 1f;

		// Token: 0x040001A4 RID: 420
		[Tooltip("Used for the Scale field.")]
		public Led.SizeMode Mode;

		// Token: 0x02000097 RID: 151
		public enum SizeMode
		{
			// Token: 0x040002AA RID: 682
			ResolutionIndependent,
			// Token: 0x040002AB RID: 683
			PixelPerfect
		}
	}
}

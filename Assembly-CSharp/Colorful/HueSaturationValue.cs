using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200004E RID: 78
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/hue-saturation-value.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Hue, Saturation, Value")]
	public class HueSaturationValue : BaseEffect
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Master", new Vector3(this.MasterHue / 360f, (this.MasterSaturation + 100f) * 0.01f, (this.MasterValue + 100f) * 0.01f));
			if (this.AdvancedMode)
			{
				base.Material.SetVector("_Reds", new Vector3(this.RedsHue / 360f, (this.RedsSaturation + 100f) * 0.01f, (this.RedsValue + 100f) * 0.01f));
				base.Material.SetVector("_Yellows", new Vector3(this.YellowsHue / 360f, (this.YellowsSaturation + 100f) * 0.01f, (this.YellowsValue + 100f) * 0.01f));
				base.Material.SetVector("_Greens", new Vector3(this.GreensHue / 360f, (this.GreensSaturation + 100f) * 0.01f, (this.GreensValue + 100f) * 0.01f));
				base.Material.SetVector("_Cyans", new Vector3(this.CyansHue / 360f, (this.CyansSaturation + 100f) * 0.01f, (this.CyansValue + 100f) * 0.01f));
				base.Material.SetVector("_Blues", new Vector3(this.BluesHue / 360f, (this.BluesSaturation + 100f) * 0.01f, (this.BluesValue + 100f) * 0.01f));
				base.Material.SetVector("_Magentas", new Vector3(this.MagentasHue / 360f, (this.MagentasSaturation + 100f) * 0.01f, (this.MagentasValue + 100f) * 0.01f));
				Graphics.Blit(source, destination, base.Material, 1);
				return;
			}
			Graphics.Blit(source, destination, base.Material, 0);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C203 File Offset: 0x0000A403
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Hue Saturation Value";
		}

		// Token: 0x04000188 RID: 392
		[Range(-180f, 180f)]
		public float MasterHue;

		// Token: 0x04000189 RID: 393
		[Range(-100f, 100f)]
		public float MasterSaturation;

		// Token: 0x0400018A RID: 394
		[Range(-100f, 100f)]
		public float MasterValue;

		// Token: 0x0400018B RID: 395
		[Range(-180f, 180f)]
		public float RedsHue;

		// Token: 0x0400018C RID: 396
		[Range(-100f, 100f)]
		public float RedsSaturation;

		// Token: 0x0400018D RID: 397
		[Range(-100f, 100f)]
		public float RedsValue;

		// Token: 0x0400018E RID: 398
		[Range(-180f, 180f)]
		public float YellowsHue;

		// Token: 0x0400018F RID: 399
		[Range(-100f, 100f)]
		public float YellowsSaturation;

		// Token: 0x04000190 RID: 400
		[Range(-100f, 100f)]
		public float YellowsValue;

		// Token: 0x04000191 RID: 401
		[Range(-180f, 180f)]
		public float GreensHue;

		// Token: 0x04000192 RID: 402
		[Range(-100f, 100f)]
		public float GreensSaturation;

		// Token: 0x04000193 RID: 403
		[Range(-100f, 100f)]
		public float GreensValue;

		// Token: 0x04000194 RID: 404
		[Range(-180f, 180f)]
		public float CyansHue;

		// Token: 0x04000195 RID: 405
		[Range(-100f, 100f)]
		public float CyansSaturation;

		// Token: 0x04000196 RID: 406
		[Range(-100f, 100f)]
		public float CyansValue;

		// Token: 0x04000197 RID: 407
		[Range(-180f, 180f)]
		public float BluesHue;

		// Token: 0x04000198 RID: 408
		[Range(-100f, 100f)]
		public float BluesSaturation;

		// Token: 0x04000199 RID: 409
		[Range(-100f, 100f)]
		public float BluesValue;

		// Token: 0x0400019A RID: 410
		[Range(-180f, 180f)]
		public float MagentasHue;

		// Token: 0x0400019B RID: 411
		[Range(-100f, 100f)]
		public float MagentasSaturation;

		// Token: 0x0400019C RID: 412
		[Range(-100f, 100f)]
		public float MagentasValue;

		// Token: 0x0400019D RID: 413
		public bool AdvancedMode;
	}
}

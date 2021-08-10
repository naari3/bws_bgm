using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200006C RID: 108
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/wiggle.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Wiggle")]
	public class Wiggle : BaseEffect
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		protected virtual void Update()
		{
			if (this.AutomaticTimer)
			{
				if (this.Timer > 100f)
				{
					this.Timer -= 100f;
				}
				this.Timer += this.Speed * Time.deltaTime;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E214 File Offset: 0x0000C414
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (Mathf.Approximately(this.Amplitude, 0f))
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector3(this.Frequency, this.Amplitude, this.Timer * ((this.Mode == Wiggle.Algorithm.Complex) ? 0.1f : 1f)));
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E290 File Offset: 0x0000C490
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Wiggle";
		}

		// Token: 0x04000212 RID: 530
		[Tooltip("Animation type. Complex is slower but looks more natural.")]
		public Wiggle.Algorithm Mode = Wiggle.Algorithm.Complex;

		// Token: 0x04000213 RID: 531
		public float Timer;

		// Token: 0x04000214 RID: 532
		[Tooltip("Wave animation speed.")]
		public float Speed = 1f;

		// Token: 0x04000215 RID: 533
		[Tooltip("Wave frequency (higher means more waves).")]
		public float Frequency = 12f;

		// Token: 0x04000216 RID: 534
		[Tooltip("Wave amplitude (higher means bigger waves).")]
		public float Amplitude = 0.01f;

		// Token: 0x04000217 RID: 535
		[Tooltip("Automatically animate this effect at runtime.")]
		public bool AutomaticTimer = true;

		// Token: 0x020000A3 RID: 163
		public enum Algorithm
		{
			// Token: 0x0400030E RID: 782
			Simple,
			// Token: 0x0400030F RID: 783
			Complex
		}
	}
}

using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000044 RID: 68
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/frost.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Frost")]
	public class Frost : BaseEffect
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000B3EC File Offset: 0x000095EC
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Scale <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetFloat("_Scale", this.Scale);
			if (this.EnableVignette)
			{
				base.Material.SetFloat("_Sharpness", this.Sharpness * 0.01f);
				base.Material.SetFloat("_Darkness", this.Darkness * 0.02f);
			}
			Graphics.Blit(source, destination, base.Material, this.EnableVignette ? 1 : 0);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B47D File Offset: 0x0000967D
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Frost";
		}

		// Token: 0x04000161 RID: 353
		[Range(0f, 16f)]
		[Tooltip("Frosting strength.")]
		public float Scale = 1.2f;

		// Token: 0x04000162 RID: 354
		[Range(-100f, 100f)]
		[Tooltip("Smoothness of the vignette effect.")]
		public float Sharpness = 40f;

		// Token: 0x04000163 RID: 355
		[Range(0f, 100f)]
		[Tooltip("Amount of vignetting on screen.")]
		public float Darkness = 35f;

		// Token: 0x04000164 RID: 356
		[Tooltip("Should the effect be applied like a vignette ?")]
		public bool EnableVignette = true;
	}
}

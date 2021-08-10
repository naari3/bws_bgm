using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000062 RID: 98
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/smart-saturation.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Smart Saturation")]
	public class SmartSaturation : BaseEffect
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000DA39 File Offset: 0x0000BC39
		protected Texture2D m_CurveTexture
		{
			get
			{
				if (this._CurveTexture == null)
				{
					this.UpdateCurve();
				}
				return this._CurveTexture;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DA58 File Offset: 0x0000BC58
		protected virtual void Reset()
		{
			this.Curve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0.5f, 0f, 0f),
				new Keyframe(1f, 0.5f, 0f, 0f)
			});
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
		protected virtual void OnEnable()
		{
			if (this.Curve == null)
			{
				this.Reset();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DAC6 File Offset: 0x0000BCC6
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this._CurveTexture != null)
			{
				Object.DestroyImmediate(this._CurveTexture);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		public virtual void UpdateCurve()
		{
			if (this._CurveTexture == null)
			{
				this._CurveTexture = new Texture2D(256, 1, TextureFormat.Alpha8, false)
				{
					name = "Saturation Curve Texture",
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0,
					filterMode = FilterMode.Bilinear,
					hideFlags = HideFlags.DontSave
				};
			}
			Color[] pixels = this._CurveTexture.GetPixels();
			for (int i = 0; i < 256; i++)
			{
				float num = Mathf.Clamp01(this.Curve.Evaluate((float)i / 255f));
				pixels[i] = new Color(num, num, num, num);
			}
			this._CurveTexture.SetPixels(pixels);
			this._CurveTexture.Apply();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DB9A File Offset: 0x0000BD9A
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetTexture("_Curve", this.m_CurveTexture);
			base.Material.SetFloat("_Boost", this.Boost);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Smart Saturation";
		}

		// Token: 0x040001EE RID: 494
		[Range(0f, 2f)]
		[Tooltip("Saturation boost. Default: 1 (no boost).")]
		public float Boost = 1f;

		// Token: 0x040001EF RID: 495
		public AnimationCurve Curve;

		// Token: 0x040001F0 RID: 496
		private Texture2D _CurveTexture;
	}
}

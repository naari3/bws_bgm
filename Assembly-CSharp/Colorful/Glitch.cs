using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000046 RID: 70
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/glitch.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Glitch")]
	public class Glitch : BaseEffect
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B7A4 File Offset: 0x000099A4
		public bool IsActive
		{
			get
			{
				return this.m_Activated;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B7AC File Offset: 0x000099AC
		protected override void Start()
		{
			base.Start();
			this.m_DurationTimerEnd = Random.Range(this.RandomDuration.x, this.RandomDuration.y);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B7D8 File Offset: 0x000099D8
		protected virtual void Update()
		{
			if (!this.RandomActivation)
			{
				return;
			}
			if (this.m_Activated)
			{
				this.m_DurationTimer += Time.deltaTime;
				if (this.m_DurationTimer >= this.m_DurationTimerEnd)
				{
					this.m_DurationTimer = 0f;
					this.m_Activated = false;
					this.m_EveryTimerEnd = Random.Range(this.RandomEvery.x, this.RandomEvery.y);
					return;
				}
			}
			else
			{
				this.m_EveryTimer += Time.deltaTime;
				if (this.m_EveryTimer >= this.m_EveryTimerEnd)
				{
					this.m_EveryTimer = 0f;
					this.m_Activated = true;
					this.m_DurationTimerEnd = Random.Range(this.RandomDuration.x, this.RandomDuration.y);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.m_Activated)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.Mode == Glitch.GlitchingMode.Interferences)
			{
				this.DoInterferences(source, destination, this.SettingsInterferences);
				return;
			}
			if (this.Mode == Glitch.GlitchingMode.Tearing)
			{
				this.DoTearing(source, destination, this.SettingsTearing);
				return;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.width, 0, RenderTextureFormat.ARGB32);
			this.DoTearing(source, temporary, this.SettingsTearing);
			this.DoInterferences(temporary, destination, this.SettingsInterferences);
			temporary.Release();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B922 File Offset: 0x00009B22
		protected virtual void DoInterferences(RenderTexture source, RenderTexture destination, Glitch.InterferenceSettings settings)
		{
			base.Material.SetVector("_Params", new Vector3(settings.Speed, settings.Density, settings.MaxDisplacement));
			Graphics.Blit(source, destination, base.Material, 0);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B960 File Offset: 0x00009B60
		protected virtual void DoTearing(RenderTexture source, RenderTexture destination, Glitch.TearingSettings settings)
		{
			base.Material.SetVector("_Params", new Vector4(settings.Speed, settings.Intensity, settings.MaxDisplacement, settings.YuvOffset));
			int pass = 1;
			if (settings.AllowFlipping && settings.YuvColorBleeding)
			{
				pass = 4;
			}
			else if (settings.AllowFlipping)
			{
				pass = 2;
			}
			else if (settings.YuvColorBleeding)
			{
				pass = 3;
			}
			Graphics.Blit(source, destination, base.Material, pass);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Glitch";
		}

		// Token: 0x04000168 RID: 360
		[Tooltip("Automatically activate/deactivate the effect randomly.")]
		public bool RandomActivation;

		// Token: 0x04000169 RID: 361
		public Vector2 RandomEvery = new Vector2(1f, 2f);

		// Token: 0x0400016A RID: 362
		public Vector2 RandomDuration = new Vector2(1f, 2f);

		// Token: 0x0400016B RID: 363
		[Tooltip("Glitch type.")]
		public Glitch.GlitchingMode Mode;

		// Token: 0x0400016C RID: 364
		public Glitch.InterferenceSettings SettingsInterferences = new Glitch.InterferenceSettings();

		// Token: 0x0400016D RID: 365
		public Glitch.TearingSettings SettingsTearing = new Glitch.TearingSettings();

		// Token: 0x0400016E RID: 366
		protected bool m_Activated = true;

		// Token: 0x0400016F RID: 367
		protected float m_EveryTimer;

		// Token: 0x04000170 RID: 368
		protected float m_EveryTimerEnd;

		// Token: 0x04000171 RID: 369
		protected float m_DurationTimer;

		// Token: 0x04000172 RID: 370
		protected float m_DurationTimerEnd;

		// Token: 0x02000094 RID: 148
		public enum GlitchingMode
		{
			// Token: 0x0400029D RID: 669
			Interferences,
			// Token: 0x0400029E RID: 670
			Tearing,
			// Token: 0x0400029F RID: 671
			Complete
		}

		// Token: 0x02000095 RID: 149
		[Serializable]
		public class InterferenceSettings
		{
			// Token: 0x040002A0 RID: 672
			public float Speed = 10f;

			// Token: 0x040002A1 RID: 673
			public float Density = 8f;

			// Token: 0x040002A2 RID: 674
			public float MaxDisplacement = 2f;
		}

		// Token: 0x02000096 RID: 150
		[Serializable]
		public class TearingSettings
		{
			// Token: 0x040002A3 RID: 675
			public float Speed = 1f;

			// Token: 0x040002A4 RID: 676
			[Range(0f, 1f)]
			public float Intensity = 0.25f;

			// Token: 0x040002A5 RID: 677
			[Range(0f, 0.5f)]
			public float MaxDisplacement = 0.05f;

			// Token: 0x040002A6 RID: 678
			public bool AllowFlipping;

			// Token: 0x040002A7 RID: 679
			public bool YuvColorBleeding = true;

			// Token: 0x040002A8 RID: 680
			[Range(-2f, 2f)]
			public float YuvOffset = 0.5f;
		}
	}
}

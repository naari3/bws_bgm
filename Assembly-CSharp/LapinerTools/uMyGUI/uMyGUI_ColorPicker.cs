using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000014 RID: 20
	public class uMyGUI_ColorPicker : MonoBehaviour
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004237 File Offset: 0x00002437
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000423F File Offset: 0x0000243F
		public Slider RedSlider
		{
			get
			{
				return this.m_redSlider;
			}
			set
			{
				this.m_redSlider = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004248 File Offset: 0x00002448
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00004250 File Offset: 0x00002450
		public Slider GreenSlider
		{
			get
			{
				return this.m_greenSlider;
			}
			set
			{
				this.m_greenSlider = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004259 File Offset: 0x00002459
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00004261 File Offset: 0x00002461
		public Slider BlueSlider
		{
			get
			{
				return this.m_blueSlider;
			}
			set
			{
				this.m_blueSlider = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000426A File Offset: 0x0000246A
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00004272 File Offset: 0x00002472
		public Color PickedColor
		{
			get
			{
				return this.m_pickedColor;
			}
			set
			{
				if (this.m_pickedColor != value)
				{
					this.m_pickedColor = value;
					this.UpdateColor();
					if (this.m_onChanged != null)
					{
						this.m_onChanged(this, new uMyGUI_ColorPicker.ColorEventArgs(this.m_pickedColor));
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000042AE File Offset: 0x000024AE
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000042B6 File Offset: 0x000024B6
		public Graphic ColorPreview
		{
			get
			{
				return this.m_colorPreview;
			}
			set
			{
				this.m_colorPreview = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000042C0 File Offset: 0x000024C0
		private void Start()
		{
			if (this.m_redSlider == null || this.m_greenSlider == null || this.m_blueSlider == null)
			{
				Debug.LogError("uMyGUI_ColorPicker: all three sliders (RGB) must be set in inspector!");
				base.enabled = false;
				return;
			}
			this.UpdateColor();
			this.m_redSlider.onValueChanged.AddListener(new UnityAction<float>(this.SetRedValue));
			this.m_greenSlider.onValueChanged.AddListener(new UnityAction<float>(this.SetGreenValue));
			this.m_blueSlider.onValueChanged.AddListener(new UnityAction<float>(this.SetBlueValue));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004364 File Offset: 0x00002564
		private void OnDestroy()
		{
			this.m_onChanged = null;
			this.m_redSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.SetRedValue));
			this.m_greenSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.SetGreenValue));
			this.m_blueSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.SetBlueValue));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043CC File Offset: 0x000025CC
		private void SetRedValue(float p_redValue)
		{
			if (this.m_pickedColor.r != p_redValue)
			{
				this.m_pickedColor.r = p_redValue;
				this.UpdateColor();
				if (this.m_onChanged != null)
				{
					this.m_onChanged(this, new uMyGUI_ColorPicker.ColorEventArgs(this.m_pickedColor));
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004418 File Offset: 0x00002618
		private void SetGreenValue(float p_greenValue)
		{
			if (this.m_pickedColor.g != p_greenValue)
			{
				this.m_pickedColor.g = p_greenValue;
				this.UpdateColor();
				if (this.m_onChanged != null)
				{
					this.m_onChanged(this, new uMyGUI_ColorPicker.ColorEventArgs(this.m_pickedColor));
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004464 File Offset: 0x00002664
		private void SetBlueValue(float p_blueValue)
		{
			if (this.m_pickedColor.b != p_blueValue)
			{
				this.m_pickedColor.b = p_blueValue;
				this.UpdateColor();
				if (this.m_onChanged != null)
				{
					this.m_onChanged(this, new uMyGUI_ColorPicker.ColorEventArgs(this.m_pickedColor));
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000044B0 File Offset: 0x000026B0
		private void UpdateColor()
		{
			this.m_redSlider.value = this.m_pickedColor.r;
			this.m_greenSlider.value = this.m_pickedColor.g;
			this.m_blueSlider.value = this.m_pickedColor.b;
			if (this.m_colorPreview != null)
			{
				this.m_colorPreview.color = this.m_pickedColor;
			}
		}

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private Slider m_redSlider;

		// Token: 0x04000049 RID: 73
		[SerializeField]
		private Slider m_greenSlider;

		// Token: 0x0400004A RID: 74
		[SerializeField]
		private Slider m_blueSlider;

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private Color m_pickedColor = Color.gray;

		// Token: 0x0400004C RID: 76
		[SerializeField]
		private Graphic m_colorPreview;

		// Token: 0x0400004D RID: 77
		public EventHandler<uMyGUI_ColorPicker.ColorEventArgs> m_onChanged;

		// Token: 0x02000074 RID: 116
		public class ColorEventArgs : EventArgs
		{
			// Token: 0x060002BC RID: 700 RVA: 0x0000E538 File Offset: 0x0000C738
			public ColorEventArgs(Color p_value)
			{
				this.Value = p_value;
			}

			// Token: 0x0400022F RID: 559
			public readonly Color Value;
		}
	}
}

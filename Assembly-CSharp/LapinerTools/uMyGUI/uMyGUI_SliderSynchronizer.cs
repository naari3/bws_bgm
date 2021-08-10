using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001F RID: 31
	public class uMyGUI_SliderSynchronizer : MonoBehaviour
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000061A4 File Offset: 0x000043A4
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000061AC File Offset: 0x000043AC
		public Slider[] Sliders
		{
			get
			{
				return this.m_sliders;
			}
			set
			{
				this.m_sliders = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000061B5 File Offset: 0x000043B5
		public bool IsSynchronizeOnStart
		{
			get
			{
				return this.m_isSynchronizeOnStart;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000061BD File Offset: 0x000043BD
		public float Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000061C8 File Offset: 0x000043C8
		private void Start()
		{
			if (this.m_sliders.Length != 0)
			{
				this.m_value = this.m_sliders[0].value;
			}
			for (int i = 0; i < this.m_sliders.Length; i++)
			{
				this.m_sliders[i].onValueChanged.AddListener(new UnityAction<float>(this.OnSliderChanged));
			}
			if (this.m_isSynchronizeOnStart)
			{
				this.OnSliderChanged(this.m_value);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006238 File Offset: 0x00004438
		private void OnDestroy()
		{
			for (int i = 0; i < this.m_sliders.Length; i++)
			{
				this.m_sliders[i].onValueChanged.RemoveListener(new UnityAction<float>(this.OnSliderChanged));
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006278 File Offset: 0x00004478
		private void OnSliderChanged(float p_value)
		{
			this.m_value = p_value;
			for (int i = 0; i < this.m_sliders.Length; i++)
			{
				if (this.m_sliders[i].value != this.m_value)
				{
					this.m_sliders[i].value = this.m_value;
				}
			}
		}

		// Token: 0x04000096 RID: 150
		[SerializeField]
		private Slider[] m_sliders = new Slider[0];

		// Token: 0x04000097 RID: 151
		[SerializeField]
		private bool m_isSynchronizeOnStart = true;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		private float m_value;
	}
}

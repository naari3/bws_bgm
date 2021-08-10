using System;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001E RID: 30
	public class uMyGUI_ScrollbarHandleUnityFix : MonoBehaviour
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000060C4 File Offset: 0x000042C4
		public void Awake()
		{
			RectTransform component = base.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.anchoredPosition3D = Vector3.zero;
			component.anchorMin = this.m_anchorMin;
			component.anchorMax = this.m_anchorMax;
			component.pivot = this.m_pivot;
			component.offsetMin = this.m_offsetMin;
			component.offsetMax = this.m_offsetMax;
		}

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private Vector2 m_anchorMin = new Vector2(0.8f, 0f);

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private Vector2 m_anchorMax = new Vector2(1f, 1f);

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private Vector2 m_pivot = new Vector2(0.5f, 0.5f);

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private Vector2 m_offsetMin = new Vector2(-10f, -10f);

		// Token: 0x04000095 RID: 149
		[SerializeField]
		private Vector2 m_offsetMax = new Vector2(10f, 10f);
	}
}

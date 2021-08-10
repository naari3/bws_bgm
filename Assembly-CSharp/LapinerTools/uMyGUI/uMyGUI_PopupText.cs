using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001C RID: 28
	public class uMyGUI_PopupText : uMyGUI_PopupButtons
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005F10 File Offset: 0x00004110
		public virtual uMyGUI_PopupText SetText(string p_headerText, string p_bodyText)
		{
			if (this.m_header != null)
			{
				this.m_header.text = p_headerText;
			}
			if (this.m_body != null)
			{
				this.m_body.text = p_bodyText;
			}
			return this;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005F47 File Offset: 0x00004147
		public override void Show()
		{
			base.Show();
			this.m_isFirstFrameShown = true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005F58 File Offset: 0x00004158
		public virtual void LateUpdate()
		{
			if (this.m_isFirstFrameShown)
			{
				this.m_isFirstFrameShown = false;
				if (this.m_useExplicitNavigation)
				{
					List<Button> list = new List<Button>();
					for (int i = 0; i < this.m_buttons.Length; i++)
					{
						if (this.m_buttons[i] != null && this.m_buttons[i].gameObject.activeSelf && this.m_buttons[i].GetComponentInChildren<Button>() != null)
						{
							list.Add(this.m_buttons[i].GetComponentInChildren<Button>());
						}
					}
					for (int j = 0; j < list.Count; j++)
					{
						Button button = list[j];
						Navigation navigation = button.navigation;
						navigation.mode = Navigation.Mode.Explicit;
						if (j > 0)
						{
							navigation.selectOnLeft = list[j - 1];
						}
						if (j < list.Count - 1)
						{
							navigation.selectOnRight = list[j + 1];
						}
						button.navigation = navigation;
					}
				}
			}
		}

		// Token: 0x0400008C RID: 140
		[SerializeField]
		protected Text m_header;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		protected Text m_body;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		protected bool m_useExplicitNavigation;

		// Token: 0x0400008F RID: 143
		protected bool m_isFirstFrameShown;
	}
}

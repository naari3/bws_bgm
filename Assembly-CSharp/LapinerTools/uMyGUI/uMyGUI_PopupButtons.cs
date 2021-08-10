using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000019 RID: 25
	public class uMyGUI_PopupButtons : uMyGUI_Popup
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005629 File Offset: 0x00003829
		public override void Show()
		{
			if (this.m_isClosing)
			{
				this.Hide();
				this.m_isCloseCanceled = true;
			}
			base.Show();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005648 File Offset: 0x00003848
		public override void Hide()
		{
			base.Hide();
			for (int i = 0; i < this.m_buttons.Length; i++)
			{
				if (this.m_buttons[i] != null)
				{
					this.m_buttons[i].gameObject.SetActive(false);
				}
			}
			this.m_onBtnClickCallbacks.Clear();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000569C File Offset: 0x0000389C
		public virtual uMyGUI_PopupButtons ShowButton(string p_buttonName)
		{
			return this.ShowButton(p_buttonName, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000056A8 File Offset: 0x000038A8
		public virtual uMyGUI_PopupButtons ShowButton(string p_buttonName, Action p_callback)
		{
			for (int i = 0; i < this.m_buttons.Length; i++)
			{
				if (this.m_buttons[i] != null && this.m_buttonNames[i] == p_buttonName)
				{
					this.m_buttons[i].gameObject.SetActive(true);
					if (this.m_improveNavigationFocus)
					{
						Selectable componentInChildren = this.m_buttons[i].GetComponentInChildren<Selectable>();
						if (componentInChildren != null)
						{
							componentInChildren.Select();
						}
					}
					if (p_callback != null)
					{
						this.m_onBtnClickCallbacks.Add(p_buttonName, p_callback);
					}
					return this;
				}
			}
			Debug.LogError("uMyGUI_PopupButtons: ShowButton: could not find button with name '" + p_buttonName + "'!");
			return this;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005748 File Offset: 0x00003948
		public virtual void OnButtonClick(RectTransform p_btn)
		{
			this.m_isClosing = true;
			this.m_isCloseCanceled = false;
			int i = 0;
			while (i < this.m_buttons.Length)
			{
				if (this.m_buttons[i] == p_btn)
				{
					Action action;
					if (this.m_onBtnClickCallbacks.TryGetValue(this.m_buttonNames[i], out action))
					{
						action();
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			this.m_isClosing = false;
			if (!this.m_isCloseCanceled)
			{
				this.Hide();
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000057BC File Offset: 0x000039BC
		protected override void Start()
		{
			base.Start();
			if (this.m_buttons.Length != this.m_buttonNames.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"uMyGUI_PopupButtons: m_buttons and m_buttonNames must have the same length (",
					this.m_buttons.Length,
					"!=",
					this.m_buttonNames.Length,
					")!"
				}));
			}
			this.m_audioSources = base.GetComponentsInChildren<AudioSource>();
			for (int i = 0; i < this.m_audioSources.Length; i++)
			{
				this.m_audioSources[i].transform.parent = base.transform.parent;
				this.m_audioSources[i].name = base.name + "_" + this.m_audioSources[i].name;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005890 File Offset: 0x00003A90
		protected void OnDestroy()
		{
			for (int i = 0; i < this.m_audioSources.Length; i++)
			{
				if (this.m_audioSources[i] != null)
				{
					Object.Destroy(this.m_audioSources[i].gameObject);
				}
			}
		}

		// Token: 0x04000079 RID: 121
		[SerializeField]
		protected RectTransform[] m_buttons = new RectTransform[0];

		// Token: 0x0400007A RID: 122
		[SerializeField]
		protected string[] m_buttonNames = new string[0];

		// Token: 0x0400007B RID: 123
		[SerializeField]
		protected bool m_improveNavigationFocus = true;

		// Token: 0x0400007C RID: 124
		protected Dictionary<string, Action> m_onBtnClickCallbacks = new Dictionary<string, Action>();

		// Token: 0x0400007D RID: 125
		protected AudioSource[] m_audioSources = new AudioSource[0];

		// Token: 0x0400007E RID: 126
		protected bool m_isClosing;

		// Token: 0x0400007F RID: 127
		protected bool m_isCloseCanceled;
	}
}

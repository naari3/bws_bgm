using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000017 RID: 23
	public class uMyGUI_PageBox : MonoBehaviour
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004E57 File Offset: 0x00003057
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004E5F File Offset: 0x0000305F
		public int PageCount
		{
			get
			{
				return this.m_pageCount;
			}
			set
			{
				this.SetPageCount(value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004E68 File Offset: 0x00003068
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004E70 File Offset: 0x00003070
		public int MaxPageBtnCount
		{
			get
			{
				return this.m_maxPageBtnCount;
			}
			set
			{
				this.m_maxPageBtnCount = value;
				this.SetPageCount(this.PageCount);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004E85 File Offset: 0x00003085
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004E8D File Offset: 0x0000308D
		public int SelectedPage
		{
			get
			{
				return this.m_selectedPage;
			}
			set
			{
				this.SelectPage(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004E98 File Offset: 0x00003098
		public RectTransform RTransform
		{
			get
			{
				if (!(this.m_rectTransform != null))
				{
					return this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600008A RID: 138 RVA: 0x00004ECC File Offset: 0x000030CC
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x00004F04 File Offset: 0x00003104
		public event Action<int> OnPageSelected;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004F3C File Offset: 0x0000313C
		private RectTransform PageButtonTransform
		{
			get
			{
				if (!(this.m_pageButtonTransform != null) && !(this.m_pageButton == null))
				{
					return this.m_pageButtonTransform = this.m_pageButton.GetComponent<RectTransform>();
				}
				return this.m_pageButtonTransform;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004F80 File Offset: 0x00003180
		public void SetPageCount(int p_newPageCount)
		{
			this.m_pageCount = Mathf.Max(1, p_newPageCount);
			if (p_newPageCount <= 1)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.m_pageButton != null)
			{
				base.gameObject.SetActive(true);
				this.UpdateUI();
				return;
			}
			Debug.LogError("uMyGUI_PageBox: SetPageCount: m_pageButton must be set in the inspector!");
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004FD6 File Offset: 0x000031D6
		public void SelectPageAndCenterOffset(int p_selectedPage)
		{
			this.m_offset = Mathf.Min(this.m_pageCount - this.m_maxPageBtnCount, Mathf.Max(0, p_selectedPage - 1 - this.m_maxPageBtnCount / 2));
			this.SelectPage(p_selectedPage);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000500C File Offset: 0x0000320C
		public void SelectPage(int p_selectedPage)
		{
			int num = Mathf.Clamp(p_selectedPage, 0, this.m_pageCount);
			bool flag = num != this.m_selectedPage;
			this.m_selectedPage = num;
			this.UpdateUI();
			if (flag && this.OnPageSelected != null)
			{
				this.OnPageSelected(p_selectedPage);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005058 File Offset: 0x00003258
		public void UpdateUI()
		{
			this.Clear();
			int num = Mathf.Min(this.m_pageCount, this.m_maxPageBtnCount);
			float size = this.GetWidth(this.m_previousButton) + this.GetWidth(this.m_nextButton) + this.GetWidth(this.m_pageButton) * (float)num;
			this.RTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			int num2 = Mathf.Max(0, this.m_selectedPage - this.m_maxPageBtnCount);
			int num3 = Mathf.Max(0, Mathf.Min(this.m_pageCount - this.m_maxPageBtnCount, this.m_selectedPage - 1));
			if (num2 - 1 >= this.m_offset)
			{
				this.m_offset = num2;
			}
			else if (num3 + 1 <= this.m_offset)
			{
				this.m_offset = num3;
			}
			this.SetText(this.m_pageButton, (1 + this.m_offset).ToString());
			this.SetOnClick(this.m_pageButton, 1 + this.m_offset);
			for (int i = 2; i <= num; i++)
			{
				Button button = Object.Instantiate<Button>(this.m_pageButton);
				RectTransform component = button.GetComponent<RectTransform>();
				component.SetParent(this.PageButtonTransform.parent, true);
				component.localScale = this.PageButtonTransform.localScale;
				component.localPosition = this.PageButtonTransform.localPosition + Vector3.right * (float)(i - 1) * this.GetWidth(this.m_pageButton);
				this.SetText(button, (i + this.m_offset).ToString());
				this.SetOnClick(button, i + this.m_offset);
				this.m_pageButtons.Add(button);
			}
			for (int j = 0; j < this.m_pageButtons.Count; j++)
			{
				int num4 = j + 1 + this.m_offset;
				this.m_pageButtons[j].enabled = (num4 != this.m_selectedPage);
			}
			if (this.m_nextButton != null)
			{
				this.m_nextButton.GetComponent<RectTransform>().localPosition = this.PageButtonTransform.localPosition + Vector3.right * (float)num * this.GetWidth(this.m_pageButton);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000528C File Offset: 0x0000348C
		private void Start()
		{
			this.SetPageCount(this.m_pageCount);
			if (this.m_previousButton != null)
			{
				this.m_previousButton.onClick.AddListener(delegate()
				{
					this.SelectPageAndCenterOffset(Mathf.Max(1, this.m_selectedPage - 1));
				});
			}
			if (this.m_nextButton != null)
			{
				this.m_nextButton.onClick.AddListener(delegate()
				{
					this.SelectPageAndCenterOffset(this.m_selectedPage + 1);
				});
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000052FC File Offset: 0x000034FC
		private void SetText(Button p_button, string p_text)
		{
			Text componentInChildren = p_button.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				componentInChildren.text = p_text;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005320 File Offset: 0x00003520
		private void SetOnClick(Button p_button, int p_pageNumber)
		{
			p_button.onClick.RemoveAllListeners();
			p_button.onClick.AddListener(delegate()
			{
				this.SelectPage(p_pageNumber);
			});
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005363 File Offset: 0x00003563
		private float GetWidth(Button p_button)
		{
			if (!(p_button != null))
			{
				return 0f;
			}
			return this.GetWidth(p_button.GetComponent<RectTransform>());
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005380 File Offset: 0x00003580
		private float GetWidth(RectTransform p_rTransform)
		{
			if (!(p_rTransform != null))
			{
				return 0f;
			}
			return p_rTransform.rect.xMax - p_rTransform.rect.xMin;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000053BC File Offset: 0x000035BC
		private void Clear()
		{
			for (int i = 0; i < this.m_pageButtons.Count; i++)
			{
				if (this.m_pageButtons[i] != null && this.m_pageButtons[i] != this.m_pageButton)
				{
					Object.Destroy(this.m_pageButtons[i].gameObject);
				}
			}
			this.m_pageButtons.Clear();
			this.m_pageButtons.Add(this.m_pageButton);
		}

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private Button m_previousButton;

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private Button m_nextButton;

		// Token: 0x0400006C RID: 108
		[SerializeField]
		private Button m_pageButton;

		// Token: 0x0400006D RID: 109
		[SerializeField]
		private int m_pageCount = 1;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private int m_maxPageBtnCount = 9;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private int m_selectedPage;

		// Token: 0x04000070 RID: 112
		private RectTransform m_rectTransform;

		// Token: 0x04000072 RID: 114
		private RectTransform m_pageButtonTransform;

		// Token: 0x04000073 RID: 115
		private int m_offset;

		// Token: 0x04000074 RID: 116
		private List<Button> m_pageButtons = new List<Button>();
	}
}

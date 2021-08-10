using System;
using System.Collections;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000018 RID: 24
	public class uMyGUI_Popup : MonoBehaviour
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600009A RID: 154 RVA: 0x00005488 File Offset: 0x00003688
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x000054C0 File Offset: 0x000036C0
		public event Action OnShow;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600009C RID: 156 RVA: 0x000054F8 File Offset: 0x000036F8
		// (remove) Token: 0x0600009D RID: 157 RVA: 0x00005530 File Offset: 0x00003730
		public event Action OnHide;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005565 File Offset: 0x00003765
		public virtual bool IsShown
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005572 File Offset: 0x00003772
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000557A File Offset: 0x0000377A
		public virtual bool DestroyOnHide { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00005583 File Offset: 0x00003783
		public virtual void Show()
		{
			base.gameObject.transform.SetAsLastSibling();
			base.gameObject.SetActive(true);
			if (this.OnShow != null)
			{
				this.OnShow();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000055B4 File Offset: 0x000037B4
		public virtual void Hide()
		{
			base.gameObject.SetActive(false);
			if (this.OnHide != null)
			{
				this.OnHide();
			}
			if (this.DestroyOnHide && this.m_createFrame != Time.frameCount && uMyGUI_PopupManager.IsInstanceSet)
			{
				uMyGUI_PopupManager.Instance.StartCoroutine(this.DestroyOnEndOfFrame());
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000560D File Offset: 0x0000380D
		protected virtual void Awake()
		{
			this.m_createFrame = Time.frameCount;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002CA8 File Offset: 0x00000EA8
		protected virtual void Start()
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000561A File Offset: 0x0000381A
		protected IEnumerator DestroyOnEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			if (uMyGUI_PopupManager.IsInstanceSet)
			{
				uMyGUI_PopupManager.Instance.RemovePopup(this);
				Object.Destroy(base.gameObject);
			}
			yield break;
		}

		// Token: 0x04000078 RID: 120
		protected int m_createFrame;
	}
}

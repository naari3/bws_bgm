using System;
using System.Collections;
using LapinerTools.Steam.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LapinerTools.Steam.UI
{
	// Token: 0x02000026 RID: 38
	public class SteamLeaderboardsScoreEntryNode : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008E19 File Offset: 0x00007019
		public RawImage Image
		{
			get
			{
				return this.m_image;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008E24 File Offset: 0x00007024
		public virtual void uMyGUI_TreeBrowser_InitNode(object p_data)
		{
			if (p_data is SteamLeaderboardsScoreEntryNode.SendMessageInitData)
			{
				SteamLeaderboardsScoreEntryNode.SendMessageInitData sendMessageInitData = (SteamLeaderboardsScoreEntryNode.SendMessageInitData)p_data;
				if (this.m_image != null)
				{
					base.StartCoroutine(this.LoadAvatarTexture(sendMessageInitData.ScoreEntry));
				}
				string format = sendMessageInitData.ScoreEntry.IsCurrentUserScore ? "<color=white>{0}</color>" : "{0}";
				if (this.m_textUserName != null)
				{
					this.m_textUserName.text = string.Format(format, sendMessageInitData.ScoreEntry.UserName);
				}
				if (this.m_textRank != null)
				{
					this.m_textRank.text = string.Format(format, sendMessageInitData.ScoreEntry.GlobalRank);
				}
				if (this.m_textScore != null)
				{
					this.m_textScore.text = string.Format(format, sendMessageInitData.ScoreEntry.ScoreString);
				}
				if (SteamLeaderboardsUI.Instance != null)
				{
					SteamLeaderboardsUI.Instance.InvokeOnEntryDataSet(sendMessageInitData.ScoreEntry, this);
					return;
				}
			}
			else
			{
				Debug.LogError("SteamLeaderboardsScoreEntryNode: uMyGUI_TreeBrowser_InitNode: expected p_data to be a SteamLeaderboardsScoreEntryNode.SendMessageInitData! p_data: " + p_data);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008F30 File Offset: 0x00007130
		public virtual void OnScroll(PointerEventData data)
		{
			if (this.m_parentScroller == null)
			{
				this.m_parentScroller = base.GetComponentInParent<ScrollRect>();
			}
			if (this.m_parentScroller == null)
			{
				return;
			}
			this.m_parentScroller.OnScroll(data);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008F67 File Offset: 0x00007167
		protected void OnDestroy()
		{
			if (this.m_avatarTexture != null)
			{
				Object.Destroy(this.m_avatarTexture);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008F82 File Offset: 0x00007182
		protected virtual IEnumerator LoadAvatarTexture(LeaderboardsScoreEntry p_entry)
		{
			bool isAvatarLoaded = !SteamMainBase<SteamLeaderboardsMain>.Instance.IsAvatarTextureSet(p_entry);
			while (!isAvatarLoaded)
			{
				if (this.m_avatarTexture != null)
				{
					Object.Destroy(this.m_avatarTexture);
				}
				this.m_avatarTexture = SteamMainBase<SteamLeaderboardsMain>.Instance.GetAvatarTexture(p_entry);
				if (this.m_avatarTexture != null)
				{
					isAvatarLoaded = true;
					if (this.m_image != null)
					{
						this.m_image.texture = this.m_avatarTexture;
					}
				}
				yield return new WaitForSeconds(0.35f);
			}
			yield break;
		}

		// Token: 0x040000DB RID: 219
		[SerializeField]
		protected Text m_textRank;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		protected Text m_textUserName;

		// Token: 0x040000DD RID: 221
		[SerializeField]
		protected Text m_textScore;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		protected RawImage m_image;

		// Token: 0x040000DF RID: 223
		protected ScrollRect m_parentScroller;

		// Token: 0x040000E0 RID: 224
		protected Texture2D m_avatarTexture;

		// Token: 0x02000088 RID: 136
		public class EntryDataSetEventArgs : EventArgsBase
		{
			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000E866 File Offset: 0x0000CA66
			// (set) Token: 0x060002EC RID: 748 RVA: 0x0000E86E File Offset: 0x0000CA6E
			public LeaderboardsScoreEntry EntryData { get; set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000E877 File Offset: 0x0000CA77
			// (set) Token: 0x060002EE RID: 750 RVA: 0x0000E87F File Offset: 0x0000CA7F
			public SteamLeaderboardsScoreEntryNode EntryUI { get; set; }
		}

		// Token: 0x02000089 RID: 137
		public class SendMessageInitData
		{
			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000E890 File Offset: 0x0000CA90
			// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000E898 File Offset: 0x0000CA98
			public LeaderboardsScoreEntry ScoreEntry { get; set; }
		}
	}
}

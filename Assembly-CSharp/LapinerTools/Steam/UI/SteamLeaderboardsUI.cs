using System;
using System.Collections.Generic;
using LapinerTools.Steam.Data;
using LapinerTools.uMyGUI;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.Steam.UI
{
	// Token: 0x02000027 RID: 39
	public class SteamLeaderboardsUI : MonoBehaviour
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008F98 File Offset: 0x00007198
		public static SteamLeaderboardsUI Instance
		{
			get
			{
				if (SteamLeaderboardsUI.s_instance == null)
				{
					SteamLeaderboardsUI.s_instance = Object.FindObjectOfType<SteamLeaderboardsUI>();
				}
				return SteamLeaderboardsUI.s_instance;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000165 RID: 357 RVA: 0x00008FB8 File Offset: 0x000071B8
		// (remove) Token: 0x06000166 RID: 358 RVA: 0x00008FF0 File Offset: 0x000071F0
		public event Action<SteamLeaderboardsScoreEntryNode.EntryDataSetEventArgs> OnEntryDataSet;

		// Token: 0x06000167 RID: 359 RVA: 0x00009025 File Offset: 0x00007225
		public void InvokeOnEntryDataSet(LeaderboardsScoreEntry p_entryData, SteamLeaderboardsScoreEntryNode p_entryUI)
		{
			this.InvokeEventHandlerSafely<SteamLeaderboardsScoreEntryNode.EntryDataSetEventArgs>(this.OnEntryDataSet, new SteamLeaderboardsScoreEntryNode.EntryDataSetEventArgs
			{
				EntryData = p_entryData,
				EntryUI = p_entryUI
			});
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009048 File Offset: 0x00007248
		public void SetScores(List<LeaderboardsScoreEntry> p_scores)
		{
			if (this.LEADERBOARD_EMPTY_MESSAGE != null)
			{
				this.LEADERBOARD_EMPTY_MESSAGE.enabled = (p_scores.Count == 0);
			}
			if (this.SCORES_BROWSER != null)
			{
				this.SCORES_BROWSER.Clear();
				if (this.ENTRIES_PER_PAGE > 0 && this.SCORES_BROWSER.ParentScroller != null)
				{
					this.SCORES_BROWSER.ForcedEntryHeight = this.SCORES_BROWSER.ParentScroller.GetComponent<RectTransform>().rect.height / (float)this.ENTRIES_PER_PAGE;
				}
				this.SCORES_BROWSER.BuildTree(this.ConvertScoresToNodes(p_scores));
				return;
			}
			Debug.LogError("SteamLeaderboardsUI: SetScores: SCORES_BROWSER is not set in inspector!");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000090F9 File Offset: 0x000072F9
		public void SetLeaderboardName(string p_leaderboardName)
		{
			if (this.LEADERBOARD_NAME_TEXT != null)
			{
				this.LEADERBOARD_NAME_TEXT.text = p_leaderboardName;
				return;
			}
			Debug.LogError("SteamLeaderboardsUI: SetLeaderboardName: LEADERBOARD_NAME_TEXT is not set in inspector!");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009120 File Offset: 0x00007320
		public void DownloadScores(string p_leaderboardName)
		{
			this.SetLeaderboardName(p_leaderboardName);
			if (this.LEADERBOARD_EMPTY_MESSAGE != null)
			{
				this.LEADERBOARD_EMPTY_MESSAGE.enabled = false;
			}
			if (this.SCORES_BROWSER != null)
			{
				this.SCORES_BROWSER.Clear();
			}
			uMyGUI_PopupManager.Instance.ShowPopup("loading");
			SteamMainBase<SteamLeaderboardsMain>.Instance.DownloadScores(p_leaderboardName, delegate(LeaderboardsDownloadedScoresEventArgs p_leaderboardArgs)
			{
				uMyGUI_PopupManager.Instance.HidePopup("loading");
			});
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000091A4 File Offset: 0x000073A4
		public void DownloadScoresAroundUser(string p_leaderboardName, int p_range)
		{
			this.SetLeaderboardName(p_leaderboardName);
			if (this.LEADERBOARD_EMPTY_MESSAGE != null)
			{
				this.LEADERBOARD_EMPTY_MESSAGE.enabled = false;
			}
			if (this.SCORES_BROWSER != null)
			{
				this.SCORES_BROWSER.Clear();
			}
			uMyGUI_PopupManager.Instance.ShowPopup("loading");
			SteamMainBase<SteamLeaderboardsMain>.Instance.DownloadScoresAroundUser(p_leaderboardName, p_range, delegate(LeaderboardsDownloadedScoresEventArgs p_leaderboardArgs)
			{
				uMyGUI_PopupManager.Instance.HidePopup("loading");
			});
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009227 File Offset: 0x00007427
		public static void UploadScore(string p_leaderboardName, int p_score)
		{
			SteamLeaderboardsUI.UploadScore(p_leaderboardName, p_score, null);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009234 File Offset: 0x00007434
		public static void UploadScore(string p_leaderboardName, int p_score, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScoreSuccessfully)
		{
			uMyGUI_PopupManager.Instance.ShowPopup("loading");
			SteamMainBase<SteamLeaderboardsMain>.Instance.UploadScore(p_leaderboardName, p_score, delegate(LeaderboardsUploadedScoreEventArgs p_leaderboardArgs)
			{
				uMyGUI_PopupManager.Instance.HidePopup("loading");
				if (!p_leaderboardArgs.IsError)
				{
					if (p_onUploadedScoreSuccessfully != null)
					{
						p_onUploadedScoreSuccessfully(p_leaderboardArgs);
					}
					if (p_leaderboardArgs.IsScoreChanged)
					{
						string text = "Score: " + p_leaderboardArgs.ScoreString;
						text = text + "\nGlobal Rank: " + p_leaderboardArgs.GlobalRankNew;
						int num = p_leaderboardArgs.GlobalRankNew - p_leaderboardArgs.GlobalRankPrevious;
						if (num != 0 && p_leaderboardArgs.GlobalRankPrevious > 0)
						{
							text += " (";
							text += ((num < 0) ? "<color=green>" : "<color=red>");
							text = text + (p_leaderboardArgs.GlobalRankPrevious - p_leaderboardArgs.GlobalRankNew).ToString("+#;-#;0") + "</color>";
							text += ")";
						}
						((uMyGUI_PopupText)uMyGUI_PopupManager.Instance.ShowPopup("text")).SetText("New Record", text).ShowButton("ok");
					}
				}
			});
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009277 File Offset: 0x00007477
		protected virtual void Start()
		{
			SteamMainBase<SteamLeaderboardsMain>.Instance.OnDownloadedScores += this.SetScores;
			SteamMainBase<SteamLeaderboardsMain>.Instance.OnError += this.ShowErrorMessage;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000092A7 File Offset: 0x000074A7
		protected virtual void Update()
		{
			this.UpdateScrollbarVisibility();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000092AF File Offset: 0x000074AF
		protected virtual void OnDestroy()
		{
			if (SteamMainBase<SteamLeaderboardsMain>.IsInstanceSet)
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.OnDownloadedScores -= this.SetScores;
				SteamMainBase<SteamLeaderboardsMain>.Instance.OnError -= this.ShowErrorMessage;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000092E8 File Offset: 0x000074E8
		protected virtual void ShowErrorMessage(ErrorEventArgs p_errorArgs)
		{
			uMyGUI_PopupManager.Instance.HidePopup("loading");
			((uMyGUI_PopupText)uMyGUI_PopupManager.Instance.ShowPopup("text")).SetText("Steam Error", p_errorArgs.ErrorMessage).ShowButton("ok");
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009334 File Offset: 0x00007534
		protected virtual void SetScores(LeaderboardsDownloadedScoresEventArgs p_leaderboardArgs)
		{
			if (!p_leaderboardArgs.IsError)
			{
				this.SetLeaderboardName(p_leaderboardArgs.LeaderboardName);
				this.SetScores(p_leaderboardArgs.Scores);
				return;
			}
			Debug.LogError("SteamLeaderboardsUI: SetScores: Steam Error: " + p_leaderboardArgs.ErrorMessage);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000936C File Offset: 0x0000756C
		protected virtual uMyGUI_TreeBrowser.Node[] ConvertScoresToNodes(List<LeaderboardsScoreEntry> p_scores)
		{
			uMyGUI_TreeBrowser.Node[] array = new uMyGUI_TreeBrowser.Node[p_scores.Count];
			for (int i = 0; i < p_scores.Count; i++)
			{
				if (p_scores[i] != null)
				{
					uMyGUI_TreeBrowser.Node node = new uMyGUI_TreeBrowser.Node(new SteamLeaderboardsScoreEntryNode.SendMessageInitData
					{
						ScoreEntry = p_scores[i]
					}, null);
					array[i] = node;
				}
				else
				{
					Debug.LogError("SteamLeaderboardsUI: ConvertScoresToNodes: score at index '" + i + "' is null!");
				}
			}
			return array;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000093DC File Offset: 0x000075DC
		protected virtual void UpdateScrollbarVisibility()
		{
			Scrollbar verticalScrollbar;
			if (this.SCORES_BROWSER != null && this.SCORES_BROWSER.ParentScroller != null && (verticalScrollbar = this.SCORES_BROWSER.ParentScroller.verticalScrollbar) != null)
			{
				verticalScrollbar.gameObject.SetActive(verticalScrollbar.size < 0.9925f);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000943C File Offset: 0x0000763C
		protected virtual void InvokeEventHandlerSafely<T>(Action<T> p_handler, T p_data)
		{
			try
			{
				if (p_handler != null)
				{
					p_handler(p_data);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"SteamLeaderboardsUI: your event handler (",
					p_handler.Target,
					" - System.Action<",
					typeof(T),
					">) has thrown an excepotion!\n",
					ex
				}));
			}
		}

		// Token: 0x040000E1 RID: 225
		protected static SteamLeaderboardsUI s_instance;

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		protected Text LEADERBOARD_NAME_TEXT;

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		protected Text LEADERBOARD_EMPTY_MESSAGE;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		protected uMyGUI_TreeBrowser SCORES_BROWSER;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		[Tooltip("If set greater 0,then the height of the entries in the SCORES_BROWSER will be scaled, so that the given amount of entries is visible without the need for a scrollbar.")]
		protected int ENTRIES_PER_PAGE = 10;
	}
}

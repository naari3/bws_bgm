using System;
using LapinerTools.Steam;
using LapinerTools.Steam.Data;
using LapinerTools.Steam.UI;
using LapinerTools.uMyGUI;
using Steamworks;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class SteamLeaderboardsExampleAdvancedSettings : MonoBehaviour
{
	// Token: 0x0600002D RID: 45 RVA: 0x00003184 File Offset: 0x00001384
	private void Start()
	{
		SteamMainBase<SteamLeaderboardsMain>.Instance.OnDownloadedScores += this.OnDownloadedScores;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000319C File Offset: 0x0000139C
	private void OnDestroy()
	{
		if (SteamMainBase<SteamLeaderboardsMain>.IsInstanceSet)
		{
			SteamMainBase<SteamLeaderboardsMain>.Instance.OnDownloadedScores -= this.OnDownloadedScores;
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000031BC File Offset: 0x000013BC
	private void OnDownloadedScores(LeaderboardsDownloadedScoresEventArgs p_leaderboardArgs)
	{
		foreach (LeaderboardsScoreEntry leaderboardsScoreEntry in p_leaderboardArgs.Scores)
		{
			if (leaderboardsScoreEntry.IsCurrentUserScore)
			{
				this.m_userScore = leaderboardsScoreEntry;
				break;
			}
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000321C File Offset: 0x0000141C
	private void OnGUI()
	{
		this.m_isShown = GUI.Toggle(new Rect(0f, 140f, 80f, 20f), this.m_isShown, "Advanced");
		if (this.m_isShown)
		{
			this.m_scrollPos = GUI.BeginScrollView(new Rect(0f, 166f, 140f, (float)(Screen.height - 166)), this.m_scrollPos, new Rect(0f, 0f, 120f, 600f));
			GUI.Box(new Rect(2f, 0f, 120f, 110f), "Source:\n" + SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadSource.ToString().Replace("k_ELeaderboardDataRequest", ""));
			if (GUI.Button(new Rect(6f, 36f, 112f, 22f), new GUIContent("Global", "Set to load global highscores")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadSource = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;
			}
			if (GUI.Button(new Rect(6f, 60f, 112f, 22f), new GUIContent("AroundUser", "Set to load scores around user's score\nAdapt range! e.g. start=-4, end=5")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadSource = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
			}
			if (GUI.Button(new Rect(6f, 84f, 112f, 22f), new GUIContent("Friends", "Set to load scores for friends only")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadSource = ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends;
			}
			int scoreDownloadRangeStart = SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadRangeStart;
			int scoreDownloadRangeEnd = SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadRangeEnd;
			GUI.Box(new Rect(2f, 116f, 120f, 30f), "");
			GUI.Label(new Rect(4f, 120f, 112f, 22f), new GUIContent("Range:         -", "Score entries range to load. For example,\n[0,10] for Global, [-4,5] for AroundUser"));
			if (int.TryParse(GUI.TextField(new Rect(new Rect(48f, 120f, 30f, 22f)), scoreDownloadRangeStart.ToString()), out scoreDownloadRangeStart))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadRangeStart = scoreDownloadRangeStart;
			}
			if (int.TryParse(GUI.TextField(new Rect(new Rect(88f, 120f, 30f, 22f)), scoreDownloadRangeEnd.ToString()), out scoreDownloadRangeEnd))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadRangeEnd = scoreDownloadRangeEnd;
			}
			GUI.Box(new Rect(2f, 152f, 120f, 86f), "Sort:\n" + SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreSortMethod.ToString().Replace("k_ELeaderboardSortMethod", ""));
			if (GUI.Button(new Rect(6f, 188f, 112f, 22f), new GUIContent("Ascending", "If you use UploadScore to create\nleaderboards, then set the sorting mode.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreSortMethod = ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending;
			}
			if (GUI.Button(new Rect(6f, 212f, 112f, 22f), new GUIContent("Descending", "If you use UploadScore to create\nleaderboards, then set the sorting mode.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreSortMethod = ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending;
			}
			GUI.Box(new Rect(2f, 242f, 120f, 110f), "Type:\n" + SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreType.ToString().Replace("k_ELeaderboardDisplayType", ""));
			if (GUI.Button(new Rect(6f, 278f, 112f, 22f), new GUIContent("Numeric", "Affects visualization of the score entries\nfor leaderboards created with UploadScore.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreType = ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric;
			}
			if (GUI.Button(new Rect(6f, 302f, 112f, 22f), new GUIContent("Seconds", "Affects visualization of the score entries\nfor leaderboards created with UploadScore.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreType = ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds;
			}
			if (GUI.Button(new Rect(6f, 326f, 112f, 22f), new GUIContent("MilliSeconds", "Affects visualization of the score entries\nfor leaderboards created with UploadScore.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreType = ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds;
			}
			GUI.Box(new Rect(2f, 356f, 120f, 88f), "Sort:\n" + SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreUploadMethod.ToString().Replace("k_ELeaderboardUploadScoreMethod", ""));
			if (GUI.Button(new Rect(6f, 394f, 112f, 22f), new GUIContent("KeepBest", "Overwrite existing scores\nonly with new records.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreUploadMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;
			}
			if (GUI.Button(new Rect(6f, 418f, 112f, 22f), new GUIContent("ForceUpdate", "Always overwrite existing scores.")))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreUploadMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate;
			}
			int scoreDownloadDetailsLength = SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadDetailsLength;
			GUI.Box(new Rect(2f, 448f, 120f, 150f), "Entry Details:");
			GUI.Label(new Rect(4f, 472f, 112f, 22f), new GUIContent("Length:", "Integers count of additional data for the\nscore entry. Could be used to save replays."));
			if (int.TryParse(GUI.TextField(new Rect(new Rect(55f, 472f, 60f, 22f)), scoreDownloadDetailsLength.ToString()), out scoreDownloadDetailsLength))
			{
				SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreDownloadDetailsLength = scoreDownloadDetailsLength;
			}
			GUI.Label(new Rect(4f, 496f, 112f, 22f), new GUIContent("Data:", "This text will be uploaded\nwhen the button below is used."));
			this.m_detailsToUpload = GUI.TextField(new Rect(new Rect(55f, 496f, 60f, 22f)), this.m_detailsToUpload);
			if (GUI.Button(new Rect(6f, 520f, 112f, 50f), new GUIContent("Upload Score\nWith Data", "Will upload the score\nwith the text entered above.")))
			{
				int p_score;
				string leaderboardName;
				if (this.GetLeaderboardNameAndScoreFromSimpleExampleScript(out leaderboardName, out p_score))
				{
					uMyGUI_PopupManager.Instance.ShowPopup("loading");
					SteamMainBase<SteamLeaderboardsMain>.Instance.UploadScore(leaderboardName, p_score, SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreSortMethod, SteamMainBase<SteamLeaderboardsMain>.Instance.ScoreType, this.m_detailsToUpload, delegate(LeaderboardsUploadedScoreEventArgs p_leaderboardArgs)
					{
						uMyGUI_PopupManager.Instance.HidePopup("loading");
						if (SteamLeaderboardsUI.Instance != null)
						{
							SteamLeaderboardsUI.Instance.DownloadScoresAroundUser(leaderboardName, 9);
						}
					});
				}
			}
			if (GUI.Button(new Rect(6f, 572f, 112f, 22f), new GUIContent("Get Score Data", "Will show the additional data\nof the current user's score entry.")))
			{
				string p_bodyText;
				if (scoreDownloadDetailsLength <= 1)
				{
					p_bodyText = "Please set entry details length to a value > 1, then hit 'Load Scores' in the top!";
				}
				else if (this.m_userScore != null)
				{
					p_bodyText = string.Concat(new object[]
					{
						"#",
						this.m_userScore.GlobalRank,
						" - ",
						this.m_userScore.UserName,
						" - ",
						this.m_userScore.ScoreString,
						"\nData: '",
						this.m_userScore.DetailsDownloadedAsString,
						"'"
					});
				}
				else
				{
					p_bodyText = "Please hit 'Load Scores' in the top first and make sure that the players score is returned in the list (try Source=AroundUser)!";
				}
				((uMyGUI_PopupText)uMyGUI_PopupManager.Instance.ShowPopup("text")).SetText("User Score", p_bodyText).ShowButton("ok");
			}
			GUI.EndScrollView();
			if (!string.IsNullOrEmpty(GUI.tooltip))
			{
				GUI.Box(new Rect(82f, 110f, 270f, 50f), GUI.tooltip);
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000039C0 File Offset: 0x00001BC0
	private bool GetLeaderboardNameAndScoreFromSimpleExampleScript(out string o_leaderboardName, out int o_uploadScore)
	{
		o_leaderboardName = "";
		o_uploadScore = 0;
		SteamLeaderboardsExampleStatic steamLeaderboardsExampleStatic;
		if ((steamLeaderboardsExampleStatic = Object.FindObjectOfType<SteamLeaderboardsExampleStatic>()) != null)
		{
			o_leaderboardName = steamLeaderboardsExampleStatic.LeaderboardName;
			o_uploadScore = steamLeaderboardsExampleStatic.UploadScore;
			return true;
		}
		SteamLeaderboardsExamplePopup steamLeaderboardsExamplePopup;
		if ((steamLeaderboardsExamplePopup = Object.FindObjectOfType<SteamLeaderboardsExamplePopup>()) != null)
		{
			o_leaderboardName = steamLeaderboardsExamplePopup.LeaderboardName;
			o_uploadScore = steamLeaderboardsExamplePopup.UploadScore;
			return true;
		}
		return false;
	}

	// Token: 0x04000024 RID: 36
	private bool m_isShown;

	// Token: 0x04000025 RID: 37
	private Vector2 m_scrollPos = Vector2.zero;

	// Token: 0x04000026 RID: 38
	private string m_detailsToUpload = "";

	// Token: 0x04000027 RID: 39
	private LeaderboardsScoreEntry m_userScore;
}

using System;
using LapinerTools.Steam;
using LapinerTools.Steam.Data;
using LapinerTools.Steam.UI;
using LapinerTools.uMyGUI;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class SteamLeaderboardsExamplePopup : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000027 RID: 39 RVA: 0x00003070 File Offset: 0x00001270
	public string LeaderboardName
	{
		get
		{
			return this.m_leaderboardName;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000028 RID: 40 RVA: 0x00003078 File Offset: 0x00001278
	public int UploadScore
	{
		get
		{
			return this.m_uploadScore;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003080 File Offset: 0x00001280
	private void Start()
	{
		SteamMainBase<SteamLeaderboardsMain>.Instance.IsDebugLogEnabled = true;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003090 File Offset: 0x00001290
	private void OnGUI()
	{
		this.m_leaderboardName = GUILayout.TextField(this.m_leaderboardName, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Load\nScores", Array.Empty<GUILayoutOption>()))
		{
			((SteamLeaderboardsPopup)uMyGUI_PopupManager.Instance.ShowPopup("steam_leaderboard")).LeaderboardUI.DownloadScores(this.m_leaderboardName);
		}
		this.m_uploadScore = (int)GUILayout.HorizontalSlider((float)this.m_uploadScore, 1f, 5000f, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Upload\nScore\n" + this.m_uploadScore, Array.Empty<GUILayoutOption>()))
		{
			SteamLeaderboardsUI.UploadScore(this.m_leaderboardName, this.m_uploadScore, delegate(LeaderboardsUploadedScoreEventArgs p_leaderboardArgs)
			{
				if (SteamLeaderboardsUI.Instance != null)
				{
					SteamLeaderboardsUI.Instance.DownloadScoresAroundUser(this.m_leaderboardName, 9);
				}
			});
		}
	}

	// Token: 0x04000022 RID: 34
	private string m_leaderboardName = "Leaderboard";

	// Token: 0x04000023 RID: 35
	private int m_uploadScore = 123;
}

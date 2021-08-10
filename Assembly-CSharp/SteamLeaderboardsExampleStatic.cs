using System;
using LapinerTools.Steam;
using LapinerTools.Steam.Data;
using LapinerTools.Steam.UI;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class SteamLeaderboardsExampleStatic : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000033 RID: 51 RVA: 0x00003A38 File Offset: 0x00001C38
	public string LeaderboardName
	{
		get
		{
			return this.m_leaderboardName;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000034 RID: 52 RVA: 0x00003A40 File Offset: 0x00001C40
	public int UploadScore
	{
		get
		{
			return this.m_uploadScore;
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003080 File Offset: 0x00001280
	private void Start()
	{
		SteamMainBase<SteamLeaderboardsMain>.Instance.IsDebugLogEnabled = true;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003A48 File Offset: 0x00001C48
	private void OnGUI()
	{
		this.m_leaderboardName = GUILayout.TextField(this.m_leaderboardName, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Load\nScores", Array.Empty<GUILayoutOption>()))
		{
			SteamLeaderboardsUI.Instance.DownloadScores(this.m_leaderboardName);
		}
		this.m_uploadScore = (int)GUILayout.HorizontalSlider((float)this.m_uploadScore, 1f, 5000f, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Upload\nScore\n" + this.m_uploadScore, Array.Empty<GUILayoutOption>()))
		{
			SteamLeaderboardsUI.UploadScore(this.m_leaderboardName, this.m_uploadScore, delegate(LeaderboardsUploadedScoreEventArgs p_leaderboardArgs)
			{
				SteamLeaderboardsUI.Instance.DownloadScoresAroundUser(this.m_leaderboardName, 9);
			});
		}
	}

	// Token: 0x04000028 RID: 40
	private string m_leaderboardName = "Leaderboard";

	// Token: 0x04000029 RID: 41
	private int m_uploadScore = 123;
}

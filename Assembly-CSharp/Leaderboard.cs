using System;
using System.Collections;
using LapinerTools.Steam.Data;
using LapinerTools.Steam.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000005 RID: 5
public class Leaderboard : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000E RID: 14 RVA: 0x00002312 File Offset: 0x00000512
	public string LeaderboardName
	{
		get
		{
			return this.m_leaderboardName;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000F RID: 15 RVA: 0x0000231A File Offset: 0x0000051A
	public int UploadScore
	{
		get
		{
			return this.m_uploadScore;
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002324 File Offset: 0x00000524
	private void Start()
	{
		base.StartCoroutine("Delay");
		this.m_uploadScore = (int)((Level.score + 0.0005f) * 1000f);
		SteamLeaderboardsUI.UploadScore(this.m_leaderboardName, this.m_uploadScore, delegate(LeaderboardsUploadedScoreEventArgs p_leaderboardArgs)
		{
			SteamLeaderboardsUI.Instance.DownloadScoresAroundUser(this.m_leaderboardName, 9);
		});
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002374 File Offset: 0x00000574
	private void Update()
	{
		if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.JoystickButton6) && this.load)
		{
			SceneManager.LoadScene("loading");
		}
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
		{
			SceneManager.LoadScene("intro");
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023CE File Offset: 0x000005CE
	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(0.1f);
		this.load = true;
		yield break;
	}

	// Token: 0x04000007 RID: 7
	private string m_leaderboardName = "PLAYERS LEADERBOARD";

	// Token: 0x04000008 RID: 8
	private int m_uploadScore;

	// Token: 0x04000009 RID: 9
	private bool load;
}

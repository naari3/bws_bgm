using System;
using Steamworks;

namespace LapinerTools.Steam.Data
{
	// Token: 0x02000029 RID: 41
	public class LeaderboardsScoreEntry
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009531 File Offset: 0x00007731
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00009539 File Offset: 0x00007739
		public string LeaderboardName { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009542 File Offset: 0x00007742
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000954A File Offset: 0x0000774A
		public string UserName { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009553 File Offset: 0x00007753
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000955B File Offset: 0x0000775B
		public int GlobalRank { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009564 File Offset: 0x00007764
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000956C File Offset: 0x0000776C
		public int Score { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009575 File Offset: 0x00007775
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000957D File Offset: 0x0000777D
		public string ScoreString { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009586 File Offset: 0x00007786
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000958E File Offset: 0x0000778E
		public ELeaderboardDisplayType ScoreType { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009597 File Offset: 0x00007797
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000959F File Offset: 0x0000779F
		public int DetailsAvailableLength { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000095A8 File Offset: 0x000077A8
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000095B0 File Offset: 0x000077B0
		public int[] DetailsDownloaded { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000095B9 File Offset: 0x000077B9
		public string DetailsDownloadedAsString
		{
			get
			{
				return SteamLeaderboardsMain.ConvertIntArrayToStr(this.DetailsDownloaded);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000095C6 File Offset: 0x000077C6
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000095CE File Offset: 0x000077CE
		public bool IsCurrentUserScore { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000095D7 File Offset: 0x000077D7
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000095DF File Offset: 0x000077DF
		public LeaderboardsScoreEntry.SteamNativeData SteamNative { get; set; }

		// Token: 0x06000193 RID: 403 RVA: 0x000095E8 File Offset: 0x000077E8
		public LeaderboardsScoreEntry()
		{
			this.SteamNative = new LeaderboardsScoreEntry.SteamNativeData();
		}

		// Token: 0x0200008D RID: 141
		public class SteamNativeData
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000EABC File Offset: 0x0000CCBC
			// (set) Token: 0x06000300 RID: 768 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
			public SteamLeaderboard_t m_hSteamLeaderboard { get; set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000EACD File Offset: 0x0000CCCD
			// (set) Token: 0x06000302 RID: 770 RVA: 0x0000EAD5 File Offset: 0x0000CCD5
			public UGCHandle_t m_hUGC { get; set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0000EADE File Offset: 0x0000CCDE
			// (set) Token: 0x06000304 RID: 772 RVA: 0x0000EAE6 File Offset: 0x0000CCE6
			public CSteamID m_steamIDUser { get; set; }

			// Token: 0x06000305 RID: 773 RVA: 0x0000E487 File Offset: 0x0000C687
			public SteamNativeData()
			{
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000EAEF File Offset: 0x0000CCEF
			public SteamNativeData(SteamLeaderboard_t p_hSteamLeaderboard, UGCHandle_t p_hUGC, CSteamID p_steamIDUser)
			{
				this.m_hSteamLeaderboard = p_hSteamLeaderboard;
				this.m_hUGC = p_hUGC;
				this.m_steamIDUser = p_steamIDUser;
			}
		}
	}
}

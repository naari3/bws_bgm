using System;
using Steamworks;

namespace LapinerTools.Steam.Data
{
	// Token: 0x0200002A RID: 42
	public class LeaderboardsUploadedScoreEventArgs : EventArgsBase
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000095FB File Offset: 0x000077FB
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009603 File Offset: 0x00007803
		public string LeaderboardName { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000960C File Offset: 0x0000780C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009614 File Offset: 0x00007814
		public int Score { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000961D File Offset: 0x0000781D
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00009625 File Offset: 0x00007825
		public string ScoreString { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000962E File Offset: 0x0000782E
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009636 File Offset: 0x00007836
		public ELeaderboardDisplayType ScoreType { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000963F File Offset: 0x0000783F
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009647 File Offset: 0x00007847
		public bool IsScoreChanged { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009650 File Offset: 0x00007850
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00009658 File Offset: 0x00007858
		public int GlobalRankNew { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009661 File Offset: 0x00007861
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009669 File Offset: 0x00007869
		public int GlobalRankPrevious { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009672 File Offset: 0x00007872
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000967A File Offset: 0x0000787A
		public LeaderboardsUploadedScoreEventArgs.SteamNativeData SteamNative { get; set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x00009683 File Offset: 0x00007883
		public LeaderboardsUploadedScoreEventArgs()
		{
			this.SteamNative = new LeaderboardsUploadedScoreEventArgs.SteamNativeData();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009696 File Offset: 0x00007896
		public LeaderboardsUploadedScoreEventArgs(EventArgsBase p_errorEventArgs) : base(p_errorEventArgs)
		{
			this.SteamNative = new LeaderboardsUploadedScoreEventArgs.SteamNativeData();
		}

		// Token: 0x0200008E RID: 142
		public class SteamNativeData
		{
			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000307 RID: 775 RVA: 0x0000EB0C File Offset: 0x0000CD0C
			// (set) Token: 0x06000308 RID: 776 RVA: 0x0000EB14 File Offset: 0x0000CD14
			public SteamLeaderboard_t m_hSteamLeaderboard { get; set; }

			// Token: 0x06000309 RID: 777 RVA: 0x0000E487 File Offset: 0x0000C687
			public SteamNativeData()
			{
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000EB1D File Offset: 0x0000CD1D
			public SteamNativeData(SteamLeaderboard_t p_hSteamLeaderboard)
			{
				this.m_hSteamLeaderboard = p_hSteamLeaderboard;
			}
		}
	}
}

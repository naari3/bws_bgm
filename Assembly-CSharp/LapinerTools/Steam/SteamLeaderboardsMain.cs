using System;
using System.Collections.Generic;
using System.Text;
using LapinerTools.Steam.Data;
using Steamworks;
using UnityEngine;

namespace LapinerTools.Steam
{
	// Token: 0x02000023 RID: 35
	public class SteamLeaderboardsMain : SteamMainBase<SteamLeaderboardsMain>
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600011B RID: 283 RVA: 0x00007810 File Offset: 0x00005A10
		// (remove) Token: 0x0600011C RID: 284 RVA: 0x00007848 File Offset: 0x00005A48
		public event Action<LeaderboardsUploadedScoreEventArgs> OnUploadedScore;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600011D RID: 285 RVA: 0x00007880 File Offset: 0x00005A80
		// (remove) Token: 0x0600011E RID: 286 RVA: 0x000078B8 File Offset: 0x00005AB8
		public event Action<LeaderboardsDownloadedScoresEventArgs> OnDownloadedScores;

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000078ED File Offset: 0x00005AED
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000078F5 File Offset: 0x00005AF5
		public ELeaderboardSortMethod ScoreSortMethod
		{
			get
			{
				return this.m_scoreSorting;
			}
			set
			{
				this.m_scoreSorting = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000078FE File Offset: 0x00005AFE
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00007906 File Offset: 0x00005B06
		public ELeaderboardDisplayType ScoreType
		{
			get
			{
				return this.m_scoreType;
			}
			set
			{
				this.m_scoreType = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000790F File Offset: 0x00005B0F
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00007917 File Offset: 0x00005B17
		public ELeaderboardDataRequest ScoreDownloadSource
		{
			get
			{
				return this.m_scoreDownloadSource;
			}
			set
			{
				this.m_scoreDownloadSource = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00007920 File Offset: 0x00005B20
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007928 File Offset: 0x00005B28
		public int ScoreDownloadRangeStart
		{
			get
			{
				return this.m_scoreDownloadRangeStart;
			}
			set
			{
				this.m_scoreDownloadRangeStart = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007931 File Offset: 0x00005B31
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00007939 File Offset: 0x00005B39
		public int ScoreDownloadRangeEnd
		{
			get
			{
				return this.m_scoreDownloadRangeEnd;
			}
			set
			{
				this.m_scoreDownloadRangeEnd = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007942 File Offset: 0x00005B42
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000794A File Offset: 0x00005B4A
		public ELeaderboardUploadScoreMethod ScoreUploadMethod
		{
			get
			{
				return this.m_scoreUploadMethod;
			}
			set
			{
				this.m_scoreUploadMethod = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007953 File Offset: 0x00005B53
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000795B File Offset: 0x00005B5B
		public int ScoreDownloadDetailsLength
		{
			get
			{
				return this.m_scoreDownloadDetailsLength;
			}
			set
			{
				if (value > 64)
				{
					Debug.LogError("ScoreDownloadDetailsLength: max. 64 integers! Tried to set '" + value + "'!");
				}
				this.m_scoreDownloadDetailsLength = Mathf.Clamp(value, 0, 64);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000798B File Offset: 0x00005B8B
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00007993 File Offset: 0x00005B93
		public string ScoreFormatNumeric
		{
			get
			{
				return this.m_scoreFormatNumeric;
			}
			set
			{
				this.m_scoreFormatNumeric = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000799C File Offset: 0x00005B9C
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000079A4 File Offset: 0x00005BA4
		public string ScoreFormatSeconds
		{
			get
			{
				return this.m_scoreFormatSeconds;
			}
			set
			{
				this.m_scoreFormatSeconds = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000079AD File Offset: 0x00005BAD
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000079B5 File Offset: 0x00005BB5
		public string ScoreFormatMilliSeconds
		{
			get
			{
				return this.m_scoreFormatMilliSeconds;
			}
			set
			{
				this.m_scoreFormatMilliSeconds = value;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000079BE File Offset: 0x00005BBE
		public bool UploadScore(string p_leaderboardName, int p_score, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			return this.UploadScore(p_leaderboardName, p_score, this.m_scoreSorting, this.m_scoreType, p_onUploadedScore);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000079D5 File Offset: 0x00005BD5
		public bool UploadScore(string p_leaderboardName, int p_score, ELeaderboardSortMethod p_scoreSorting, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			return this.UploadScore(p_leaderboardName, p_score, p_scoreSorting, this.m_scoreType, p_onUploadedScore);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000079E8 File Offset: 0x00005BE8
		public bool UploadScore(string p_leaderboardName, int p_score, ELeaderboardDisplayType p_scoreType, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			return this.UploadScore(p_leaderboardName, p_score, this.m_scoreSorting, p_scoreType, p_onUploadedScore);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000079FB File Offset: 0x00005BFB
		public bool UploadScore(string p_leaderboardName, int p_score, ELeaderboardSortMethod p_scoreSorting, ELeaderboardDisplayType p_scoreType, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			return this.UploadScore(p_leaderboardName, p_score, p_scoreSorting, p_scoreType, null, p_onUploadedScore);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007A0C File Offset: 0x00005C0C
		public bool UploadScore(string p_leaderboardName, int p_score, ELeaderboardSortMethod p_scoreSorting, ELeaderboardDisplayType p_scoreType, string p_scoreDetails, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			if (p_scoreDetails != null && p_scoreDetails.Length > 252)
			{
				Debug.LogError("UploadScore: max. score details string length is 252 characters! Provided '" + p_scoreDetails.Length + "'! Data will be cutoff!");
				p_scoreDetails = p_scoreDetails.Substring(0, 252);
			}
			return this.UploadScore(p_leaderboardName, p_score, p_scoreSorting, p_scoreType, SteamLeaderboardsMain.ConvertStrToIntArray(p_scoreDetails), p_onUploadedScore);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A70 File Offset: 0x00005C70
		public bool UploadScore(string p_leaderboardName, int p_score, ELeaderboardSortMethod p_scoreSorting, ELeaderboardDisplayType p_scoreType, int[] p_scoreDetails, Action<LeaderboardsUploadedScoreEventArgs> p_onUploadedScore)
		{
			if (SteamManager.Initialized)
			{
				if (p_scoreDetails != null && p_scoreDetails.Length > 64)
				{
					Debug.LogError("UploadScore: max. score details array length is 64 integers! Provided '" + p_scoreDetails.Length + "'! Data will be cutoff!");
				}
				this.SetSingleShotEventHandler<LeaderboardsUploadedScoreEventArgs>(this.GetEventNameForOnUploadedScore(p_leaderboardName, p_score), ref this.OnUploadedScore, p_onUploadedScore);
				base.Execute<LeaderboardFindResult_t>(SteamUserStats.FindOrCreateLeaderboard(p_leaderboardName, p_scoreSorting, p_scoreType), delegate(LeaderboardFindResult_t p_callback, bool p_bIOFailure)
				{
					this.OnUploadScoreFindOrCreateLeaderboardCallCompleted(p_leaderboardName, p_score, p_scoreSorting, p_scoreType, p_scoreDetails, p_callback, p_bIOFailure);
				});
				return true;
			}
			ErrorEventArgs errorEventArgs = ErrorEventArgs.CreateSteamNotInit();
			this.InvokeEventHandlerSafely<LeaderboardsUploadedScoreEventArgs>(p_onUploadedScore, new LeaderboardsUploadedScoreEventArgs(errorEventArgs));
			this.HandleError("UploadScore: failed! ", errorEventArgs);
			return false;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007B5A File Offset: 0x00005D5A
		public bool DownloadScores(string p_leaderboardName, Action<LeaderboardsDownloadedScoresEventArgs> p_onDownloadedScores)
		{
			return this.DownloadScores(p_leaderboardName, this.ScoreDownloadSource, this.ScoreDownloadRangeStart, this.ScoreDownloadRangeEnd, p_onDownloadedScores);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007B76 File Offset: 0x00005D76
		public bool DownloadScoresAroundUser(string p_leaderboardName, int p_range, Action<LeaderboardsDownloadedScoresEventArgs> p_onDownloadedScores)
		{
			return this.DownloadScores(p_leaderboardName, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, -p_range / 2, p_range - p_range / 2, p_onDownloadedScores);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007B8C File Offset: 0x00005D8C
		public bool DownloadScores(string p_leaderboardName, ELeaderboardDataRequest p_scoreSource, int p_rangeStart, int p_rangeEnd, Action<LeaderboardsDownloadedScoresEventArgs> p_onDownloadedScores)
		{
			if (SteamManager.Initialized)
			{
				this.SetSingleShotEventHandler<LeaderboardsDownloadedScoresEventArgs>("OnDownloadedScores", ref this.OnDownloadedScores, p_onDownloadedScores);
				base.Execute<LeaderboardFindResult_t>(SteamUserStats.FindLeaderboard(p_leaderboardName), delegate(LeaderboardFindResult_t p_callback, bool p_bIOFailure)
				{
					this.OnDownloadScoresFindLeaderboardCallCompleted(p_leaderboardName, p_scoreSource, p_rangeStart, p_rangeEnd, p_callback, p_bIOFailure);
				});
				return true;
			}
			ErrorEventArgs errorEventArgs = ErrorEventArgs.CreateSteamNotInit();
			this.InvokeEventHandlerSafely<LeaderboardsDownloadedScoresEventArgs>(p_onDownloadedScores, new LeaderboardsDownloadedScoresEventArgs(errorEventArgs));
			this.HandleError("DownloadScores: failed! ", errorEventArgs);
			return false;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007C1D File Offset: 0x00005E1D
		public bool IsAvatarTextureSet(LeaderboardsScoreEntry p_scoreEntry)
		{
			return this.IsAvatarTextureSet(p_scoreEntry.SteamNative.m_steamIDUser);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007C30 File Offset: 0x00005E30
		public bool IsAvatarTextureSet(CSteamID p_steamIDUser)
		{
			return SteamFriends.GetLargeFriendAvatar(p_steamIDUser) != 0;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007C3B File Offset: 0x00005E3B
		public Texture2D GetAvatarTexture(LeaderboardsScoreEntry p_scoreEntry)
		{
			return this.GetAvatarTexture(p_scoreEntry.SteamNative.m_steamIDUser);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007C50 File Offset: 0x00005E50
		public Texture2D GetAvatarTexture(CSteamID p_steamIDUser)
		{
			int largeFriendAvatar = SteamFriends.GetLargeFriendAvatar(p_steamIDUser);
			if (largeFriendAvatar == -1)
			{
				SteamFriends.RequestUserInformation(p_steamIDUser, false);
			}
			else if (largeFriendAvatar != 0)
			{
				return this.GetSteamImageAsTexture2D(largeFriendAvatar);
			}
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007C80 File Offset: 0x00005E80
		public string FormatScore(int p_score, ELeaderboardDisplayType p_scoreType)
		{
			string result;
			try
			{
				switch (p_scoreType)
				{
				case ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds:
				{
					TimeSpan timeSpan = TimeSpan.FromSeconds((double)p_score);
					return string.Format(this.m_scoreFormatSeconds, (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
				}
				case ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds:
				{
					TimeSpan timeSpan2 = TimeSpan.FromMilliseconds((double)p_score);
					return string.Format(this.m_scoreFormatMilliSeconds, (int)timeSpan2.TotalMinutes, timeSpan2.Seconds, timeSpan2.Milliseconds);
				}
				}
				result = p_score.ToString(this.m_scoreFormatNumeric);
			}
			catch (Exception ex)
			{
				this.HandleError("FormatScore: invalid format string! ", new ErrorEventArgs(ex.Message));
				result = p_score.ToString();
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007D60 File Offset: 0x00005F60
		public static string ConvertIntArrayToStr(int[] p_array)
		{
			if (p_array == null || p_array.Length == 0)
			{
				return "";
			}
			string text = "";
			for (int i = 1; i < p_array.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(p_array[i]);
				if (p_array[0] > i * 4)
				{
					text += Encoding.ASCII.GetString(bytes);
				}
				else
				{
					text += Encoding.ASCII.GetString(bytes, 0, p_array[0] - (i - 1) * 4);
				}
			}
			return text;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public static int[] ConvertStrToIntArray(string p_str)
		{
			if (p_str == null || p_str.Length == 0)
			{
				return new int[0];
			}
			List<int> list = new List<int>();
			list.Add(p_str.Length);
			byte[] bytes = Encoding.ASCII.GetBytes(p_str);
			byte[] array = new byte[4];
			for (int i = 0; i < bytes.Length; i += 4)
			{
				for (int j = 0; j < 4; j++)
				{
					if (j + i < bytes.Length)
					{
						array[j] = bytes[j + i];
					}
					else
					{
						array[j] = 0;
					}
				}
				list.Add(BitConverter.ToInt32(array, 0));
			}
			return list.ToArray();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007E60 File Offset: 0x00006060
		protected override void LateUpdate()
		{
			base.LateUpdate();
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_scores.Count > 0 && this.m_scoresMissingUserNames.Count > 0)
				{
					int count = this.m_scoresMissingUserNames.Count;
					for (int i = this.m_scoresMissingUserNames.Count - 1; i >= 0; i--)
					{
						CSteamID csteamID = this.m_scoresMissingUserNames[i];
						if (!SteamFriends.RequestUserInformation(csteamID, true))
						{
							for (int j = 0; j < this.m_scores.Count; j++)
							{
								if (this.m_scores[j].SteamNative.m_steamIDUser == csteamID)
								{
									this.m_scores[j].UserName = SteamFriends.GetFriendPersonaName(csteamID);
									break;
								}
							}
							this.m_scoresMissingUserNames.RemoveAt(i);
						}
					}
					if (base.IsDebugLogEnabled && count != this.m_scoresMissingUserNames.Count)
					{
						Debug.Log(string.Concat(new object[]
						{
							"SteamLeaderboardsMain: loaded '",
							count - this.m_scoresMissingUserNames.Count,
							"' user names, still missing count: '",
							this.m_scoresMissingUserNames.Count,
							"'"
						}));
					}
					if (this.m_scoresMissingUserNames.Count == 0)
					{
						this.InvokeEventHandlerSafely<LeaderboardsDownloadedScoresEventArgs>(this.OnDownloadedScores, new LeaderboardsDownloadedScoresEventArgs(this.m_scoresLeaderboardName, new List<LeaderboardsScoreEntry>(this.m_scores)));
						this.ClearSingleShotEventHandlers<LeaderboardsDownloadedScoresEventArgs>("OnDownloadedScores", ref this.OnDownloadedScores);
						this.m_scores.Clear();
					}
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008020 File Offset: 0x00006220
		private void OnUploadScoreFindOrCreateLeaderboardCallCompleted(string p_leaderboardName, int p_score, ELeaderboardSortMethod p_scoreSorting, ELeaderboardDisplayType p_scoreType, int[] p_scoreDetails, LeaderboardFindResult_t p_callbackFind, bool p_bIOFailureFind)
		{
			EResult p_result = (p_callbackFind.m_bLeaderboardFound == 1) ? EResult.k_EResultOK : EResult.k_EResultFileNotFound;
			if (this.CheckAndLogResult<LeaderboardFindResult_t, LeaderboardsUploadedScoreEventArgs>("OnUploadScoreFindOrCreateLeaderboardCallCompleted", p_result, p_bIOFailureFind, this.GetEventNameForOnUploadedScore(p_leaderboardName, p_score), ref this.OnUploadedScore))
			{
				ELeaderboardSortMethod leaderboardSortMethod = SteamUserStats.GetLeaderboardSortMethod(p_callbackFind.m_hSteamLeaderboard);
				if (leaderboardSortMethod != p_scoreSorting)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"UploadScore: sorting mismatch for leaderboard '",
						p_leaderboardName,
						"' sort mode on Steam is '",
						leaderboardSortMethod,
						"', expected '",
						p_scoreSorting,
						"'!"
					}));
				}
				ELeaderboardDisplayType leaderboardDisplayType = SteamUserStats.GetLeaderboardDisplayType(p_callbackFind.m_hSteamLeaderboard);
				if (leaderboardDisplayType != p_scoreType)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"UploadScore: type mismatch for leaderboard '",
						p_leaderboardName,
						"' type on Steam is '",
						leaderboardDisplayType,
						"', expected '",
						p_scoreType,
						"'!"
					}));
				}
				if (p_scoreDetails == null)
				{
					p_scoreDetails = new int[0];
				}
				base.Execute<LeaderboardScoreUploaded_t>(SteamUserStats.UploadLeaderboardScore(p_callbackFind.m_hSteamLeaderboard, this.m_scoreUploadMethod, p_score, p_scoreDetails, Mathf.Min(64, p_scoreDetails.Length)), delegate(LeaderboardScoreUploaded_t p_callbackUpload, bool p_bIOFailureUpload)
				{
					this.OnUploadLeaderboardScoreCallCompleted(p_leaderboardName, p_score, p_callbackUpload, p_bIOFailureUpload);
				});
				if (base.IsDebugLogEnabled)
				{
					Debug.Log("UploadScore: leaderboard '" + p_leaderboardName + "' found, starting score upload");
				}
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000081A4 File Offset: 0x000063A4
		private void OnUploadLeaderboardScoreCallCompleted(string p_leaderboardName, int p_score, LeaderboardScoreUploaded_t p_callback, bool p_bIOFailure)
		{
			EResult p_result = (p_callback.m_bSuccess == 1) ? EResult.k_EResultOK : EResult.k_EResultUnexpectedError;
			if (this.CheckAndLogResult<LeaderboardScoreUploaded_t, LeaderboardsUploadedScoreEventArgs>("OnUploadLeaderboardScoreCallCompleted", p_result, p_bIOFailure, this.GetEventNameForOnUploadedScore(p_leaderboardName, p_score), ref this.OnUploadedScore) && this.OnUploadedScore != null)
			{
				ELeaderboardDisplayType leaderboardDisplayType = SteamUserStats.GetLeaderboardDisplayType(p_callback.m_hSteamLeaderboard);
				this.InvokeEventHandlerSafely<LeaderboardsUploadedScoreEventArgs>(this.OnUploadedScore, new LeaderboardsUploadedScoreEventArgs
				{
					LeaderboardName = p_leaderboardName,
					Score = p_callback.m_nScore,
					ScoreString = this.FormatScore(p_callback.m_nScore, leaderboardDisplayType),
					ScoreType = leaderboardDisplayType,
					IsScoreChanged = (p_callback.m_bScoreChanged == 1),
					GlobalRankNew = p_callback.m_nGlobalRankNew,
					GlobalRankPrevious = p_callback.m_nGlobalRankPrevious,
					SteamNative = new LeaderboardsUploadedScoreEventArgs.SteamNativeData(p_callback.m_hSteamLeaderboard)
				});
				this.ClearSingleShotEventHandlers<LeaderboardsUploadedScoreEventArgs>(this.GetEventNameForOnUploadedScore(p_leaderboardName, p_score), ref this.OnUploadedScore);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008282 File Offset: 0x00006482
		private string GetEventNameForOnUploadedScore(string p_leaderboardName, int p_score)
		{
			return "OnUploadedScore" + p_leaderboardName + p_score;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008298 File Offset: 0x00006498
		private void OnDownloadScoresFindLeaderboardCallCompleted(string p_leaderboardName, ELeaderboardDataRequest p_scoreSource, int p_rangeStart, int p_rangeEnd, LeaderboardFindResult_t p_callbackFind, bool p_bIOFailureFind)
		{
			EResult p_result = (p_callbackFind.m_bLeaderboardFound == 1) ? EResult.k_EResultOK : EResult.k_EResultFileNotFound;
			if (this.CheckAndLogResult<LeaderboardFindResult_t, LeaderboardsDownloadedScoresEventArgs>("OnDownloadScoresFindLeaderboardCallCompleted", p_result, p_bIOFailureFind, "OnDownloadedScores", ref this.OnDownloadedScores))
			{
				base.Execute<LeaderboardScoresDownloaded_t>(SteamUserStats.DownloadLeaderboardEntries(p_callbackFind.m_hSteamLeaderboard, p_scoreSource, p_rangeStart, p_rangeEnd), delegate(LeaderboardScoresDownloaded_t p_callbackDownload, bool p_bIOFailureDownload)
				{
					this.OnDownloadLeaderboardEntriesCallCompleted(p_leaderboardName, p_callbackDownload, p_bIOFailureDownload);
				});
				if (base.IsDebugLogEnabled)
				{
					Debug.Log("DownloadScores: leaderboard '" + p_leaderboardName + "' found, starting scores download");
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008328 File Offset: 0x00006528
		private void OnDownloadLeaderboardEntriesCallCompleted(string p_leaderboardName, LeaderboardScoresDownloaded_t p_callback, bool p_bIOFailure)
		{
			if (this.CheckAndLogResult<LeaderboardScoresDownloaded_t, LeaderboardsDownloadedScoresEventArgs>("OnDownloadLeaderboardEntriesCallCompleted", EResult.k_EResultOK, p_bIOFailure, "OnDownloadedScores", ref this.OnDownloadedScores) && this.OnDownloadedScores != null)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					this.m_scores.Clear();
					this.m_scoresMissingUserNames.Clear();
					this.m_scoresLeaderboardName = p_leaderboardName;
					for (int i = 0; i < p_callback.m_cEntryCount; i++)
					{
						int[] array = new int[Mathf.Max(0, this.m_scoreDownloadDetailsLength)];
						LeaderboardEntry_t leaderboardEntry_t;
						if (SteamUserStats.GetDownloadedLeaderboardEntry(p_callback.m_hSteamLeaderboardEntries, i, out leaderboardEntry_t, array, array.Length))
						{
							if (SteamFriends.RequestUserInformation(leaderboardEntry_t.m_steamIDUser, true))
							{
								this.m_scoresMissingUserNames.Add(leaderboardEntry_t.m_steamIDUser);
							}
							int[] array2 = new int[Mathf.Min(array.Length, leaderboardEntry_t.m_cDetails)];
							Array.Copy(array, array2, array2.Length);
							string friendPersonaName = SteamFriends.GetFriendPersonaName(leaderboardEntry_t.m_steamIDUser);
							ELeaderboardDisplayType leaderboardDisplayType = SteamUserStats.GetLeaderboardDisplayType(p_callback.m_hSteamLeaderboard);
							LeaderboardsScoreEntry item = new LeaderboardsScoreEntry
							{
								LeaderboardName = p_leaderboardName,
								UserName = friendPersonaName,
								GlobalRank = leaderboardEntry_t.m_nGlobalRank,
								Score = leaderboardEntry_t.m_nScore,
								ScoreString = this.FormatScore(leaderboardEntry_t.m_nScore, leaderboardDisplayType),
								ScoreType = leaderboardDisplayType,
								DetailsAvailableLength = leaderboardEntry_t.m_cDetails,
								DetailsDownloaded = array2,
								IsCurrentUserScore = (leaderboardEntry_t.m_steamIDUser == SteamUser.GetSteamID()),
								SteamNative = new LeaderboardsScoreEntry.SteamNativeData(p_callback.m_hSteamLeaderboard, leaderboardEntry_t.m_hUGC, leaderboardEntry_t.m_steamIDUser)
							};
							this.m_scores.Add(item);
						}
					}
				}
				if (this.m_scoresMissingUserNames.Count == 0)
				{
					this.InvokeEventHandlerSafely<LeaderboardsDownloadedScoresEventArgs>(this.OnDownloadedScores, new LeaderboardsDownloadedScoresEventArgs(p_leaderboardName, new List<LeaderboardsScoreEntry>(this.m_scores)));
					this.ClearSingleShotEventHandlers<LeaderboardsDownloadedScoresEventArgs>("OnDownloadedScores", ref this.OnDownloadedScores);
					return;
				}
				if (base.IsDebugLogEnabled)
				{
					Debug.Log("OnDownloadLeaderboardEntriesCallCompleted: missing user names count: '" + this.m_scoresMissingUserNames.Count + "'");
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008558 File Offset: 0x00006758
		private Texture2D GetSteamImageAsTexture2D(int p_imageID)
		{
			Texture2D texture2D = null;
			uint num;
			uint num2;
			if (SteamUtils.GetImageSize(p_imageID, out num, out num2))
			{
				byte[] array = new byte[num * num2 * 4U];
				if (SteamUtils.GetImageRGBA(p_imageID, array, array.Length))
				{
					int num3 = (int)(num * 4U);
					byte[] array2 = new byte[array.Length];
					int i = 0;
					int num4 = array.Length - num3;
					while (i < array.Length)
					{
						for (int j = 0; j < num3; j++)
						{
							array2[i + j] = array[num4 + j];
						}
						i += num3;
						num4 -= num3;
					}
					texture2D = new Texture2D((int)num, (int)num2, TextureFormat.RGBA32, false, true);
					texture2D.LoadRawTextureData(array2);
					texture2D.Apply();
				}
			}
			return texture2D;
		}

		// Token: 0x040000C5 RID: 197
		private List<LeaderboardsScoreEntry> m_scores = new List<LeaderboardsScoreEntry>();

		// Token: 0x040000C6 RID: 198
		private List<CSteamID> m_scoresMissingUserNames = new List<CSteamID>();

		// Token: 0x040000C7 RID: 199
		private string m_scoresLeaderboardName;

		// Token: 0x040000CA RID: 202
		[SerializeField]
		[Tooltip("If you use SteamLeaderboardsMain.UploadScore to create leaderboards, then set the correct sorting mode here. Alternatively, you can also pass the desired sorting mode to the SteamLeaderboardsMain.UploadScore method.")]
		private ELeaderboardSortMethod m_scoreSorting = ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		[Tooltip("If you use SteamLeaderboardsMain.UploadScore to create leaderboards, then set the correct display type here. Alternatively, you can also pass the desired display type to the SteamLeaderboardsMain.UploadScore method.")]
		private ELeaderboardDisplayType m_scoreType = ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		[Tooltip("Define the leaderboard source here.\nGlobal: global highscores.\nAroundUser: scores around user's score (adapt range!).\nFriends: scores for friends only.")]
		private ELeaderboardDataRequest m_scoreDownloadSource;

		// Token: 0x040000CD RID: 205
		[SerializeField]
		[Tooltip("Score entries range to load. For example, [0,10] for Global, [-4,5] for AroundUser.")]
		private int m_scoreDownloadRangeStart;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		[Tooltip("Score entries range to load. For example, [0,10] for Global, [-4,5] for AroundUser.")]
		private int m_scoreDownloadRangeEnd = 10;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		[Tooltip("Select when the users score should be updated here.\nKeepBest: overwrite existing scores only with new records.\nForceUpdate: always overwrite existing scores.")]
		private ELeaderboardUploadScoreMethod m_scoreUploadMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		[Tooltip("Determines the maximal integers count of additional score entry data downloaded from Steam. Could be used to load replays. Max. 64 integers -> use SteamUGC to upload big data amounts.")]
		private int m_scoreDownloadDetailsLength;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		[Tooltip("The pattern used to format scores of Numeric type.")]
		private string m_scoreFormatNumeric = "";

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		[Tooltip("The pattern used to format scores of Seconds type.")]
		private string m_scoreFormatSeconds = "{0:00}:{1:D2}";

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		[Tooltip("The pattern used to format scores of MilliSeconds type.")]
		private string m_scoreFormatMilliSeconds = "{0:00}:{1:D2}:{2:D3}";
	}
}

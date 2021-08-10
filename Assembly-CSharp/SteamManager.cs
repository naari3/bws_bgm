using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x0200000E RID: 14
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000039 RID: 57 RVA: 0x00003B1B File Offset: 0x00001D1B
	private static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00003B3F File Offset: 0x00001D3F
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003B4B File Offset: 0x00001D4B
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003B54 File Offset: 0x00001D54
	private void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException arg)
		{
			Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + arg, this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		SteamManager.s_EverInitialized = true;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003C28 File Offset: 0x00001E28
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003C76 File Offset: 0x00001E76
	private void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003C9A File Offset: 0x00001E9A
	private void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x0400002A RID: 42
	private static SteamManager s_instance;

	// Token: 0x0400002B RID: 43
	private static bool s_EverInitialized;

	// Token: 0x0400002C RID: 44
	private bool m_bInitialized;

	// Token: 0x0400002D RID: 45
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}

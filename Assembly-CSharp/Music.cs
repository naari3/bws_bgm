using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000008 RID: 8
public class Music : MonoBehaviour
{
	// Token: 0x0600001C RID: 28 RVA: 0x00002CCD File Offset: 0x00000ECD
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		this.music = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002CE8 File Offset: 0x00000EE8
	private void Update()
	{
		if (SceneManager.GetActiveScene().name != "level")
		{
			this.music.pitch -= 0.025f * Time.deltaTime * 60f;
			if (this.music.pitch < 0f)
			{
				this.music.Stop();
			}
		}
	}

	// Token: 0x0400001C RID: 28
	private AudioSource music;
}

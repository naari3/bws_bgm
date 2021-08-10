using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000007 RID: 7
public class Loading : MonoBehaviour
{
	// Token: 0x0600001A RID: 26 RVA: 0x00002CAC File Offset: 0x00000EAC
	private void Awake()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}

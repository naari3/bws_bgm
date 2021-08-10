using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000009 RID: 9
public class Result : MonoBehaviour
{
	// Token: 0x0600001F RID: 31 RVA: 0x00002D4E File Offset: 0x00000F4E
	private void Start()
	{
		base.StartCoroutine("Delay");
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002D5C File Offset: 0x00000F5C
	private void Update()
	{
		if (this.score < Level.score)
		{
			this.score += 6f * Time.deltaTime;
		}
		if (this.score > Level.score)
		{
			this.score = Level.score;
		}
		GameObject gameObject = GameObject.Find("text");
		if (Level.score != Level.record)
		{
			gameObject.GetComponent<Text>().text = this.score.ToString("F3") + " m / " + Level.record.ToString("F3") + " m";
		}
		else if (Level.score > 0f)
		{
			gameObject.GetComponent<Text>().text = this.score.ToString("F3") + " m = RECORD";
		}
		else
		{
			gameObject.GetComponent<Text>().text = this.score.ToString("F3") + " m";
		}
		if (SteamManager.Initialized)
		{
			if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.JoystickButton6) && this.score == Level.score && this.load)
			{
				SceneManager.LoadScene("leaderboard");
			}
		}
		else if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.JoystickButton6) && this.score == Level.score && this.load)
		{
			SceneManager.LoadScene("loading");
		}
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
		{
			SceneManager.LoadScene("intro");
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002EEE File Offset: 0x000010EE
	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(0.1f);
		this.load = true;
		yield break;
	}

	// Token: 0x0400001D RID: 29
	private float score;

	// Token: 0x0400001E RID: 30
	private bool load;
}

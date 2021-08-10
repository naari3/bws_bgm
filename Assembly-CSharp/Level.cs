using System;
using RootMotion.Dynamics;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000006 RID: 6
public class Level : MonoBehaviour
{
	// Token: 0x06000015 RID: 21
	private void Start()
	{
		this.fadeIn = true;
		if (GameObject.Find("music"))
		{
			this.music = GameObject.Find("music").GetComponent<AudioSource>();
			string path = string.Format("{0}/{1}", Application.dataPath, "bgm.wav");
			this.music.clip = WavUtility.ToAudioClip(path);
			this.music.Stop();
			this.music.pitch = 1f;
		}
		this.baby = GameObject.Find("baby");
		this.puppet = GameObject.Find("PuppetMaster").GetComponent<PuppetMaster>();
		this.pixels = GameObject.Find("pixels").GetComponent<ParticleSystem>();
		Level.score = 0f;
		Level.record = PlayerPrefs.GetFloat("record", 0f);
		if (Level.record == 0f)
		{
			this.firstRecord = true;
		}
		Vector3 vector = GameObject.Find("line_record").transform.position;
		vector.z = Level.record * 2f;
		GameObject.Find("line_record").transform.position = vector;
		if (Level.repeat)
		{
			this.baby.transform.position = Level.position;
			return;
		}
		Level.balance = 0f;
	}

	// Token: 0x06000016 RID: 22
	private void Update()
	{
		GameObject gameObject4 = GameObject.Find("fade");
		Vector3 localScale = gameObject4.transform.localScale;
		if (this.fadeIn)
		{
			localScale.z -= 0.01f * Time.deltaTime * 30f;
			if (localScale.z <= 0f)
			{
				localScale.z = 0f;
				this.fadeIn = false;
			}
		}
		if (this.fadeOut)
		{
			localScale.z += 0.01f * Time.deltaTime * 30f;
			if (localScale.z >= 0.16f)
			{
				localScale.z = 0.16f;
			}
		}
		if (this.fadeIn && this.fadeOut)
		{
			this.fadeIn = false;
		}
		gameObject4.transform.localScale = localScale;
		if (localScale.z >= 0.16f)
		{
			Level.position = this.baby.transform.position;
			if (!Level.repeat && Level.score >= 3f)
			{
				Level.repeat = true;
			}
			else
			{
				Level.repeat = false;
			}
			SceneManager.LoadScene("result");
		}
		if (GameObject.Find("music") && !this.fadeIn && !this.music.isPlaying)
		{
			this.music.Play();
		}
		if (GameObject.Find("music") && this.fadeOut)
		{
			this.music.pitch -= 0.025f * Time.deltaTime * 60f;
			if (this.music.pitch < 0f)
			{
				this.music.pitch = 0f;
			}
		}
		GameObject gameObject2 = GameObject.Find("camera_target");
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, gameObject2.transform.position, Time.deltaTime);
		GameObject gameObject3 = GameObject.Find("text");
		Level.score = Mathf.Round(this.baby.transform.position.z / 2f * 1000f) / 1000f;
		if (Level.score > 0.1f)
		{
			if (this.startControl)
			{
				if (Level.score < Level.record)
				{
					gameObject3.GetComponent<Text>().text = Level.score.ToString("F2") + " m <color=#ffc800>/ " + Level.record.ToString("F2") + " m</color>";
				}
				if (Level.score > Level.record)
				{
					if (!this.firstRecord)
					{
						gameObject3.GetComponent<Text>().text = Level.score.ToString("F2") + " m <color=#ffc800>= RECORD</color>";
					}
					else
					{
						gameObject3.GetComponent<Text>().text = Level.score.ToString("F2") + " m";
					}
				}
			}
			else
			{
				gameObject3.GetComponent<Text>().text = "<color=#ffc800>LAST CHANCE</color>";
			}
		}
		else
		{
			gameObject3.GetComponent<Text>().text = "HOLD-RELEASE <color=#ffc800>SPACEBAR</color>";
		}
		if (Level.score > Level.record)
		{
			Level.record = Level.score;
			PlayerPrefs.SetFloat("record", Level.record);
		}
		if (Level.score >= 5f && !this.pixels.isPlaying)
		{
			this.pixels.Play();
		}
		if (GameObject.Find("Baby_bone_Head").transform.position.y < 0.3f)
		{
			this.fall = true;
			this.fadeOut = true;
		}
		if (!this.startControl)
		{
			this.stability = 2;
		}
		if (this.stability == 5)
		{
			this.puppet.pinPow = 1f;
		}
		if (this.stability == 4)
		{
			this.puppet.pinPow = 2f;
		}
		if (this.stability == 3)
		{
			this.puppet.pinPow = 2.2f;
		}
		if (this.stability == 2)
		{
			this.puppet.pinPow = 4f;
		}
		if (this.stability == 1)
		{
			this.puppet.pinPow = 5f;
		}
		if (this.stability == 0)
		{
			this.puppet.pinPow = 6f;
		}
		if (this.stability == 3)
		{
			this.speed = 0.1f;
		}
		if (this.stability == 4)
		{
			this.speed = 0.15f;
		}
		if (this.stability == 5)
		{
			this.speed = 0.2f;
		}
		if (this.stability < 3)
		{
			this.speed = 0f;
		}
		if (!this.fall)
		{
			if ((int)Mathf.Abs(Level.balance) == 0)
			{
				this.stability = 0;
			}
			if ((int)Mathf.Abs(Level.balance) == 1)
			{
				this.stability = 1;
			}
			if ((int)Mathf.Abs(Level.balance) == 2)
			{
				this.stability = 2;
			}
			if ((int)Mathf.Abs(Level.balance) == 3)
			{
				this.stability = 3;
			}
			if ((int)Mathf.Abs(Level.balance) == 4)
			{
				this.stability = 4;
			}
			if ((int)Mathf.Abs(Level.balance) == 5)
			{
				this.stability = 5;
			}
			if ((int)Mathf.Abs(Level.balance) == 6)
			{
				this.stability = 4;
			}
			if ((int)Mathf.Abs(Level.balance) == 7)
			{
				this.stability = 3;
			}
			if ((int)Mathf.Abs(Level.balance) == 8)
			{
				this.stability = 2;
			}
			if ((int)Mathf.Abs(Level.balance) == 9)
			{
				this.stability = 1;
			}
			if ((int)Mathf.Abs(Level.balance) == 10)
			{
				this.stability = 1;
			}
			if (Level.balance < 0f)
			{
				Level.balance = 0f;
			}
			if (Level.balance > 10f)
			{
				Level.balance = 10f;
			}
		}
		else
		{
			this.stability = 0;
		}
		if (Input.GetButton("one button") || Input.GetMouseButton(0))
		{
			this.startControl = true;
			this.automaticDecreaseBalance = false;
		}
		else
		{
			this.automaticDecreaseBalance = true;
		}
		float length = 1f;
		if (Level.score > 1f && Level.score < 15f)
		{
			length = Level.score * 2f;
		}
		else if (Level.score > 1f)
		{
			length = 30f;
		}
		if (this.startControl && this.automaticDecreaseBalance)
		{
			Level.balance -= 0.03f * (Mathf.PingPong(Time.time, length) + 1f) * Time.deltaTime * 60f;
		}
		if (Input.GetButton("one button") || Input.GetMouseButton(0))
		{
			Level.balance += 0.09f * (Mathf.PingPong(Time.time, length) + 1f) * Time.deltaTime * 60f;
		}
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
		{
			SceneManager.LoadScene("intro");
		}
		if (SteamManager.Initialized)
		{
			if (Level.score >= 5f && Level.score < 6f)
			{
				SteamUserStats.SetAchievement("5m");
				SteamUserStats.StoreStats();
			}
			if (Level.score >= 10f && Level.score < 11f)
			{
				SteamUserStats.SetAchievement("10m");
				SteamUserStats.StoreStats();
			}
			if (Level.score >= 15f && Level.score < 16f)
			{
				SteamUserStats.SetAchievement("15m");
				SteamUserStats.StoreStats();
			}
			if (Level.score >= 20f && Level.score < 21f)
			{
				SteamUserStats.SetAchievement("unknown");
				SteamUserStats.StoreStats();
			}
		}
	}

	// Token: 0x06000017 RID: 23
	private void FixedUpdate()
	{
		this.baby.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
	}

	// Token: 0x0400000A RID: 10
	private float timer;

	// Token: 0x0400000B RID: 11
	private bool fadeIn;

	// Token: 0x0400000C RID: 12
	private bool fadeOut;

	// Token: 0x0400000D RID: 13
	private AudioSource music;

	// Token: 0x0400000E RID: 14
	private PuppetMaster puppet;

	// Token: 0x0400000F RID: 15
	private GameObject baby;

	// Token: 0x04000010 RID: 16
	private ParticleSystem pixels;

	// Token: 0x04000011 RID: 17
	private static Vector3 position;

	// Token: 0x04000012 RID: 18
	private static bool repeat;

	// Token: 0x04000013 RID: 19
	private float speed;

	// Token: 0x04000014 RID: 20
	[Range(0f, 10f)]
	public static float balance;

	// Token: 0x04000015 RID: 21
	[Range(0f, 5f)]
	public int stability = 2;

	// Token: 0x04000016 RID: 22
	private bool startControl;

	// Token: 0x04000017 RID: 23
	private bool automaticDecreaseBalance;

	// Token: 0x04000018 RID: 24
	private bool fall;

	// Token: 0x04000019 RID: 25
	public static float score;

	// Token: 0x0400001A RID: 26
	public static float record;

	// Token: 0x0400001B RID: 27
	private bool firstRecord;
}

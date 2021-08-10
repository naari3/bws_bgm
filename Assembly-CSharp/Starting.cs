using System;
using System.Collections;
using Colorful;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200000A RID: 10
public class Starting : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00002F00 File Offset: 0x00001100
	private void Awake()
	{
		Application.targetFrameRate = 60;
		Cursor.visible = false;
		this.pixels = GameObject.Find("camera_ui").GetComponent<Pixelate>();
		this.pixels.Scale = 0f;
		this.sound = base.GetComponent<AudioSource>();
		base.StartCoroutine("Pixelize");
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002F58 File Offset: 0x00001158
	private void Update()
	{
		if (!this.pixelizeCamera)
		{
			if (this.pixels.Scale < 250f)
			{
				this.pixels.Scale += 600f * Time.deltaTime;
			}
			else
			{
				this.pixels.Scale = 250f;
			}
		}
		if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.JoystickButton6))
		{
			this.pixelizeCamera = true;
		}
		if (this.pixelizeCamera)
		{
			this.sound.pitch -= 0.025f * Time.deltaTime * 60f;
			if (this.pixels.Scale > 0f)
			{
				this.pixels.Scale -= 600f * Time.deltaTime;
			}
			else
			{
				this.pixels.Scale = 0f;
				SceneManager.LoadScene("intro");
			}
		}
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
		{
			Application.Quit();
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003061 File Offset: 0x00001261
	private IEnumerator Pixelize()
	{
		yield return new WaitForSeconds(2f);
		this.pixelizeCamera = true;
		yield break;
	}

	// Token: 0x0400001F RID: 31
	private Pixelate pixels;

	// Token: 0x04000020 RID: 32
	private bool pixelizeCamera;

	// Token: 0x04000021 RID: 33
	private AudioSource sound;
}

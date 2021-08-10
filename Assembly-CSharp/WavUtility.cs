using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class WavUtility
{
	// Token: 0x06000346 RID: 838
	public static AudioClip ToAudioClip(string filePath)
	{
		if (!filePath.StartsWith(Application.persistentDataPath) && !filePath.StartsWith(Application.dataPath))
		{
			Debug.LogWarning("This only supports files which are in Application data path. https://docs.unity3d.com/ScriptReference/Resources.Load.html");
			return null;
		}
		return WavUtility.ToAudioClip(File.ReadAllBytes(filePath), "wav");
	}

	// Token: 0x0600035E RID: 862
	public static AudioClip ToAudioClip(byte[] fileBytes, string name = "wav")
	{
		List<float> samples = new List<float>();
		WaveFileReader wave = new WaveFileReader(new MemoryStream(fileBytes));
		for (float[] tmpSamples = wave.ReadNextSampleFrame(); tmpSamples != null; tmpSamples = wave.ReadNextSampleFrame())
		{
			samples.AddRange(tmpSamples);
		}
		float[] data = samples.ToArray();
		AudioClip audioClip = AudioClip.Create(name, data.Length, wave.WaveFormat.Channels, wave.WaveFormat.SampleRate, false);
		audioClip.SetData(data, 0);
		return audioClip;
	}
}

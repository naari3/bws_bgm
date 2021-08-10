using System;
using System.IO;
using System.Text;
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

	// Token: 0x06000349 RID: 841
	private static float[] Convert16BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
	{
		int wavSize = dataSize;
		if (headerOffset != 0)
		{
			wavSize = BitConverter.ToInt32(source, headerOffset);
			headerOffset += 4;
		}
		int x = 2;
		int convertedSize = wavSize / x;
		float[] data = new float[convertedSize];
		short maxValue = short.MaxValue;
		for (int i = 0; i < convertedSize; i++)
		{
			int offset = i * x + headerOffset;
			data[i] = (float)BitConverter.ToInt16(source, offset) / (float)maxValue;
		}
		return data;
	}

	// Token: 0x0600035E RID: 862
	public static AudioClip ToAudioClip(byte[] fileBytes, string name = "wav")
	{
		int headerOffset = 0;
		int sampleRate = 16000;
		ushort channels = 1;
		int subchunk2 = fileBytes.Length;
		bool includeWavFileHeader = true;
		byte[] fileHeaderChars = new byte[4];
		Array.Copy(fileBytes, 0, fileHeaderChars, 0, 4);
		if (!Encoding.ASCII.GetString(fileHeaderChars).Equals("RIFF"))
		{
			includeWavFileHeader = false;
		}
		if (includeWavFileHeader)
		{
			int subchunk3 = BitConverter.ToInt32(fileBytes, 16);
			BitConverter.ToUInt16(fileBytes, 20);
			channels = BitConverter.ToUInt16(fileBytes, 22);
			sampleRate = BitConverter.ToInt32(fileBytes, 24);
			BitConverter.ToUInt16(fileBytes, 34);
			byte[] fileMetaHeaderChars = new byte[4];
			Array.Copy(fileBytes, 36, fileMetaHeaderChars, 0, 4);
			if (Encoding.ASCII.GetString(fileMetaHeaderChars).Equals("LIST"))
			{
				int infoLength = BitConverter.ToInt32(fileBytes, 40);
				headerOffset = 20 + subchunk3 + 4 + 8 + infoLength;
			}
			else
			{
				headerOffset = 20 + subchunk3 + 4;
			}
			subchunk2 = BitConverter.ToInt32(fileBytes, headerOffset);
		}
		float[] data = WavUtility.Convert16BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
		AudioClip audioClip = AudioClip.Create(name, data.Length, (int)channels, sampleRate, false);
		audioClip.SetData(data, 0);
		return audioClip;
	}
}

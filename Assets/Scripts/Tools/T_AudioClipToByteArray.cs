using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class T_AudioClipToByteArray {
    const int HEADER_SIZE = 44;

    public static byte[] AudioClipToByteArray(AudioClip clip) {
        int BODY_SIZE = clip.samples * 2;
        byte[] bytes = new byte[HEADER_SIZE + BODY_SIZE];

        WriteHeader(bytes, clip);
        WriteBody(bytes, clip);
        return bytes;
    }

    static void WriteBody(byte[] bytes, AudioClip clip) {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        Int16[] intData = new Int16[samples.Length];
        int rescaleFactor = 32767; //to convert float to Int16    

        for (int i = 0; i < samples.Length; i++) {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytes, HEADER_SIZE + i * 2);
        }
    }

    static void WriteHeader(byte[] bytes, AudioClip clip) {
        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        int pos = 0;

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        riff.CopyTo(bytes, pos);
        pos += 4;

        
        Byte[] chunkSize = BitConverter.GetBytes(bytes.Length - 8);
        chunkSize.CopyTo(bytes, pos);
        pos += 4;

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        wave.CopyTo(bytes, pos);
        pos += 4;

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fmt.CopyTo(bytes, pos);
        pos += 4;

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        subChunk1.CopyTo(bytes, pos);
        pos += 4;

        //UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        audioFormat.CopyTo(bytes, pos);
        pos += 2;

        Byte[] numChannels = BitConverter.GetBytes(channels);
        numChannels.CopyTo(bytes, pos);
        pos += 2;

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        sampleRate.CopyTo(bytes, pos);
        pos += 4;

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2  
        byteRate.CopyTo(bytes, pos);
        pos += 4;

        UInt16 blockAlign = (ushort)(channels * 2);
        BitConverter.GetBytes(blockAlign).CopyTo(bytes, pos);
        pos += 2;

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        bitsPerSample.CopyTo(bytes, pos);
        pos += 2;

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        datastring.CopyTo(bytes, pos);
        pos += 4;

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        subChunk2.CopyTo(bytes, pos);
        pos += 4;

    }

}

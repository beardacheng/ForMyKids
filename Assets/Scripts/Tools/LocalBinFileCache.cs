using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class LocalBinFileCache {
    string _saveFilePath;

    public LocalBinFileCache(string key) {

        string tempFilePath;
#if UNITY_EDITOR
        tempFilePath = Application.persistentDataPath + "/temp";
#else
		tempFilePath = Application.persistentDataPath;
#endif
        _saveFilePath = tempFilePath + "/" + key;

        if (!Directory.Exists(_saveFilePath)) System.IO.Directory.CreateDirectory(_saveFilePath);
        Debug.Log("cache directory: " + tempFilePath);
    }

    public void Save(string name, byte[] data, bool force) {
        string filePath = _FilePath(name);

        FileInfo t = new FileInfo(filePath);
        if (!t.Exists || force) {
            File.WriteAllBytes(filePath, data);
        }
    }

    public byte[] Get(string name) {
        string filePath = _FilePath(name);

        FileInfo t = new FileInfo(filePath);
        if (t.Exists) {
            return File.ReadAllBytes(filePath);
        }
        else {
            return null;
        }
    }

    public string GetFilePath(string name) {
        var filePath = _FilePath(name);
        FileInfo t = new FileInfo(filePath);
        if (t.Exists) return filePath;
        else return "";
    }

    private string _FilePath(string name) {
        return _saveFilePath + "/" + T_Base64.EncodeBase64String(name);
    }

}

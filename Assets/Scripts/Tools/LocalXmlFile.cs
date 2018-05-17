using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;

public class LocalXmlFile<DataType> {
    private string _path;
    private string _filename;

    public LocalXmlFile(string path, string filename) {
        _path = path;
        _filename = filename;
    }

    public DataType GetData() {
        FileInfo t = new FileInfo(_path + "//" + _filename);
        if (t.Exists) {
            return _LoadXML();
        }
        else {
            return default(DataType);
        }
    }

    public void SaveData(DataType data) {
        _CreateXML(data);
    }

    void _CreateXML(DataType data) {
        StreamWriter writer;
        FileInfo t = new FileInfo(_path + "//" + _filename);

        if (!t.Exists) {
            writer = t.CreateText();
        }
        else {
            t.Delete();
            writer = t.CreateText();
        }
        writer.Write(SerializeObject(data));
        writer.Close();
        Debug.Log("File written.");
    }

    DataType _LoadXML() {
        StreamReader r = File.OpenText(_path + "//" + _filename);
        string info = r.ReadToEnd();
        r.Close();
        return DeserializeObject(info);
    }

    /* The following metods came from the referenced URL */
    string UTF8ByteArrayToString(byte[] characters) {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    byte[] StringToUTF8ByteArray(string pXmlString) {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    // Here we serialize our UserData object of myData   
    string SerializeObject(DataType pObject) {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(DataType));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    // Here we deserialize it back into its original form   
    DataType DeserializeObject(string pXmlizedString) {
        XmlSerializer xs = new XmlSerializer(typeof(DataType));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return (DataType)(xs.Deserialize(memoryStream));
    }
}




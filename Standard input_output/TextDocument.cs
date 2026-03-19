using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TextDocument
{
  public string FilePath;
  public string Content;
  public DateTime lastModified;

  public string FileName
  {
    get
    {
      string fileName;
      fileName = Path.GetFileName(FilePath);
      return fileName;
    }
  }

  public string content { get; internal set; }

  public TextDocument() { }

  public TextDocument(string path)
  {
    FilePath = path;
    LoadFromFile();
  }

  public void LoadFromFile()
  {
    if (File.Exists(FilePath))
    {
      Content = File.ReadAllText(FilePath);
      lastModified = File.GetLastWriteTime(FilePath);
    }
  }

  public void SaveToFile()
  {
    File.WriteAllText(FilePath, Content);
    lastModified = DateTime.Now;
  }

  public void BinarySerialize(string path)
  {
    BinaryFormatter binaryFormatter;
    binaryFormatter = new BinaryFormatter();

    using (FileStream fileStream = new FileStream(path, FileMode.Create))
    {
      binaryFormatter.Serialize(fileStream, this);
    }
  }

  public static TextDocument BinaryDeserialize(string path)
  {
    BinaryFormatter binaryFormatter;
    binaryFormatter = new BinaryFormatter();

    using (FileStream fileStream = new FileStream(path, FileMode.Open))
    {
      TextDocument deserializedDocument;
      deserializedDocument = (TextDocument)binaryFormatter.Deserialize(fileStream);
      return deserializedDocument;
    }
  }

  public void XmlSerialize(string path)
  {
    XmlSerializer xmlSerializer;
    xmlSerializer = new XmlSerializer(typeof(TextDocument));

    using (StreamWriter streamWriter = new StreamWriter(path))
    {
      xmlSerializer.Serialize(streamWriter, this);
    }
  }

  public static TextDocument XmlDeserialize(string path)
  {
    XmlSerializer xmlSerializer;
    xmlSerializer = new XmlSerializer(typeof(TextDocument));

    using (StreamReader streamReader = new StreamReader(path))
    {
      TextDocument deserializedDocument;
      deserializedDocument = (TextDocument)xmlSerializer.Deserialize(streamReader);
      return deserializedDocument;
    }
  }
}
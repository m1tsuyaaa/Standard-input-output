using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TextDocument
{
  public string filePath;
  public string content;
  public DateTime lastModified;

  public string FileName => Path.GetFileName(filePath);

  public TextDocument() { }

  public TextDocument(string path)
  {
    filePath = path;
    LoadFromFile();
  }

  public void LoadFromFile()
  {
    if (File.Exists(filePath))
    {
      content = File.ReadAllText(filePath);
      lastModified = File.GetLastWriteTime(filePath);
    }
  }

  public void SaveToFile()
  {
    File.WriteAllText(filePath, content);
    lastModified = DateTime.Now;
  }

  public void BinarySerialize(string path)
  {
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    using (FileStream fileStream = new FileStream(path, FileMode.Create))
    {
      binaryFormatter.Serialize(fileStream, this);
    }
  }

  public static TextDocument BinaryDeserialize(string path)
  {
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    using (FileStream fileStream = new FileStream(path, FileMode.Open))
    {
      return (TextDocument)binaryFormatter.Deserialize(fileStream);
    }
  }

  public void XmlSerialize(string path)
  {
    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TextDocument));
    using (StreamWriter streamWriter = new StreamWriter(path))
    {
      xmlSerializer.Serialize(streamWriter, this);
    }
  }

  public static TextDocument XmlDeserialize(string path)
  {
    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TextDocument));
    using (StreamReader streamReader = new StreamReader(path))
    {
      return (TextDocument)xmlSerializer.Deserialize(streamReader);
    }
  }
}
using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TextDocument
{
  public string FilePath;
  public string Content;
  public DateTime LastModified;

  public string FileName;
  public BinaryFormatter BinaryFormatter;
  public FileStream FileStream;
  public TextDocument DeserializedDocument;
  public XmlSerializer XmlSerializer;
  public StreamWriter StreamWriter;
  public StreamReader StreamReader;

  public string FileName
  {
    get
    {
      FileName = Path.GetFileName(FilePath);
      return FileName;
    }
  }

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
      LastModified = File.GetLastWriteTime(FilePath);
    }
  }

  public void SaveToFile()
  {
    File.WriteAllText(FilePath, Content);
    LastModified = DateTime.Now;
  }

  public void BinarySerialize(string path)
  {
    BinaryFormatter = new BinaryFormatter();
    FileStream = new FileStream(path, FileMode.Create);

    using (FileStream)
    {
      BinaryFormatter.Serialize(FileStream, this);
    }
  }

  public static TextDocument BinaryDeserialize(string path)
  {
    TextDocument document;
    BinaryFormatter formatter;
    FileStream stream;

    formatter = new BinaryFormatter();
    stream = new FileStream(path, FileMode.Open);

    using (stream)
    {
      document = (TextDocument)formatter.Deserialize(stream);
      return document;
    }
  }

  public void XmlSerialize(string path)
  {
    XmlSerializer = new XmlSerializer(typeof(TextDocument));
    StreamWriter = new StreamWriter(path);

    using (StreamWriter)
    {
      XmlSerializer.Serialize(StreamWriter, this);
    }
  }

  public static TextDocument XmlDeserialize(string path)
  {
    TextDocument document;
    XmlSerializer serializer;
    StreamReader reader;

    serializer = new XmlSerializer(typeof(TextDocument));
    reader = new StreamReader(path);

    using (reader)
    {
      document = (TextDocument)serializer.Deserialize(reader);
      return document;
    }
  }
}
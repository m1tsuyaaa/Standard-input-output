using System;
using System.Collections.Generic;
using System.IO;

public class DocumentSearcher
{
  public string RootDirectory;
  public List<TextDocument> Documents;

  public string[] TextFiles;
  public int Index;
  public TextDocument Document;

  public List<TextDocument> SearchResults;
  public StringComparison ComparisonType;
  public int KeywordNotFound;
  public int DocumentIndex;
  public int KeywordIndex;
  public int KeywordPosition;
  public TextDocument CurrentDocument;
  public string CurrentKeyword;
  public bool DocumentMatches;

  public DocumentSearcher(string directory)
  {
    RootDirectory = directory;
    Documents = new List<TextDocument>();
  }

  public void IndexAll()
  {
    TextFiles = Directory.GetFiles(RootDirectory, "*.txt", SearchOption.AllDirectories);

    for (Index = 0; Index < TextFiles.Length; ++Index)
    {
      try
      {
        Document = new TextDocument(TextFiles[Index]);
        Documents.Add(Document);
      }
      catch
      {
      }
    }
  }

  public List<TextDocument> Search(List<string> keywords, bool caseSensitive = false)
  {
    SearchResults = new List<TextDocument>();

    if (caseSensitive)
    {
      ComparisonType = StringComparison.Ordinal;
    }
    else
    {
      ComparisonType = StringComparison.OrdinalIgnoreCase;
    }

    KeywordNotFound = -1;

    for (DocumentIndex = 0; DocumentIndex < Documents.Count; ++DocumentIndex)
    {
      CurrentDocument = Documents[DocumentIndex];
      DocumentMatches = true;

      for (KeywordIndex = 0; KeywordIndex < keywords.Count; ++KeywordIndex)
      {
        CurrentKeyword = keywords[KeywordIndex];
        KeywordPosition = CurrentDocument.Content.IndexOf(CurrentKeyword, ComparisonType);

        if (KeywordPosition == KeywordNotFound)
        {
          DocumentMatches = false;
          break;
        }
      }

      if (DocumentMatches)
      {
        SearchResults.Add(CurrentDocument);
      }
    }

    return SearchResults;
  }
}
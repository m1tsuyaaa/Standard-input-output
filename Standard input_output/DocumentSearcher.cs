using System;
using System.Collections.Generic;
using System.IO;

public class DocumentSearcher
{
  public string RootDirectory;
  public List<TextDocument> Documents;

  public DocumentSearcher(string directory)
  {
    RootDirectory = directory;
    Documents = new List<TextDocument>();
  }

  public void IndexAll()
  {
    string[] textFiles;
    textFiles = Directory.GetFiles(RootDirectory, "*.txt", SearchOption.AllDirectories);

    int index;
    TextDocument document;

    for (index = 0; index < textFiles.Length; ++index)
    {
      try
      {
        document = new TextDocument(textFiles[index]);
        Documents.Add(document);
      }
      catch
      {
      }
    }
  }

  public List<TextDocument> Search(List<string> keywords, bool caseSensitive = false)
  {
    List<TextDocument> searchResults;
    searchResults = new List<TextDocument>();

    StringComparison comparisonType;

    if (caseSensitive)
    {
      comparisonType = StringComparison.Ordinal;
    }
    else
    {
      comparisonType = StringComparison.OrdinalIgnoreCase;
    }

    int keywordNotFound;
    keywordNotFound = -1;

    int documentIndex;
    int keywordIndex;
    int keywordPosition;
    TextDocument currentDocument;
    string currentKeyword;
    bool documentMatches;

    for (documentIndex = 0; documentIndex < Documents.Count; ++documentIndex)
    {
      currentDocument = Documents[documentIndex];
      documentMatches = true;

      for (keywordIndex = 0; keywordIndex < keywords.Count; ++keywordIndex)
      {
        currentKeyword = keywords[keywordIndex];
        keywordPosition = currentDocument.Content.IndexOf(currentKeyword, comparisonType);

        if (keywordPosition == keywordNotFound)
        {
          documentMatches = false;
          break;
        }
      }

      if (documentMatches)
      {
        searchResults.Add(currentDocument);
      }
    }

    return searchResults;
  }
}
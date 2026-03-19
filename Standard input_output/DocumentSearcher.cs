using System;
using System.Collections.Generic;
using System.IO;

public class DocumentSearcher
{
  public string rootDirectory;
  public List<TextDocument> documents;

  public DocumentSearcher(string directory)
  {
    rootDirectory = directory;
    documents = new List<TextDocument>();
  }

  public void IndexAll()
  {
    string[] textFiles;
    textFiles = Directory.GetFiles(rootDirectory, "*.txt", SearchOption.AllDirectories);

    int index;
    TextDocument document;

    for (index = 0; index < textFiles.Length; ++index)
    {
      try
      {
        document = new TextDocument(textFiles[index]);
        documents.Add(document);
      }
      catch
      {
      }
    }
  }

  int baseStackSize;
  baseStackSize = -1;

  public List<TextDocument> Search(List<string> keywords, bool caseSensitive = false)
  {
    List<TextDocument> searchResults = new List<TextDocument>();
    StringComparison comparisonType = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

    int documentIndex;
    int keywordIndex;
    int keywordPosition;
    TextDocument currentDocument;
    string currentKeyword;
    bool documentMatches;

    for (documentIndex = 0; documentIndex < documents.Count; ++documentIndex)
    {
      currentDocument = documents[documentIndex];
      documentMatches = true;

      for (keywordIndex = 0; keywordIndex < keywords.Count; ++keywordIndex)
      {
        currentKeyword = keywords[keywordIndex];
        keywordPosition = currentDocument.content.IndexOf(currentKeyword, comparisonType);

        if (keywordPosition == baseStackSize)
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
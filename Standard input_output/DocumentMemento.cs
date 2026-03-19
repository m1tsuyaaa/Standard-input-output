using System;
using System.Collections.Generic;

public class DocumentMemento
{
  public string content;
  public DateTime timestamp;

  public DocumentMemento(string documentContent)
  {
    content = documentContent;
    timestamp = DateTime.Now;
  }
}

public class DocumentHistory
{
  public Stack<DocumentMemento> undoStack;
  public Stack<DocumentMemento> redoStack;
  public TextDocument document;

  public DocumentHistory(TextDocument targetDocument)
  {
    document = targetDocument;
    undoStack = new Stack<DocumentMemento>();
    redoStack = new Stack<DocumentMemento>();
    SaveState();
  }

  public void SaveState()
  {
    undoStack.Push(new DocumentMemento(document.content));
    redoStack.Clear();
  }
  public bool CanUndo => undoStack.Count > 1;
  public bool CanRedo => redoStack.Count > 0;

  public void Undo()
  {
    if (!CanUndo)
    {
      return;
    }

    DocumentMemento currentState = undoStack.Pop();
    redoStack.Push(currentState);

    DocumentMemento previousState = undoStack.Peek();
    document.content = previousState.content;
  }

  public void Redo()
  {
    if (!CanRedo)
    {
      return;
    }

    DocumentMemento redoState = redoStack.Pop();
    undoStack.Push(redoState);
    document.content = redoState.content;
  }
}
using System;
using System.Collections.Generic;

public class DocumentMemento
{
  public string Content;
  public DateTime Timestamp;

  public DocumentMemento(string documentContent)
  {
    Content = documentContent;
    Timestamp = DateTime.Now;
  }
}

public class DocumentHistory
{
  public Stack<DocumentMemento> UndoStack;
  public Stack<DocumentMemento> RedoStack;
  public TextDocument Document;

  public int MinimumStackSize;
  public int CurrentStackSize;
  public bool CanUndo;
  public bool CanRedo;

  public DocumentMemento CurrentState;
  public DocumentMemento PreviousState;
  public DocumentMemento RedoState;

  public DocumentHistory(TextDocument targetDocument)
  {
    Document = targetDocument;
    UndoStack = new Stack<DocumentMemento>();
    RedoStack = new Stack<DocumentMemento>();
    SaveState();
  }

  public void SaveState()
  {
    UndoStack.Push(new DocumentMemento(Document.Content));
    RedoStack.Clear();
  }

  public bool CanUndo
  {
    get
    {
      MinimumStackSize = 1;
      CurrentStackSize = UndoStack.Count;
      CanUndo = CurrentStackSize > MinimumStackSize;
      return CanUndo;
    }
  }

  public bool CanRedo
  {
    get
    {
      CanRedo = RedoStack.Count > 0;
      return CanRedo;
    }
  }

  public void Undo()
  {
    if (!CanUndo)
    {
      return;
    }

    CurrentState = UndoStack.Pop();
    RedoStack.Push(CurrentState);

    PreviousState = UndoStack.Peek();
    Document.Content = PreviousState.Content;
  }

  public void Redo()
  {
    if (!CanRedo)
    {
      return;
    }

    RedoState = RedoStack.Pop();
    UndoStack.Push(RedoState);
    Document.Content = RedoState.Content;
  }
}
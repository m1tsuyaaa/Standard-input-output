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
      bool canUndo;

      int minimumStackSize;
      minimumStackSize = 1;

      int currentStackSize;
      currentStackSize = UndoStack.Count;

      canUndo = currentStackSize > minimumStackSize;

      return canUndo;
    }
  }

  public bool CanRedo
  {
    get
    {
      bool canRedo;
      canRedo = RedoStack.Count > 0;
      return canRedo;
    }
  }

  public void Undo()
  {
    if (!CanUndo)
    {
      return;
    }

    DocumentMemento currentState;
    currentState = UndoStack.Pop();

    RedoStack.Push(currentState);

    DocumentMemento previousState;
    previousState = UndoStack.Peek();

    Document.Content = previousState.Content;
  }

  public void Redo()
  {
    if (!CanRedo)
    {
      return;
    }

    DocumentMemento redoState;
    redoState = RedoStack.Pop();
    UndoStack.Push(redoState);
    Document.Content = redoState.Content;
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateController : BaseButtonController
{
  // 定数
  private const float TRANSITION_TIME = 0.2f;

// 後でscriptableobjectにしよう
  private static class StateName
  {
    public const string ROOT = "Root";
    public const string FOLDER = "Folder";
    public const string DESK = "Desk";
    public const string MAILBOX = "MailBox";
  }

  private static class RootState
  {
    public const string PANEL = "RootPanel";
    public static Vector3 POSITION {get {return Vector3.zero;}}
    public static Quaternion POSTURE {get {return Quaternion.Euler(Vector3.zero);}}
  }

  private static class FolderState
  {
    public const string PANEL = "FolderPanel";
    public static Vector3 POSITION {get {return new Vector3(10f, 0f, 0f);}}
    public static Quaternion POSTURE {get {return Quaternion.AngleAxis(90, Vector3.up);}}
  }

  private static class DeskState
  {
    public const string PANEL = "DeskPanel";
    public static Vector3 POSITION {get {return new Vector3(0f, 10f, 0f);}}
    public static Quaternion POSTURE {get {return Quaternion.AngleAxis(-90, Vector3.up);}}
  }

  private static class MailBoxState
  {
    public const string PANEL = "MailBoxPanel";
    public static Vector3 POSITION {get {return new Vector3(0f, 0f, 10f);}}
    public static Quaternion POSTURE {get {return Quaternion.AngleAxis(180, Vector3.up);}}
  }

  private class State
  {
    public string panel;
    public Vector3 position;
    public Quaternion posture;

    // コンストラクタ
    public State(string panel, Vector3 position, Quaternion posture)
    {
      this.panel = panel;
      this.position = position;
      this.posture = posture;
    }

    public static State Null()
    {
      return new State("", new Vector3(999, 999, 999), Quaternion.Euler(Vector3.zero));
    }
  }

  // 変数
  private Stack<string> stateStack = new Stack<string>();

  public void Start()
  {
    stateStack.Push(StateName.ROOT);
  }

  protected override void OnClick(string objectName)
  {
    if (stateStack.Peek() == null) Debug.LogError("ステートスタックが適切に設定されていないよ！");
    switch (objectName)
    {
      case StateName.FOLDER:
        this.FolderButtonClick();
        break;
      case StateName.DESK:
        this.DeskButtonClick();
        break;
      case StateName.MAILBOX:
        this.MailBoxButtonClick();
        break;
      // 例外処理
      case "Out":
        this.OutButtonClick();
        break;
      case "Back":
        this.BackButtonClick();
        break;
      default:
        Debug.LogError("対応するボタンがないよ！");
        break;
    }
  }

  private void FolderButtonClick()
  {
    ChangeState(stateStack.Peek(), StateName.FOLDER);
  }

  private void DeskButtonClick()
  {
    ChangeState(stateStack.Peek(), StateName.DESK);
  }
  private void MailBoxButtonClick()
  {
    ChangeState(stateStack.Peek(), StateName.MAILBOX);
  }

  private void OutButtonClick()
  {
    stateStack = new Stack<string>();
    stateStack.Push(StateName.ROOT);
    SceneManager.LoadScene("Beach");
  }

  private void BackButtonClick()
  {
    string now = stateStack.Pop();
    if (stateStack.Peek() == null) Debug.LogError("backできないよ！");
    ChangeState(now, stateStack.Peek());
  }

  private void ChangeState(string start, string target)
  {
    stateStack.Push(target);
    State startState = GetState(start);
    State targetState = GetState(target);
    Camera camera = Camera.main;
    float t = Time.time;
    while (true)
    {
      float diff = (Time.time - t) / TRANSITION_TIME;
      camera.transform.position = Vector3.Lerp(startState.position, targetState.position, diff);
      camera.transform.rotation = Quaternion.Lerp(startState.posture, targetState.posture, diff);
      if (diff > 1f) break;
    }
  }

  private State GetState(string state)
  {
    switch (state)
    {
      case StateName.ROOT:
        return new State(RootState.PANEL, RootState.POSITION, RootState.POSTURE);
      case StateName.DESK:
        return new State(DeskState.PANEL, DeskState.POSITION, DeskState.POSTURE);
      case StateName.FOLDER:
        return new State(FolderState.PANEL, FolderState.POSITION, FolderState.POSTURE);
      case StateName.MAILBOX:
        return new State(MailBoxState.PANEL, MailBoxState.POSITION, MailBoxState.POSTURE);
      default:
        Debug.LogError("ステートが存在しないよ");
        return State.Null();
    }
  }
}

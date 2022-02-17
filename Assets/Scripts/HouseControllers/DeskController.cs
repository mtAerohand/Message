using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : BaseButtonController
{
    private static List<string> deskRoot = new List<string>(){"Create", "Back", "ShowTmpSaveLetters"};
    private static List<string> deskEdit = new List<string>(){"TmpSave", "Trash"};
    // Start is called before the first frame update
    void Start()
    {
      GameObject button;
      foreach(string name in deskRoot)
      {
        button = GameObject.Find(name);
        if (button != null)
        {
          button.SetActive(true);
        }
        else
        {
          Debug.LogError("DeskController/Start(): ボタンが見つからないよ！" + name);
        }
      }
      foreach(string name in deskEdit)
      {
        button = GameObject.Find(name);
        if (button != null)
        {
          button.SetActive(false);
        }
        else
        {
          Debug.LogError("DeskController/Start(): ボタンが見つからないよ！:" + name);
        }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ボタン関係
    protected override void OnClick(string objectName)
    {
      switch (objectName)
      {
        case "Create":
          CreateButtonClick();
          break;
        case "TmpSave":
          TmpSaveButtonClick();
          break;
        case "Trash":
         TrashButtonClick();
         break;
        case "ShowTmpSaveLetters":
          ShowTmpSaveLettersButtonClick();
          break;
        default:
          Debug.LogError("対応するボタンがないよ！");
          break;
      }
    }

    private void CreateButtonClick()
    {}

    private void TmpSaveButtonClick()
    {}

    private void TrashButtonClick()
    {}

    private void ShowTmpSaveLettersButtonClick()
    {}
}

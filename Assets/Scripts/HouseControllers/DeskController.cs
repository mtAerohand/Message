using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.HouseState;

public class DeskController : BaseButtonController
{
    private static List<string> deskRoot = new List<string>(){"Create", "Back", "ShowTmpSaveLetters"};
    private static List<string> deskEdit = new List<string>(){"TmpSave", "Trash"};
    private GameObject letter;


    // public
    public GameObject letterPrefab;
    // Start is called before the first frame update
    void Start()
    {
      ChangeState(0);
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
    {
      ChangeState(1);
      letter = Instantiate(letterPrefab, new Vector3(160, 55, 0), Quaternion.identity);
      letter.transform.SetParent(transform);
      if (letter == null) Debug.LogError("DeskController/CreateButtonClick():レターインスタンスが生成できていないよ!");
    }

    private void TmpSaveButtonClick()
    {}

    private void TrashButtonClick()
    {
      if (letter != null)
      {
        Destroy(letter);
      }
      else
      {
        Debug.LogError("DeskController/TrashButtonClick(): レターインスタンスが存在していないよ");
      }
      ChangeState(0);

    }

    private void ShowTmpSaveLettersButtonClick()
    {}

    private void ChangeState(int state)
    {
      // 0:root, 1:edit, 2:両方non_active
      if (state < 0 || state > 2) Debug.LogError("DeskController/ChangeState(): stateの値が適切に設定されてないよ");
      bool root=false;
      bool edit=false;
      switch (state)
      {
        case 0:
          root = true;
          edit = false;
          break;
        case 1:
          root = false;
          edit = true;
          break;
        case 2:
          root = false;
          edit = false;
          break;
        default:
          Debug.LogError("DeskController/ChangeState(): stateの値が適切に設定されてないよ");
          break;
      }

      GameObject button;
      foreach(string name in deskRoot)
      {
        button = transform.Find(name).gameObject;
        if (button != null)
        {
          button.SetActive(root);
        }
        else
        {
          Debug.LogError("DeskController/ChangeState(): ボタンが見つからないよ" + name);
        }
      }
      foreach(string name in deskEdit)
      {
        button = transform.Find(name).gameObject;
        if (button != null)
        {
          button.SetActive(edit);
        }
        else
        {
          Debug.LogError("DeskController/ChangeState(): ボタンが見つからないよ！:" + name);
        }
      }

    }
}

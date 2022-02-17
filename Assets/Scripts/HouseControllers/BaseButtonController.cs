using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public BaseButtonController button;

    public void OnClick()
    {
      if (button == null) throw new System.Exception("ボタンインスタンスが存在していない");
      button.OnClick(this.gameObject.name);
    }

    protected virtual void OnClick(string objectName)
    {
        Debug.Log("Base Button");
    }
}

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaunchManager : MonoBehaviour
{
    /** UI部品 */
    private const string REGIST_USER_NAME_TEXT = "RegistUserNameText";
    private const string START_USER_NAME_TEXT = "StartUserNameText";
    private const string REGIST_MSG_TEXT = "RegistMsg";
    private const string START_CANVAS = "StartCanvas";
    private const string REGIST_CANVAS = "RegistCanvas";
    private InputField registUserNameText;
    private Text startUserNameText;
    private Text registMsgText;
    private GameObject StartCanvas;
    private GameObject RegistCanvas;

    /** DBモデル */
    private UserProfileModel userProfileModel;

    private void Awake()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath)) {
            File.Create(DBPath);
        }
        // テーブル作成処理
        UserProfile.CreateTable();
    }

    void Start()
    {
        StartCanvas = GameObject.Find(START_CANVAS);
        RegistCanvas = GameObject.Find(REGIST_CANVAS);
        registUserNameText = GameObject.Find(REGIST_USER_NAME_TEXT).GetComponent<InputField>();
        startUserNameText = GameObject.Find(START_USER_NAME_TEXT).GetComponent<Text>();
        registMsgText = GameObject.Find(REGIST_MSG_TEXT).GetComponent<Text>();

        // UserProfileの取得
        userProfileModel = UserProfile.Get();
        if (!string.IsNullOrEmpty(userProfileModel.user_id))
        {
            // ユーザ登録済：StartCanvas表示
            StartCanvas.SetActive(true);
            RegistCanvas.SetActive(false);
            startUserNameText.text = "User：" + userProfileModel.user_name;
        }
        else
        {
            // ユーザ未登録：RegistCanvas表示
            StartCanvas.SetActive(false);
            RegistCanvas.SetActive(true);
        }
    }

    // --------------- ボタン押下時処理 ---------------
    // 登録ボタン押下
    public void PushRegistButton()
    {
        if (string.IsNullOrEmpty(registUserNameText.text))
        {
            // ユーザ名未入力の場合
            registMsgText.text = "ちゃんと入力しましょうね";

        }
        else if (registUserNameText.text.Length > 10)
        {
            registMsgText.text = "10文字以内で入力してね";
        }
        else
        {
            // ユーザ登録処理
            Action action = () => {
                StartCanvas.SetActive(true);
                RegistCanvas.SetActive(false);
                startUserNameText.text = "User：" + registUserNameText.text;
            };
            StartCoroutine(CommunicationManager.ConnectServer("registration", "?user_name=" + registUserNameText.text, action));
        }
    }

    // スタートボタン押下
    public void PushStartButton()
    {
        SceneManager.LoadScene("House");
    }
}

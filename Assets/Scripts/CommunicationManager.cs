using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

[Serializable]
public class ResponseObjects
{
	public UserProfileModel user_profile;
}

public class CommunicationManager : MonoBehaviour
{

	public static IEnumerator ConnectServer(string endpoint, string paramater, Action action = null)
  {
		// *** リクエストの送付 ***
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(GameUtil.Const.SERVER_URL + endpoint + paramater);
		yield return unityWebRequest.SendWebRequest();
		// エラーの場合
		if (!string.IsNullOrEmpty(unityWebRequest.error))
		{
			Debug.LogError(unityWebRequest.error);
			yield break;
		}

		// *** レスポンスの取得 ***
		string text = unityWebRequest.downloadHandler.text;
		Debug.Log("レスポンス : " + text);
		// エラーの場合
		if (text.All(char.IsNumber))
		{
			switch (text)
			{
				case GameUtil.Const.ERROR_DB_UPDATE:
					Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
					break;
				default:
					Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
					break;
			}
			yield break;
		}

		// *** SQLiteへの保存処理 ***
		ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
		if (!string.IsNullOrEmpty(responseObjects.user_profile.user_id))
			UserProfile.Set(responseObjects.user_profile);
		// 正常終了アクション実行
		if (action != null)
		{
			action();
			action = null;
		}
	}
}

using System;
using SQLiteUnity;

[Serializable]
public class LetterModel
{
	public string user_id;
	public string address;
	public string text;
	public int in_bottle;
	public float create_at;
}

public static class Letter
{
	public static void CreateTable()
    {
		string query = "create table if not exists letter (user_id text, address text, text text, in_bottle int, create_at none, primary key(user_id))";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		sqlDB.ExecuteQuery(query);
	}

	public static void Set(LetterModel letter)
  {
		string query = "insert or replace into letter (user_id, address, text, in_bottle, create_at) values (\"" + letter.user_id + "\", \"" + letter.address + "\", " + letter.text + ", " + letter.in_bottle + ", " + letter.create_at + ")";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		sqlDB.ExecuteNonQuery(query);
	}

	public static LetterModel Get()
  {
		string query = "select * from letter";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		SQLiteTable dataTable = sqlDB.ExecuteQuery(query);
		LetterModel letterModel = new LetterModel();
		foreach (SQLiteRow dr in dataTable.Rows)
    {
  		letterModel.user_id = dr["user_id"].ToString();
  		letterModel.address = dr["address"].ToString();
  		letterModel.text = dr["text"].ToString();
  		letterModel.in_bottle = int.Parse(dr["in_bottle"].ToString());
  		letterModel.create_at = float.Parse(dr["create_at"].ToString());
		}
		return letterModel;
    }
}

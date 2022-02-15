using System;
using SQLiteUnity;

[Serializable]
public class UserProfileModel
{
	public string user_id;
	public string user_name;
	public int saved_letter;
	public int bottle;
	public int writing_paper;
}

public static class UserProfile
{
	public static void CreateTable()
    {
		string query = "create table if not exists user_profile (user_id text, user_name text, saved_letter int, bottle int, writing_paper int, primary key(user_id))";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		sqlDB.ExecuteQuery(query);
	}

	public static void Set(UserProfileModel user_profile)
  {
		string query = "insert or replace into user_profile (user_id, user_name, saved_letter, bottle, writing_paper) values (\"" + user_profile.user_id + "\", \"" + user_profile.user_name + "\", " + user_profile.saved_letter + ", " + user_profile.bottle + ", " + user_profile.writing_paper + ")";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		sqlDB.ExecuteNonQuery(query);
	}

	public static UserProfileModel Get()
  {
		string query = "select * from user_profile";
		SQLite sqlDB = new SQLite(GameUtil.Const.SQLITE_FILE_NAME);
		SQLiteTable dataTable = sqlDB.ExecuteQuery(query);
		UserProfileModel userProfileModel = new UserProfileModel();
		foreach (SQLiteRow dr in dataTable.Rows)
    {
  		userProfileModel.user_id = dr["user_id"].ToString();
  		userProfileModel.user_name = dr["user_name"].ToString();
  		userProfileModel.saved_letter = int.Parse(dr["saved_letter"].ToString());
  		userProfileModel.bottle = int.Parse(dr["bottle"].ToString());
  		userProfileModel.writing_paper = int.Parse(dr["writing_paper"].ToString());
		}
		return userProfileModel;
    }
}

using System;
using UnityEngine;

namespace GameUtil
{
    public static class Const
    {
      public const string SERVER_URL = "http://ec2-3-94-80-229.compute-1.amazonaws.com/";
      public const string SQLITE_FILE_NAME = "Message";

      // エラーID
      public const string ERROR_DB_UPDATE = "1";
    }

    namespace HouseState
    {
      public static class StateName
      {
        public const string ROOT = "Root";
        public const string FOLDER = "Folder";
        public const string DESK = "Desk";
        public const string MAILBOX = "MailBox";
      }

      public static class RootState
      {
        public const string PANEL = "RootPanel";
        public static Vector3 POSITION {get {return Vector3.zero;}}
        public static Quaternion POSTURE {get {return Quaternion.Euler(Vector3.zero);}}
      }

      public static class FolderState
      {
        public const string PANEL = "FolderPanel";
        public static Vector3 POSITION {get {return new Vector3(10f, 0f, 0f);}}
        public static Quaternion POSTURE {get {return Quaternion.AngleAxis(90, Vector3.up);}}
      }

      public static class DeskState
      {
        public const string PANEL = "DeskPanel";
        public static Vector3 POSITION {get {return new Vector3(0f, 10f, 0f);}}
        public static Quaternion POSTURE {get {return Quaternion.AngleAxis(-90, Vector3.up);}}
      }

      public static class MailBoxState
      {
        public const string PANEL = "MailBoxPanel";
        public static Vector3 POSITION {get {return new Vector3(0f, 0f, 10f);}}
        public static Quaternion POSTURE {get {return Quaternion.AngleAxis(180, Vector3.up);}}
      }
    }
}

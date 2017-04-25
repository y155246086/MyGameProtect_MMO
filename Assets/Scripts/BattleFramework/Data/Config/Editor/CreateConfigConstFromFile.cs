using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BattleFramework.Data
{
	public class CreateConfigConstFromFile : EditorWindow {

		[MenuItem("Data/SysConfig/CreateClass")]
		static void AddWindow ()
		{
			EditorWindow.GetWindow (typeof(CreateConfigConstFromFile));
		}

        const string configDirection = @"Assets/Resources/Configs";
        const string classFilePath = @"Assets/Scripts/BattleFramework/Data/Config";
		const string className = "SysConfigConst";

		int top = 20;
		int height = 30;
		int index = 0;
		int width = 200;
		void OnGUI ()
		{
			if (GUI.Button (new Rect (60, top + (height + 5) * index, width, height), "Create")) {
				Create();
			}
		}

		void Create()
		{
			string fullClassName = classFilePath + "/" + className + ".cs";

			if (!Directory.Exists (classFilePath)) {
				Directory.CreateDirectory (classFilePath);
			}
			if (File.Exists (fullClassName)) {
				Debug.Log ("Delete " + fullClassName);
				File.Delete (fullClassName);
			}
			StreamWriter file = new StreamWriter (fullClassName, false);
			file.WriteLine ("using System;");
			file.WriteLine ("using System.Collections.Generic;");
			file.WriteLine ("using System.Collections;");
			file.WriteLine ("using UnityEngine;");
			
			file.WriteLine ("namespace BattleFramework.Data{");
			file.WriteLine ("    [System.Serializable]");
			file.WriteLine ("    public class " + className + " {");
			Dictionary<string,string> properties = SysConfig.LoadPropertyNames ();
			foreach(string key in properties.Keys)
			{
				string line = "        public const string " + key + "=" + "\"" + key + "\"" + ";" ;
				string comment = "";
				Debug.Log(properties[key]);
				if(properties[key] != "")
				{
					comment = "//" + properties[key];
				}
				file.WriteLine (line + comment);
			}
			file.WriteLine ("    }");
			file.WriteLine ("}");
			file.Flush ();
			file.Close ();
			Debug.Log ("Create " + fullClassName);
		}
	}
}

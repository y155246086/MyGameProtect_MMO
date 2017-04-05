using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace BattleFramework.Data
{

	public class CreateClassFromCSV : EditorWindow
	{

		[MenuItem("CSV/CreateClass")]
		static void AddWindow ()
		{
			EditorWindow.GetWindow (typeof(CreateClassFromCSV));
		}

		string csvDirection = @"Assets/Resources/Configs";
		string classFilePath = @"Assets/Scripts/BattleFramework/Data/Entity";

		int top;
		int height;
		int index;
		int width;
		FileInfo[] filePaths;
		void OnGUI ()
		{
			top = 20;
			height = 30;
			index = 0;
			width = 200;
//			if(GUI.Button(new Rect(60,top + (height + 5) * index,width,height),"select CSV folder"))
//			{
//				csvDirection = EditorUtility.OpenFolderPanel("Please select folder" , csvDirection , "");
//				filePaths = null;
//				if(csvDirection!=null && csvDirection!= "")
//				{
//
//				}
//			}
//			index ++;

			if (GUI.Button (new Rect (60, top + (height + 5) * index, width, height), "Create")) {
				DirectoryInfo dir = new DirectoryInfo (csvDirection);
				filePaths = dir.GetFiles ("*.csv");
				if (filePaths != null) {
					//foreach (FileInfo file in filePaths) {
					//	Create (file);
					//}

					Create (filePaths);
				}
			}
		}

		void Create (FileInfo[] filePaths)
		{
			string mainClassName = "ExcelData";
			string mainFileName = classFilePath + "/" + mainClassName + ".cs";
			if (Directory.Exists (classFilePath)) {
				Directory.Delete (classFilePath, true);
			}
			if (!Directory.Exists (classFilePath)) {
				Directory.CreateDirectory (classFilePath);
			}
			if (File.Exists (mainFileName)) {
				Debug.Log ("Delete " + mainFileName);
				File.Delete (mainFileName);
			}
			StreamWriter file = new StreamWriter (mainFileName, false);
			file.WriteLine ("using System;");
			file.WriteLine ("using System.Collections.Generic;");
			file.WriteLine ("using System.Collections;");
			file.WriteLine ("using UnityEngine;");
			
			file.WriteLine ("namespace BattleFramework.Data{");
			file.WriteLine ("    [System.Serializable]");
			file.WriteLine ("    public static class " + mainClassName + " {");



			foreach (FileInfo fileInfo in filePaths) {

				string fileName = fileInfo.Name;
				string className = fileName.Replace (".csv", "");
				string head = className.Substring (0, 1).ToUpper ();
				string body = className.Substring (1, className.Length - 1);
				className = head + body;

				file.WriteLine ("//--------------------" + className + "---------------------------//");
				file.WriteLine ("        public class " + className + " {");
				string resourcesPath = "Configs/" + className;
				file.WriteLine ("            public static string csvFilePath = \"" + resourcesPath + "\";");
				CSVFile csvFile = new CSVFile ();
				csvFile.Open (resourcesPath);
				
				
				file.WriteLine ("            public  static List<" + className + "> LoadDatas(){");
				file.WriteLine ("                  CSVFile csvFile = new CSVFile();");
				file.WriteLine ("                  csvFile.Open (csvFilePath);");
				file.WriteLine ("                  List<" + className + "> dataList = new List<" + className + ">();");
				file.WriteLine ("                  for(int i = 0;i < csvFile.mapData.Count;i ++){");
				file.WriteLine ("                        " + className + " data = new " + className + "();");
				
				List<string> fields = new List<string> ();
				string columnName;
				string type;
				string fieldName;
				for (int i = 0; i < csvFile.listColumnName.Count; i ++) {
					columnName = csvFile.listColumnName [i];
					//default csv column name is column name plus column type ,e.g NAME_STRING,ID_INT;
					//so we can know the column type and then generate class field;
					if (columnName.LastIndexOf ("_") != -1) {
						type = columnName.Substring (columnName.LastIndexOf ("_") + 1, columnName.Length - columnName.LastIndexOf ("_") - 1).ToUpper ();
					} else {
						type = "";
						fieldName = columnName;
					}
					if (type.ToUpper () == "INT") {
						type = "int";
						fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
						
					} else if (type.ToUpper () == "FLOAT") {
						type = "float";
						fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
					} else if (type.ToUpper () == "BOOL") {
						type = "bool";
						fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
					} else if (type.ToUpper () == "STRING") {
						type = "string";
						fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
					} else if (type.ToUpper () == "LIST") {
						type = "List<int>";
						fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
					} else {
						type = "string";
						fieldName = columnName;
					}
					
					if (type == "string") {
						file.WriteLine ("                    data." + fieldName + " = " + "csvFile.mapData[i].data[" + i + "];");
					} else if (type == "List<int>") {
						file.WriteLine ("                    data." + fieldName + "= new List<int>();");
						file.WriteLine ("                    string[] strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
						file.WriteLine ("                    for(int j=0;j<strs.Length;j++){");
						file.WriteLine ("                        data." + fieldName + ".Add(int.Parse(strs[j]));");
						file.WriteLine ("                    }");
					} else {
						file.WriteLine ("                    " + type + ".TryParse(" + "csvFile.mapData[i].data[" + i + "],out data." + fieldName + ")" + ";");
					}
					
					fields.Add ("                public " + type + " " + fieldName + ";//" + csvFile.listColumnComment [i]);
				}
				
				
				file.WriteLine ("                    dataList.Add(data);");
				file.WriteLine ("                }");
				file.WriteLine ("                return dataList;");
				file.WriteLine ("               }");
				file.WriteLine (" ");

				for (int i=0; i<fields.Count; i++) {
					file.WriteLine (fields [i]);
				}
				file.WriteLine ("            }");
				file.WriteLine (" ");
				file.Flush ();
				Debug.Log ("Create " + fileName);
			}

			file.WriteLine ("    }");
			file.WriteLine ("}");
			file.Flush ();
			file.Close ();

		}


	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace BattleFramework.Data
{
	//根据CSV文件表头和模板生成数据类
	public class CreateClassFromCSV : EditorWindow
	{

		[MenuItem("Data/CSV/CreateClass")]
		static void AddWindow ()
		{
			EditorWindow.GetWindow (typeof(CreateClassFromCSV));
		}

        string csvDirection = @"Assets/Resources/Configs";
        string classFilePath = @"Assets/Scripts/BattleFramework/Data/Entity";
        string dataCenterFilePath = @"Assets/Scripts/BattleFramework/Data/DataCenter.cs";

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
					foreach (FileInfo file in filePaths) {
						Create (file);
					}

					//格式化生成DataCenter
					CreateDataCenter (filePaths);
				}
			}
		}

		void Create (FileInfo fileInfo)
		{
			string fileName = fileInfo.Name;
			string className = fileName.Replace (".csv", "");
			string head = className.Substring (0, 1).ToUpper ();
			string body = className.Substring (1, className.Length - 1);
			className = head + body;
			fileName = classFilePath + "/" + head + body + ".cs";
			if (!Directory.Exists (classFilePath)) {
				Directory.CreateDirectory (classFilePath);
			}
			if (File.Exists (fileName)) {
				Debug.Log ("Delete " + fileName);
				File.Delete (fileName);
			}
			StreamWriter file = new StreamWriter (fileName, false);
			file.WriteLine ("using System;");
			file.WriteLine ("using System.Collections.Generic;");
			file.WriteLine ("using System.Collections;");
			file.WriteLine ("using UnityEngine;");

			file.WriteLine ("namespace BattleFramework.Data{");
			file.WriteLine ("    [System.Serializable]");
			file.WriteLine ("    public class " + className + " {");

			string resourcesPath = "Configs/" + className;
			file.WriteLine ("        public static string csvFilePath = \"" + resourcesPath + "\";");
			CSVFile csvFile = new CSVFile ();
			csvFile.Open (resourcesPath);
			file.WriteLine ("        public static string[] columnNameArray = new string[" + csvFile.listColumnName.Count + "];");
            file.WriteLine ("        public static List<" + className+ "> dataList;");
            file.WriteLine ("        public static Dictionary<int, " + className + "> dataMap;");
            
			file.WriteLine ("        public static List<" + className + "> LoadDatas(){");
			file.WriteLine ("            CSVFile csvFile = new CSVFile();");
			file.WriteLine ("            csvFile.Open (csvFilePath);");
			file.WriteLine ("            dataList = new List<" + className + ">();");
            file.WriteLine ("            dataMap = new Dictionary<int, " + className + ">();");
			file.WriteLine ("            string[] strs;");
			file.WriteLine ("            string[] strsTwo;");
			file.WriteLine ("            List<int> listChild;");
			file.WriteLine ("            columnNameArray = new string[" + csvFile.listColumnName.Count + "];");
			file.WriteLine ("            for(int i = 0;i < csvFile.mapData.Count;i ++){");
			file.WriteLine ("                " + className + " data = new " + className + "();");
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
                }
                else if (type.ToUpper() == "MAP<INT,FLOAT>")
                {
                    type = "Dictionary<int, float>";
                    fieldName = columnName.Substring(0, columnName.LastIndexOf("_"));
                }
                else if (type.ToUpper() == "LIST<INT>")
                {
                    type = "List<int>";
                    fieldName = columnName.Substring(0, columnName.LastIndexOf("_"));
                }
                else if (type.ToUpper() == "LIST<FLOAT>")
                {
                    type = "List<float>";
                    fieldName = columnName.Substring(0, columnName.LastIndexOf("_"));
                }
                else if (type.ToUpper() == "LIST<STRING>")
                {
                    type = "List<string>";
                    fieldName = columnName.Substring(0, columnName.LastIndexOf("_"));
                }
                else if (type.ToUpper() == "LIST2")
                {
					type = "List<List<int>>";
					fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
				} else if (type.ToUpper () == "VECTOR3") {
					type = "Vector3";
					fieldName = columnName.Substring (0, columnName.LastIndexOf ("_"));
				} else {
					type = "string";
					fieldName = columnName;
				}

				if (type == "string") {
					file.WriteLine ("                data." + fieldName + " = " + "csvFile.mapData[i].data[" + i + "];");
				} else if (type == "Vector3") {
					file.WriteLine ("                data." + fieldName + "= new Vector3();");
					file.WriteLine ("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
					file.WriteLine ("                    data." + fieldName + ".x = (float.Parse(strs[0]));");
					file.WriteLine ("                    data." + fieldName + ".y = (float.Parse(strs[1]));");
					file.WriteLine ("                    data." + fieldName + ".z = (float.Parse(strs[2]));");
                }
                else if (type == "List<int>")
                {
                    file.WriteLine("                data." + fieldName + "= new List<int>();");
                    file.WriteLine("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
                    file.WriteLine("                for(int j=0;j<strs.Length;j++){");
                    file.WriteLine("                    data." + fieldName + ".Add(int.Parse(strs[j]));");
                    file.WriteLine("                }");
                }
                else if (type == "List<float>")
                {
                    file.WriteLine("                data." + fieldName + "= new List<float>();");
                    file.WriteLine("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
                    file.WriteLine("                for(int j=0;j<strs.Length;j++){");
                    file.WriteLine("                    data." + fieldName + ".Add(float.Parse(strs[j]));");
                    file.WriteLine("                }");
                }
                else if (type == "List<string>")
                {
                    file.WriteLine("                data." + fieldName + "= new List<string>();");
                    file.WriteLine("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
                    file.WriteLine("                for(int j=0;j<strs.Length;j++){");
                    file.WriteLine("                    data." + fieldName + ".Add(strs[j]);");
                    file.WriteLine("                }");
                }
                else if (type == "Dictionary<int, float>")
                {
                    file.WriteLine("                data." + fieldName + "= new Dictionary<int, float>();");
                    file.WriteLine("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\',\'});");
                    file.WriteLine("                for(int j=0;j<strs.Length;j++){");
                    file.WriteLine("                    strsTwo = " + "strs[j].Split(new char[1]{\':\'});");
                    file.WriteLine("                    if (strsTwo.Length == 2)");
                    file.WriteLine("                        data." + fieldName + ".Add(int.Parse(strsTwo[0]),float.Parse(strsTwo[1]));");
                    file.WriteLine("                }");
                }
                else if (type == "List<List<int>>")
                {
					file.WriteLine ("                data." + fieldName + "= new List<List<int>>();");
					file.WriteLine ("                strs = " + "csvFile.mapData[i].data[" + i + "].Split(new char[1]{\';\'});");
					file.WriteLine ("                for(int j=0;j<strs.Length;j++){");
					file.WriteLine ("                      listChild = new List<int>();");
					file.WriteLine ("                      strsTwo = " + "strs[j].Split(new char[1]{\',\'});");
					file.WriteLine ("                      for(int m=0;m<strsTwo.Length;m++){");	
					file.WriteLine ("                            listChild.Add(int.Parse(strsTwo[m]));");
					file.WriteLine ("                      }");			
					file.WriteLine ("                    data." + fieldName + ".Add(listChild);");
					file.WriteLine ("                }");
				} else {
					file.WriteLine ("                " + type + ".TryParse(" + "csvFile.mapData[i].data[" + i + "],out data." + fieldName + ")" + ";");
				}

				fields.Add ("        public " + type + " " + fieldName + ";//" + csvFile.listColumnComment [i]);
				file.WriteLine ("                columnNameArray [" + i + "] = \"" + fieldName + "\";");
			}
			file.WriteLine ("                dataList.Add(data);");
            file.WriteLine("                if (!dataMap.ContainsKey(data.id))");
            file.WriteLine("                    dataMap.Add(data.id,data);");
			file.WriteLine ("            }");
			file.WriteLine ("            return dataList;");
			file.WriteLine ("        }");


			//添加根据ID查询的方法  
			file.WriteLine ("  ");
			file.WriteLine ("        public static " + className + " GetByID (int id,List<" + className + "> data)");
			file.WriteLine ("        {");
			file.WriteLine ("            foreach (" + className + " item in " + "data) {");
			file.WriteLine ("                if (id == item.id) {");
			file.WriteLine ("                     return item;");
			file.WriteLine ("                }");
			file.WriteLine ("            }");
			file.WriteLine ("            return null;");
			file.WriteLine ("        }");
			file.WriteLine ("  ");

            //添加根据ID查询的方法  
            file.WriteLine("  ");
            file.WriteLine("        public static " + className + " GetByID (int id)");
            file.WriteLine("        {");
            file.WriteLine("            return GetByID(id,dataList);");
            file.WriteLine("        }");
            file.WriteLine("  ");

			for (int i=0; i<fields.Count; i++) {
				file.WriteLine (fields [i]);
			}

			//添加根据ID查询的方法  

			file.WriteLine ("    }");
			file.WriteLine ("}");
			file.Flush ();
			file.Close ();
			Debug.Log ("Create " + fileName);
		}
		//生成DataCenter 功能
		void CreateDataCenter (FileInfo[] filePathsAll)
		{
			StreamWriter file = new StreamWriter (dataCenterFilePath, false);
			file.WriteLine ("using UnityEngine;");
			file.WriteLine ("using System.Collections;");
			file.WriteLine ("using System.Collections.Generic;");
			file.WriteLine (" ");
            file.WriteLine("//if you want to update this,please go to CreateClassFromCSV.cs");
			file.WriteLine ("namespace BattleFramework.Data");
			file.WriteLine ("{");
            file.WriteLine("    public class DataCenter : MonoBehaviour");
			file.WriteLine ("    {");
            file.WriteLine("        static DataCenter instance;");
			file.WriteLine ("  ");

			//list<CSV>
			foreach (FileInfo nowfile in filePathsAll) {
				string fileName = nowfile.Name;
				string className = fileName.Replace (".csv", "");
				string head = className.Substring (0, 1).ToUpper ();
				string body = className.Substring (1, className.Length - 1);

				string classNameLower = "list_" + head + body;
				string classNameUpper = head + body;  //类名
				file.WriteLine ("        public List<" + classNameUpper + "> list_" + classNameUpper + ";");
			}
			file.WriteLine ("  ");
			Debug.Log ("list<CSV>");


			//SingleTon ()
			file.WriteLine ("        public static DataCenter Instance ()");
			file.WriteLine ("        {");
			file.WriteLine ("            if (instance == null) {");
			file.WriteLine ("                Debug.Log (\"new _DataCenter\");");
			file.WriteLine ("                GameObject go = new GameObject (\"_DataCenter\");");
			file.WriteLine ("                DataCenter dataCenter = go.AddComponent<DataCenter> ();");
			file.WriteLine ("                dataCenter.LoadCSV ();");
			file.WriteLine ("                DontDestroyOnLoad (go);");
			file.WriteLine ("                instance = dataCenter;");
			file.WriteLine ("            }");
			file.WriteLine ("            return instance;");
			file.WriteLine ("        }");

			file.WriteLine ("   ");
			file.WriteLine ("   ");
			Debug.Log ("//SingleTon ()");

			//LoadCSV
			file.WriteLine ("        public void LoadCSV ()");
			file.WriteLine ("        {");
			foreach (FileInfo nowfile in filePathsAll) {
				string fileName = nowfile.Name;
				string className = fileName.Replace (".csv", "");
				string head = className.Substring (0, 1).ToUpper ();
				string body = className.Substring (1, className.Length - 1);
				
				string classNameLowerList = "list_" + head + body;
				string classNameUpper = head + body;  //类名
				file.WriteLine ("            " + classNameLowerList + " = " + classNameUpper + ".LoadDatas ();");
			}
			file.WriteLine ("        }");
			Debug.Log ("//LoadCSV");
            /*
			//get id  ITEM
			file.WriteLine ("  ");
			file.WriteLine ("  ");
			file.WriteLine ("//*****************get id Item***********************************");
			foreach (FileInfo nowfile in filePathsAll) {
				string fileName = nowfile.Name;
				string className = fileName.Replace (".csv", "");
				string head = className.Substring (0, 1).ToUpper ();
				string body = className.Substring (1, className.Length - 1);
				
				string classNameLowerList = "list_" + head + body;
				string classNameUpper = head + body;  //类名
				file.WriteLine ("  ");
				file.WriteLine ("//" + classNameUpper + "-----------------------------------------");
				file.WriteLine ("        public " + classNameUpper + " CSV_" + classNameUpper + " (int id)");
				file.WriteLine ("        {");
				file.WriteLine ("            foreach (" + classNameUpper + " item in " + classNameLowerList + ") {");
				file.WriteLine ("                if (id == item.id) {");
				file.WriteLine ("                return item;");
				file.WriteLine ("                }");
				file.WriteLine ("            }");
				file.WriteLine ("            return null;");
				file.WriteLine ("        }");
				file.Flush ();
			}
             * */
			file.WriteLine (" ");
			file.WriteLine (" ");
			file.WriteLine ("    }");
			file.WriteLine ("}");
			file.Flush ();
			file.Close ();
			Debug.Log ("//get id  ITEM");
		}
		
	}
}

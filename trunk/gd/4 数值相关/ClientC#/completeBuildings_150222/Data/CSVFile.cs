using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFramework.Data{
	//Core of csv reader;
	public class CSVFile
	{

		private List<Row> m_mapInfo = null;
		private List<string> m_listColumns = null;
		private List<string> m_listColumnComments = null;

		public List<Row> mapData{ get{ return m_mapInfo; } }//csv datas,read only;
		public int rowAmount{ get{ return m_mapInfo.Count; } }//csv row count,read only;
		public List<string> listColumnName{ get{ return m_listColumns; } }//csv column names,read only;
		public List<string> listColumnComment{ get{ return m_listColumnComments; } }//csv column comment,read only;
		public int columnAmount{ get{ return m_listColumns.Count; } }//csv column count,read only;
		public const char mSplitMark = '|';
		public string PathFile { get; set; }//csv path,base on Resources folder;


		public CSVFile()
		{
	        _Initialize();
		}

	    void _Initialize()
	    {
			m_mapInfo = new List<Row>();
			m_listColumns = new List<string>();
			m_listColumnComments = new List<string> ();
	    }

	    public void Clear()
	    {
	        mapData.Clear();
	        listColumnName.Clear();
	    }

	    public bool OpenFile(string strFileName)
	    {
			Clear();
	        if (strFileName.Equals(""))
	            return false;
	        try
	        {
				FileStream fs = new FileStream(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read,System.IO.FileShare.ReadWrite);
	            StreamReader sr = new StreamReader(fs, Encoding.Default);
	            bool bRet = Open(sr);
				this.PathFile = strFileName;
	            sr.Close();
	            fs.Close();
	            return bRet;
	        }
	        catch (System.Exception ex)
	        {
				Debug.LogError(ex.Message);
	            return false;
	        }
	    }

		public bool Open(string strFileName)
		{
	        if (strFileName.Equals(""))
	            return false;
	        try
	        {
				TextAsset buildText = Resources.Load<TextAsset>(strFileName);
	            StreamReader sr = new StreamReader(new MemoryStream(buildText.bytes)/*, Encoding.ASCII*/);
	            bool bRet = Open(sr);
	            sr.Close();
	            return bRet;
	        }
	        catch (System.Exception ex)
	        {
				Debug.LogError(ex.Message);
	            return false;
	        }
		}

	    public bool Open(StreamReader sr)
	    {
	        //记录每次读取的一行记录
	        string strLine = "";
	        //记录读取每一个Trunk数据
	        string strTemp = "";
	        //标示是否是读取的第一行
	        bool IsFirst = true;

			bool IsSecond = false;
	        //逐行读取CSV中的数据
	        do
	        {
	            strLine = sr.ReadLine();
	            if (strLine == null)
	                break;

	            if (strLine.Length >= 2 && strLine[0] == '/' && strLine[1] == '/')
	                continue;

	            strTemp = "";
	            if (IsFirst)
	            {
	                IsFirst = false;
					IsSecond = true;
	                for (int i = 0; i < strLine.Length; i++)
	                {
	                    char c = strLine[i];
	                    char cEx = '\0'; // 空值
	                    if ((i + 1) < strLine.Length)
	                        cEx = strLine[i + 1];

						if (c == mSplitMark)
	                    {
	                        m_listColumnComments.Add(strTemp);
	                        strTemp = "";
	                    }
	                    else
	                        strTemp += c;

	                    if ((i == strLine.Length - 1) || (c == 0x0D && cEx == 0x0A) || c == 0x0A)
	                    {
							m_listColumnComments.Add(strTemp);
	                        strTemp = "";
	                    }
	                }
	            }
				else if(IsSecond)
				{
					IsSecond = false;
					for (int i = 0; i < strLine.Length; i++)
					{
						char c = strLine[i];
						char cEx = '\0'; // 空值
						if ((i + 1) < strLine.Length)
							cEx = strLine[i + 1];
						
						if (c == mSplitMark)
						{
							m_listColumns.Add(strTemp);
							strTemp = "";
						}
						else
							strTemp += c;
						
						if ((i == strLine.Length - 1) || (c == 0x0D && cEx == 0x0A) || c == 0x0A)
						{
							m_listColumns.Add(strTemp);
							strTemp = "";
						}
					}
				}
	            else
	            {
					Row trunk = new Row();
	                for (int i = 0; i < strLine.Length; i++)
	                {
	                    char c = strLine[i];
	                    char cEx = '\0'; // 空值
	                    if ((i + 1) < strLine.Length)
	                        cEx = strLine[i + 1];

	                    if (c == mSplitMark || (i == strLine.Length - 1)) // read field   // 
	                    {
	                        if (i == strLine.Length - 1)
	                            strTemp += c;

	                        trunk.data.Add(strTemp);

	                        strTemp = "";
	                    }
	                    else
	                        strTemp += c;

	                    // add trunk data
	                    if ((i == strLine.Length - 1) || (c == 0x0D && cEx == 0x0A) || c == 0x0A)
	                    {
	                        m_mapInfo.Add(trunk);
	                    }

	                }

	            }

	        } while (true);

	        if (rowAmount == 0)
	        {
	            return false;
	        }
	        return true;
	    }

		public bool save(string strTitle, string strDirectory, string strExtensionName, string strDefaultName = "")
		{
			if(strDefaultName == "")
			{
				DateTime dt = DateTime.Now;
				strDefaultName = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "-" +
							dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
			}

			string strPath = "";
			//#if UNITY_EDITOR
			//strPath = EditorUtility.SaveFilePanel( strTitle, strDirectory, strDefaultName, strExtensionName);
			//#else
			strPath = PathFile + "/Data/Configs/" + strTitle;
	#if UNITY_WEBPLAYER
	        bool bRet = false;
	        Debug.LogError("Can't save on webplayer");
	#else
	        bool bRet = save(strPath);
	#endif
	       
			return bRet;
		}

	#if UNITY_WEBPLAYER

	#else
	    public bool save(string strFile)
	    {
	        if(this.listColumnName.Count == 0)
	            return false;
	        
	        if(this.m_mapInfo.Count == 0)
	            return false;
	        
	        FileInfo fi = new FileInfo(strFile);
	        if (!fi.Directory.Exists)
	        {
	            fi.Directory.Create();
	        }
	        
	        FileStream fs = new FileStream(strFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
	        
	        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
	        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
	        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
	        string data = "";
	        
	        // write column name
	        foreach (string item in this.m_listColumns)
	        {
	            data += item;
	            data += mSplitMark;
	        }
	        data += 0x0A; // 加换行符
	        sw.WriteLine(data);
	        
	        // write data
	        foreach(Row trunkItem in m_mapInfo)
	        {
	            data = "";
				List<string> st = trunkItem.data;
	            
	            // each column data
	            foreach (var colItem in st)
	            {
	                if(colItem != null)
	                {
	                    data += colItem;
	                    data += ",";
	                }
	            }
	            
	            data += 0x0A; // 加换行符
	            sw.WriteLine(data);
	        }
	        sw.Close();
	        fs.Close();
	        return true;
	    }
	#endif

	}

	public class Row
	{
		public List<string> data = new List<string>();

		 int GetIndex(System.Enum key)
		{
			if (data == null)
				return -1;
			int value = Convert.ToInt32(key);
			if (value < 0 || value >= data.Count)
				return -1;
			return value;
		}

		public bool GetIntValue(System.Enum key, out int value)
		{
			int index = GetIndex (key);
			if (index != -1) {
				if (System.Int32.TryParse(data[index], out value))
					return true;
				float fValue;
				if (GetFloatValue(key, out fValue))
					value = (int)fValue;
			}
			Debug.LogError ("GetIntValue " + key + " false!");
			value = -1;
			return false;
		}

		public bool GetFloatValue(System.Enum key,out float value)
		{
			int index = GetIndex (key);
			if (index != -1) {
				if (System.Single.TryParse (data[index], out value)) {
					return true;
				}
			}
			Debug.LogError ("GetFloatValue " + key + " false!");
			value = -1;
			return false;
		}

		public bool GetBoolValue(System.Enum key,out bool value)
		{
			int index = GetIndex (key);
			if (index != -1) {
				if (System.Boolean.TryParse (data[index], out value))
					return true;
			}
			Debug.LogError ("GetBoolValue " + key + " false!");
			value = false;
			return false;  
		}

		public bool GetStringValue(System.Enum key, out string value)
		{
			int index = GetIndex(key);
			if (index != -1)
			{
				value = data[index];
				return true;
			}
			Debug.LogError ("GetStringValue " + key + " false!");
			value = "";
			return false;
		}

	}

}

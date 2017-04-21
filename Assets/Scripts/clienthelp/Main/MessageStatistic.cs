using System;
using System.Collections.Generic;
using System.Text;

namespace ClientHelper
{
    public class MessageStatistic
    {
		public static bool isOpen=false;
		private static MessageStatistic instance = new MessageStatistic();
		public static MessageStatistic GetInstance() { return instance; }

		public class MessageStatisticNode{
			double totalValue;
			double maxValue;
			double minValue;
			int count;

			public MessageStatisticNode(){

			}

			public void Add(int value)
			{
				if(count==0)
				{
					++count;
					totalValue = value;
					maxValue = value;
					minValue=value;
					
				}
				else{
					++count;
					totalValue+=value;
					if(maxValue<value){maxValue=value;}
					if(minValue>value){minValue=value;}
				}
			}

			public string GetDump(int protocolId){
				//avg max min count
				if(count==0){
					return "\t"+protocolId.ToString("x")+"\t0\t0\t0\t0";
				}
				else{
					return "\t" + protocolId.ToString("x") + "\t" + totalValue / count + "\t" + maxValue + "\t" + minValue + "\t" + count;
				}
			}

			static string getHeadDump(int protocolId)
			{
				return "\tprotocolId\tavg\tmax\tmin\tcount";
			}
		}


		private const int IntervalTimeMS = 60000;
		public static string logFileName = "statistic.log";

		private DateTime logDate;
		private SortedList<int, MessageStatisticNode> myList = new SortedList<int, MessageStatisticNode>();
		private int LastWriteTime = curTimeMS();
		private List<SortedList<int, MessageStatisticNode>> LogQueue = new List<SortedList<int, MessageStatisticNode>>();
		private System.IO.FileStream fs = null;
		private System.IO.StreamWriter sw = null;

		public void AddMsgTime(int protocolId, int time)
		{
			lock(this)
			{
				WriteStatistic();

				MessageStatisticNode outValue = null;
				myList.TryGetValue(protocolId, out outValue);
				if (outValue == null)
				{
					outValue = new MessageStatisticNode();
					myList.Add(protocolId, outValue);
				}

				outValue.Add(time);
			}
		}

		
		private void WriteLine(string text)
		{
			if (fs == null && sw==null){
				fs = new System.IO.FileStream(logFileName, System.IO.FileMode.Create);
				sw = new System.IO.StreamWriter(fs, Encoding.Default);
			}

			if(sw!=null){
				sw.Write(text);
				sw.Write("\n");
			}
		}

		private void WriteFlush(){
			if (sw != null)
			{
				sw.Flush();
			}

			if (fs != null)
			{
				fs.Flush();
			}
		}

		private void WriteStatistic(){

			if(logDate.Ticks==0)
			{
				logDate = DateTime.Now;
			}

			int now= curTimeMS();

			if(now-LastWriteTime>=IntervalTimeMS){

				LastWriteTime = now;

				
				WriteLine(logDate.ToString() +"\t"+ DateTime.Now.ToString() + "\tStatistic Num=" + myList.Count);

				IEnumerator<KeyValuePair<int, MessageStatisticNode>> it=myList.GetEnumerator();
				while (it.MoveNext())
				{
					WriteLine(it.Current.Value.GetDump(it.Current.Key));
				}
				WriteFlush();

				myList = new SortedList<int, MessageStatisticNode>();
				logDate = new DateTime();
				
			}
		}


		public static int curTimeMS()
		{
			return (int) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) ;
		}
    }
}

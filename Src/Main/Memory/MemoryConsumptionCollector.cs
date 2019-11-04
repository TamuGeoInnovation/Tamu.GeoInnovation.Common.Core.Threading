using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace USC.GISResearchLab.Common.Core.Utils.Memory
{
    public class MemoryConsumptionItem
    {
        public DateTime ItemTime;
        public string Label;
        public long MemUsage;
        public static readonly string Header = "Time,Label,MemUsage";

        public MemoryConsumptionItem(DateTime itemTime, string label, long memUsage)
        {
            ItemTime = itemTime;
            Label = label;
            MemUsage = memUsage;
        }

        public string GetAsCSVRow()
        {
            return "'" + ItemTime.ToLongTimeString() + "','" + Label + "'," + MemUsage;
        }
    }

    public class MemoryConsumptionCollector
    {
        bool forceFullCollection;
        int intervalMiliSecond;
        string label;
        Thread thread;
        bool stopRequested;
        List<MemoryConsumptionItem> collection;

        public MemoryConsumptionCollector() : this(false, 5) { }

        public void SetLabel(string Label)
        {
            lock (this)
            {
                label = Label;
            }
        }

        public MemoryConsumptionCollector(bool ForceFullCollection, int IntervalMiliSecond)
        {
            forceFullCollection = ForceFullCollection;
            intervalMiliSecond = IntervalMiliSecond;
            label = string.Empty;
            thread = new Thread(new ThreadStart(this.ThreadJob));
            thread.Priority = ThreadPriority.BelowNormal;
        }

        public void StartAsync()
        {
            if (thread.ThreadState == ThreadState.Stopped || thread.ThreadState == ThreadState.Unstarted)
            {
                stopRequested = false;
                collection = new List<MemoryConsumptionItem>();
                thread.Start();
            }
            else throw new ThreadStateException("Thread is already started.");
        }

        void ThreadJob()
        {
            while (!stopRequested)
            {
                lock (this)
                {
                    collection.Add(new MemoryConsumptionItem(DateTime.Now, label, GC.GetTotalMemory(forceFullCollection)));
                }
                Thread.Sleep(intervalMiliSecond);
            }
        }

        public void StopAsync()
        {
            if (thread.ThreadState == ThreadState.Running)
            {
                stopRequested = true;
                thread.Join();
            }
            else throw new ThreadStateException("Thread is never started.");
        }

        public void FlushResultsAsCSV(string csvFilename)
        {
            var lines = new List<string>(collection.Count + 1);
            lines.Add(MemoryConsumptionItem.Header);
            collection.ForEach(i => lines.Add(i.GetAsCSVRow()));
            File.WriteAllLines(csvFilename, lines.ToArray());
        }
    }
}

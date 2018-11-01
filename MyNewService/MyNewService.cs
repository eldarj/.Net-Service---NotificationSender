using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyNewService
{
    public partial class MyNewService : ServiceBase
    {
        private int eventId = 1;

        public MyNewService()
        {
            InitializeComponent();

            eventLog1 = new EventLog();
            if (EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            Timer timer = new Timer();
            timer.Interval = 20000;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // TODO: Monitor activites
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In Continue");
        }
    }
}

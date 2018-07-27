using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Management;


namespace DreamConfigService
{
    public partial class Service1 : ServiceBase
    {
        public System.Management.ManagementEventWatcher mgmtWtch;

        public Service1()
        {
            InitializeComponent();
            mgmtWtch = new System.Management.ManagementEventWatcher("Select * From Win32_ProcessStartTrace");
            mgmtWtch.EventArrived += new System.Management.EventArrivedEventHandler(mgmtWtch_EventArrived);
            
        }

        protected override void OnStart(string[] args)
        {
            mgmtWtch.Start();
        }

        protected override void OnStop()
        {
            mgmtWtch.Stop();
        }

        void mgmtWtch_EventArrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            Treatment.doTreatment(e);

            
        }
    }
}

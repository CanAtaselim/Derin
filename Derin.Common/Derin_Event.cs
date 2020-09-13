using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Derin.Common
{
    class Derin_Event
    {
        public Derin_Event()
        {
            if (ConstantVarible._Project_Settings_EventLoggingEnabled)
            {
                serviceTimer = System.Diagnostics.Stopwatch.StartNew();
            }
        }

        public void Stop<T>(
            string v_functionName,
            _EventDetail v_eventDetail,
            T item, RequestInfo info = null)
        {
            if (ConstantVarible._Project_Settings_EventLoggingEnabled)
            {
                v_eventDetail.EventSource = SerializeTObject(item);
                serviceTimer.Stop();
                var serviceTimer_Duration = serviceTimer.ElapsedMilliseconds;

                AddNewEvent(
                v_functionName,
                serviceTimer_Duration,
                v_eventDetail, info
                );
            }
        }

        public void AddNewEvent(
            string functionName,
            long v_executionTime,
            _EventDetail eventDetail,
            RequestInfo info = null
            )
        {
            MyEvent evnt = new MyEvent();

            evnt.ControllerName = eventDetail.ControllerName;
            evnt.MethodName = eventDetail.MethodName;
            evnt.EventSource = eventDetail.EventSource;
            evnt.ExecutionTime = v_executionTime;
            evnt.EventDate = DateTime.Now;
            evnt.FunctionName = functionName;
            evnt.OperationIdUserRef = info.IdUserRef;
            evnt.OperationDate = DateTime.Now;
            evnt.OperationIsDeleted = _Enumeration._TypeOperationIsDeleted.UnDeleted;
            evnt.OperationIP = info.OperationIP;
            evnt.MachineName = info.MachineName;
            evnt.MachineIp = info.MachineIP;
            evnt.ClientBrowser = info.ClientBrowser;

            Derin_Logging.WriteToEventQueue(Derin_Logging.Type.Event, SerializeObject(evnt));
        }

        private System.Diagnostics.Stopwatch serviceTimer { get; set; }

        private static string SerializeTObject<T>(T item)
        {
            if (typeof(T).Name.Contains("IDictionary"))
            {
                IDictionary<string, object> dicItem = (IDictionary<string, object>)item;
                return new XElement(
                     "items",
                     dicItem.Select(x => new XElement("item", new XAttribute("id", x.Key), new XAttribute("value", x.Value == null ? "" : x.Value)))
                  ).ToString();
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                TextWriter TW = new StringWriter();
                serializer.Serialize(TW, item);
                return TW.ToString();
            }

        }

        private static string SerializeObject(MyEvent ex)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyEvent));
            TextWriter TW = new StringWriter();
            serializer.Serialize(TW, ex);
            return TW.ToString();
        }

        public static _EventDetail GenerateEventSource(
            _EventDetail eventDetail,
            string query,
            string paramList)
        {
            if (paramList == null)
            {
                eventDetail.EventSource = query;
                return eventDetail;
            }

            eventDetail.EventSource = query;

            return eventDetail;
        }
    }

    [Serializable]
    public class MyEvent
    {
        public long IdEvent { get; set; }

        public long OperationIdUserRef { get; set; }

        public string OperationIP { get; set; }

        public DateTime OperationDate { get; set; }

        public _Enumeration._TypeOperationIsDeleted OperationIsDeleted { get; set; }

        public string ControllerName { get; set; }

        public string MethodName { get; set; }

        public string FunctionName { get; set; }

        public string EventSource { get; set; }

        public DateTime EventDate { get; set; }

        public long? ExecutionTime { get; set; }

        public string MachineName { get; set; }

        public string MachineIp { get; set; }

        public string ClientBrowser { get; set; }

    }

    [Serializable]
    public class RequestInfo
    {
        public long IdUserRef { get; set; }
        public string OperationIP { get; set; }
        public string MachineName { get; set; }
        public string MachineIP { get; set; }
        public string ClientBrowser { get; set; }
    }
}

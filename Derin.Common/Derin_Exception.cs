using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Derin.Common
{
    public class _EventDetail
    {
        public _EventDetail(
            string controllerName,
            string methodName,
            string eventSource
            )
        {
            ControllerName = controllerName;
            EventSource = eventSource;
            MethodName = methodName;
        }
        public _EventDetail(
           string controllerName,
           string methodName
           )
        {
            ControllerName = controllerName;
            MethodName = methodName;
        }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string EventSource { get; set; }
    }

    public class Derin_Exception : Exception
    {
        private _Enumeration._TypeException common;
        #region Fields
        private readonly int _errorCode;
        private readonly string _errorMessage;
        private readonly string _functionCode;

        #endregion

        public Derin_Exception()
            : this(_Enumeration._TypeException.Common)
        {
        }

        public Derin_Exception(_Enumeration._TypeException common)
        {
            this.common = common;
        }

        public Derin_Exception(string message)
            : this(message, (int)_Enumeration._TypeException.Common)
        {
        }

        public Derin_Exception(int errorCode)
            : this(String.Format("Error({0})", errorCode), errorCode)
        {
        }

        public Derin_Exception(string message, int errorCode)
            : base(message)
        {
            _errorCode = errorCode;
            _functionCode = message;
        }

        public Derin_Exception(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            _errorCode = errorCode;
            if (innerException.InnerException != null)
                _errorMessage = innerException.InnerException.Message;
            else
                _errorMessage = innerException.Message;
            _functionCode = message + " -> " + innerException.Message;

            if (innerException.InnerException != null)
            {
                if (NewMethod(innerException))
                {
                    int i = ((System.Data.SqlClient.SqlException)(innerException.InnerException)).Number;
                }
            }

            if (errorCode != (int)_Enumeration._TypeException.Validation)
                Save(_errorCode, _errorMessage, _functionCode);
        }

        private static bool NewMethod(Exception innerException)
        {
            return innerException.InnerException is System.Data.SqlClient.SqlException;
        }

        public Derin_Exception(string message, int errorCode, Exception innerException, _EventDetail _evntDetail)
            : base(message, innerException)
        {
            _errorCode = errorCode;
            if (innerException.InnerException != null)
                _errorMessage = innerException.InnerException.Message;
            else
                _errorMessage = innerException.Message;
            _functionCode = message + " -> " + innerException.Message; ;

            if (innerException.InnerException != null)
            {
                if (innerException.InnerException is System.Data.SqlClient.SqlException)
                {
                    int i = ((System.Data.SqlClient.SqlException)(innerException.InnerException)).Number;
                }
            }

            if (errorCode != (int)_Enumeration._TypeException.Validation)
                Save(_errorCode, _errorMessage, _functionCode, _evntDetail);
        }

        public Derin_Exception(string message, int errorCode, Exception innerException, string errorDetail, _EventDetail _evntDetail)
            : base(message, innerException)
        {
            _errorCode = errorCode;
            if (innerException.InnerException != null)
                _errorMessage = innerException.InnerException.Message;
            else
                _errorMessage = innerException.Message;
            _functionCode = message + " -> " + innerException.Message; ;

            if (innerException.InnerException != null)
            {
                if (innerException.InnerException is System.Data.SqlClient.SqlException)
                {
                    int i = ((System.Data.SqlClient.SqlException)(innerException.InnerException)).Number;
                }
            }

            if (errorCode != (int)_Enumeration._TypeException.Validation)
                Save(_errorCode, _errorMessage + "->" + errorDetail, _functionCode, _evntDetail);
        }
        protected static void Save(int errorCode, string errorMessage, string functionCode, _EventDetail eventDetail)
        {
            MyException ex = new MyException();
            ex.FunctionName = functionCode;
            ex.ErrorCode = errorCode;
            ex.ExceptionSource = errorMessage;
            ex.ControllerName = FilterParameter.ToSafeString(eventDetail.ControllerName);
            ex.MethodName = FilterParameter.ToSafeString(eventDetail.MethodName);
            ex.OperationIdUserRef = SessionVariable._IdUserRef;
            ex.OperationIP = SessionVariable._OperationIP;
            ex.OperationIsDeleted = 1;
            ex.MachineName = SessionVariable._MachineName;
            ex.MachineIp = SessionVariable._MachineIp;
            ex.ClientBrowser = SessionVariable._ClientBrowser;

            Derin_Logging.WriteToQueue(Derin_Logging.Type.Exception, SerializeObject(ex));
        }

        protected static void Save(int errorCode, string errorMessage, string functionCode)
        {
            MyException ex = new MyException();
            ex.FunctionName = functionCode;
            ex.ErrorCode = errorCode;
            ex.ExceptionSource = errorMessage;
            ex.ControllerName = "";
            ex.MethodName = "";
            ex.OperationIdUserRef = SessionVariable._IdUserRef;
            ex.OperationIP = SessionVariable._OperationIP;
            ex.OperationIsDeleted = 1;
            ex.MachineName = SessionVariable._MachineName;
            ex.MachineIp = SessionVariable._MachineIp;
            ex.ClientBrowser = SessionVariable._ClientBrowser;

            Derin_Logging.WriteToQueue(Derin_Logging.Type.Exception, SerializeObject(ex));
        }

        [Serializable]
        public class MyException
        {
            public long OperationIdUserRef = 0;
            public string OperationIP = string.Empty;
            public DateTime OperationDate = DateTime.Now;
            public Int16 OperationIsDeleted = 1;
            public int ErrorCode = 0;
            public string ControllerName = string.Empty;
            public string MethodName = string.Empty;
            public string FunctionName = string.Empty;
            public string ExceptionSource = string.Empty;
            public DateTime ExceptionDate = DateTime.Now;
            public string MachineName = string.Empty;
            public string MachineIp = string.Empty;
            public string ClientBrowser = string.Empty;
        }
        [Serializable]
        public class GlobalExceptionObject
        {
            public string ErrorMessage { get; set; }
            public string InnerException { get; set; }
            public string StackTrace { get; set; }
            public string Area { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public long? UserId { get; set; }
            public string IPAddress { get; set; }
            public string ClientBrowser { get; set; }
            public Guid ExCode { get; set; }
        }

        private static string SerializeObject(MyException ex)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyException));
            TextWriter TW = new StringWriter();
            serializer.Serialize(TW, ex);
            return TW.ToString();
        }
        public static string SerializeObject(GlobalExceptionObject ex)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GlobalExceptionObject));
            TextWriter TW = new StringWriter();
            serializer.Serialize(TW, ex);
            return TW.ToString();
        }

        private static MyException DeserializeXml(string XmlData)
        {
            MyException ex = new MyException();
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(MyException));

            StringReader SR = new StringReader(XmlData);
            XmlReader XR = new XmlTextReader(SR);
            if (MyDeserializer.CanDeserialize(XR))
            {
                ex = (MyException)MyDeserializer.Deserialize(XR);
            }
            return ex;
        }
    }
}

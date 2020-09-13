using System;
using System.Messaging;

namespace Derin.Common
{
    public class Derin_Logging
    {
        public enum Type
        {
            Log = 0,
            Exception = 1,
            Event = 2
        }

        public static void WriteToQueue(Type Label, string Body)
        {
            CreateQueue();

            MessageQueue msqueue = new MessageQueue();
            msqueue.Path = @".\private$\Derin";
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Label = Label.ToString();
            message.Body = Body;
            msqueue.Send(message);
            msqueue.Close();
        }
        public static void WriteToEventQueue(Type Label, string Body)
        {
            CreateEventQueue();

            MessageQueue msqueue = new MessageQueue();
            msqueue.Path = @".\private$\Derin_Event";
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Label = Label.ToString();
            message.Body = Body;
            msqueue.Send(message);
            msqueue.Close();
        }

        #region  CreateQueue
        private static void CreateQueue()
        {
            if (!MessageQueue.Exists(@".\Private$\Derin"))
            {
                MessageQueue.Create(@".\private$\Derin");
            }
        }
        private static void CreateEventQueue()
        {
            if (!MessageQueue.Exists(@".\Private$\Derin_Event"))
            {
                MessageQueue.Create(@".\private$\Derin_Event");
            }
        }
        #endregion

        private static void ReadFromQueue()
        {

            MessageQueue queue = new MessageQueue();
            queue.Path = @".\private$\Derin";

            Message message = queue.Receive();
            message.Formatter = new BinaryMessageFormatter();
            Console.WriteLine("Message Label: {0}", message.Label);
            Console.WriteLine("Message Body : {0}", message.Body);

            queue.Close();

        }
        private static void ReadFromEventQueue()
        {

            MessageQueue queue = new MessageQueue();
            queue.Path = @".\private$\Derin_Event";

            Message message = queue.Receive();
            message.Formatter = new BinaryMessageFormatter();
            Console.WriteLine("Message Label: {0}", message.Label);
            Console.WriteLine("Message Body : {0}", message.Body);

            queue.Close();

        }
    }
}

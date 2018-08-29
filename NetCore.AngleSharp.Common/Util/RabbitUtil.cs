using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.AngleSharpProgram.Common.Util
{
    public class RabbitUtil
    {
        private static Lazy<IBus> _instance = null;
        private static object lockStr = new object();

        public static IBus Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (lockStr)
                    {
                        if (null == _instance)
                        {
                            _instance = new Lazy<IBus>(RabbitHutch.CreateBus("host =192.168.200.187:5672; virtualHost=/;username=rabbitadm;password=Id49erUi3q"));
                        }
                    }
                }
                return _instance.Value;
            }
        }


        public static bool Send(string message)
        {
            bool result = true;
            try
            {
                RabbitUtil.Instance.Send("CNBlog", message);
                result = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}

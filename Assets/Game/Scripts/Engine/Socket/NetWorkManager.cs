using Google.Protobuf;
using System;
using System.IO;
using System.Text;
using UnityEngine;
namespace Engine
{
    public class NetWorkManager : Singleton<NetWorkManager>
    {
        /// <summary>
        /// 实例化一个socket客户端连接
        /// </summary>
        private SocketClient m_SocketClient = null;

        private string mHost;
        private int mPort;

        //消息回调
        private Action<string,string> messageAction;
        //网络状态回调
        private Action<int> netStatusAction;
        public void Init() { }

        /// <summary>
        /// 是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnect()
        {
            if (m_SocketClient == null)
            {
                return false;
            }
            return m_SocketClient.IsConnect;
        }

        /// <summary>
        /// 开始连接
        /// </summary>
        public void StartConnect(string host, int port)
        {
            if (m_SocketClient == null)
            {
                m_SocketClient = new SocketClient();
            }

            mHost = host;
            mPort = port;

            m_SocketClient.ConnectServer(host, port);
        }
        /// <summary>
        /// 添加消息回调
        /// </summary>
        /// <param name="messageAction"></param>
        public void AddMessageListener(Action<string,string> messageAction)
        {
            this.messageAction = messageAction;
        }
        //添加网络状态回调
        public void AddNetStatusListener(Action<int> netStatusAction)
        {
            this.netStatusAction = netStatusAction;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg(byte[] bt)
        {
            m_SocketClient.SaveMessage(bt);
        }


        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (m_SocketClient != null)
            {
                m_SocketClient.SubjectiveClose();
            }
        }

        /// <summary>
        /// 发送数据给接口接收者
        /// </summary>
        /// <returns></returns>
        public void Update()
        {
            if (m_SocketClient != null)
            {
                //解析数据
                if (m_SocketClient.m_ReceiveQueue != null)
                {
                    if (m_SocketClient.m_ReceiveQueue.Count > 0)
                    {
                        if (messageAction != null)
                        {
                            byte[] bt = m_SocketClient.m_ReceiveQueue.Dequeue();

                            int nameLength = bt[1] * 256 + bt[0];

                            byte[] nameData = new MemoryStream(bt, 2, nameLength).ToArray();

                            
                            string name = Encoding.UTF8.GetString(nameData);

                            byte[] contentData = new MemoryStream(bt, nameLength+2, bt.Length- (nameLength + 2)).ToArray();

                            string content = Encoding.UTF8.GetString(contentData);

                            messageAction(name, content);
                        }
                    }
                }

                //分发网络状态
                if (m_SocketClient.m_NetStatusQuene.Count > 0)
                {
                    if (netStatusAction != null)
                    {
                        SocketClient.NetStatus status = m_SocketClient.m_NetStatusQuene.Dequeue();
                        netStatusAction((int)status);
                    }
                }

            }

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void SendMsg<T>(int cmd, T t) where T : IMessage
        {
            Com.Protobuf.CommonMsg commonMsg = new Com.Protobuf.CommonMsg
            {
                Cmd = cmd,
                Body = t.ToByteString()
            };

            SendMsg(commonMsg.ToByteArray());
        }



        //-----------------------------------------------其他方式发送和解析-----------------------------------------------

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg(string msgName, string jsonMsg)
        {
            Debug.Log("发送消息 : " + msgName + " jsonMsg : " + jsonMsg);
            //JsonUtility.ToJson(obj)
            //数据编码
            byte[] nameBytes = EncodeName(msgName);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(jsonMsg);
            int len = nameBytes.Length + bodyBytes.Length;
            byte[] sendBytes = new byte[2 + len];
            //组装长度
            sendBytes[0] = (byte)(len % 256);
            sendBytes[1] = (byte)(len / 256);
            //组装名字
            Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
            //组装消息体
            Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);

            SendMsg(sendBytes);
        }

        private byte[] EncodeName(string name)
        {
            //名字bytes和长度
            string[] parts = name.Split('+');
            byte[] nameBytes = Encoding.UTF8.GetBytes(parts[0]);
            Int16 len = (Int16)nameBytes.Length;
            //申请bytes数值
            byte[] bytes = new byte[2 + len];
            //组装2字节的长度信息
            bytes[0] = (byte)(len % 256);
            bytes[1] = (byte)(len / 256);
            //组装名字bytes
            Array.Copy(nameBytes, 0, bytes, 2, len);
            return bytes;
        }
    }
}
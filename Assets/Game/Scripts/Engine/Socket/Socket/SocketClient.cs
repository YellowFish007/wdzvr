using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
namespace Engine
{
    public class SocketClient
    {
        /// <summary>
        /// 网络状态
        /// </summary>
        public enum NetStatus
        {
            ConnectFailed = 0,  //连接失败
            ConnectSuccess = 1,  //连接成功
            ERROR = 2,  //网络异常 这个时候要尝试重新连接
            Close = 3,  //网络主动关闭 不自动连接
        }

        //----------------基础配置属性----------------
        // IP
        private string m_Host = "";
        //端口 Port
        private int m_Port = -1;

        //接收数据的队列
        public Queue<byte[]> m_ReceiveQueue;
        //发送数据的队列
        public Queue<byte[]> m_SendMsgQueue;
        //保存网络的状态值，以免丢失
        public Queue<NetStatus> m_NetStatusQuene;

        //----------------初始化属性----------------
        //Socket对象
        private Socket m_Socket = null;

        public SocketClient()
        {
            m_SendMsgQueue = new Queue<byte[]>();

            m_NetStatusQuene = new Queue<NetStatus>();

            m_ReceiveQueue = new Queue<byte[]>();
        }

        public bool IsConnect
        {
            get
            {
                return m_Socket == null ? false : m_Socket.Connected;
            }
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        private void Reset()
        {
            //实例化一个新的 Tcp
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            m_Socket.ReceiveTimeout = 100;

            m_SendMsgQueue.Clear();
            m_ReceiveQueue.Clear();
        }

        /// <summary>
        /// 开启连接
        /// </summary>
        /// <param name="host">ip</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public void ConnectServer(string host, int port)
        {
            m_Host = host;
            m_Port = port;
            //未连接状态
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UpdateNetState(NetStatus.ERROR);
            }
            else
            {
                //开启一个线程去处理数据流
                Thread thread = new Thread(new ThreadStart(CheckReceiveNew));
                //开始咯我的宝贝
                thread.Start();

            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        private bool Open()
        {
            //如果没有关闭的话，就关闭当前的Socket重新开
            Close();
            try
            {
                //重置数据，包括初始化Socket
                Reset();
                //尝试连接
                m_Socket.Connect(m_Host, m_Port);
            }
            catch (Exception)
            {
                UpdateNetState(NetStatus.ConnectFailed);
                return false;
            }

            return true;
        }

        private void CheckReceiveNew()
        {

            //打开连接
            if (Open())
            {
                UpdateNetState(NetStatus.ConnectSuccess);
            }
            else
            {
                return;
            }

            //每次获取长度
            int blockLen = 1024;
            int defaultLen = 1024 * 64;
            int allLen = defaultLen;
            byte[] data = new byte[allLen];
            //有效长度
            int usefulLen = 0;
            //开始有效位置
            int startIndex = 0;

            while (true)
            {
                if (m_Socket == null) break;
                Send();
                if (m_Socket == null) continue;

                try
                {
                    if (m_Socket.Poll(5, SelectMode.SelectRead))
                    {
                        if (m_Socket.Available == 0)
                        {
                            Debug.Log("Close Socket");
                            UpdateNetState(NetStatus.ERROR);
                            break;
                        }
                        bool loop = true;
                        while (loop)
                        {
                            loop = false;
                            //判断是否超过
                            if ((defaultLen - (startIndex + usefulLen)) < blockLen)
                            {
                                for (int i = 0; i < usefulLen; i++) data[i] = data[startIndex + i];
                                startIndex = 0;
                                //Debug.LogError("判断是否超过!!");
                            }
                            int rev = m_Socket.Receive(data, startIndex + usefulLen, blockLen, SocketFlags.None);
                            
                            //Debug.Log("消息" + rev);

                            if (rev > 0)
                            {
                                usefulLen += rev;
                                while (usefulLen > 2)
                                {
                                    byte[] lengthBt = new MemoryStream(data, startIndex, 2).ToArray();

                                    int datalen = lengthBt[1] * 256 + lengthBt[0];
                                    //BitConverter.ToInt32(lengthBt, 0);

                                    int completeLen = datalen + 2;
                                    if (usefulLen < completeLen)
                                    {
                                        if (completeLen > blockLen && rev == blockLen)
                                        {
                                            loop = true;
                                        }
                                        break;
                                    }

                                    byte[] receiveData = new MemoryStream(data, startIndex + 2, datalen).ToArray();

                                    m_ReceiveQueue.Enqueue(receiveData);

                                    usefulLen -= completeLen;
                                    startIndex += completeLen;
                                }
                            }
                        }
                    }
                    else if (m_Socket.Poll(5, SelectMode.SelectError))
                    {
                        UpdateNetState(NetStatus.ERROR);
                        Debug.Log("SelectError Close Socket");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    UpdateNetState(NetStatus.ERROR);
                    Debug.Log("catch" + ex.ToString());
                }
                Thread.Sleep(50);
            }
        }

        // 发送数据
        public bool Send()
        {
            if (m_SendMsgQueue.Count > 0)
            {
                byte[] sendData = m_SendMsgQueue.Dequeue();

                if (m_Socket != null)
                {
                    int len = sendData.Length;

                    int sendcount = 0;

                    while (sendcount < len)
                    {
                        int ret = m_Socket.Send(sendData, sendcount, len - sendcount, SocketFlags.None);

                        //如果这==0说明有情况了
                        if (ret == 0)
                        {
                            UpdateNetState(NetStatus.ERROR);
                        }
                        sendcount += ret;
                    }
                }
            }
            return true;
        }

        //保存要发送的数据
        public void SaveMessage(byte[] data)
        {
            //ByteBuffer byteBuffer = new ByteBuffer();
            //byteBuffer.WriteBytes(data);
            //byteBuffer.ToBytes()
            m_SendMsgQueue.Enqueue(data);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void Close()
        {
            if (m_Socket == null)
            {
                return;
            }
            try
            {
                //断掉Socket的连接
                if (m_Socket.Connected)
                {
                    m_Socket.Shutdown(SocketShutdown.Both);
                }
                //关闭整个Socket
                m_Socket.Close();
                m_Socket = null;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
                m_Socket = null;
            }
        }

        /// <summary>
        /// 主观关闭
        /// </summary>
        public void SubjectiveClose()
        {
            Close();
            //这个时候是玩家主动关闭网络，所以可能是重登
            //不要重连
            UpdateNetState(NetStatus.Close);
        }

        /// <summary>
        /// socket数据的状态值
        /// </summary>
        enum SocketDataStatus
        {
            Write_OK,       //可写
            Read_OK,        //可读
            No              //数据异常断开
        }

        //得到网络状态
        public void UpdateNetState(NetStatus status)
        {
            if (status == NetStatus.ERROR)
            {
                //关掉整个Socket
                Close();
            }
            m_NetStatusQuene.Enqueue(status);
        }
    }
}
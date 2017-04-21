using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Network
{
    class BufferManager
    {
        int m_numBytes;                 // the total number of bytes controlled by the buffer pool
        byte[] m_buffer;                // the underlying byte array maintained by the Buffer Manager
        Stack<int> m_freeIndexPool;     // 
        int m_currentIndex;
        int m_bufferSize;

        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }

        /// <summary>
        /// Allocates buffer space used by the buffer pool
        /// </summary>
        public void InitBuffer()
        {
            // create one big large buffer and divide that out to each SocketAsyncEventArg object
            m_buffer = new byte[m_numBytes];
        }

        /// <summary>
        /// Assigns a buffer from the buffer pool to the specified SocketAsyncEventArgs object
        /// </summary>
        /// <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            lock(this)
            {
                if (m_freeIndexPool.Count > 0)
                {
                    args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
                }
                else
                {
                    if ((m_numBytes - m_bufferSize) < m_currentIndex)
                    {
                        return false;
                    }
                    args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                    m_currentIndex += m_bufferSize;
                }
                return true;
            }
        }

        public bool SetBuffer(SocketAsyncEventArgs args, byte[] buffer, int offset, int count)
        {
            lock (this)
            {
                if (m_freeIndexPool.Count > 0)
                {
                    int _offset = m_freeIndexPool.Pop();
                    Buffer.BlockCopy(buffer, offset, m_buffer, _offset, count);
                    args.SetBuffer(m_buffer, _offset, count);
                }
                else
                {
                    if ((m_numBytes - m_bufferSize) < m_currentIndex)
                    {
                        return false;
                    }
                    Buffer.BlockCopy(buffer, offset, m_buffer, m_currentIndex, count);
                    args.SetBuffer(m_buffer, m_currentIndex, count);
                    m_currentIndex += m_bufferSize;
                }
                return true;
            }
        }

        /// <summary>
        /// Removes the buffer from a SocketAsyncEventArg object.  This frees the buffer back to the 
        /// buffer pool
        /// </summary>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            lock(this)
            {
                m_freeIndexPool.Push(args.Offset);
                args.SetBuffer(null, 0, 0);
            }
        }
    }
}

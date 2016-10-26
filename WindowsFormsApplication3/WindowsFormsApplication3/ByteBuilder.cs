//! Copyright (C) 2007 Phychips
//! 
//! ByteBuilder.cs
//!
//! Description
//! 	ByteBuilder
//!-------------------------------------------------------------------
//! History
//!-------------------------------------------------------------------
//! 1.0	2007/09/01	Jinsung Yi		Initial Release

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Phychips.Helper
{
    public class ByteBuilder
    {
        private readonly int MAX_CAPACITY = (256*256);
        private readonly int DEFAULT_CAPACITY = (256);
        private byte[] m_ArrayBytes;
        private int m_nCapacity;
        private int m_nPos;

        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            IStructuralEquatable eqa1 = a1;
            return eqa1.Equals(a2, StructuralComparisons.StructuralEqualityComparer);
        } 
        
        public ByteBuilder(int nCap)
        {
            m_nCapacity = nCap;
            m_ArrayBytes = new byte[m_nCapacity];  
            m_nPos = 0;
        }

        public ByteBuilder(byte[] byteArray)
        {
            if (byteArray == null) 
                return;
            m_nCapacity = byteArray.Length;
            m_ArrayBytes = byteArray;
            m_nPos = byteArray.Length;
        }

        public ByteBuilder()
        {
            m_nCapacity = DEFAULT_CAPACITY;
            m_ArrayBytes = new byte[m_nCapacity];            
            m_nPos = 0;
        }
                
        public void Append(sbyte b)
        {
            if (m_nPos < Capacity - 2)
            {
                m_ArrayBytes[m_nPos++] = (byte) b;
            }
            else
            {
                Capacity += DEFAULT_CAPACITY;
                m_ArrayBytes[m_nPos++] = (byte) b;
            }
            
        }

        public void Append(byte b)
        {
            if (m_nPos < Capacity - 2)
            {
                m_ArrayBytes[m_nPos++] = b;
            }
            else
            {
                Capacity += DEFAULT_CAPACITY;
                m_ArrayBytes[m_nPos++] = b;
            }

        }

        public void Append(byte[] ba)
        {
            if (ba == null || ba.Length == 0) return;

            for (int i = 0; i < ba.Length; i++)
            {
                this.Append((byte)ba[i]);
            }
 
        }

        public void Append(byte[] ba, int length)
        {
            if (ba == null || ba.Length == 0) return;

            for (int i = 0; i < length; i++)
            {
                this.Append((byte)ba[i]);
            }

        }

        public void Append(UInt16 val)
        {
            byte[] ba = BitConverter.GetBytes(val);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(ba);
            }

            Append(ba);
        }

        public int Length
        {
            get
            {
                return m_nPos;
            }
        }

        public int Capacity
        {
            get
            {
                return m_nCapacity;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "ArgumentOutOfRange NegativeCapacity");

                if (value < this.m_ArrayBytes.Length)
                {
                    throw new ArgumentOutOfRangeException("value", "ArgumentOutOfRange SmallCapacity");
                }
                if (value > this.MAX_CAPACITY)
                {
                    throw new ArgumentOutOfRangeException("value", "ArgumentOutOfRange Capacity");
                }

                int currentCapacity = this.m_ArrayBytes.Length;
                if (value != currentCapacity)
                {
                    Array.Resize(ref m_ArrayBytes, value);
                }

                m_nCapacity = value;
            }
        }

        public void Clear()
        {
            m_nCapacity = DEFAULT_CAPACITY;
            m_ArrayBytes = new byte[m_nCapacity];
            m_nCapacity = DEFAULT_CAPACITY;
            m_nPos = 0;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_nPos; i++)
            {
                sb.AppendFormat(m_ArrayBytes[i].ToString("X2"));
            }           
            return sb.ToString();            
        }

        public string ToString(string format)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_nPos; i++)
            {
                sb.AppendFormat(m_ArrayBytes[i].ToString(format));                
            }
            return sb.ToString();
        }

        public byte[] GetByteArray()
        {
            byte[] ret = new byte[m_nPos];
            Array.Copy(m_ArrayBytes, ret, m_nPos);
            return ret;
        }

        public byte[] GetByteArray(int index, int length)
        {
            byte[] ret = new byte[length];
 //           Array.Copy(m_ArrayBytes, ret, m_nPos);
            Array.Copy(m_ArrayBytes, index, ret, 0, length);
            return ret;
        }

        //20110216 Add Function (by sjpark)s
        public byte GetAt(int index)
        {
            
            return m_ArrayBytes[index];

        }


    }
}

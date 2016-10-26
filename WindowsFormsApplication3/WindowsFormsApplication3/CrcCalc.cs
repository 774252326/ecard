//! Copyright (C) 2007 Phychips
//! 
//! CrcCalc.cs
//!
//! Description
//! 	CRC Calculator
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
 
    public class CrcCalc 
    {
        static public BitArray Crc16Calc(bool[] inputData)
        {
            bool[] C = new bool[16];
            bool[] outputData = new bool[inputData.Length];

            for (int i = 0; i < 16; i++)
            {
                C[i] = true;
            }

            for (int i = 0; i < inputData.Length; i++)
            {
                bool boolInput = C[15] ^ inputData[i];
                C[4] = C[4] ^ boolInput;
                C[11] = C[11] ^ boolInput;

                for (int j = 15; j > 0; j--)
                {
                    C[j] = C[j - 1];
                }

                C[0] = boolInput;

            }

            BitArray ba = new BitArray(C);

            return ba.Not();
        }


        static public BitArray Crc5Calc(bool[] inputData)
        {
            bool[] C = new bool[5];
            bool[] outputData = new bool[inputData.Length];

            C[4] = false;
            C[3] = true;
            C[2] = false;
            C[1] = false;
            C[0] = true;


            for (int i = 0; i < inputData.Length; i++)
            {
                bool boolInput = C[4] ^ inputData[i];
                C[2] = C[2] ^ boolInput;

                for (int j = 4; j > 0; j--)
                {
                    C[j] = C[j - 1];
                }

                C[0] = boolInput;

                
            }

            BitArray ba = new BitArray(C);

            return ba;
        }

    }
}

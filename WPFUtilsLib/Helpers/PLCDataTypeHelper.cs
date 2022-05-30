﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUtilsLib.Helpers
{
    public static class PLCDataTypeHelper
    {
        public static bool PLCGetBool(byte[] databuffer, int position, int bit)
        {
            return Sharp7.S7.GetBitAt(databuffer, position, bit);
        }

        public static ushort PLCGetWord(byte[] databuffer, int position)
        {
            return Sharp7.S7.GetWordAt(databuffer, position);
        }

        public static byte PLCGetByte(byte[] databuffer, int position)
        {
            return Sharp7.S7.GetByteAt(databuffer, position);
        }

        public static void PLCSetBool(byte[] databuffer, int position, int bit, bool value)
        {
            Sharp7.S7.SetBitAt(databuffer, position, bit, value);
        }
    }
}

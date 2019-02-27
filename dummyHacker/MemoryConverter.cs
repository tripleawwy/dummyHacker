﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dummyHacker
{
    public static class MemoryConverter
    {

        public static byte[] TextBoxContentAsByteArray(string textboxtext, int InputType)
        {
            byte[] textboxContent = new byte[0];
            switch (InputType)
            {
                case 1:
                    if (byte.TryParse(textboxtext, out byte result1))
                    {
                        textboxContent = new byte[1] { result1 };
                    }
                    break;
                case 2:
                    if (short.TryParse(textboxtext, out short result2))
                    {
                        textboxContent = new byte[2] { (byte)(result2 & 255), (byte)((result2 >> 8) & 255) };
                    }
                    break;
                case 4:
                    if (int.TryParse(textboxtext, out int result4))
                    {
                        textboxContent = new byte[4] { (byte)(result4 & 255)
                            , (byte)((result4 >> 8) & 255)
                            , (byte)((result4 >> 16) & 255)
                            , (byte)((result4 >> 24) & 255) };
                    }
                    break;
                case 8:
                    if (long.TryParse(textboxtext, out long result8))
                    {
                        textboxContent = new byte[8] { (byte)(result8 & 255)
                            , (byte)((result8 >> 8) & 255)
                            , (byte)((result8 >> 16) & 255)
                            , (byte)((result8 >> 24) & 255)
                            , (byte)((result8 >> 32) & 255)
                            , (byte)((result8 >> 40) & 255)
                            , (byte)((result8 >> 48) & 255)
                            , (byte)((result8 >> 56) & 255)};
                    }
                    break;
                default:
                    textboxContent = new byte[textboxtext.Length];
                    byte[] arsch = System.Text.Encoding.Default.GetBytes(textboxtext);

                    for (int i = 0; i < arsch.Length; i++)
                    {
                        textboxContent[i] = arsch[i];
                    }
                    break;
            }
            return textboxContent;
        }

    }
}

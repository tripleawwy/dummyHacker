using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dummyHacker
{
    public struct DatagridSource
    {
        public DatagridSource(string address, string value, string previousValue)
        {
            Address = address;
            Value = value;
            PreviousValue = previousValue;
        }
        public string Address { get; set; }
        public string Value { get; set; }
        public string PreviousValue { get; set; }
    }


    public static class MemoryConverter
    {

        public static List<DatagridSource> CreateDataGrid(List<List<ScanStructure>> scanLists, bool isString)
        {
            List<DatagridSource> datagrid = new List<DatagridSource>();
            string _value = "";
            string _previousValue;


            _value = ByteArrayToString(scanLists, isString, 1);
            if (scanLists.Count() == 1)
            {
                _previousValue = _value;
            }
            else
            {
                _previousValue = ByteArrayToString(scanLists, isString, 2);
            }

            foreach (ScanStructure pair in scanLists.Last())
            {
                DatagridSource element = new DatagridSource(pair.Address.ToString("X8"), _value, _previousValue);
                datagrid.Add(element);
            }
            return datagrid;
        }

        private static string ByteArrayToString(List<List<ScanStructure>> scanLists, bool IsString, int listNumber)
        {
            string value = "";

            if (!IsString)
            {
                switch (scanLists.Last().ElementAt(0).Value.Length)
                {
                    case 1:
                        value = (scanLists.ElementAt(scanLists.Count() - 1).ElementAt(0).Value[0]).ToString();
                        break;
                    case 2:
                        value = BitConverter.ToInt16(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(0).Value, 0).ToString();
                        break;
                    case 4:
                        value = BitConverter.ToInt32(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(0).Value, 0).ToString();
                        break;
                    case 8:
                        value = BitConverter.ToInt64(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(0).Value, 0).ToString();
                        break;
                }
            }
            else
            {
                value = System.Text.Encoding.Default.GetString(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(0).Value);
            }
            return value;
        }




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

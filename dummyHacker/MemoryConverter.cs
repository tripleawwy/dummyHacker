using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace dummyHacker
{
    public struct DatagridSource : IComparable<DatagridSource>
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

        public int CompareTo(DatagridSource other)
        {
            return this.Address.CompareTo(other.Address);
        }
    }


    public static class MemoryConverter
    {

        public static List<string[]> CreateDataGridForPointer(List<List<uint[]>> scanLists)
        {
            Process process = Process.GetProcessById((int)scanLists.Last().ElementAt(0)[1]);
            List<string[]> datagrid = new List<string[]>();
            string _offset;
            string moduleName = "";



            if (scanLists.Last().Count() == 0)
            {
                datagrid.Add(new string[] { "no results found", "no results found", "no results found" });
                return datagrid;
            }
            else
            {
                foreach (uint[] structure in scanLists.Last())
                {
                    foreach (ProcessModule item in process.Modules)
                    {
                        if (structure[0] >= (uint)item.BaseAddress && structure[0] <= (uint)(item.BaseAddress + item.ModuleMemorySize))
                        {
                            moduleName = item.ModuleName;
                        }
                        else moduleName = "";
                    }

                    _offset = "+" + structure[2].ToString("X");
                    string[] element = new string[] { structure[0].ToString("X8"), moduleName, _offset };
                    datagrid.Add(element);
                }
                return datagrid;
            }
        }

        public static List<DatagridSource> CreateDataGrid(List<List<ScanStructure>> scanLists, bool isString)
        {
            List<DatagridSource> datagrid = new List<DatagridSource>();
            string _value = "";
            string _previousValue;



            if (scanLists.Last().Count() == 0)
            {
                datagrid.Add(new DatagridSource("no results found", "no results found", "no results found"));
                return datagrid;
            }
            else
            {
                _value = ByteArrayToString(scanLists, isString, 0, 1);
                if (scanLists.Count() == 1)
                {
                    _previousValue = _value;
                }
                else
                {
                    _previousValue = ByteArrayToString(scanLists, isString, 0, 2);
                }

                foreach (ScanStructure pair in scanLists.Last())
                {
                    DatagridSource element = new DatagridSource(pair.Address.ToString("X8"), _value, _previousValue);
                    datagrid.Add(element);
                }
                return datagrid;
            }
        }

        public static List<DatagridSource> RefreshDatagrid(List<List<ScanStructure>> scanLists, bool isString)
        {
            List<DatagridSource> output = new List<DatagridSource>();
            string _previousValue = ByteArrayToString(scanLists, isString, 0, 2);

            int i = 0;
            foreach (ScanStructure pair in scanLists.Last())
            {
                output.Add(new DatagridSource(pair.Address.ToString("X8"), ByteArrayToString(scanLists, isString, i, 1), _previousValue));
                i++;
            }

            return output;
        }


        private static string ByteArrayToString(List<List<ScanStructure>> scanLists, bool IsString, int controlVariable, int listNumber)
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
                        value = BitConverter.ToInt16(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(controlVariable).Value, 0).ToString();
                        break;
                    case 4:
                        value = BitConverter.ToInt32(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(controlVariable).Value, 0).ToString();
                        break;
                    case 8:
                        value = BitConverter.ToInt64(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(controlVariable).Value, 0).ToString();
                        break;
                }
            }
            else
            {
                value = System.Text.Encoding.Default.GetString(scanLists.ElementAt(scanLists.Count() - listNumber).ElementAt(0).Value);
            }
            return value;
        }




        public static byte[] TextBoxContentToByteArray(string textboxtext, int InputType)
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

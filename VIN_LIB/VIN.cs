using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VIN_LIB {
    public class VIN {
        static string ValidSimb = "0 1 2 3 4 5 6 7 8 9 A B C D E F G H J K L M N P R S T U V W X Y Z".Replace(" ", "");
        private Dictionary<string, int> years;

        public VIN() { 
            years = new Dictionary<string, int>();
		    years.Add("A", 1980);
		    years.Add("B", 1981);
		    years.Add("C", 1982);
		    years.Add("D", 1983);
		    years.Add("E", 1984);
		    years.Add("F", 1985);
		    years.Add("G", 1986);
		    years.Add("H", 1987);
		    years.Add("J", 1988);
		    years.Add("K", 1989);
		    years.Add("L", 1990);
		    years.Add("M", 1991);
		    years.Add("N", 1992);
		    years.Add("P", 1992);
		    years.Add("R", 1994);
		    years.Add("S", 1995);
		    years.Add("T", 1996);
		    years.Add("V", 1997);
		    years.Add("W", 1998);
		    years.Add("X", 1999);
		    years.Add("Y", 2000);
		    years.Add("1", 2001);
		    years.Add("2", 2002);
		    years.Add("3", 2003);
		    years.Add("4", 2004);
		    years.Add("5", 2005);
		    years.Add("6", 2006);
		    years.Add("7", 2007);
		    years.Add("8", 2008);
		    years.Add("9", 2009);
        }

        static public bool CheckVIN(string vin) {
            Console.WriteLine(ValidSimb);

            if (vin.Length != 17)
                return false;

            foreach (var symb in vin)
                if (!ValidSimb.Contains(symb))
                    return false;

            return true;
        }

        static public void VINCountryAdd() {
            var file = File.ReadAllLines("codes.txt");

            for (int i = 1; i < file.Length; i += 2)  {
                var CodeRange = file[i - 1];
                var Country = file[i];
                File.AppendAllLines("text.txt", CodeListFromRange(CodeRange, Country));
            }

        }
        static private List<string> CodeListFromRange(string CodeRange, string Counrty) {
            var listCode = new List<string>();
            string startPos = CodeRange.Split('-')[0];
            string endPos = startPos;

            try {
                endPos = CodeRange.Split('-')[1];
            }
            catch (Exception) {
                endPos = startPos;
            }

            bool isWrireover = false;
            bool isWriteStart = false;

            foreach (var symb in ValidSimb) {
                if (symb == startPos[1])
                    isWriteStart = true;
                if (isWriteStart)
                    listCode.Add(startPos[0] + symb.ToString());
                if (isWriteStart && symb == endPos[1]) {
                    isWrireover = true;
                    break;
                }
            }

            if (isWrireover == false) 
                foreach (var symb in ValidSimb) {
                    listCode.Add(startPos[0] + symb.ToString());
                    if (symb == endPos[1]) {
                        isWrireover = true;
                        break;
                    }
                }
            
            var listCodeWithCountry = new List<string>();

            listCode.ForEach(p => listCodeWithCountry.Add(String.Format(p + "|" + Counrty)));
            return listCodeWithCountry;

        }

        static public string GetVINCounty(string vin) {
            var CodesCounty = File.ReadAllLines("text.txt");

            if (!CheckVIN(vin))
                return "Неверный vin";
            string countryCodeVIN = vin[0].ToString() + vin[1].ToString();

            foreach (var item in CodesCounty)
                if (item.Contains(countryCodeVIN))
                    return item.Split('|')[1];
            
            return "Не используется";
        }

        public int GetTransportYear(string vin) {
            string key = vin[9].ToString();
            return years[key];
        }
    }
}

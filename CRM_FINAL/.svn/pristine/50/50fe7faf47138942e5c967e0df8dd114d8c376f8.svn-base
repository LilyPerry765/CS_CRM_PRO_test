using System;
using System.Collections.Generic;
using System.Text;

public class Codeconvert
{
    public string[] map = new string[256];
    public void init()
    {
        map[32] = " ";
        map[33] = "!"; map[34] = "\""; map[35] = "#"; map[36] = "$";
        map[37] = "%"; map[38] = "&"; map[39] = "'"; map[40] = "(";
        map[41] = ")"; map[42] = "*"; map[43] = "+"; map[44] = ",";
        map[45] = "-"; map[46] = "."; map[47] = "/";

        map[48] = "0"; map[49] = "1"; map[50] = "2"; map[51] = "3";
        map[52] = "4"; map[53] = "5"; map[54] = "6"; map[55] = "7";
        map[56] = "8"; map[57] = "9"; map[58] = ":"; map[59] = ";";
        map[60] = "<"; map[61] = "="; map[62] = ">"; map[63] = "?";
        map[64] = "@"; map[65] = "A"; map[66] = "B"; map[67] = "C";
        map[68] = "D"; map[69] = "E"; map[70] = "F"; map[71] = "G";
        map[72] = "H"; map[73] = "I"; map[74] = "J"; map[75] = "K";
        map[76] = "L"; map[77] = "M"; map[78] = "N"; map[79] = "O";
        map[80] = "P"; map[81] = "Q"; map[82] = "R"; map[83] = "S";
        map[84] = "T"; map[85] = "U"; map[86] = "V"; map[87] = "W";
        map[88] = "X"; map[89] = "Y"; map[90] = "Z"; map[91] = "[";
        map[92] = "\\"; map[93] = "]"; map[94] = "^"; map[95] = "_";
        map[96] = "`"; map[97] = "a"; map[98] = "b"; map[99] = "c";
        map[100] = "d"; map[101] = "e"; map[102] = "f"; map[103] = "g";
        map[104] = "h"; map[105] = "i"; map[106] = "j"; map[107] = "k";
        map[108] = "l"; map[109] = "m"; map[110] = "n"; map[111] = "o";
        map[112] = "p"; map[113] = "q"; map[114] = "s"; map[115] = "s";
        map[116] = "t"; map[117] = "u"; map[118] = "v"; map[119] = "w";
        map[120] = "x"; map[121] = "y"; map[122] = "z"; map[123] = "{";
        map[124] = "|"; map[125] = "}"; map[126] = "~";

        map[128] = "0"; map[129] = "1"; map[130] = "2"; map[131] = "3";
        map[132] = "4"; map[133] = "5"; map[134] = "6"; map[135] = "7";
        map[136] = "8"; map[137] = "9"; map[138] = "،"; map[139] = "-";
        map[140] = "؟"; map[141] = "آ"; map[142] = "ئ"; map[143] = "ء";
        map[144] = "ا"; map[145] = "ا"; map[146] = "ب "; map[147] = "ب";
        map[148] = "پ "; map[149] = "پ"; map[150] = "ت "; map[151] = "ت";
        map[152] = "ث "; map[153] = "ث"; map[154] = "ج "; map[155] = "ج";
        map[156] = "چ "; map[157] = "چ"; map[158] = "ح "; map[159] = "ح";
        map[160] = "خ "; map[161] = "خ"; map[162] = "د"; map[163] = "ذ";
        map[164] = "ر"; map[165] = "ز"; map[166] = "‍‍‍ژ"; map[167] = "س ";
        map[168] = "س"; map[169] = "ش "; map[170] = "ش"; map[171] = "ص ";
        map[172] = "ص"; map[173] = "ض "; map[174] = "ض"; map[175] = "ط";
        map[224] = "ظ"; map[225] = "ع "; map[226] = "ع "; map[227] = "ع";
        map[228] = "ع"; map[229] = "غ "; map[230] = "غ"; map[231] = "غ";
        map[232] = "غ"; map[233] = "ف "; map[234] = "ف"; map[235] = "ق ";
        map[236] = "ق"; map[237] = "ك "; map[238] = "ك"; map[239] = "گ ";
        map[240] = "گ"; map[241] = "ل "; map[242] = "لا"; map[243] = "ل";
        map[244] = "م "; map[245] = "م"; map[246] = "ن "; map[247] = "ن";
        map[248] = "و"; map[249] = "ه "; map[250] = "ه"; map[251] = "ه";
        map[252] = "ی "; map[253] = "ی "; map[254] = "ی";
    }

    public string Dos_Convert_ToWin(byte[] Dos_Text)
    {
        string Result = "";
        string[] col = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string ch;
        int Num;
        string st_num = "";
        bool isNum = false;

        for (int i = Dos_Text.Length - 1; i >= 0; i--)
        {
            ch = map[Dos_Text[i]];
            isNum = int.TryParse(ch, out Num);
            if ((isNum == true) || (ch == ".") || ch == "-" || ch == "/")
            {
                st_num = ch + st_num;

            }
            else
            {
                Result = Result + st_num + map[Dos_Text[i]];
                st_num = "";
            }
        }
        if (st_num != "")
        {
            Result = st_num + Result;
        }
        Result = Result.Replace("'", "");

        return Result;
    }
}


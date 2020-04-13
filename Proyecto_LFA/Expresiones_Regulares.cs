using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Expresiones_Regulares
    {
        public static void LlenarOP(List<char> operadores)
        {
            operadores.Add('*');
            operadores.Add('+');
            operadores.Add('|');
            operadores.Add('?');
            operadores.Add('(');
            operadores.Add(')');
            operadores.Add('.');//CONCATENACION
        }
        public static void Generar_ST(List<char> operadores, List<char> tipo, string expresion_Regular)
        {
            for (int i = 0; i < expresion_Regular.Length; i++)
            {
                if (!operadores.Contains(expresion_Regular[i]) && expresion_Regular[i] != '\\' && !tipo.Contains(expresion_Regular[i]))
                {
                    tipo.Add(expresion_Regular[i]);
                }
                if (i - 1 > 0)
                {
                    if (expresion_Regular[i - 1] == '\\' && !tipo.Contains(expresion_Regular[i]))
                    {
                        tipo.Add(expresion_Regular[i]);
                    }
                }
            }
        }

        public static void ST_Sintactico(List<string> sT, List<char> operadores, string expresion_Regular)
        {
            for (int i = 0; i < expresion_Regular.Length; i++)
            {
                if (char.IsLetter(expresion_Regular[i]) && i+1 < expresion_Regular.Length)
                {
                    var tmp = string.Empty;
                    while (char.IsLetter(expresion_Regular[i]))
                    {
                        tmp += expresion_Regular[i];
                        i++;
                    }
                    if (!sT.Contains(tmp))
                    {
                        sT.Add(tmp);
                    }
                }
                if (!operadores.Contains(expresion_Regular[i]) && expresion_Regular[i] != '\\' && !sT.Contains(expresion_Regular[i].ToString()))
                {
                    sT.Add(expresion_Regular[i].ToString());
                }
                if (i - 1 > 0)
                {
                    if (expresion_Regular[i - 1] == '\\' && !sT.Contains(expresion_Regular[i].ToString()))
                    {
                        sT.Add(expresion_Regular[i].ToString());
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_LFA
{
    public class ErrorMessage
    {
        public static string errorMessage;
        public static bool findError;
    }
    public class Implementation
    {
        public static char[] reservedWordSETS = { 'S', 'E', 'T', 'S' };
        public static char[] reservedWordTOKENS = { 'T', 'O', 'K', 'E', 'N', 'S' };
        public static char[] reservedWordACTIONS = { 'A', 'C', 'T', 'I', 'O', 'N', 'S' };
        public static char[] reservedWordRESERVADAS = { 'R', 'E', 'S', 'E', 'R', 'V', 'A', 'D', 'A', 'S', '(', ')' };
        public static List<string> text = new List<string>();
        public static List<string> originalList = new List<string>();

        public static bool EmptyFile(string path)
        {
            FileInfo propieties = new FileInfo(path);
            if (propieties.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static bool OnlySpaces(string line)
        {
            var flag = false;
            if (flag = line.All(x => x == ' ') == true)
            {
                return true;
            }
            return false;
        }

        public static bool OnlyTabs(string line)
        {
            var flag = false;
            if (flag = line.All(x => x == '\t') == true)
            {
                return true;
            }
            return false;
        }

        public static void ReadFile(string filePath, Node treeSETS, Node treeTOKENS, Node treeACTIONS, Node treeERROR)
        {
            var line = string.Empty;
            var columnError = 0;
            var firstChar = new char();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    originalList.Add(line);
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        if (OnlySpaces(line) == false && OnlyTabs(line) == false)
                        {
                            text.Add(line.Trim('\t'));
                        }
                    }
                }
                reader.Close();
            }

            var firstLine = text[0].Trim(' ').ToCharArray();
            switch (firstLine[0])
            {
                case 'S':
                    if (AnalyzeReservedWord(reservedWordSETS, firstLine, 0, ref columnError, 1) != true)
                    {
                        ShowError(ErrorMessage.errorMessage);
                        ErrorMessage.findError = true;
                    }
                    firstChar = 'S';
                    break;
                case 'T':
                    if (AnalyzeReservedWord(reservedWordTOKENS, firstLine, 0, ref columnError, 1) != true)
                    {
                        ShowError(ErrorMessage.errorMessage);
                        ErrorMessage.findError = true;
                    }
                    firstChar = 'T';
                    break;
                default:
                    ErrorMessage.errorMessage = $"Error al inicio del archivo: SE ESPERABA DEFINICIÓN DE TOKENS.";
                    ShowError(ErrorMessage.errorMessage);
                    ErrorMessage.findError = true;
                    break;
            }
            var i = 1;
            if (ErrorMessage.findError == false)
            {
                var grammarStart = i;
                if (firstChar == 'S')
                {
                    while (text[i].Contains("TOKENS") == false && ErrorMessage.findError == false)
                    {
                        var prueba = text[i].Contains("TOKENS");
                        var filtro = text[i].ToCharArray();
                        if (filtro[filtro.Length - 1] == '\'' || filtro[filtro.Length - 1] == ')' || filtro[filtro.Length - 1] == ' ')
                        {
                            columnError = 0;
                            CompareTree(treeSETS, text[i], ref columnError, InitialForm.stSETS, i);
                            i++;
                        }
                        else
                        {
                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA.";
                            ErrorMessage.findError = true;
                            ShowError(ErrorMessage.errorMessage);
                        }
                    }
                    if (i - grammarStart == 0 && ErrorMessage.findError == false)
                    {
                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA DEFINICIÓN DE SETS.";
                        ErrorMessage.findError = true;
                        ShowError(ErrorMessage.errorMessage);
                    }
                    grammarStart = i;
                }
                var LineaTokensLeida = false;
                while (text[i].Contains("ACTIONS") == false && ErrorMessage.findError == false)
                {
                    if (LineaTokensLeida == false && firstChar == 'S')
                    {
                        if (AnalyzeReservedWord(reservedWordTOKENS, text[i].ToCharArray(), 0, ref columnError, 1) != true)
                        {
                            ShowError(ErrorMessage.errorMessage);
                            ErrorMessage.findError = true;
                        }
                        LineaTokensLeida = true;
                        i++;
                    }
                    else
                    {
                        var filtro = text[i].ToCharArray();
                        var cerrar = false;
                        if (text[i].Contains('(') && text[i].Contains(')') == false)
                        {
                            cerrar = true;
                        }
                        if (filtro[filtro.Length - 1] == '\'' || filtro[filtro.Length - 1] == '}' || filtro[filtro.Length - 1] == ' ' || filtro[filtro.Length - 1] == '*' && cerrar == false)
                        {
                            columnError = 0;
                            CompareTree(treeTOKENS, text[i], ref columnError, InitialForm.stTOKENS, i);
                            i++;
                        }
                        else
                        {
                            if (text[i].Contains('(') && !text[i].Contains(')'))
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA ) EN TOKEN.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                            else
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                        }
                    }
                }
                if (i - grammarStart == 0 && ErrorMessage.findError == false)
                {
                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA: SE ESPERABA LA DEFINICIÓN DE ALGUN TOKEN.";
                    ErrorMessage.findError = true;
                    ShowError(ErrorMessage.errorMessage);
                }

                if (AnalyzeReservedWord(reservedWordACTIONS, text[i].ToCharArray(), 0, ref columnError, i) == true && ErrorMessage.findError == false)
                {
                    columnError = 0;
                    i++;
                    if (AnalyzeReservedWord(reservedWordRESERVADAS, text[i].ToCharArray(), 0, ref columnError, i) == true && ErrorMessage.findError == false)
                    {
                        i++;
                        if (text[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (text[i].Contains("}") == false && i < text.Count && ErrorMessage.findError == false)
                            {
                                columnError = 0;
                                var filtro = text[i].ToCharArray();
                                if (filtro[filtro.Length - 1] == '\'')
                                {
                                    CompareTree(treeACTIONS, text[i], ref columnError, InitialForm.stACTIONS, i);
                                    i++;
                                }
                                else
                                {
                                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA.";
                                    ErrorMessage.findError = true;
                                    ShowError(ErrorMessage.errorMessage);
                                }
                            }
                            if (i - contador_Actions == 0 || i == text.Count && ErrorMessage.findError == false)
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA " + "}.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                            if (text[i] == "}" && i < text.Count)
                            {
                                i++;
                            }
                            else
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA " + "}" + "DE LA FUNCIÓN.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA PALABRA RESERVADA CON ACTIONS.";
                        ErrorMessage.findError = true;
                        ShowError(ErrorMessage.errorMessage);
                    }
                }
                else
                {
                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA ACTIONS.";
                    ErrorMessage.findError = true;
                    ShowError(ErrorMessage.errorMessage);
                }
                while (text[i].Contains("ERROR") == false && i < text.Count && ErrorMessage.findError == false)
                {
                    if (text[i].Contains('(') && text[i].Contains(')'))
                    {
                        i++;
                        if (text[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (text[i].Contains("}") == false && i < text.Count && ErrorMessage.findError == false)
                            {
                                columnError = 0;
                                var filtro = text[i].ToCharArray();
                                if (filtro[filtro.Length - 1] == '\'')
                                {
                                    CompareTree(treeACTIONS, text[i], ref columnError, InitialForm.stACTIONS, i);
                                    i++;
                                }
                                else
                                {
                                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA.";
                                    ErrorMessage.findError = true;
                                    ShowError(ErrorMessage.errorMessage);
                                }
                            }
                            if (i - contador_Actions == 0 || i == text.Count && ErrorMessage.findError == false)
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA " + "}" + " AL FINAL.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                            if (text[i] == "}" && i < text.Count)
                            {
                                i++;
                            }
                            else
                            {
                                ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA " + "}" + " AL FINAL DE LA FUNCIÓN.";
                                ErrorMessage.findError = true;
                                ShowError(ErrorMessage.errorMessage);
                            }
                        }
                        else
                        {
                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA " + "{" + " AL INCIO DE LA FUNCIÓN.";
                            ErrorMessage.findError = true;
                            ShowError(ErrorMessage.errorMessage);
                        }
                    }
                    else
                    {
                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA DEFINICIÓN DE UNA FUNCIÓN.";
                        ErrorMessage.findError = true;
                        ShowError(ErrorMessage.errorMessage);
                    }
                }
                if (i < text.Count)
                {
                    while (i < text.Count && ErrorMessage.findError == false)
                    {
                        var filtro = text[i].ToCharArray();
                        if (char.IsNumber(filtro[filtro.Length - 1]))
                        {
                            columnError = 0;
                            CompareTree(treeERROR, text[i], ref columnError, InitialForm.stERROR, i);
                            i++;
                        }
                        else
                        {
                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[i])}: SE ESPERABA NÚMEROS, EN ERRORES.";
                            ErrorMessage.findError = true;
                            ShowError(ErrorMessage.errorMessage);
                        }
                    }
                }
            }
            if (ErrorMessage.findError == false && i == text.Count)
            {
                CorrectoFile();
            }
        }
        public static bool AnalyzeReservedWord(char[] reserved, char[] line, int cont, ref int columnError, int lineError)
        {
            if (cont < reserved.Length)
            {
                columnError++;
                if (cont < line.Length)
                {
                    if (line[cont] == reserved[cont])
                    {
                        return AnalyzeReservedWord(reserved, line, cont + 1, ref columnError, lineError);
                    }
                }
            }
            if (cont == reserved.Length)
            {
                return true;
            }
            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(text[lineError])}: SE ESPERABA {reserved[cont]}.";
            return false;
        }

        static bool flagLeftOR = false;
        static bool flagRightOR = false;
        static List<string> ids = new List<string>();
        public static void CompareTree(Node tree, string line, ref int column, List<char> st, int lineError)
        {
            if (tree != null && ErrorMessage.findError == false)
            {
                CompareTree(tree.leftNode, line, ref column, st, lineError);
                if (tree.info == '|' && NodeRoute(tree.leftNode) == true)
                {
                    tree.route = true;
                    flagLeftOR = true;
                }
                else if (tree.info == '|' && flagRightOR == true)
                {
                    flagRightOR = false;
                }
                else if (tree.info == '|' && NodeRoute(tree.leftNode) == false && NodeRoute(tree.rightNode) == false)
                {
                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA UN IDENTIFICADOR.";
                    ErrorMessage.findError = true;
                    ShowError(ErrorMessage.errorMessage);
                }
                if (ErrorMessage.findError == false && flagLeftOR == false && flagRightOR == false && column < line.Length)
                {
                    if (st.Contains(tree.info) && IsLeaf(tree) != false)
                    {
                        if (tree.father.info == '*')
                        {
                            if (tree.info == 'Z')
                            {
                                while (line[column] == 9 || line[column] == 32)
                                {
                                    column++;
                                    if (column == line.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                while (line[column] == tree.info)
                                {
                                    column++;
                                    if (column == line.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (tree.father.info == '+')
                        {
                            var verificador = column;
                            switch (tree.info)
                            {
                                case 'L':
                                    if (flagLeftOR == false)
                                    {
                                        while ((char.IsUpper(line[column]) || line[column] == '*' || line[column] == '|' || line[column] == '(' || line[column] == '{' || line[column] == ')' || line[column] == '}') && ErrorMessage.findError == false)
                                        {
                                            switch (line[column])
                                            {
                                                case '*':
                                                    var item = string.Empty;
                                                    for (int i = verificador; i < column; i++)
                                                    {
                                                        item += line[verificador];
                                                    }
                                                    ids.Add(item);
                                                    break;
                                                case '|':
                                                    if (column < line.Length)
                                                    {
                                                        var contador_S = 0;
                                                        var punto = column;
                                                        while (punto < line.Length)
                                                        {
                                                            if (char.IsUpper(line[punto]))
                                                            {
                                                                contador_S++;
                                                            }
                                                            punto++;
                                                        }
                                                        if (contador_S == 0)
                                                        {
                                                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA EXPRESIÓN LUEGO DE |.";
                                                            ErrorMessage.findError = true;
                                                            ShowError(ErrorMessage.errorMessage);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA EXPRESIÓN LUEGO DE |.";
                                                        ErrorMessage.findError = true;
                                                        ShowError(ErrorMessage.errorMessage);
                                                    }
                                                    break;
                                                case '(':
                                                    if (line.Contains(')') == false)
                                                    {
                                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA ) AL FINAL DE LA EXPRESIÓN.";
                                                        ErrorMessage.findError = true;
                                                        ShowError(ErrorMessage.errorMessage);
                                                    }
                                                    break;
                                                case '{':
                                                    if (line.Contains('}') == false)
                                                    {
                                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA " + "}" + " AL FINAL DE LA EXPRESIÓN.";
                                                        ErrorMessage.findError = true;
                                                        ShowError(ErrorMessage.errorMessage);
                                                    }
                                                    break;
                                            }
                                            column++;
                                            if (column == line.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (column - verificador == 0)
                                        {
                                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA UN IDENTIFICADOR.";
                                            ErrorMessage.findError = true;
                                            ShowError(ErrorMessage.errorMessage);
                                        }
                                    }
                                    break;
                                case 'N':
                                    verificador = column;
                                    while (char.IsNumber(line[column]))
                                    {
                                        column++;
                                        if (column == line.Length)
                                        {
                                            break;
                                        }
                                        //IF BANDERA_ERROR == TRUE ---> DEVOLVER ERROR
                                    }
                                    if (column - verificador == 0)
                                    {
                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA UN NÚMERO.";
                                        ErrorMessage.findError = true;
                                        ShowError(ErrorMessage.errorMessage);
                                    }
                                    break;
                                case ' ':
                                    while (line[column] == ' ')
                                    {
                                        column++;
                                        if (column == line.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (column - verificador == 0)
                                    {
                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: NO VENIA ESPACIOS.";
                                        ErrorMessage.findError = true;
                                        ShowError(ErrorMessage.errorMessage);
                                    }
                                    break;
                                case 'S':
                                    var parentesis_A = false;
                                    var parentesis_C = false;
                                    while (line[column] >= 32 && line[column] < 255 && ErrorMessage.findError == false)
                                    {
                                        if (char.IsLetter(line[column]) == true && char.IsUpper(line[column]) == false)
                                        {
                                            ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABAN MAYUSCULAS.";
                                            ErrorMessage.findError = true;
                                            ShowError(ErrorMessage.errorMessage);
                                        }
                                        column++;
                                        if (column == line.Length)
                                        {
                                            break;
                                        }
                                        if (line[column] == '(')
                                        {
                                            parentesis_A = true;
                                        }
                                        if (line[column] == ')')
                                        {
                                            parentesis_A = true;
                                        }
                                    }
                                    if (column - verificador == 0 && flagLeftOR == false && flagRightOR == false)
                                    {
                                        ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA UN SIMBOLO O TERMINAL.";
                                        ErrorMessage.findError = true;
                                        ShowError(ErrorMessage.errorMessage);
                                    }
                                    break;
                            }
                        }
                        else if (tree.father.info == '.')
                        {
                            var verificador = column;
                            var contador_Simbolos = 0;
                            switch (tree.info)
                            {
                                case 'S':
                                    if (line[column] >= 32 && line[column] < 255)
                                    {
                                        column++;
                                        contador_Simbolos++;
                                    }
                                    break;
                            }
                            if (column - verificador == 0)
                            {
                                if (line[column] == tree.info)
                                {
                                    column++;
                                }
                                else if (line[column] != tree.info && CheckFather(tree, '|') == true)
                                {
                                    flagRightOR = true;
                                }
                                else
                                {
                                    ErrorMessage.errorMessage = $"Error en línea #{ReturnErrorLine(line)}: SE ESPERABA UN {tree.info}";
                                    ErrorMessage.findError = true;
                                    ShowError(ErrorMessage.errorMessage);
                                }
                            }
                        }
                        else if (tree.father.info == '|')
                        {
                            if (line[column] == tree.info)
                            {
                                column++;
                            }
                        }
                    }
                    if ((tree.info == '*' || tree.info == '+') && tree.father.father == null && column < line.Length)
                    {
                        TurnOffRoute(tree);
                        CompareTree(tree, line, ref column, st, lineError);
                    }
                    tree.route = true;
                }
                CompareTree(tree.rightNode, line, ref column, st, lineError);
                if (tree.info == '|' && (NodeRoute(tree.leftNode) == true || NodeRoute(tree.rightNode) == true))
                {
                    flagRightOR = false;
                    flagLeftOR = false;

                }
            }
        }

        public static bool IsLeaf(Node node)
        {
            if (node.rightNode == null && node.leftNode == null)
            {
                return true;
            }
            return false;
        }

        static bool flagFather = false;
        public static bool CheckFather(Node node, char searched)
        {
            while (node.father != null)
            {
                if (node.father.info == searched)
                {
                    flagFather = true;
                }
                return CheckFather(node.father, searched);
            }
            return flagFather;
        }

        public static bool NodeRoute(Node node)
        {
            if (node.route == true)
            {
                return true;
            }
            return false;
        }

        public static void TurnOffRoute(Node node)
        {
            if (node != null)
            {
                TurnOffRoute(node.leftNode);
                node.route = false;
                TurnOffRoute(node.rightNode);
            }
        }

        public static int ReturnErrorLine(string line)
        {
            var cont = 1;
            foreach (var item in originalList)
            {
                if (line == item)
                {
                    return cont;
                }
                cont++;
            }
            return cont;
        }

        public static void CorrectoFile()
        {
            MessageBox.Show("ARCHIVO DE PRUEBA SIN NINGUN ERROR");
        }
        public static void ShowError(string message)
        {
            MessageBox.Show(message);
        }
    }
}

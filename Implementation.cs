using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_LFA
{
    public class MessageError
    {
        public static string messageError;
        public static bool errorExistente;
    }
    public class Implementation
    {
        public static char[] reservedWordSet = { 'S', 'E', 'T', 'S'};
        public static char[] reservedWordToken = { 'T', 'O', 'K', 'E', 'N', 'S' };
        public static char[] reservedWordActions = { 'A', 'C', 'T', 'I', 'O', 'N', 'S' };
        public static char[] reservedWordReservadas = { 'R', 'E', 'S', 'E', 'R', 'V', 'A', 'D', 'A', 'S', '(', ')' };
        public static List<string> text = new List<string>();
        public static List<string> correctText = new List<string>();

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

        public static void ReadFile(string path, Node treeSETS, Node treeTOKENS, Node treeACTIONS, Node treeERROR)
        {
            var line = "";
            var columnError = 0;
            var firstChar = new char();

            using(StreamReader reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    correctText.Add(line);
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        if ((OnlySpaces(line) == false) && (OnlyTabs(line) == false))
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
                    if (analyzeReserved(reservedWordSet, firstLine, 0, ref columnError, 1) != true)
                    {
                        ShowError(MessageError.messageError);
                        MessageError.errorExistente = true;
                    }
                    firstChar = 'S';
                    break;
                case 'T':
                    if (analyzeReserved(reservedWordToken, firstLine, 0, ref columnError, 1) != true)
                    {
                        ShowError(MessageError.messageError);
                        MessageError.errorExistente = true;
                    }
                    firstChar = 'T';
                    break;
                default:
                    MessageError.messageError = $"Error al inicio del arhivo: SE ESPERABA DEFINICIO´N DE TOKENS.";
                    ShowError(MessageError.messageError);
                    MessageError.errorExistente = true;
                    break;
            }

            var i = 1;
            if (MessageError.errorExistente == false)
            {
                var grammarStart = i;
                if (firstChar == 'S')
                {
                    while (text[i].Contains("TOKENS") == false && MessageError.errorExistente == false)
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
                            MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA O NO TERMINO CORRECTAMENTE LA EXPRESION";
                            MessageError.errorExistente = true;
                            ShowError(MessageError.messageError);
                        }
                    }
                    if (i - grammarStart == 0 && MessageError.errorExistente == false)
                    {
                        MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA LA DEFINICIÓN DE ALGUN SET";
                        MessageError.errorExistente = true;
                        ShowError(MessageError.messageError);
                    }
                    grammarStart = i;
                }
                var LineaTokensLeida = false;
                while (text[i].Contains("ACTIONS") == false && MessageError.errorExistente == false)
                {
                    if (LineaTokensLeida == false && firstChar == 'S')
                    {
                        if (analyzeReserved(reservedWordToken, text[i].ToCharArray(), 0, ref columnError, 1) != true)
                        {
                            ShowError(MessageError.messageError);
                            MessageError.errorExistente = true;
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
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA UN ) EN TOKEN.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                            else
                            {
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: DEFINICION INCOMPLETA.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                        }
                    }
                }
                if (i - grammarStart == 0 && MessageError.errorExistente == false)
                {
                    MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA LA DEFINICIÓN DE UN TOKEN.";
                    MessageError.errorExistente = true;
                    ShowError(MessageError.messageError);
                }

                if (analyzeReserved(reservedWordActions, text[i].ToCharArray(), 0, ref columnError, i) == true && MessageError.errorExistente == false)
                {
                    columnError = 0;
                    i++;
                    if (analyzeReserved(reservedWordReservadas, text[i].ToCharArray(), 0, ref columnError, i) == true && MessageError.errorExistente == false)
                    {
                        i++;
                        if (text[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (text[i].Contains("}") == false && i < text.Count && MessageError.errorExistente == false)
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
                                    MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: DEFINICIÓN INCOMPLETA.";
                                    MessageError.errorExistente = true;
                                    ShowError(MessageError.messageError);
                                }
                            }
                            if (i - contador_Actions == 0 || i == text.Count && MessageError.errorExistente == false)
                            {
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA " + "}.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                            if (text[i] == "}" && i < text.Count)
                            {
                                i++;
                            }
                            else
                            {
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA " + "}" + " DE LA FUNCIÓN.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                        }
                    }
                    else
                    {
                        MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA PALABRA RESERVADA ACOMPAÑADO DE ACTIONS.";
                        MessageError.errorExistente = true;
                        ShowError(MessageError.messageError);
                    }
                }
                else
                {
                    MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA ACTIONS.";
                    MessageError.errorExistente = true;
                    ShowError(MessageError.messageError);
                }
                while (text[i].Contains("ERROR") == false && i < text.Count && MessageError.errorExistente == false)
                {
                    if (text[i].Contains('(') && text[i].Contains(')'))
                    {
                        i++;
                        if (text[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (text[i].Contains("}") == false && i < text.Count && MessageError.errorExistente == false)
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
                                    MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: DEFINICIÓN INCOMPLETA.";
                                    MessageError.errorExistente = true;
                                    ShowError(MessageError.messageError);

                                }
                            }
                            if (i - contador_Actions == 0 || i == text.Count && MessageError.errorExistente == false)
                            {
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA SE ESPERABA " + "}.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                            if (text[i] == "}" && i < text.Count)
                            {
                                i++;
                            }
                            else
                            {
                                MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA " + "}" + "DE LA FUNCIÓN.";
                                MessageError.errorExistente = true;
                                ShowError(MessageError.messageError);
                            }
                        }
                        else
                        {
                            MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA " + "{" + " DE LA FUNCIÓN.";
                            MessageError.errorExistente = true;
                            ShowError(MessageError.messageError);
                        }
                    }
                    else
                    {
                        MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA LA DEFINICIÓN DE UNA FUNCIÓN.";
                        MessageError.errorExistente = true;
                        ShowError(MessageError.messageError);
                    }
                }
                if (i < text.Count)
                {
                    while (i < text.Count && MessageError.errorExistente == false)
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
                            MessageError.messageError = $"Error en la línea {ReturnErrorLine(text[i])}: SE ESPERABA ALGUN NÚMERO EN ERRORES.";
                            MessageError.errorExistente = true;
                            ShowError(MessageError.messageError);
                        }
                    }
                }
                if (MessageError.errorExistente == false && i == text.Count)
                {
                    CorrectFile();
                }
            }
        }

        public static bool analyzeReserved(char[] reserverd, char[] line, int cont, ref int columnError, int lineError)
        {
            if (cont < reserverd.Length)
            {
                columnError++;
                if (cont < line.Length)
                {
                    if (line[cont] == reserverd[cont])
                    {
                        return analyzeReserved(reserverd, line, cont + 1, ref columnError, lineError);
                    }
                }
            }
            if (cont == reserverd.Length)
            {
                return true;
            }
            MessageError.messageError = $"Error en línea {lineError}, columna {columnError}: FALTA {reserverd[cont]}.";
            return false;
        }

        static bool flagLeftOR = false;
        static bool flagRightOR = false;
        static List<string> ids = new List<string>();
        public static void CompareTree(Node tree, string line, ref int column, List<char> st, int lineError)
        {
            if (tree != null && MessageError.errorExistente == false)
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
                    MessageError.messageError = $"Error en la línea {ReturnErrorLine(line)}: FALTA IDENTIFICADOR.";
                    MessageError.errorExistente = true;
                    ShowError(MessageError.messageError);
                }
                if (MessageError.errorExistente == false && flagLeftOR == false && flagRightOR == false && column < line.Length)
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
                                        while ((char.IsUpper(line[column]) || line[column] == '*' || line[column] == '|' || line[column] == '(' || line[column] == '{' || line[column] == ')' || line[column] == '}') && MessageError.errorExistente == false)
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
                                                            MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: FALTA EXPRESIÓN LUEGO DE |.";
                                                            MessageError.errorExistente = true;
                                                            ShowError(MessageError.messageError);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: FALTA EXPRESIÓN LUEGO DE |.";
                                                        MessageError.errorExistente = true;
                                                        ShowError(MessageError.messageError);
                                                    }
                                                    break;
                                                case '(':
                                                    if (line.Contains(')') == false)
                                                    {
                                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: FALTA CERRAR PARENTESIS.";
                                                        MessageError.errorExistente = true;
                                                        ShowError(MessageError.messageError);
                                                    }
                                                    break;
                                                case '{':
                                                    if (line.Contains('}') == false)
                                                    {
                                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: FALTA CERRAR LLAVES.";
                                                        MessageError.errorExistente = true;
                                                        ShowError(MessageError.messageError);
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
                                            MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABA UN IDENTIFICADOR.";
                                            MessageError.errorExistente = true;
                                            ShowError(MessageError.messageError);
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
                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABA UN NÚMERO";
                                        MessageError.errorExistente = true;
                                        ShowError(MessageError.messageError);
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
                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABAN ESPACIOS.";
                                        MessageError.errorExistente = true;
                                        ShowError(MessageError.messageError);
                                    }
                                    break;
                                case 'S':
                                    var parentesis_A = false;
                                    var parentesis_C = false;
                                    while (line[column] >= 32 && line[column] < 255 && MessageError.errorExistente == false)
                                    {
                                        if (char.IsLetter(line[column]) == true && char.IsUpper(line[column]) == false)
                                        {
                                            MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABA MAYUSCULA.";
                                            MessageError.errorExistente = true;
                                            ShowError(MessageError.messageError);
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
                                        MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABA UN TERMINAL O SIMBOLO.";
                                        MessageError.errorExistente = true;
                                        ShowError(MessageError.messageError);
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
                                    MessageError.messageError = $"Error en línea {ReturnErrorLine(line)}: SE ESPERABA UN {tree.info}.";
                                    MessageError.errorExistente = true;
                                    ShowError(MessageError.messageError);
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
                        TurnOffTour(tree);
                        CompareTree(tree, line, ref column, st, lineError);
                    }
                    tree.route = true;
                }
                CompareTree(tree.rightNode, line, ref column, st, lineError);
                if (tree.info == '|' && (NodeRoute(tree.leftNode) == true || NodeRoute(tree.rightNode) == true))
                {
                    flagLeftOR = false;
                    flagRightOR = false;

                }
            }
        }

        public static bool IsLeaf(Node node)
        {
            if (node.rightNode == null && node. leftNode == null)
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

        public static void TurnOffTour(Node node)
        {
            if (node != null)
            {
                TurnOffTour(node.leftNode);
                node.route = false;
                TurnOffTour(node.rightNode);
            }
        }

        public static int ReturnErrorLine(string line)
        {
            var cont = 1;
            foreach (var item in correctText)
            {
                if (line == item)
                {
                    return cont;
                }
                cont++;
            }
            return cont;
        }

        public static void CorrectFile()
        {
            MessageBox.Show("Archivo sin NINGUN error.");
        }

        public static void ShowError(string text) 
        {
            MessageBox.Show(text);
        }
    }
}

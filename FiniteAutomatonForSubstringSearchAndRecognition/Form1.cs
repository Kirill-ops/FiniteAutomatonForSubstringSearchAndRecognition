using System.Windows.Forms;

namespace FiniteAutomatonForSubstringSearchAndRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            linkLabelOutResult.Text = "Результат будет выделен так";
            linkLabelOutResult.Links.Clear();
            linkLabelOutResult.Links.Add(24, 3).Enabled = false;

        }

        public string TextFromFileTxt = "";

        private void button1_Click(object sender, EventArgs e)
        {
            var pattern = textBoxInputPattern.Text;
            linkLabelOutResult.Links.Clear();

            if (pattern != "" && pattern != null && TextFromFileTxt != "" && TextFromFileTxt != null)
            {
                if (comboBoxVariantSearch.Text == comboBoxVariantSearch.Items[0].ToString())
                {
                    var list = Search(pattern.ToCharArray(), TextFromFileTxt.ToCharArray());
                    foreach (var item in list)
                        linkLabelOutResult.Links.Add(item, pattern.Length).Enabled = false;
                }
                else
                {
                    var list = KMPSearch(pattern, TextFromFileTxt);
                    foreach (var item in list)
                        linkLabelOutResult.Links.Add(item, pattern.Length).Enabled = false;
                }
            }
        }

        private void buttonOpenFileTxt_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            TextFromFileTxt = System.IO.File.ReadAllText(filename);

            linkLabelOutResult.Links.Clear();
            linkLabelOutResult.Text = TextFromFileTxt;
        }


        public static int NO_OF_CHARS = 256;
        public static int GetNextState(char[] pattern, int lengthPattern, int state, int symbol)
        {

            // Если символ symbol совпадает со следующим символом в шаблоне,то просто увеличиваем состояние
            if (state < lengthPattern && (char)symbol == pattern[state])
                return state + 1;

            // nextState сохраняет результат
            // который является следующим состоянием
            int nextState, i;
            for (nextState = state; nextState > 0; nextState--)
            {
                if (pattern[nextState - 1] == (char)symbol)
                {
                    for (i = 0; i < nextState - 1; i++)
                        if (pattern[i] != pattern[state - nextState + 1 + i])
                            break;
                    if (i == nextState - 1)
                        return nextState;
                }
            }

            return 0;
        }

        // Эта функция строит таблицу TF, которая представляет конечные автоматы для заданного шаблона
        public static void ComputeTransitionTable(char[] pattern, int lengthPattern, int[][] transitionTable)
        {

            for (int state = 0; state <= lengthPattern; state++)
            {
                for (int symbol = 0; symbol < NO_OF_CHARS; symbol++)
                {
                    transitionTable[state][symbol] = GetNextState(pattern, lengthPattern, state, symbol);
                }
            }
        }

        // Печатает все вхождения pat в формате txt 
        public static List<int> Search(char[] pattern, char[] Text)
        {
            var listResul = new List<int>();
            int lengthPattern = pattern.Length;
            int lengthText = Text.Length;

            var transitionTable = RectangularArrays.ReturnRectangularIntArray(lengthPattern + 1, NO_OF_CHARS);

            ComputeTransitionTable(pattern, lengthPattern, transitionTable);

            // Обработать txt через конечный автомат.
            int i, state = 0;
            for (i = 0; i < lengthText; i++)
            {
                state = transitionTable[state][Text[i]];
                if (state == lengthPattern)
                    listResul.Add(i - lengthPattern + 1);
            }
            return listResul;
        }

        public static class RectangularArrays
        {
            public static int[][] ReturnRectangularIntArray(int size1, int size2)
            {
                int[][] newArray = new int[size1][];
                for (int array1 = 0; array1 < size1; array1++)
                    newArray[array1] = new int[size2];
                return newArray;
            }
        }


        public static List<int> KMPSearch(string pattern, string text)
        {
            var listResult = new List<int>();

            int lengthPattern = pattern.Length;
            int lengthText = text.Length;

            // создайте lps[], который будет содержать самые длинные значения префикса-суффикса для шаблона
            var lps = new int[lengthPattern];
            int j = 0; // index for pat[]

            // Предварительная обработка шаблона (вычисление массива lps[])
            computeLPSArray(pattern, lengthPattern, lps);

            int i = 0; // индекс для txt[]
            while (i < lengthText)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == lengthPattern)
                {
                    listResult.Add(i - j);
                    j = lps[j - 1];
                }
                // несоответствие после j совпадений
                else if (i < lengthText && pattern[j] != text[i])
                {
                    // Не совпадайте с символами lps[0..lps[j-1]],
                    // они все равно будут совпадать
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
            return listResult;
        }

        public static void computeLPSArray(string pattern, int lengthPattern, int[] lps)
        {
            // длина предыдущего самого длинного префикса-суффикса
            int length = 0;
            int i = 1;
            lps[0] = 0; // lps[0] всегда равно 0

            // цикл вычисляет lps[i] для значений от i = 1 до M-1
            while (i < lengthPattern)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    // Это сложно. Рассмотрим пример.
                    // AAACAAAA и i = 7. Идея аналогична
                    // шагу поиска.
                    if (length != 0)
                    {
                        length = lps[length - 1];
                        // Также обратите внимание, что мы здесь не увеличиваем значение i
                    }
                    else
                    {
                        lps[i] = length;
                        i++;
                    }
                }
            }
        }

    }
}
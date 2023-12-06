using System.Windows.Forms;

namespace FiniteAutomatonForSubstringSearchAndRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            linkLabelOutResult.Text = "��������� ����� ������� ���";
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

            // ���� ������ symbol ��������� �� ��������� �������� � �������,�� ������ ����������� ���������
            if (state < lengthPattern && (char)symbol == pattern[state])
                return state + 1;

            // nextState ��������� ���������
            // ������� �������� ��������� ����������
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

        // ��� ������� ������ ������� TF, ������� ������������ �������� �������� ��� ��������� �������
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

        // �������� ��� ��������� pat � ������� txt 
        public static List<int> Search(char[] pattern, char[] Text)
        {
            var listResul = new List<int>();
            int lengthPattern = pattern.Length;
            int lengthText = Text.Length;

            var transitionTable = RectangularArrays.ReturnRectangularIntArray(lengthPattern + 1, NO_OF_CHARS);

            ComputeTransitionTable(pattern, lengthPattern, transitionTable);

            // ���������� txt ����� �������� �������.
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

            // �������� lps[], ������� ����� ��������� ����� ������� �������� ��������-�������� ��� �������
            var lps = new int[lengthPattern];
            int j = 0; // index for pat[]

            // ��������������� ��������� ������� (���������� ������� lps[])
            computeLPSArray(pattern, lengthPattern, lps);

            int i = 0; // ������ ��� txt[]
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
                // �������������� ����� j ����������
                else if (i < lengthText && pattern[j] != text[i])
                {
                    // �� ���������� � ��������� lps[0..lps[j-1]],
                    // ��� ��� ����� ����� ���������
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
            // ����� ����������� ������ �������� ��������-��������
            int length = 0;
            int i = 1;
            lps[0] = 0; // lps[0] ������ ����� 0

            // ���� ��������� lps[i] ��� �������� �� i = 1 �� M-1
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
                    // ��� ������. ���������� ������.
                    // AAACAAAA � i = 7. ���� ����������
                    // ���� ������.
                    if (length != 0)
                    {
                        length = lps[length - 1];
                        // ����� �������� ��������, ��� �� ����� �� ����������� �������� i
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
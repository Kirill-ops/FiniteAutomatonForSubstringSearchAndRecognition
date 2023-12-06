namespace FiniteAutomatonForSubstringSearchAndRecognition
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            linkLabelOutResult = new LinkLabel();
            button1 = new Button();
            textBoxInputPattern = new TextBox();
            buttonOpenFileTxt = new Button();
            openFileDialog1 = new OpenFileDialog();
            comboBoxVariantSearch = new ComboBox();
            SuspendLayout();
            // 
            // linkLabelOutResult
            // 
            linkLabelOutResult.BackColor = Color.White;
            linkLabelOutResult.Location = new Point(12, 92);
            linkLabelOutResult.Name = "linkLabelOutResult";
            linkLabelOutResult.Size = new Size(776, 421);
            linkLabelOutResult.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(12, 55);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "Найти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxInputPattern
            // 
            textBoxInputPattern.AccessibleDescription = "";
            textBoxInputPattern.Location = new Point(12, 12);
            textBoxInputPattern.Name = "textBoxInputPattern";
            textBoxInputPattern.Size = new Size(776, 31);
            textBoxInputPattern.TabIndex = 2;
            textBoxInputPattern.Tag = "";
            // 
            // buttonOpenFileTxt
            // 
            buttonOpenFileTxt.Location = new Point(634, 49);
            buttonOpenFileTxt.Name = "buttonOpenFileTxt";
            buttonOpenFileTxt.Size = new Size(154, 34);
            buttonOpenFileTxt.TabIndex = 3;
            buttonOpenFileTxt.Text = "Открыть file.txt";
            buttonOpenFileTxt.UseVisualStyleBackColor = true;
            buttonOpenFileTxt.Click += buttonOpenFileTxt_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBoxVariantSearch
            // 
            comboBoxVariantSearch.FormattingEnabled = true;
            comboBoxVariantSearch.Items.AddRange(new object[] { "Метод матрицей перехода", "Алгоритм КМП" });
            comboBoxVariantSearch.Location = new Point(130, 55);
            comboBoxVariantSearch.Name = "comboBoxVariantSearch";
            comboBoxVariantSearch.Size = new Size(262, 33);
            comboBoxVariantSearch.TabIndex = 4;
            comboBoxVariantSearch.Text = "Метод матрицей перехода";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 522);
            Controls.Add(comboBoxVariantSearch);
            Controls.Add(buttonOpenFileTxt);
            Controls.Add(textBoxInputPattern);
            Controls.Add(button1);
            Controls.Add(linkLabelOutResult);
            Name = "Form1";
            Text = "Конечный автомат для поиска и разпознавания подстрок";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel linkLabelOutResult;
        private Button button1;
        private TextBox textBoxInputPattern;
        private Button buttonOpenFileTxt;
        private OpenFileDialog openFileDialog1;
        private ComboBox comboBoxVariantSearch;
    }
}
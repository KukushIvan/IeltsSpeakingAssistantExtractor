using System;
using System.Windows.Forms;

namespace IeltsSpeakingAssistantExtractor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartGeneration = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectLatex = new System.Windows.Forms.Button();
            this.tbLatexLocation = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnResultFolder = new System.Windows.Forms.Button();
            this.tbResultFolder = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbResultFileName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cbGenerateDictionary = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDictionaryPrefix = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.cbGenerateIdeas = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIdeaPrefix = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.cbGenerateAnswers = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tbAnswerPrefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ofdLatex = new System.Windows.Forms.OpenFileDialog();
            this.fbdResultsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartGeneration
            // 
            this.btnStartGeneration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartGeneration.Location = new System.Drawing.Point(3, 375);
            this.btnStartGeneration.Name = "btnStartGeneration";
            this.btnStartGeneration.Size = new System.Drawing.Size(794, 72);
            this.btnStartGeneration.TabIndex = 0;
            this.btnStartGeneration.Text = "Сделать хорошо";
            this.btnStartGeneration.UseVisualStyleBackColor = true;
            this.btnStartGeneration.Click += new System.EventHandler(this.btnStartGeneration_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnStartGeneration, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnSelectLatex, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbLatexLocation, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnSelectLatex
            // 
            this.btnSelectLatex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectLatex.Location = new System.Drawing.Point(3, 3);
            this.btnSelectLatex.Name = "btnSelectLatex";
            this.btnSelectLatex.Size = new System.Drawing.Size(391, 50);
            this.btnSelectLatex.TabIndex = 0;
            this.btnSelectLatex.Text = "Выберите расположение pdfLatex";
            this.btnSelectLatex.UseVisualStyleBackColor = true;
            this.btnSelectLatex.Click += new System.EventHandler(this.btnSelectLatex_Click);
            // 
            // tbLatexLocation
            // 
            this.tbLatexLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLatexLocation.Location = new System.Drawing.Point(400, 3);
            this.tbLatexLocation.Name = "tbLatexLocation";
            this.tbLatexLocation.ReadOnly = true;
            this.tbLatexLocation.Size = new System.Drawing.Size(391, 20);
            this.tbLatexLocation.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnResultFolder, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbResultFolder, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 65);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnResultFolder
            // 
            this.btnResultFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResultFolder.Location = new System.Drawing.Point(3, 3);
            this.btnResultFolder.Name = "btnResultFolder";
            this.btnResultFolder.Size = new System.Drawing.Size(391, 50);
            this.btnResultFolder.TabIndex = 0;
            this.btnResultFolder.Text = "Выберите расположение папки с результатами";
            this.btnResultFolder.UseVisualStyleBackColor = true;
            this.btnResultFolder.Click += new System.EventHandler(this.btnResultFolder_Click);
            // 
            // tbResultFolder
            // 
            this.tbResultFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResultFolder.Location = new System.Drawing.Point(400, 3);
            this.tbResultFolder.Name = "tbResultFolder";
            this.tbResultFolder.ReadOnly = true;
            this.tbResultFolder.Size = new System.Drawing.Size(391, 20);
            this.tbResultFolder.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbResultFileName, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 127);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(256, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя файла по умолчанию";
            // 
            // tbResultFileName
            // 
            this.tbResultFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResultFileName.Location = new System.Drawing.Point(400, 3);
            this.tbResultFileName.Name = "tbResultFileName";
            this.tbResultFileName.Size = new System.Drawing.Size(391, 20);
            this.tbResultFileName.TabIndex = 1;
            this.tbResultFileName.TextChanged += new System.EventHandler(this.txbResultFileName_TextChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.cbGenerateDictionary, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 189);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // cbGenerateDictionary
            // 
            this.cbGenerateDictionary.AutoSize = true;
            this.cbGenerateDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbGenerateDictionary.Location = new System.Drawing.Point(3, 3);
            this.cbGenerateDictionary.Name = "cbGenerateDictionary";
            this.cbGenerateDictionary.Size = new System.Drawing.Size(391, 50);
            this.cbGenerateDictionary.TabIndex = 0;
            this.cbGenerateDictionary.Text = "Генерировать словарь";
            this.cbGenerateDictionary.UseVisualStyleBackColor = true;
            this.cbGenerateDictionary.CheckedChanged += new System.EventHandler(this.cbGenerateDictionary_CheckedChanged);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tbDictionaryPrefix, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(400, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(391, 50);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(385, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Добавить префикс к имени файла";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbDictionaryPrefix
            // 
            this.tbDictionaryPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDictionaryPrefix.Location = new System.Drawing.Point(3, 28);
            this.tbDictionaryPrefix.Name = "tbDictionaryPrefix";
            this.tbDictionaryPrefix.Size = new System.Drawing.Size(385, 20);
            this.tbDictionaryPrefix.TabIndex = 1;
            this.tbDictionaryPrefix.TextChanged += new System.EventHandler(this.tbDictionaryPrefix_TextChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.cbGenerateIdeas, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 251);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // cbGenerateIdeas
            // 
            this.cbGenerateIdeas.AutoSize = true;
            this.cbGenerateIdeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbGenerateIdeas.Location = new System.Drawing.Point(3, 3);
            this.cbGenerateIdeas.Name = "cbGenerateIdeas";
            this.cbGenerateIdeas.Size = new System.Drawing.Size(391, 50);
            this.cbGenerateIdeas.TabIndex = 0;
            this.cbGenerateIdeas.Text = "Генерировать идеи";
            this.cbGenerateIdeas.UseVisualStyleBackColor = true;
            this.cbGenerateIdeas.CheckedChanged += new System.EventHandler(this.cbGenerateIdeas_CheckedChanged);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tbIdeaPrefix, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(400, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(391, 50);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(385, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Добавить префикс к имени файла";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbIdeaPrefix
            // 
            this.tbIdeaPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbIdeaPrefix.Location = new System.Drawing.Point(3, 28);
            this.tbIdeaPrefix.Name = "tbIdeaPrefix";
            this.tbIdeaPrefix.Size = new System.Drawing.Size(385, 20);
            this.tbIdeaPrefix.TabIndex = 1;
            this.tbIdeaPrefix.TextChanged += new System.EventHandler(this.tbIdeaPrefix_TextChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.cbGenerateAnswers, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel10, 2, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 313);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(794, 56);
            this.tableLayoutPanel7.TabIndex = 6;
            // 
            // cbGenerateAnswers
            // 
            this.cbGenerateAnswers.AutoSize = true;
            this.cbGenerateAnswers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbGenerateAnswers.Location = new System.Drawing.Point(3, 3);
            this.cbGenerateAnswers.Name = "cbGenerateAnswers";
            this.cbGenerateAnswers.Size = new System.Drawing.Size(391, 50);
            this.cbGenerateAnswers.TabIndex = 0;
            this.cbGenerateAnswers.Text = "Генерировать ответы";
            this.cbGenerateAnswers.UseVisualStyleBackColor = true;
            this.cbGenerateAnswers.CheckedChanged += new System.EventHandler(this.cbGenerateAnswers_CheckedChanged);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Controls.Add(this.tbAnswerPrefix, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(400, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(391, 50);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // tbAnswerPrefix
            // 
            this.tbAnswerPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAnswerPrefix.Location = new System.Drawing.Point(3, 28);
            this.tbAnswerPrefix.Name = "tbAnswerPrefix";
            this.tbAnswerPrefix.Size = new System.Drawing.Size(385, 20);
            this.tbAnswerPrefix.TabIndex = 1;
            this.tbAnswerPrefix.TextChanged += new System.EventHandler(this.tbAnswerPrefix_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(385, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Добавить префикс к имени файла";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ofdLatex
            // 
            this.ofdLatex.FileName = "pdflatex.exe";
            this.ofdLatex.InitialDirectory = "C:\\Program Files\\MiKTeX 2.9\\miktex\\bin\\x64\\";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "IELTS Speaking Assistant Extractor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartGeneration;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSelectLatex;
        private System.Windows.Forms.TextBox tbLatexLocation;
        private System.Windows.Forms.OpenFileDialog ofdLatex;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnResultFolder;
        private TextBox tbResultFolder;
        private FolderBrowserDialog fbdResultsFolder;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private TextBox tbResultFileName;
        private TableLayoutPanel tableLayoutPanel5;
        private CheckBox cbGenerateDictionary;
        private TableLayoutPanel tableLayoutPanel6;
        private CheckBox cbGenerateIdeas;
        private TableLayoutPanel tableLayoutPanel7;
        private CheckBox cbGenerateAnswers;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private Label label2;
        private TextBox tbDictionaryPrefix;
        private Label label3;
        private TextBox tbIdeaPrefix;
        private TextBox tbAnswerPrefix;
        private Label label5;
    }
}


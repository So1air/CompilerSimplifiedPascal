namespace CompilerSimplifiedPascal
{
    partial class frmTranslator
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbC_Main = new System.Windows.Forms.TabControl();
            this.tbP_textEditor = new System.Windows.Forms.TabPage();
            this.btnCompile = new System.Windows.Forms.Button();
            this.grB_File = new System.Windows.Forms.GroupBox();
            this.lblStatusInFile = new System.Windows.Forms.Label();
            this.btnNewFile = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txB_PathToFile = new System.Windows.Forms.TextBox();
            this.rTB_SourceCode = new System.Windows.Forms.RichTextBox();
            this.tbP_ResultLA = new System.Windows.Forms.TabPage();
            this.grB_AnalyzedSentence = new System.Windows.Forms.GroupBox();
            this.dGV_AnalyzedSentence = new System.Windows.Forms.DataGridView();
            this.grB_TableL = new System.Windows.Forms.GroupBox();
            this.dGV_TableOfLexemas = new System.Windows.Forms.DataGridView();
            this.Hits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameLex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassLex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Attributes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sFD_SaveSourceCode = new System.Windows.Forms.SaveFileDialog();
            this.oFD_LoadSourceCode = new System.Windows.Forms.OpenFileDialog();
            this.tbC_Main.SuspendLayout();
            this.tbP_textEditor.SuspendLayout();
            this.grB_File.SuspendLayout();
            this.tbP_ResultLA.SuspendLayout();
            this.grB_AnalyzedSentence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AnalyzedSentence)).BeginInit();
            this.grB_TableL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_TableOfLexemas)).BeginInit();
            this.SuspendLayout();
            // 
            // tbC_Main
            // 
            this.tbC_Main.Controls.Add(this.tbP_textEditor);
            this.tbC_Main.Controls.Add(this.tbP_ResultLA);
            this.tbC_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbC_Main.Font = new System.Drawing.Font("Leelawadee", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbC_Main.Location = new System.Drawing.Point(0, 0);
            this.tbC_Main.Name = "tbC_Main";
            this.tbC_Main.SelectedIndex = 0;
            this.tbC_Main.Size = new System.Drawing.Size(891, 452);
            this.tbC_Main.TabIndex = 0;
            // 
            // tbP_textEditor
            // 
            this.tbP_textEditor.Controls.Add(this.btnCompile);
            this.tbP_textEditor.Controls.Add(this.grB_File);
            this.tbP_textEditor.Controls.Add(this.rTB_SourceCode);
            this.tbP_textEditor.Location = new System.Drawing.Point(4, 24);
            this.tbP_textEditor.Name = "tbP_textEditor";
            this.tbP_textEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tbP_textEditor.Size = new System.Drawing.Size(883, 424);
            this.tbP_textEditor.TabIndex = 0;
            this.tbP_textEditor.Text = "Text Editor";
            this.tbP_textEditor.UseVisualStyleBackColor = true;
            // 
            // btnCompile
            // 
            this.btnCompile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompile.AutoSize = true;
            this.btnCompile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCompile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCompile.Location = new System.Drawing.Point(8, 391);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(254, 27);
            this.btnCompile.TabIndex = 2;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = false;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // grB_File
            // 
            this.grB_File.Controls.Add(this.lblStatusInFile);
            this.grB_File.Controls.Add(this.btnNewFile);
            this.grB_File.Controls.Add(this.btnSaveChanges);
            this.grB_File.Controls.Add(this.btnOpenFile);
            this.grB_File.Controls.Add(this.txB_PathToFile);
            this.grB_File.Dock = System.Windows.Forms.DockStyle.Top;
            this.grB_File.Location = new System.Drawing.Point(3, 3);
            this.grB_File.MaximumSize = new System.Drawing.Size(265, 110);
            this.grB_File.Name = "grB_File";
            this.grB_File.Size = new System.Drawing.Size(265, 109);
            this.grB_File.TabIndex = 1;
            this.grB_File.TabStop = false;
            this.grB_File.Text = "File";
            // 
            // lblStatusInFile
            // 
            this.lblStatusInFile.AutoSize = true;
            this.lblStatusInFile.Location = new System.Drawing.Point(6, 80);
            this.lblStatusInFile.Name = "lblStatusInFile";
            this.lblStatusInFile.Size = new System.Drawing.Size(38, 16);
            this.lblStatusInFile.TabIndex = 4;
            this.lblStatusInFile.Text = "label";
            // 
            // btnNewFile
            // 
            this.btnNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewFile.AutoSize = true;
            this.btnNewFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnNewFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewFile.Location = new System.Drawing.Point(191, 48);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(68, 26);
            this.btnNewFile.TabIndex = 3;
            this.btnNewFile.Text = "New file";
            this.btnNewFile.UseVisualStyleBackColor = false;
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.AutoSize = true;
            this.btnSaveChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveChanges.Location = new System.Drawing.Point(84, 48);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(100, 26);
            this.btnSaveChanges.TabIndex = 2;
            this.btnSaveChanges.Text = "Save changes";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.AutoSize = true;
            this.btnOpenFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenFile.Location = new System.Drawing.Point(3, 48);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 26);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "Open file";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txB_PathToFile
            // 
            this.txB_PathToFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.txB_PathToFile.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txB_PathToFile.Location = new System.Drawing.Point(3, 19);
            this.txB_PathToFile.Name = "txB_PathToFile";
            this.txB_PathToFile.ReadOnly = true;
            this.txB_PathToFile.Size = new System.Drawing.Size(259, 23);
            this.txB_PathToFile.TabIndex = 0;
            this.txB_PathToFile.Text = "<path to file>";
            // 
            // rTB_SourceCode
            // 
            this.rTB_SourceCode.AcceptsTab = true;
            this.rTB_SourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rTB_SourceCode.AutoWordSelection = true;
            this.rTB_SourceCode.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rTB_SourceCode.Location = new System.Drawing.Point(268, 3);
            this.rTB_SourceCode.Name = "rTB_SourceCode";
            this.rTB_SourceCode.Size = new System.Drawing.Size(612, 418);
            this.rTB_SourceCode.TabIndex = 0;
            this.rTB_SourceCode.Text = "";
            this.rTB_SourceCode.WordWrap = false;
            // 
            // tbP_ResultLA
            // 
            this.tbP_ResultLA.Controls.Add(this.grB_AnalyzedSentence);
            this.tbP_ResultLA.Controls.Add(this.grB_TableL);
            this.tbP_ResultLA.Location = new System.Drawing.Point(4, 24);
            this.tbP_ResultLA.Name = "tbP_ResultLA";
            this.tbP_ResultLA.Padding = new System.Windows.Forms.Padding(3);
            this.tbP_ResultLA.Size = new System.Drawing.Size(883, 424);
            this.tbP_ResultLA.TabIndex = 1;
            this.tbP_ResultLA.Text = "Resultations lexical analysis";
            this.tbP_ResultLA.UseVisualStyleBackColor = true;
            // 
            // grB_AnalyzedSentence
            // 
            this.grB_AnalyzedSentence.Controls.Add(this.dGV_AnalyzedSentence);
            this.grB_AnalyzedSentence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grB_AnalyzedSentence.Location = new System.Drawing.Point(3, 274);
            this.grB_AnalyzedSentence.Name = "grB_AnalyzedSentence";
            this.grB_AnalyzedSentence.Size = new System.Drawing.Size(877, 147);
            this.grB_AnalyzedSentence.TabIndex = 3;
            this.grB_AnalyzedSentence.TabStop = false;
            this.grB_AnalyzedSentence.Text = "Analyzed sentence";
            // 
            // dGV_AnalyzedSentence
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGV_AnalyzedSentence.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dGV_AnalyzedSentence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dGV_AnalyzedSentence.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dGV_AnalyzedSentence.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dGV_AnalyzedSentence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_AnalyzedSentence.ColumnHeadersVisible = false;
            this.dGV_AnalyzedSentence.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dGV_AnalyzedSentence.EnableHeadersVisualStyles = false;
            this.dGV_AnalyzedSentence.Location = new System.Drawing.Point(3, 51);
            this.dGV_AnalyzedSentence.Name = "dGV_AnalyzedSentence";
            this.dGV_AnalyzedSentence.ReadOnly = true;
            this.dGV_AnalyzedSentence.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dGV_AnalyzedSentence.Size = new System.Drawing.Size(871, 93);
            this.dGV_AnalyzedSentence.TabIndex = 1;
            // 
            // grB_TableL
            // 
            this.grB_TableL.Controls.Add(this.dGV_TableOfLexemas);
            this.grB_TableL.Dock = System.Windows.Forms.DockStyle.Top;
            this.grB_TableL.Location = new System.Drawing.Point(3, 3);
            this.grB_TableL.Name = "grB_TableL";
            this.grB_TableL.Size = new System.Drawing.Size(877, 271);
            this.grB_TableL.TabIndex = 2;
            this.grB_TableL.TabStop = false;
            this.grB_TableL.Text = "Table of lexemas";
            // 
            // dGV_TableOfLexemas
            // 
            this.dGV_TableOfLexemas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_TableOfLexemas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hits,
            this.NameLex,
            this.ClassLex,
            this.Attributes});
            this.dGV_TableOfLexemas.Dock = System.Windows.Forms.DockStyle.Top;
            this.dGV_TableOfLexemas.Location = new System.Drawing.Point(3, 19);
            this.dGV_TableOfLexemas.Name = "dGV_TableOfLexemas";
            this.dGV_TableOfLexemas.ReadOnly = true;
            this.dGV_TableOfLexemas.RowHeadersVisible = false;
            this.dGV_TableOfLexemas.Size = new System.Drawing.Size(871, 203);
            this.dGV_TableOfLexemas.TabIndex = 0;
            // 
            // Hits
            // 
            this.Hits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Hits.FillWeight = 25F;
            this.Hits.HeaderText = "Hits";
            this.Hits.Name = "Hits";
            this.Hits.ReadOnly = true;
            // 
            // NameLex
            // 
            this.NameLex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameLex.FillWeight = 150F;
            this.NameLex.HeaderText = "Name lexema";
            this.NameLex.Name = "NameLex";
            this.NameLex.ReadOnly = true;
            // 
            // ClassLex
            // 
            this.ClassLex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ClassLex.HeaderText = "Class of lexema";
            this.ClassLex.Name = "ClassLex";
            this.ClassLex.ReadOnly = true;
            // 
            // Attributes
            // 
            this.Attributes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Attributes.FillWeight = 300F;
            this.Attributes.HeaderText = "Attributes of lexema";
            this.Attributes.Name = "Attributes";
            this.Attributes.ReadOnly = true;
            // 
            // sFD_SaveSourceCode
            // 
            this.sFD_SaveSourceCode.DefaultExt = "simpas";
            this.sFD_SaveSourceCode.Filter = "Simplified Pascal Source code files|*.simpas";
            this.sFD_SaveSourceCode.InitialDirectory = ".";
            // 
            // oFD_LoadSourceCode
            // 
            this.oFD_LoadSourceCode.DefaultExt = "simpas";
            this.oFD_LoadSourceCode.Filter = "Simplified Pascal Source code files|*.simpas";
            this.oFD_LoadSourceCode.InitialDirectory = ".";
            // 
            // frmTranslator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 452);
            this.Controls.Add(this.tbC_Main);
            this.Name = "frmTranslator";
            this.Text = "Translator";
            this.tbC_Main.ResumeLayout(false);
            this.tbP_textEditor.ResumeLayout(false);
            this.tbP_textEditor.PerformLayout();
            this.grB_File.ResumeLayout(false);
            this.grB_File.PerformLayout();
            this.tbP_ResultLA.ResumeLayout(false);
            this.grB_AnalyzedSentence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AnalyzedSentence)).EndInit();
            this.grB_TableL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_TableOfLexemas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbC_Main;
        private System.Windows.Forms.TabPage tbP_textEditor;
        private System.Windows.Forms.GroupBox grB_File;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txB_PathToFile;
        private System.Windows.Forms.RichTextBox rTB_SourceCode;
        private System.Windows.Forms.TabPage tbP_ResultLA;
        private System.Windows.Forms.SaveFileDialog sFD_SaveSourceCode;
        private System.Windows.Forms.OpenFileDialog oFD_LoadSourceCode;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.Label lblStatusInFile;
        private System.Windows.Forms.Button btnNewFile;
        private System.Windows.Forms.DataGridView dGV_AnalyzedSentence;
        private System.Windows.Forms.DataGridView dGV_TableOfLexemas;
        private System.Windows.Forms.GroupBox grB_AnalyzedSentence;
        private System.Windows.Forms.GroupBox grB_TableL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hits;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameLex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassLex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Attributes;

    }
}


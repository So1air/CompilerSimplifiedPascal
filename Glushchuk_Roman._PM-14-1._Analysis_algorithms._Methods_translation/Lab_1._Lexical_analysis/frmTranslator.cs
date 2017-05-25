using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CompilerSimplifiedPascal
{
    public partial class frmTranslator : Form, IOutInfoCompiler
    {
        readonly Color COLOR_COMMENT = Color.Green;
        readonly Color COLOR_STRING = Color.Red;
        readonly Color COLOR_KEYWORD = Color.Blue;
        readonly Color COLOR_ID = Color.FromArgb(0, 50, 50);
        private Font fontErroneusWord;
        private Font fontBold, fontReg;

        private Compiler _compilerSimPas;

        public frmTranslator()
        {
            InitializeComponent();
            lblStatusInFile.Text = "";
            fontErroneusWord = new Font(rTB_SourceCode.Font, FontStyle.Strikeout);
            fontBold = new Font(rTB_SourceCode.Font, FontStyle.Bold);
            fontReg = new Font(rTB_SourceCode.Font, FontStyle.Regular);
            dGV_AnalyzedSentence.RowCount = 3;
            dGV_AnalyzedSentence.Rows[0].HeaderCell.Value = "Word";
            dGV_AnalyzedSentence.Rows[1].HeaderCell.Value = "LexClass";
            dGV_AnalyzedSentence.Rows[2].HeaderCell.Value = "Error";
            if ((_compilerSimPas = Compiler.CreateInstanceCompiler(new EnterStream_fromUI(this.rTB_SourceCode), this)) == null) 
                btnCompile.Enabled = false;
        }        

        public void Comment(uint beg, uint end = 0)
        {
            if(beg <rTB_SourceCode.Text.Length)
                if (end > beg)
                    rTB_SourceCode.Select((int)beg, (int)end - (int)beg + 1);
                else
                    rTB_SourceCode.Select((int)beg, rTB_SourceCode.Text.Length - (int)beg);
            rTB_SourceCode.SelectionColor = COLOR_COMMENT;
        }

        public void CurrentContext(LexClass l_cl_word, uint beg_word, uint end_word)
        {
            if (beg_word < rTB_SourceCode.Text.Length)
                if (end_word >= beg_word)
                {
                    rTB_SourceCode.Select((int)beg_word, (int)end_word - (int)beg_word + 1);
                    switch (l_cl_word)
                    {
                        case LexClass.ID:
                            rTB_SourceCode.SelectionColor = COLOR_ID;
                            rTB_SourceCode.SelectionFont = this.fontReg;
                            break;

                        case LexClass.Const_str:
                            rTB_SourceCode.SelectionColor = COLOR_STRING;
                            rTB_SourceCode.SelectionFont = this.fontReg;
                            break;

                        case LexClass.Const_int:
                            rTB_SourceCode.SelectionFont = this.fontBold;
                            break;

                        case LexClass.Sc_AddOp:
                        case LexClass.Sc_Asnt:
                        case LexClass.Sc_Colon:
                        case LexClass.Sc_Comma:
                        case LexClass.Sc_Dot:
                        case LexClass.Sc_Dots:
                        case LexClass.Sc_Lprnts:
                        case LexClass.Sc_LsqBrk:
                        case LexClass.Sc_MultOp:
                        case LexClass.Sc_RelOp:
                        case LexClass.Sc_Rprnts:
                        case LexClass.Sc_RsqBrk:
                        case LexClass.Sc_Semic:
                            rTB_SourceCode.SelectionFont = this.fontReg;
                            break;

                        default:
                            rTB_SourceCode.SelectionColor = COLOR_KEYWORD;
                            rTB_SourceCode.SelectionFont = this.fontBold;
                            break;
                    }

                }            
        }

        public void ErroneousWord(string errorMessage, uint beg_word, uint end_word)
        {
            if (beg_word < rTB_SourceCode.Text.Length)
                if (end_word >= beg_word)
                    rTB_SourceCode.Select((int)beg_word, (int)end_word - (int)beg_word + 1);
                else
                    rTB_SourceCode.Select((int)beg_word, rTB_SourceCode.Text.Length - (int)beg_word);

            rTB_SourceCode.SelectionFont = this.fontErroneusWord;
        }

        public void ReportLA(LexAnalysis.WordInSentence[] analyzed_sentence)
        {
            if (analyzed_sentence.Length != 0)
            {
                dGV_AnalyzedSentence.ColumnCount = (analyzed_sentence.Length > 655) ? 655 : analyzed_sentence.Length;

                for (int i = 0; i < dGV_AnalyzedSentence.ColumnCount; i++)
                {
                    dGV_AnalyzedSentence.Columns[i].FillWeight = 1;
                    dGV_AnalyzedSentence.Rows[0].Cells[i].Value = analyzed_sentence[i].Word;
                    if (analyzed_sentence[i].CoincidentLexema != null)
                        dGV_AnalyzedSentence.Rows[1].Cells[i].Value = analyzed_sentence[i].CoincidentLexema.Class.ToString();
                    else
                        dGV_AnalyzedSentence.Rows[1].Cells[i].Value = "...";
                    if (analyzed_sentence[i].ErrorCode != LexAnalysis.ErrorCodeLA.NOT_ERROR)
                        dGV_AnalyzedSentence.Rows[2].Cells[i].Value = analyzed_sentence[i].ErrorCode.ToString();
                    else
                        dGV_AnalyzedSentence.Rows[2].Cells[i].Value = "...";
                }
            }
            else dGV_AnalyzedSentence.ColumnCount = 1;
        }

        public void ReportLA(Lexema[] tableOfLexemas)
        {
            dGV_TableOfLexemas.Rows.Clear();
            dGV_TableOfLexemas.RowCount = tableOfLexemas.Length;
            for (int i = 0; i < tableOfLexemas.Length; i++)
            {
                dGV_TableOfLexemas.Rows[i].Cells["NameLex"].Value = tableOfLexemas[i].Name;
                dGV_TableOfLexemas.Rows[i].Cells["ClassLex"].Value = tableOfLexemas[i].Class;
                dGV_TableOfLexemas.Rows[i].Cells["Hits"].Value = tableOfLexemas[i].CountHits;
            }
        }

        private async void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (oFD_LoadSourceCode.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(oFD_LoadSourceCode.FileName);
                try
                {
                    rTB_SourceCode.Clear();
                    rTB_SourceCode.Text += await reader.ReadLineAsync();
                    lblStatusInFile.ForeColor = Color.Yellow;
                    lblStatusInFile.Text = "Loading started...";

                    while (!reader.EndOfStream)
                    {
                        rTB_SourceCode.Text += '\n';
                        rTB_SourceCode.Text += await reader.ReadLineAsync();
                    }

                    lblStatusInFile.ForeColor = Color.Green;
                    lblStatusInFile.Text = "Load source code from file is successfull";
                    btnSaveChanges.Enabled = true;
                }
                finally
                {
                    reader.Close();
                }

                txB_PathToFile.ForeColor = Color.Black;
                txB_PathToFile.Text = oFD_LoadSourceCode.FileName;
            }
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (File.Exists(oFD_LoadSourceCode.FileName))
            {
                StreamWriter writer = new StreamWriter(oFD_LoadSourceCode.FileName, false);
                try
                {
                    string[] lines = rTB_SourceCode.Lines;
                    lblStatusInFile.ForeColor = Color.Yellow;
                    lblStatusInFile.Text = "Saving started...";

                    foreach (string line in lines)
                        await writer.WriteLineAsync(line);

                    lblStatusInFile.ForeColor = Color.Green;
                    lblStatusInFile.Text = "Saving changes to file is successfull";
                }
                finally
                {
                    writer.Close();
                }
            }
            else
            {
                MessageBox.Show("First, download the file.", "File is not loaded");
            }
        }

        private async void btnNewFile_Click(object sender, EventArgs e)
        {
            if (sFD_SaveSourceCode.ShowDialog() == DialogResult.OK)
            {
                bool rewrite = false; //флаг, указывающий на то, нужна ли перезапись файла 
                if (File.Exists(sFD_SaveSourceCode.FileName))
                {
                    rewrite = true;
                    if (MessageBox.Show("Rewrite file?", "Checked name file is exist", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                StreamWriter writer = new StreamWriter(sFD_SaveSourceCode.FileName);
                try
                {
                    string[] lines = rTB_SourceCode.Lines;

                    foreach (string line in lines)
                        await writer.WriteLineAsync(line);

                    if (rewrite)
                    {
                        lblStatusInFile.ForeColor = Color.Green;
                        lblStatusInFile.Text = "File rewrited is successfull";
                    }
                    else
                    {
                        lblStatusInFile.ForeColor = Color.Green;
                        lblStatusInFile.Text = "New file created and writed is successfull";
                    }
                    txB_PathToFile.Text = sFD_SaveSourceCode.FileName;
                    btnSaveChanges.Enabled = true;
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
           _compilerSimPas.Compile();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    internal class FictitiousRecipient: IOutInfoCompiler
    {
        public void Comment(uint beg, uint end = 0)
        {            
        }

        public void CurrentContext(LexClass l_cl_word, uint beg_word, uint end_word)
        {
        }

        public void ErroneousWord(string errorMessage, uint beg_word, uint end_word)
        {
        }

        public void ReportLA(LexAnalysis.WordInSentence[] analyzed_sentence)
        {
        }

        public void ReportLA(Lexema[] tableOfLexemas)
        {
        }
    }
}

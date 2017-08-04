using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    class ErrorInOrderWordsException : Exception
    {
        private LexClass wait;
        private LexClass meet;

        public ErrorInOrderWordsException(LexClass w, LexClass m)            
        {
            wait = w;
            meet = m;
        }

        public override string ToString()
        {
            return "Ожидалось " + wait.ToString() + ", встретилось " + meet.ToString();
        }
    }
}

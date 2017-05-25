using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    public class Compiler
    {
        SyntaxAnalysis _sA;

        public void Compile()
        {            
            //await Task.Run(new Func<Task>(_sA.Start));
            _sA.Start();
        }        

        private Compiler(IEnterStream input_stream, IOutInfoCompiler info)
        {
            _sA = new SyntaxAnalysis(input_stream, info);
        }

        public static Compiler CreateInstanceCompiler(IEnterStream input_stream, IOutInfoCompiler info)
        {
            if (input_stream == null)
                return null;
            else 
                return new Compiler(input_stream, info);
        }
    }
}

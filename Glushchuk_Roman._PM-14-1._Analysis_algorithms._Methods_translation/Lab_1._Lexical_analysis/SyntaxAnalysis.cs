using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    internal class SyntaxAnalysis
    {
        public enum ErrorCodeSA //вопрос: а допустимо или скорее рационально ли использовать перечисление?
                                //лучше добавить сообщение типа "ожидалось..., встретилось..."
        {
        }

        private Lexema _currLexema;
        private LexAnalysis.ErrorCodeLA errorLA;
        private LexAnalysis _lA;

        private SemanticAnalysis _semAn;

        internal void/*async Task*/ Start()
        {
            _lA.StartAgain();
            errorLA = new LexAnalysis.ErrorCodeLA();
            do
            {
                _currLexema = _lA.GetNextWord(out errorLA);
            }
            while ((errorLA != LexAnalysis.ErrorCodeLA.END_INPUT_TAPE) && (errorLA != LexAnalysis.ErrorCodeLA.UNKNOWN));

            if (errorLA != LexAnalysis.ErrorCodeLA.UNKNOWN)
            {
                _lA.Broadcast.ReportLA(_lA.Table.Lexemas);
                _lA.Broadcast.ReportLA(_lA.Sentence);
            }
        }

        private void Program()
        {
            if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
            {
                //выдать ошибку
                return;
            }

            if (_currLexema.Class == LexClass.Kw_program)
            {
                if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                {
                    //выдать ошибку
                    return;
                }
                if (_currLexema.Class == LexClass.ID)
                {
                    if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                    {
                        //выдать ошибку
                        return;
                    }
                    if (_currLexema.Class == LexClass.Sc_Semic)
                        if (Unit())
                        {
                            if (_currLexema == null)
                            {
                                //выдать ошибку
                                return;
                            }
                            if (_currLexema.Class == LexClass.Sc_Dot)
                                ;
                            else ;
                        }
                        else ;
                    else ;
                }
                else ;
            }
            else ;
            
        }

        private bool Unit()
        {
            if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
            {
                //выдать ошибку
                return false;
            }
                
            DefinitionTypes();
            DefinitionConstants();
            DeclarationVariables();
            MultipleAction();
            return false;
        }

        private void DefinitionTypes()
        {
            if (_currLexema.Class == LexClass.Kw_type)
                do
                {
                    TypeDefinition();
                    if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                    {
                        //выдать ошибку
                        return;
                    }
                } while (_currLexema.Class == LexClass.Sc_Semic);
        }

        private void TypeDefinition()
        {
            if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
            {
                //выдать ошибку
                return;
            }
            if (_currLexema.Class == LexClass.ID)
            {
                if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                {
                    //выдать ошибку
                    return;
                }
                if (_currLexema.Class == LexClass.Sc_Asnt)
                {
                    Type();
                }
                else ;
            }
            else ;
        }

        private void Type()
        { 
            if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
            {
                //выдать ошибку
                return;
            }
            switch (_currLexema.Class)
            {
                case LexClass.ID:
                    break;
                case LexClass.Kw_array:
                    break;
                case LexClass.Const_int:
                    break;
                default:
                    break;
            }
        }

        private void SubscriptRange()
        {

        }

        private void DefinitionConstants()
        {
            if (_currLexema.Class == LexClass.Kw_const)
                do
                {
                    ConstantDefinition();
                    if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                    {
                        //выдать ошибку
                        return;
                    }
                } while (_currLexema.Class == LexClass.Sc_Semic);
        }

        private void ConstantDefinition()
        {

        }

        private void Constant()
        {

        }

        private void VariableDeclaration()
        {

        }

        private void IdentifierList()
        {

        }        

        private void DeclarationVariables()
        {
            if (_currLexema.Class == LexClass.Kw_var)
                do
                {
                    VariableDeclaration();
                    if ((_currLexema = _lA.GetNextWord(out errorLA)) == null)
                    {
                        //выдать ошибку
                        return;
                    }
                } while (_currLexema.Class == LexClass.Sc_Semic);
        }

        private void MultipleAction()
        {

        }

        private void SequenceOfActions()
        {

        }

        private void Action()
        {
        }

        private void SimpleAction()
        {
        }

        private void Assignment()
        {
        }

        private void Variable()
        {
        }

        private void SelectionComponent()
        {
        }

        private void Expression()
        {
        }

        private void SimpleExpression()
        {
        }

        private void Addend()
        {
        }

        private void Multiplier()
        {
        }

        private void MultiplicativeOperation()
        {
        }

        private void AdditiveOperation()
        {
        }

        private void RelationalOperator()
        {
        }

        private void ComplexAction()
        {
        }

        internal SyntaxAnalysis(IEnterStream input_stream, IOutInfoCompiler info)
        {
            _lA = new LexAnalysis(input_stream, info);
        }
    }
}

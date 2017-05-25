using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Класс, реализущий входной поток символов для компилятора из компонента пользовательского интерфейса класса Control 
    /// или классов, которые наследуются от него
    /// </summary>
    class EnterStream_fromUI: EnterStream
    {
        /// <summary>
        /// Текущий считываемый символ
        /// </summary>
        private char _currSymbol = '\0';
        /// <summary>
        /// Источник текстовых данных
        /// </summary>
        private Control _textUISource;

        public override char CurrentSymbol
        {
            get 
            {
                //if (_textUISource != null)
                //    return _textUISource.Text[(int)_indexCurrentSymbol];
                //else
                //    return '\0';

                return _currSymbol;
            }
        }

        public override char GetNextSymbol()
        {            
            if (_textUISource != null)
                if (_indexCurrentSymbol + 1 < _textUISource.Text.Length)
                    return (_currSymbol = _textUISource.Text[(int)(++_indexCurrentSymbol)]);
                else
                {
                    _indexCurrentSymbol = (uint)_textUISource.Text.Length;
                    return (_currSymbol = Convert.ToChar(28u));
                }
            return (_currSymbol = '\0');     
        }

        public override void ToBegin()
        {                    
            _indexCurrentSymbol = 0;
            if (_textUISource != null)
                if (_textUISource.Text.Length > _indexCurrentSymbol)
                    _currSymbol = _textUISource.Text[(int)_indexCurrentSymbol];
                else
                {
                    _indexCurrentSymbol = (uint)_textUISource.Text.Length;
                    _currSymbol = (char)28u;
                }
            else _currSymbol = '\0';
        }

        public EnterStream_fromUI(Control entry_field)
        {
            _textUISource = entry_field;
            if (this._textUISource != null)            
                if (this._textUISource.Text.Length > 0)                   
                    _currSymbol = _textUISource.Text[0];
        }

        public EnterStream_fromUI(Control entry_field, uint start_position)
        {
            _textUISource = entry_field;
            if (this._textUISource != null)
                if (start_position < this._textUISource.Text.Length)
                {
                    _indexCurrentSymbol = start_position;
                    _currSymbol = _textUISource.Text[(int)_indexCurrentSymbol];
                }
                else
                {
                    _indexCurrentSymbol = (uint)_textUISource.Text.Length;
                    _currSymbol = (char)28u;
                }            
        }
    }
}

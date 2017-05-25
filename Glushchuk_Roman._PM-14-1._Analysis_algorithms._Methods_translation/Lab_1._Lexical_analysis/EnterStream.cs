using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Класс-шаблон для создания классов, реализующих интерфейс IEnterStream 
    /// </summary>
    abstract class  EnterStream: IEnterStream
    {
        /// <summary>
        /// Индекс текущего считываемого символа
        /// </summary>
        protected uint _indexCurrentSymbol = 0;      
       
        public uint IndexCurrentSymbol
        {
            get 
            { 
                return _indexCurrentSymbol; 
            }
        }

        public abstract char CurrentSymbol
        {
            get;
        }

        public abstract char GetNextSymbol();

        public abstract void ToBegin();        
    }
}

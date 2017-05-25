using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Интерфейс, задающий необходимые компилятору методы для посимвольного считывания исходного текста программы из источника данных 
    /// </summary>
    public interface IEnterStream
    {
        /// <summary>
        /// Получает текущий считываемый символ
        /// </summary>
        char CurrentSymbol { get; }
        /// <summary>
        /// Получает индекс текущего считываемого символа
        /// </summary>
        uint IndexCurrentSymbol { get; }
        /// <summary>
        /// Считывает следующий символ и возвращает его
        /// </summary>
        /// <returns>следующий символ в источнике данных</returns>
        char GetNextSymbol();
        /// <summary>
        /// Переводит указатель на считываемый символ в начало источника данных и считывает первый символ
        /// </summary>
        void ToBegin();
    }
}

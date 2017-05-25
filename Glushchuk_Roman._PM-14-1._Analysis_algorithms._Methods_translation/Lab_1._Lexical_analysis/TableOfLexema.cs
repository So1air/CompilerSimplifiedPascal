using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Класс-множество, для хранения информации о всех лексемах
    /// </summary>
    class TableOfLexema
    {
        /// <summary>
        /// Словарь всех добавленных лексем
        /// </summary>
        private Dictionary<string, Lexema> _correctWords = new Dictionary<string, Lexema>();
        /// <summary>
        /// Позволяет добавить новую лексему. Если лексема с одинаковым именем уже есть в множестве, 
        /// то добавление не произойдёт. Если параметр addHit равен true, то свойство 
        /// CountHits, уже имеющейся или только добавленной лексемы будет увеличено на единицу.
        /// </summary>
        /// <param name="newLexema">добавляемая лексема</param>
        /// <param name="addHit">флаг, означающий, увеличивать ли CountHits после попытки добавления</param>
        /// <returns></returns>
        internal bool AddLexema(Lexema newLexema, bool addHit = false)
        {
            if (newLexema != null)
                if ((newLexema.Name != null) && (newLexema.Name.Length != 0))
                    if (! _correctWords.ContainsKey(newLexema.Name))
                    {
                        _correctWords.Add(newLexema.Name, (addHit ? ++newLexema : newLexema));
                        return true;
                    }
                    else
                        if (addHit)
                        {
                            ++_correctWords[newLexema.Name];
                            return true;
                        }

            return false;
        }
        /// <summary>
        /// Возвращает ссылку на лексему по её строковому представлению
        /// </summary>
        /// <param name="name_word">строковое представление требуемой лексемы</param>
        /// <returns></returns>
        internal Lexema GetLexema(string name_word)
        {
            if ((name_word != null) && (_correctWords.ContainsKey(name_word)))
                return _correctWords[name_word];
            else
                return null;
        }
        /// <summary>
        /// Возвращает ссылку на лексему по её порядковому номеру
        /// </summary>
        /// <param name="index">порядковый номер требуемой лексемы</param>
        /// <returns></returns>
        internal Lexema GetLexema(uint index)
        {
            if (index < _correctWords.Count)
                return _correctWords.Values.ToArray<Lexema>()[index];
            else
                return null;
        }
        /// <summary>
        /// Возвращает все лексемы в множестве в виде массива
        /// </summary>
        internal Lexema[] Lexemas
        {
            get { return _correctWords.Values.ToArray<Lexema>(); }
        }
        /// <summary>
        /// Удаляет все лексемы из множества
        /// </summary>
        internal void Clear()
        {
            _correctWords.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Интерфейс, позволящий компилятору динамически на этапе выполнения компиляции 
    /// сообщать внешней программе информацию о процессе компиляции
    /// </summary>
    public interface IOutInfoCompiler
    {
        /// <summary>
        /// Указывает внешней программе, на каком промежутке символов встретился комментарий 
        /// </summary>
        /// <param name="beg">индекс символа, с которого начался комментарий</param>
        /// <param name="end">индекс символа, на котором закончился комментарий, если это значение равно нулю, то комментарий закончился в конце источника</param>
        void Comment(uint beg, uint end = 0);
        /// <summary>
        /// Сообщает внешней программе, что на указаном промежутке символов встретилась лексема указаного класса 
        /// </summary>
        /// <param name="l_cl_word">лексический класс выделеного слова</param>
        /// <param name="beg_word">индекс символа, с которого началась лексема</param>
        /// <param name="end_word">индекс символа, на котором лексема закончилась</param>
        void CurrentContext(LexClass l_cl_word, uint beg_word, uint end_word);
        /// <summary>
        /// Сообщает внешней программе, что на указаном промежутке символов встретилось недопустимое слово
        /// </summary>
        /// <param name="errorMessage">информация об ошибке</param>
        /// <param name="beg_word">индекс символа, с которого началось ошибочное слово</param>
        /// <param name="end_word">индекс символа, на котором ошибочное слово закончилось</param>
        void ErroneousWord(string errorMessage, uint beg_word, uint end_word);
        /// <summary>
        /// Выдает отчёт о лексическом анализе исходного текста 
        /// </summary>
        /// <param name="analyzed_sentence">проанализированое предложение</param>
        void ReportLA(LexAnalysis.WordInSentence[] analyzed_sentence);
        /// <summary>
        /// Выдает отчёт о встреченых в тексте лексемах 
        /// </summary>
        /// <param name="tableOfLexemas">сформированная на этапе лексического анализа таблица лексем</param>
        void ReportLA(Lexema[] tableOfLexemas);
    }
}

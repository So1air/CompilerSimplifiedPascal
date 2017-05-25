using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Класс для хранения информации о употребляемых в исходном тексте программы словах-лексемах
    /// </summary>
    public class Lexema
    {
        /// <summary>
        /// Строковое представление слова-лексемы 
        /// </summary>
        private string _name;
        /// <summary>
        /// Лексический класс слова-лексемы
        /// </summary>
        private LexClass _class;
        /// <summary>
        /// Количество встретившихся в исходном тексте программы экземпляров даного слова-лексемы 
        /// </summary>
        private ushort _countHits;       
        //private <атрибуты> _attributes;        
        /// <summary>
        /// Получает строковое представление лексемы
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// Получает лексический класс лексемы
        /// </summary>
        internal LexClass Class
        {
            get { return _class; }
        }
        /// <summary>
        /// Получает количество встретившихся в исходном тексте программы экземпляров даной лексемы
        /// </summary>
        public ushort CountHits
        {
            get { return _countHits; }
        }
        /// <summary>
        /// Увеличивает значение свойства CountHits на 1
        /// </summary>
        /// <param name="obj1"></param>
        /// <returns></returns>
        public static Lexema operator++(Lexema obj1)
        {
            obj1._countHits += 1;
            return obj1;
        }
        /// <summary>
        /// Конструктор, инициализирующий свойства Name и Class, свойство CountHits инициализируется нулём 
        /// </summary>
        /// <param name="name">строковое представление лексемы</param>
        /// <param name="cl">лексический класс лексемы</param>
        public Lexema(string name, LexClass cl)
        {
            _name = name;
            _class = cl;
            _countHits = 0;
        }
    }
}

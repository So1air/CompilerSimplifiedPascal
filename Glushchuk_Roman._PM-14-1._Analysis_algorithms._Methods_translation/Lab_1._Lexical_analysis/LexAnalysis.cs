using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    public class LexAnalysis
    {
        /// <summary>
        /// Полный набор всех возможных ошибок компиляции на этапе лексического анализа 
        /// </summary>
        public enum ErrorCodeLA
        {
            /// <summary>
            /// Ошибки нет
            /// </summary>
            NOT_ERROR = 0,
            /// <summary>
            /// Невозможно выделить следующее слово: конец входного потока символов
            /// </summary>
            END_INPUT_TAPE = 1,
            /// <summary>
            /// В исходном тексте программы встретился недопустимый символ
            /// </summary>
            INVALID_SYMBOL = 2,
            /// <summary>
            /// Не удалось распознать числовой литерал: встречены недопустимые символы
            /// </summary>
            INVALID_NUMBER = 3,
            /// <summary>
            /// Неизвестная ошибка
            /// </summary>
            UNKNOWN = 4,
            /// <summary>
            /// Недопустимый идентификатор
            /// </summary>
            INVALID_ID = 5,
            /// <summary>
            /// Недопустимый строковый литерал: до конца строки не обнаружена закрывающая кавычка
            /// </summary>
            INVALID_STRING = 6,
            /// <summary>
            /// Недопустимый многострочный комментарий: до конца входного потока символов не обнаружена закрывающая скобка
            /// </summary>
            INVALID_MULTILINE_COMMENT = 7
        }
        
        /// <summary>
        /// Вспомагательная структура для хранения информации о словах анализируемого предложения
        /// </summary>
        public struct WordInSentence
        {
            /// <summary>
            /// Проанализированное слово
            /// </summary>
            private string _word;
            /// <summary>
            /// Ссылка на соответствующую лексему
            /// </summary>
            private Lexema _coincidentLexema;
            /// <summary>
            /// Информация о наличии ошибки в даном слове
            /// </summary>
            private ErrorCodeLA _errorCode;
            /// <summary>
            /// Получает строку, представлящую проанализированое слово
            /// </summary>
            public string Word
            {
                get { return _word; }
            }
            /// <summary>
            /// Получает ссылку на соответствующую лексему
            /// </summary>
            public Lexema CoincidentLexema
            {
                get { return _coincidentLexema; }
            }
            /// <summary>
            /// Получает код ошибки типа ErrorCodeLA
            /// </summary>
            public ErrorCodeLA ErrorCode
            {
                get { return _errorCode; }
            }
            /// <summary>
            /// Конструктор, позволящий создать экземпляр этого класса для хранения информации о корректном 
            /// в смысле лексического анализа слове 
            /// </summary>
            /// <param name="word">проанализированное слово</param>
            /// <param name="lex">ссылка на соответствующую лексему</param>
            public WordInSentence(string word, Lexema lex)
            {
                _word = word;
                _coincidentLexema = lex;
                _errorCode = ErrorCodeLA.NOT_ERROR;
            }
            /// <summary>
            /// Конструктор, позволящий создать экземпляр этого класса для хранения информации об ошибочном 
            /// в смысле лексического анализа слове 
            /// </summary>
            /// <param name="word">проанализированное слово</param>
            /// <param name="error_code">код ошибки</param>
            public WordInSentence(string word, ErrorCodeLA error_code)
            {
                _word = word;
                _coincidentLexema = null;
                _errorCode = error_code;
            }
        }
        
        public const char SPACE = ' ',
                          ENTER = '\n',
                          TAB = '\t',
                          SPLITTER_FILES = (char)28u,
                          NULL_SYMBOL = '\0';
        /// <summary>
        /// Последнее считаное слово
        /// </summary>
        private string _lastWord = "";
        /// <summary>
        /// Информативная переменная для хранения начального индекса считываемого слова или комментария
        /// </summary>
        private uint _indBegWord;
        /// <summary>
        /// Таблица лексем
        /// </summary>
        private TableOfLexema _table = new TableOfLexema();
        /// <summary>
        /// Ссылка на предоставленный интерфейс общения с внешней программой
        /// </summary>
        private IOutInfoCompiler _broadcast;
        /// <summary>
        /// Ссылка на предоставленный источник исходного кода
        /// </summary>
        private IEnterStream _inputTape;
        /// <summary>
        /// Список проанализированных слов
        /// </summary>
        private List<WordInSentence> _orderWordsInSentence = new List<WordInSentence>();

        /// <summary>
        /// Получает последнее считаное слово
        /// </summary>
        internal string LastWord
        {
            get { return _lastWord; }
        }
        /// <summary>
        /// Получает количество считаных слов
        /// </summary>
        internal int CountReadedWords
        {
            get { return _orderWordsInSentence.Count; }
        }
        /// <summary>
        /// Возвращает копию списка проаназированных слов
        /// </summary>
        internal WordInSentence[] Sentence
        {            
            get { return _orderWordsInSentence.ToArray(); }
        }

        //public bool AllWordsIsCorrect
        //{
        //    get { return _allWordsIsCorrect; }
        //}

        /// <summary>
        /// Получает ссылку на таблицу лексем
        /// </summary>
        internal TableOfLexema Table
        {
            get { return _table; }
        }
        /// <summary>
        /// Получает ссылку на предоставленный интерфейс общения с внешней программой
        /// </summary>
        internal IOutInfoCompiler Broadcast
        {
            get { return _broadcast; }
            set { _broadcast = value; }
        }
        /// <summary>
        /// Пропускает все символы после символа однострочного комментария до конца строки
        /// </summary>
        /// <param name="error_code"></param>
        private void IgnoreOneLineComment(ref ErrorCodeLA error_code)
        {
            do
            {
                switch (_inputTape.GetNextSymbol())
                {
                    case ENTER:
                    case SPLITTER_FILES:
                        _broadcast.Comment(_indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return;

                    case NULL_SYMBOL:
                        error_code = ErrorCodeLA.UNKNOWN;
                        _broadcast.Comment(_indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return;

                    default:
                        break;
                }
            }
            while (true);
        }
        /// <summary>
        /// Пропускает все символы после начинающего символа многострочного комментария до завершающего 
        /// символа. Выдает ошибку если завершающий символ не обнаружен
        /// </summary>
        /// <param name="error_code">ссылка на переменную фиксирующую код лексической ошибки</param>
        private void IgnoreMultiLineComment(ref ErrorCodeLA error_code)
        {
            do
            {
                switch (_inputTape.GetNextSymbol())
                {
                    case '}':
                        _broadcast.Comment(_indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return;

                    case SPLITTER_FILES:
                        _broadcast.Comment(_indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        error_code = ErrorCodeLA.INVALID_MULTILINE_COMMENT;
                        return;

                    case NULL_SYMBOL:
                        _broadcast.Comment(_indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        error_code = ErrorCodeLA.UNKNOWN;
                        return;

                    default:
                        break;
                }
            } 
            while (true);
        }
        /// <summary>
        /// Распознает во входном потоке символов идентификатор или ключевое слово 
        /// </summary>
        /// <param name="error_code">ссылка на переменную фиксирующую код лексической ошибки</param>
        /// <returns></returns>
        private Lexema ReadID_or_Keyword(ref ErrorCodeLA error_code)
        {
            _lastWord += _inputTape.CurrentSymbol;
            error_code = ErrorCodeLA.NOT_ERROR;
            do
            {
                if (IsLetter(_inputTape.GetNextSymbol()))
                    _lastWord += _inputTape.CurrentSymbol;
                else
                    if (IsDigit(_inputTape.CurrentSymbol))
                        _lastWord += _inputTape.CurrentSymbol;
                    else
                        switch (_inputTape.CurrentSymbol)
                        {
                            case '_':
                                _lastWord += '_';
                                break;

                            case ';':
                            case SPACE:                            
                            case ENTER:
                            case TAB:
                            case '/':
                            case '{':
                            case ':':
                            case '.':
                            case '=':
                            case '[':
                            case ']':
                            case '(':
                            case ')':
                            case ',':
                            case '<':
                            case '>':
                            case '+':
                            case '-':                            
                            case '*':
                            case SPLITTER_FILES:
                                if (error_code == ErrorCodeLA.NOT_ERROR)
                                {
                                    //Lexema resultLexema /*= _table.GetLexema(_lastWord)*/;
                                    //if (resultLexema == null)
                                        _table.AddLexema(/*resultLexema = */new Lexema(_lastWord, LexClass.ID), true);
                                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, /*resultLexema*/_table.GetLexema(_lastWord)));
                                    _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                                    return /*resultLexema*/_table.GetLexema(_lastWord);
                                }
                                else
                                {
                                    _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                                    _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                                    return null;
                                }

                            case NULL_SYMBOL:
                                error_code = ErrorCodeLA.UNKNOWN;
                                _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                                _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                                return null;
                            //case '\'':
                            //    error_code = ErrorCodeLA.INVALID_ID;
                            //    break;
                            default:
                                error_code = ErrorCodeLA.INVALID_ID;
                                _lastWord += _inputTape.CurrentSymbol;
                                break;
                        }
            }
            while (true);
        }
        /// <summary>
        /// Распознает во входном потоке символов строковый литерал
        /// </summary>
        /// <param name="error_code">ссылка на переменную фиксирующую код лексической ошибки</param>
        /// <returns></returns>
        private Lexema ReadStringLiteral(ref ErrorCodeLA error_code)
        {
            _lastWord += '\'';
            error_code = ErrorCodeLA.NOT_ERROR;
            do
            {
                switch (_inputTape.GetNextSymbol())
                {
                    case ENTER:
                        error_code = ErrorCodeLA.INVALID_STRING;
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                        _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        _inputTape.GetNextSymbol();
                        return null;

                    case SPLITTER_FILES:
                        error_code = ErrorCodeLA.END_INPUT_TAPE;
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                        _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return null;

                    case NULL_SYMBOL:
                        error_code = ErrorCodeLA.UNKNOWN;
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                        _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return null;

                    case '\'':
                        _lastWord += '\'';                        
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Const_str), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    default:
                        _lastWord += _inputTape.CurrentSymbol;
                        break;
                }
            }
            while (true);
        }
        /// <summary>
        /// Распознает во входном потоке символов числовой литерал
        /// </summary>
        /// <param name="error_code">ссылка на переменную фиксирующую код лексической ошибки</param>
        /// <returns></returns>
        private Lexema ReadNumberLiteral(ref ErrorCodeLA error_code)
        {
            _lastWord += _inputTape.CurrentSymbol;
            error_code = ErrorCodeLA.NOT_ERROR;
            do
            {
                if (IsDigit(_inputTape.GetNextSymbol()))
                    _lastWord += _inputTape.CurrentSymbol;
                else
                    switch (_inputTape.CurrentSymbol)
                    {
                        case ';':
                        case SPLITTER_FILES:
                        case SPACE:
                        case ENTER:
                        case TAB:
                        case '/':
                        case '{':
                        case '\'':
                        case ':':
                        case '.':
                        case '=':
                        case '[':
                        case ']':
                        case '(':
                        case ')':
                        case ',':
                        case '<':
                        case '>':
                        case '+':
                        case '-':
                        case '*':
                            if (error_code == ErrorCodeLA.NOT_ERROR)
                            {
                                _table.AddLexema(new Lexema(_lastWord, LexClass.Const_int), true);
                                _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                                _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                                return _table.GetLexema(_lastWord);
                            }
                            else
                            {
                                _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                                _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                                return null;
                            }

                        case NULL_SYMBOL:
                            error_code = ErrorCodeLA.UNKNOWN;
                            _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                            _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                            return null;

                        default:                            
                            error_code = ErrorCodeLA.INVALID_NUMBER;
                            _lastWord += _inputTape.CurrentSymbol;
                            break;
                    }
            }
            while (true);
        }
        /// <summary>
        /// Возвращает true, если переданный символ является латинской буквой, иначе возвращает false
        /// </summary>
        /// <param name="symbol">проверяемый символ</param>
        /// <returns></returns>
        internal bool IsLetter(char symbol)
        {
            return (((symbol >= 'a') && (symbol <= 'z')) || ((symbol >= 'A') && (symbol <= 'Z')));
        }
        /// <summary>
        /// Возвращает true, если переданный символ является цифрой, иначе возвращает false
        /// </summary>
        /// <param name="symbol">проверяемый символ</param>
        /// <returns></returns>
        internal bool IsDigit(char symbol)
        {
            return (symbol >= '0') && (symbol <= '9');
        }
        /// <summary>
        /// Распознает следующее слово во входном потоке символов. Если слово лексически верно, возвращает ссылку на соответствующую лексему
        /// </summary>
        /// <returns></returns>
        internal Lexema GetNextWord()
        {
            ErrorCodeLA error_c;
            return GetNextWord(out error_c);
        }
        /// <summary>
        /// Распознает следующее слово во входном потоке символов. Если слово лексически верно, возвращает ссылку на соответствующую лексему
        /// </summary>
        /// <param name="error_code">ссылка на переменную фиксирующую код лексической ошибки</param>
        /// <returns></returns>
        internal Lexema GetNextWord(out ErrorCodeLA error_code)
        {
            error_code = ErrorCodeLA.NOT_ERROR;           
            
            _lastWord = "";
            _indBegWord = _inputTape.IndexCurrentSymbol;
            if (IsLetter(_inputTape.CurrentSymbol))
                return ReadID_or_Keyword(ref error_code);
            else 
                if (IsDigit(_inputTape.CurrentSymbol))
                    return ReadNumberLiteral(ref error_code);
                else
                switch (_inputTape.CurrentSymbol)
                {            
                    case SPACE: 
                    case ENTER:
                    case TAB:
                        _inputTape.GetNextSymbol();            
                        return GetNextWord(out error_code);                   
                    
                    case ';':
                        _lastWord = ";";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Semic), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);
                    
                    case ':':
                        if (_inputTape.GetNextSymbol() == '=')
                        {
                            _lastWord = ":=";
                            _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Asnt), true);
                            _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                            _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                            _inputTape.GetNextSymbol();
                            return _table.GetLexema(_lastWord);
                        }
                        else
                        {
                            _lastWord = ":";
                            _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Colon), true);
                            _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                            _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                            return _table.GetLexema(_lastWord);
                        }                 

                    case '.':
                        if (_inputTape.GetNextSymbol() == '.')
                        {
                            _lastWord = "..";
                            _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Dots), true);
                            _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                            _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                            _inputTape.GetNextSymbol();
                            return _table.GetLexema(_lastWord);
                        }
                        else
                        {
                            _lastWord = ".";
                            _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Dot), true);
                            _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord))); 
                            _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                            return _table.GetLexema(_lastWord);
                        }

                    case '=':
                        _lastWord = "=";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_RelOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);                        
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '[':
                        _lastWord = "[";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_LsqBrk), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);                        
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case ']':
                        _lastWord = "]";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_RsqBrk), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));                        
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '(':
                        _lastWord = "(";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Lprnts), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);                        
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case ')':
                        _lastWord = ")";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Rprnts), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord))); 
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();                       
                        return _table.GetLexema(_lastWord);

                    case ',':
                        _lastWord = ",";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_Comma), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '<':
                        switch (_inputTape.GetNextSymbol())
                        {
                            case '=':
                                _lastWord = "<=";                                                        
                                _inputTape.GetNextSymbol();
                                break;

                            case '>':
                                _lastWord = "<>";
                                _inputTape.GetNextSymbol();
                                break;

                            default:
                                _lastWord = "<";
                                break;
                        }
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_RelOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return _table.GetLexema(_lastWord);

                    case '>':
                        if (_inputTape.GetNextSymbol() == '=')
                        {
                            _lastWord = ">=";
                            _inputTape.GetNextSymbol();
                        }
                        else                        
                            _lastWord = ">";

                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_RelOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                        return _table.GetLexema(_lastWord);

                    case '+':
                        _lastWord = "+";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_AddOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '-': 
                        _lastWord = "-";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_AddOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '*':
                        _lastWord = "*";
                        _table.AddLexema(new Lexema(_lastWord, LexClass.Sc_MultOp), true);
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, _table.GetLexema(_lastWord)));
                        _broadcast.CurrentContext(_orderWordsInSentence[_orderWordsInSentence.Count - 1].CoincidentLexema.Class, _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return _table.GetLexema(_lastWord);

                    case '\'':
                        return ReadStringLiteral(ref error_code);

                    case '/':
                        if (_inputTape.GetNextSymbol() == '/')
                        {
                            IgnoreOneLineComment(ref error_code);
                            if (error_code == ErrorCodeLA.NOT_ERROR)
                                return GetNextWord(out error_code);
                            else                                
                                return null;
                        }
                        else
                        {
                            error_code = ErrorCodeLA.INVALID_SYMBOL;
                            _orderWordsInSentence.Add(new WordInSentence(_inputTape.CurrentSymbol.ToString(), error_code)); 
                            _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol - 1);
                            return null;
                        }

                    case '{':
                        IgnoreMultiLineComment(ref error_code);
                        if (error_code == ErrorCodeLA.NOT_ERROR)
                            return GetNextWord(out error_code);
                        else
                            return null;

                    case SPLITTER_FILES:
                        error_code = ErrorCodeLA.END_INPUT_TAPE;
                        return null;

                    case NULL_SYMBOL:
                        error_code = ErrorCodeLA.UNKNOWN;
                        return null;

                    default:
                        error_code = ErrorCodeLA.INVALID_SYMBOL;
                        _lastWord += _inputTape.CurrentSymbol;
                        _orderWordsInSentence.Add(new WordInSentence(_lastWord, error_code));
                        _broadcast.ErroneousWord("ErrorCodeLA." + error_code.ToString(), _indBegWord, _inputTape.IndexCurrentSymbol);
                        _inputTape.GetNextSymbol();
                        return null;
                }
        }
        /// <summary>
        /// Повторно инициализирует лексический анализатор для повторного анализа 
        /// </summary>
        public void StartAgain()
        {
            _table.Clear();
            InitializeTable();
            _orderWordsInSentence.Clear();
            _inputTape.ToBegin();
            _lastWord = "";
        }
        /// <summary>
        /// Инициализирует таблицу лексем ключевыми словами языка и специальными символами представленными в языке в виде буквеной строки
        /// </summary>
        private void InitializeTable()
        {            
            _table.AddLexema(new Lexema("array", LexClass.Kw_array));
            _table.AddLexema(new Lexema("begin", LexClass.Kw_begin));
            _table.AddLexema(new Lexema("const", LexClass.Kw_const));
            _table.AddLexema(new Lexema("do", LexClass.Kw_do));
            _table.AddLexema(new Lexema("else", LexClass.Kw_else));
            _table.AddLexema(new Lexema("end", LexClass.Kw_end));
            _table.AddLexema(new Lexema("for", LexClass.Kw_for));
            _table.AddLexema(new Lexema("if", LexClass.Kw_if));
            _table.AddLexema(new Lexema("not", LexClass.Kw_not));
            _table.AddLexema(new Lexema("of", LexClass.Kw_of));
            _table.AddLexema(new Lexema("program", LexClass.Kw_program));
            _table.AddLexema(new Lexema("then", LexClass.Kw_then));
            _table.AddLexema(new Lexema("to", LexClass.Kw_to));
            _table.AddLexema(new Lexema("type", LexClass.Kw_type));
            _table.AddLexema(new Lexema("var", LexClass.Kw_var));
            _table.AddLexema(new Lexema("while", LexClass.Kw_while));
            _table.AddLexema(new Lexema("str", LexClass.Pd_str));
            _table.AddLexema(new Lexema("integer", LexClass.Pd_integer));
         
            _table.AddLexema(new Lexema("or", LexClass.Sc_AddOp));
            _table.AddLexema(new Lexema("div", LexClass.Sc_MultOp));
            _table.AddLexema(new Lexema("mod", LexClass.Sc_MultOp));
            _table.AddLexema(new Lexema("and", LexClass.Sc_MultOp));
        }

        internal LexAnalysis(IEnterStream source)
            :this(source, null)
        {
        }

        internal LexAnalysis(IEnterStream source, IOutInfoCompiler informator)
        {
            InitializeTable();
            _inputTape = source;
            if (informator == null)
                _broadcast = new FictitiousRecipient(); //глухой слушатель
            else
                _broadcast = informator;
        }    
    }
}
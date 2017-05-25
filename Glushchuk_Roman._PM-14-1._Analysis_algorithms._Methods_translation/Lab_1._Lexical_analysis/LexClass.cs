using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerSimplifiedPascal
{
    /// <summary>
    /// Перечисление всех лексических классов, использующихся в компиляторе
    /// </summary>
    public enum LexClass
    {        
        Kw_array,
        Kw_begin,
        Kw_const,
        Kw_do,
        Kw_else,
        Kw_end,
        Kw_for,
        Kw_if,
        Kw_not,
        Kw_of,
        Kw_program,
        Kw_then,
        Kw_to,
        Kw_type,
        Kw_var,
        Kw_while,
        Pd_integer,
        Pd_str,
        ID,
        Const_int,        
        Const_str,
        Sc_Semic,  //;
        Sc_Dot,    //.
        Sc_Colon,  //:
        Sc_Asnt,   //:=
        Sc_LsqBrk, //[
        Sc_RsqBrk, //]
        Sc_Lprnts, //(
        Sc_Rprnts, //)
        Sc_Dots,   //..
        Sc_Comma,  //,
        Sc_RelOp,  //=, <>, >, <, >=, <=  
        Sc_AddOp,  //+, -, or
        Sc_MultOp  //*, div, mod, and
    }
}

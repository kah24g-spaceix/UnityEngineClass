using System;
using System.Collections.Generic;

public interface ITextParser<T>
{
    T Parse(String pData);
}
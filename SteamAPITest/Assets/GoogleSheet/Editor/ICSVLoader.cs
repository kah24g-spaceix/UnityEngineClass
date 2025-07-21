using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICSVLoader
{
    List<List<String>> LoadCSV(String pPath);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SpreadSheetCSVLoader : ICSVLoader
{
    private readonly String m_apiKey;
    private readonly String m_sheetID;

    public SpreadSheetCSVLoader(String pApiKey, String pSheetID)
    {
        m_apiKey = pApiKey;
        m_sheetID = pSheetID;
    }
    public List<List<string>> LoadCSV(string pPath)
    {
        List<List<String>> result = null;
        using(HttpClient client = new HttpClient())
        {
            try
            {
                String url 
                    = $"https://sheets.googleapis.com/v4/spreadsheets/{m_sheetID}/values/{pPath}?key={m_apiKey}";
                HttpResponseMessage response = client.GetAsync(url).Result;
                
                response.EnsureSuccessStatusCode();

                String content = response.Content.ReadAsStringAsync().Result;

                JObject jsonObject = JObject.Parse(content);
                JArray valuesArray = (JArray)jsonObject["values"];
                result = valuesArray.Select(
                    subArray => subArray.Select(token => (String)token).ToList()
                    ).ToList();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        return result;
    }
}
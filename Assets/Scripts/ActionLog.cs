using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActionLog : MonoBehaviour {

    public List<string[]> radioCsvDatas = new List<string[]>();
    public TextAsset csvFile;

    public void CsvRead(string logName)
    {
        csvFile = Resources.Load(logName) as TextAsset;
        StringReader strReader = new StringReader(csvFile.text);
        while (strReader.Peek() > -1)
        {
            string line = strReader.ReadLine();
            radioCsvDatas.Add(line.Split(','));
        }
    }

    void Start()
    {
        for (int i = 0; i < radioCsvDatas.Count; i++)
        {
            for (int j = 0; j < radioCsvDatas[i].Length; j++)
            {
                Debug.LogFormat("{0}", radioCsvDatas[i][j]);
            }
        }
    }

}

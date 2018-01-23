using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActionLog : MonoBehaviour {
    /*ログ表示用のCSVファイルを格納するクラス*/

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
    }

}

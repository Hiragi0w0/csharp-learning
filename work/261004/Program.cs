using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

const int TEST_SIZE = 5;

for (int i=1; i<=TEST_SIZE; i++)
{
	var dicCheckedLog = new List<WorkingLog>();

	string fileName = i.ToString("d2") + ".csv";
	string filePath = "./testdata/" + fileName;

	Console.WriteLine($"## {fileName}");
	if (!File.Exists(filePath))
	{
		Console.WriteLine($"ERROR: ファイルが見つかりません: {filePath}");
		continue;
	}
	using (StreamReader sr = new StreamReader(filePath))
	{
		string? line;
		int lineCount = 0;
		while ((line = sr.ReadLine()) != null)
		{
			lineCount++;
			if (lineCount == 1)
			{
				continue;
			}
			if (!IsValid(line, lineCount))
			{
				continue;
			}

			string[] splited = line.Split(',');
			WorkingLog log = new WorkingLog();
			log.name = splited[0].Trim();
			log.workTime = int.Parse(splited[1].Trim());
			dicCheckedLog.Add(log);
		}
	}
	
	var sortedLog = dicCheckedLog.OrderBy(el => el.name).ToList();

	var outputList = new List<WorkingLog>();
	int dataSize = sortedLog.Count();
	for (int j=0; j<dataSize; j++)
	{
		if (outputList.Any(el => el.name == sortedLog[j].name))
		{
			int outputSize = outputList.Count;
			for (int k=0; k< outputSize; k++)
			{
				if (outputList[k].name == sortedLog[j].name)
				{
					outputList[k].workTime += sortedLog[j].workTime;
					break;
				}
			}
		}
		else
		{
			outputList.Add(sortedLog[j]);
		}
	}
	outputList = outputList.OrderByDescending(el => el.workTime).ToList();

	int outputListSize = outputList.Count;
	for (int j=0; j<outputListSize; j++)
	{
		string output = outputList[j].name;
		output += ",";
		output += outputList[j].workTime.ToString();
		Console.WriteLine(output);
	}	
	Console.WriteLine();
}

bool IsValid(string checkData, int lineCount)
{
	string[] splited = checkData.Split(',');
	int iDataCount = splited.Length;
	if (iDataCount != 2)
	{
		Console.WriteLine($"ERROR {lineCount}: 項目数が2つではない");
		return false;
	}

	string name = splited[0].Trim();
	if (string.IsNullOrWhiteSpace(name))
	{
		Console.WriteLine($"ERROR {lineCount}: 担当者名が空");
		return false;
	}

	int workTime = 0;
	if (!int.TryParse(splited[1].Trim(), out workTime))
	{
		Console.WriteLine($"ERROR {lineCount}: 作業時間が整数ではない");
		return false;
	}
	if (workTime < 0)
	{
		Console.WriteLine($"ERROR {lineCount}: 作業時間が負数");
		return false;
	}

	return true;
}

public class WorkingLog
{
	public string name = string.Empty;
	public int workTime;
};

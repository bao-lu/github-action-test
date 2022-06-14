using System;
using System.IO;
using System.Collections;

/**
 * TestCase_IOT_List
 * <p/>
 * The results of assessing this file should be a single input/output stack traces with main
 * as the root.  The taint is carried by the list collection.
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_List {
    public static void main(string[] args) {
        try {
            TestCase_IOT_List testCase = new TestCase_IOT_List();
            ArrayList strs = new ArrayList();
            string file = args[0];
			strs.Add(testCase.getVulnerableSource(file));
            testCase.writeToVulnerableSink(file, strs);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    public string getVulnerableSource(string file) {
		FileStream fis = new FileStream(file, FileMode.Open);
		StreamReader reader = new StreamReader(fis);
		char[] buffer = new char[100];
		reader.Read(buffer, 0, buffer.Length);
		fis.Close();

		return new string(buffer);
	}

	public void writeToVulnerableSink(string file, IList strs) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		foreach(string str in strs) 
		{
			writer.Write(str);
		}
		writer.Close();
	}
}

using System;
using System.IO;

/**
 * TestCase_IOT_Array
 * <p/>
 * The results of assessing this file should be a two input/output stack traces with main
 * as the root.  This case is equivalent to TestCase_IOT_Instance, except that the taint is
 * transmitted in an array.
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_Array {
    public static void Main(string[] args) {
        try {
            TestCase_IOT_Array testCase = new TestCase_IOT_Array();
            string file = args[0];
            string[] taintedStuff = new string[] {
                testCase.getVulnerableSource(file)
            };

            testCase.writeToVulnerableSink(file, taintedStuff);
            testCase.writeToVulnerableSink(file, taintedStuff[0]);
        } catch (Exception e) {
			System.Console.Write(e.StackTrace);
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

    public void writeToVulnerableSink(string file, string[] strs) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		foreach(string str in strs) {
			writer.Write(str);
        }
        writer.Close();
    }

    private void writeToVulnerableSink(string file, string str) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
    }
}

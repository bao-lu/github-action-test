using System;
using System.IO;

/**
 * TestCase_IOT_XFile_Part1
 * <p/>
 * The results of assessing this file should be a single input/output stack traces with main as the root.  The file
 * relies on TestCase_IOT_XFile_Part2 since we are trying to do a cross file analysis.
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_XFile_Part1 {
    public static void main(string[] args) {
        try {
            TestCase_IOT_XFile_Part1 testCase = new TestCase_IOT_XFile_Part1();
            TestCase_IOT_XFile_Part2 testCase2 = new TestCase_IOT_XFile_Part2();
            testCase2.writeToVulnerableSink(testCase.getVulnerableSource(args[0]));
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
}

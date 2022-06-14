using System;
using System.IO;

/**
 * TestCase_IOT_Lost_XFile_Part1
 * <p/>
 * The results of assessing this file should be a single input/output stack traces with main as the root.  The file
 * relies on TestCase_IOT_Lost_XFile_Part2 since we are trying to do a cross file analysis.  The stack trace
 * will be associated with the source since the stack trace will be lost when calling writeToUnknownSink
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_Lost_XFile_Part1 {
    public static void main(string[] args) {
        try {
            string file = args[0];
            string key = args[1];

            TestCase_IOT_Lost_XFile_Part1 testCase = new TestCase_IOT_Lost_XFile_Part1();
            TestCase_IOT_Lost_XFile_Part2 testCase2 = new TestCase_IOT_Lost_XFile_Part2();
            string source = testCase.getVulnerableSource(file);
            testCase2.writeToUnknownSink(key, source);
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

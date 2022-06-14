using System;
using System.IO;

/**
 * TestCase_IOT_OverlapTrace
 * <p/>
 * Tne result of running this testcase is to find two stack traces, one that has main as the root,
 * and the other that has writeToSinkCheck as the root.  What makes this testcase special is that
 * the two stack traces overlap each other.
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_OverlapTrace {
    public static void main(string[] args) {
        try {
            TestCase_IOT_OverlapTrace testCase = new TestCase_IOT_OverlapTrace();
            string file = args[0];
            string str = testCase.readFromVulnerableSource(file);
            testCase.writeToSinkCheck(file, str);
        } catch (Exception ex) {
			Console.Write(ex.StackTrace);
        }
    }

    private string readFromVulnerableSource(string file) {
		FileStream fis = new FileStream(file, FileMode.Open);
		StreamReader reader = new StreamReader(fis);
		char[] buffer = new char[100];
		reader.Read(buffer, 0, buffer.Length);
		fis.Close();

		return new string(buffer);
	}

    private void writeToSinkCheck(string file, string str) {
        if (str == null) {
            str = readFromVulnerableSource(file);
        }
        writeToVulnerableSink(str);
    }

    private void writeToVulnerableSink(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
	}
}

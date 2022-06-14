using System;
using System.IO;

/**
 * TestCase_IOT_Recursion
 * <p/>
 * The results of assessing this file should a one input/output stack traces: with main at the root,
 * This class has a couple of recursive methods that carry the taint.
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_Recursion {
    public static void main(string[] args) {
        try {
            TestCase_IOT_Recursion testCase = new TestCase_IOT_Recursion();
            string file = args[0];
            string str = testCase.readFromVulnerableSource(file);
            testCase.writeToVulnerableSink(str);
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

		string ret = new string(buffer);

        ret = recursiveTrim(ret);

        return ret;
    }

    private string recursiveTrim(string ret) {
        if (ret.StartsWith(" ")) {
            ret = recursiveTrim(ret.Substring(1));
        }
        if (ret.EndsWith(" ")) {
            ret = recursiveTrim(ret.Substring(0, ret.Length - 1));
        }
        return ret;
    }

    private void writeToVulnerableSink(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
        writer.Close();
    }
}

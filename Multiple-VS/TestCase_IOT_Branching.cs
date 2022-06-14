using System;
using System.IO;

/**
 * TestCase_IOT_Branching
 * <p/>
 * The results of assessing this file should be three input/output stack traces with main
 * as the root.  The first trace uses a simple if, the second trace adds a else, the third trace
 * uses a switch statement
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_Branching 
{
    public static void main(string[] args) {
        try {
            // simple null check (first trace)
            TestCase_IOT_Branching testCase = new TestCase_IOT_Branching();
            string source = testCase.getVulnerableSource(args[0]);
            if (source != null) {
                testCase.writeToVulnerableSink1(source);
            }

            // simple if/else (second trace)
            int i = 5;
            string source2;
            if (i < 2) {
                source2 = testCase.getVulnerableSource(args[0]);
            } else {
                source2 = "Safe";
            }
            testCase.writeToVulnerableSink2(source2);

            // simple switch (third trace)
            string source3;
            switch (i) {
                case 1:
                    source3 = "fine";
                    break;

                case 2:
                    source3 = "great";
                    break;

                case 3:
                    source3 = testCase.getVulnerableSource(args[0]);
                    break;

                default:
                    source3 = null;
                    break;
            }
            testCase.writeToVulnerableSink3(source3);
        } catch (Exception e) {
			System.Console.Write(e.StackTrace);
        }
    }

    public string getVulnerableSource(string file) {
        string ret = "";
        if (file != null) {
			FileStream fis = new FileStream(file, FileMode.Open);
			StreamReader reader = new StreamReader(fis);
			char[] buffer = new char[100];
			reader.Read(buffer, 0, buffer.Length);
			fis.Close();

			ret = new string(buffer);
		}
        return ret;
    }

    public void writeToVulnerableSink1(string str) {
        if (str.Length > 0) {
			FileStream fos = new FileStream(str, FileMode.CreateNew);
			StreamWriter writer = new StreamWriter(fos);
			writer.Write(str);
			writer.Close();
		}
    }

    public void writeToVulnerableSink2(string str) {
		StreamWriter writer = new StreamWriter(str);
		writer.Write(str);
		writer.Close();
    }

    public void writeToVulnerableSink3(string str) {
		StreamWriter writer = new StreamWriter(str);
		writer.Write(str);
		writer.Close();
	}
}

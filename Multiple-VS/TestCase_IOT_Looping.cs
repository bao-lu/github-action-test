using System;
using System.IO;

/**
 * TestCase_IOT_Looping
 * <p/>
 * The results of assessing this file should be two input/output stack traces with main as the root.
 * The program passes taint in a for loop and while loop
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_Looping {
    public static void main(string[] args) {
        try {
            TestCase_IOT_Looping testCase = new TestCase_IOT_Looping();

            string source2;
            for (int x = 0; x < args.Length; ++x) {
                if (x < 2) {
                    source2 = testCase.getVulnerableSource(args[x]);
                } else {
                    source2 = "Safe";
                }
                testCase.writeToVulnerableSink(source2);
            }

            string source1 = "";
            int y = 0;
            while (y < args.Length) {
                source1 = source1 + testCase.getVulnerableSource(args[y]);
            }
            testCase.writeToVulnerableSink2(source1);
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

    public void writeToVulnerableSink(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
	}

    public void writeToVulnerableSink2(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
	}
}

using System;
using System.IO;

/**
 * TestCase_Lost_IOT_Instance_Lost
 * <p/>
 * The results of running this test is an input/output stack trace that is associated with the
 * source and has a root at the main method.
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_Lost_Instance {
    public static void main(string[] args) {
        try {
            TestCase_IOT_Lost_Instance testCase = new TestCase_IOT_Lost_Instance();
            string file = args[0];
            string source = testCase.getVulnerableSource(file);

            string key = args[1];
            testCase.writeToUnknownSink(key, source);
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

    public void writeToUnknownSink(string key, string str) {
		System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(key);
		writer.AddResource(key, str);
    }
}

using System;
using System.IO;

/**
 * TestCase_IOT_Instance
 * <p/>
 * The results of assessing this file should be a input output stack trace
 * leading from getVulnerableSource to writeToVulnerableSink.  Unlike the TestCase_IOT_Static
 * class, all methods in this class are not static.  We do not use temporaries to store the
 * result of getVulnerableSource().
 * <p/>
 * Complexity: Easy
 */
public class TestCase_IOT_Instance 
{
    public static void main(string[] args) {
        try {
            TestCase_IOT_Instance testCase = new TestCase_IOT_Instance();
            testCase.writeToVulnerableSink(testCase.getVulnerableSource(args[0]));
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
}

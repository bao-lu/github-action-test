using System;
using System.IO;

/**
 * TestCase_IOT_Instance_Assigns
 * <p/>
 * The results of assessing this file should be a input output stack trace
 * leading from getVulnerableSource to writeToVulnerableSink.  Unlike the TestCase_IOT_Static
 * class, all methods in this class are not static.  We do use temporaries to store the
 * result of getVulnerableSource(), making this slightly more complex then TestCase_IOT_Instance
 * <p/>
 * Complexity: Easy
 */
public class TestCase_IOT_Instance_Assigns 
{
    public static void main(string[] args) {
        try {
            TestCase_IOT_Instance_Assigns testCase = new TestCase_IOT_Instance_Assigns();
            string source = testCase.getVulnerableSource(args[0]);
            testCase.writeToVulnerableSink(source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    public string getVulnerableSource(string file){
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
    }
}

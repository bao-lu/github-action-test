using System;
using System.IO;

/**
 * TestCase_IOT_Instance_Val_Encode
 * <p/>
 * The results of assessing this file should be a input output stack trace leading from getVulnerableSource to
 * writeToVulnerableSink the first time.  After adding validate or encode, the stack trace should be gone and
 * the vulnerabilities cleared.
 * <p/>
 * Complexity: Medium
 */
public class TestCase_IOT_Instance_Val_Encode {
    public static void main(string[] args) {
        try {
            TestCase_IOT_Instance_Val_Encode testCase = new TestCase_IOT_Instance_Val_Encode();
            string file = args[0];
            string source = testCase.getVulnerableSource(file);
            testCase.validate(source);
            string encodedStr = testCase.encode(source);
            testCase.writeToVulnerableSink(file, encodedStr);
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

    public void writeToVulnerableSink(string file, string str) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
	}

    private void validate(string source) {
        if (source.Length > 100) {
            throw new Exception("Length too long: " + source.Length);
        }
    }

    private string encode(string source) {
        return source.Trim();
    }
}

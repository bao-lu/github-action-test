using System;
using System.IO;

/**
 * TestCase_IOT_UserValidation2
 * <p/>
 * The results of assessing this file the first time should be an input output trace with the root in
 * the trace1 method.  After adding the validate method, the vulnerability and trace should go away.
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_UserValidation2 {
    FileStream fileInputStream;
	StreamReader reader;
	StreamWriter writer;
	char[] buffer;

    public static void main(string[] args) {
        TestCase_IOT_UserValidation2 testCase = new TestCase_IOT_UserValidation2();
		testCase.reader = new StreamReader(testCase.fileInputStream);
        testCase.trace1();
    }

    private void trace1() {
        string source = getVulnerableSource();
        callValidateEventually(source);
        writeToVulnerableSink(source);
    }

    public string getVulnerableSource() {
        reader.Read(buffer, 0, buffer.Length);
        return new string(buffer);
    }

    public void writeToVulnerableSink(string str) {
        writer.Write(str);
    }

    private void callValidateEventually(string source)  {
        callValidateAlmostThere(source);
    }

    private void callValidateAlmostThere(string source) {
        callValidate(source);
    }

    private void callValidate(string source) {
        validate(source);
    }

    private void validate(string source) {
        if (source.Length > 100) {
            throw new Exception("Out of bounds");
        }
    }
}

using System.IO;


public class TestCase_IOT_UserValidation {
    FileStream fileInputStream;
	StreamReader reader;
	StreamWriter writer;
	char[] buffer;

    public static void main(string[] args) {
        TestCase_IOT_UserValidation testCase = new TestCase_IOT_UserValidation();
		testCase.reader = new StreamReader(testCase.fileInputStream);
		testCase.writer = new StreamWriter(testCase.fileInputStream);
        testCase.trace1();
        testCase.trace2();
        testCase.trace3();
    }

    private void trace1() {
        string source = getVulnerableSource1();
        validate(source);
        writeToVulnerableSink(source);
    }

    private void trace2() {
        string source = getVulnerableSource2();
        validate(source);
        writeToVulnerableSink(source);
    }

    private void trace3() {
        string source = getVulnerableSource3();
        validate(source);
        writeToVulnerableSink(source);
    }

    public string getVulnerableSource1() {
        reader.Read(buffer, 0, buffer.Length);
        return new string(buffer);
    }

    public string getVulnerableSource2() {
        reader.Read(buffer, 0, buffer.Length);
        return new string(buffer);
    }

    public string getVulnerableSource3() {
        //return resultSet.getstring("x");
		reader.Read(buffer, 0, buffer.Length);
		return new string(buffer);
	}

    public void writeToVulnerableSink(string str) {
        writer.Write(str);
    }

    private void validate(string source) {
        // validate
    }
}

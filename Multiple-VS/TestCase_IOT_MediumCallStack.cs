using System;
using System.IO;

/**
 * TestCase_IOT_MediumCallStack
 * <p/>
 * The results of assessing this file should be a single input/output stack trace with main as the
 * root.  The program is equivalent to TestCase_IOT_Instance_Assigns except that it uses a lot of
 * specialized methods that create a deeper call stack. 
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_MediumCallStack {
    public static void main(string[] args) {
        try {
            TestCase_IOT_MediumCallStack testCase = new TestCase_IOT_MediumCallStack();
            testCase.writeToVulnerableSink(testCase.getVulnSourceFromFile(args[0]));
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    private string getVulnSourceFromFile(string file) {
        return getVulnerableSource(file);
    }

    public string getVulnerableSource(string file) {
        return convertBufTostring(readBufFromFile(file));
    }

    private string convertBufTostring(byte[] buf) {
		return System.Convert.ToString(buf);
    }

    private byte[] readBufFromFile(string file) {
		FileStream fis = new FileStream(file, FileMode.Open);
        byte[] buf = getBuffer();
        readIntoBuf(fis, buf);
        return buf;
    }

    private void readIntoBuf(FileStream fis, byte[] buf) {
        fis.Read(buf, 0, buf.Length);
        fis.Close();
    }

    private byte[] getBuffer() {
        return new byte[100];
    }

    public void writeToVulnerableSink(string str) {
        createWriter(str).Write(str);
    }

    private StreamWriter createWriter(string str) {
        return new StreamWriter(createFileOutput(str));
    }

    private FileStream createFileOutput(string str) {
        return new FileStream(str, FileMode.CreateNew);
    }
}

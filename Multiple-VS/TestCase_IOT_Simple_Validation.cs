using System;
using System.IO;

/**
 * TestCase_IOT_Simple_Validation
 * <p/>
 * The results of assessing this file should be no vulnerability. 
 * The validate() method must be tied as a validation rouine
 * to the fis.read call in getCoveredVulnerableSource().
 * <p/>
 * Complexity: Medium
 */
public class TestCase_IOT_Simple_Validation {
    public static void main(string[] args) {
        try {
            testSinkValidated( args );
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }
    
    /**
     *  In this case, external data goes through a validation routine before
     *  reaching a sink.
     *  <p/>
     *  There should be no vulnerability derived from this call, when
     *  the validate() method is specified as a validation routine.
     *
     */
    private static void testSinkValidated( string[] args)
    {
        try {
            TestCase_IOT_Simple_Validation testCase = new TestCase_IOT_Simple_Validation();
            string file = args[0];
            string source = testCase.getCoveredVulnerableSource(file);
            testCase.validate(source);
            testCase.writeToVulnerableSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     * This is used as a source of input for which the input call
     * has an associated validation routine. For the test to get expected
     * results, the validate() method should be associated with the specific read()
     * call inside this method.
     */
    public string getCoveredVulnerableSource(string file) {
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

} // class TestCase_IOT_Simple_Validation

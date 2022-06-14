using System;
using System.IO;

/**
 * TestCase_IOT_Instance_Val_Confidence
 * <p/>
 * This file has several tests to make sure we get confidence level correct in the
 * face of 6 different scenarios in a matrix encompassing 2 axes.
 * One axis is whether the trace goes to a sink, or is 'lost' (reaches a non-sink
 * that is a leaf in the call graph.
 * The other axis has 3 choices:
 *    - a validation routine exists and the trace goes through it
 *    - a validation routine exists and the trace does not go through i.
 *    - there is no validation routine
 * <p/>
 * For call trees rooted at the test routines listed here, 
 * the results of assessing this file should be:
 *  - testSinkValidated: no vulnerability
 *  - testSinkValidationNotUsed: high confidence (Vulnerability)
 *  - testSinkNoValidationAvailable: medium confidence (Type I)
 *  - testLostValidated: no vulnerability
 *  - testLostValidationNotUsed: medium confidence (Type I)
 *  - testLostNoValidationAvailable: low confidence (Type II)
 * <p/>
 * For this test to work correctly, this class' 'validate()' method
 * must be tied to the specific fis.read(..) call in the method
 * 'getCoveredVulnerableSource()'.
 * <p/>
 * Complexity: Medium
 */
public class TestCase_IOT_Instance_Val_Confidence 
{
    public static void main(string[] args) {
        try {
            testSinkValidated( args );
            testSinkValidationNotUsed( args );
            testSinkNoValidationAvailable( args );

            testLostValidated( args );
            testLostValidationNotUsed( args );
            testLostNoValidationAvailable( args );
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }
    
    /**
     *  In this case, external data goes through a validation routine before
     *  reaching a sink.
     *  <p/>
     *  There should be no vulnerability derived from this call.
     *
     */
    private static void testSinkValidated( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getCoveredVulnerableSource(file);
            testCase.validate(source);
            testCase.writeToVulnerableSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     *  In this case, external data reaches a sink, and a validation routine
     *  is available, but is not used.
     *  <p/>
     *  There should be a high confidence vulnerability derived from this call.
     *
     */
    private static void testSinkValidationNotUsed( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getCoveredVulnerableSource(file);
            testCase.writeToVulnerableSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     *  In this case, external data reaches a sink, but there is no validation
     *  routine available.  
     *  <p/>
     *  There should be a medium confidence vulnerability derived from this call.
     *
     */
    private static void testSinkNoValidationAvailable( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getUnCoveredVulnerableSource(file);
            testCase.writeToVulnerableSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     *  In this case, external data goes through a validation routine before
     *  getting lost.
     *  <p/>
     *  There should be no vulnerability derived from this call.
     *
     */
    private static void testLostValidated( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getCoveredVulnerableSource(file);
            testCase.validate(source);
            testCase.writeToUnknownSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     *  In this case, external data gets lost, and a validation routine
     *  is available, but is not used.
     *  <p/>
     *  There should be a medium confidence vulnerability derived from this call.
     *
     */
    private static void testLostValidationNotUsed( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getCoveredVulnerableSource(file);
            testCase.writeToUnknownSink(file, source);
        } catch (Exception e) {
			Console.Write(e.StackTrace);
        }
    }

    /**
     *  In this case, external data gets lost, but there is no validation
     *  routine available.  
     *  <p/>
     *  There should be a low confidence vulnerability derived from this call.
     *
     */
    private static void testLostNoValidationAvailable( string[] args)
    {
        try {
            TestCase_IOT_Instance_Val_Confidence testCase = new TestCase_IOT_Instance_Val_Confidence();
            string file = args[0];
            string source = testCase.getUnCoveredVulnerableSource(file);
            testCase.writeToUnknownSink(file, source);
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

    /**
     * This is used as a source of input for which the input call
     * has *no* associated validation routine. For the test to get expected
     * results, no validation routine should be associated with the read()
     * call inside this method.
     */
    public string getUnCoveredVulnerableSource(string file) {
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

    public void writeToUnknownSink(string key, string str) {
		System.Environment.GetEnvironmentVariable(key);
    }

    private void validate(string source) {
        if (source.Length > 100) {
            throw new Exception("Length too long: " + source.Length);
        }
    }

} // class TestCase_IOT_Instance_Val_Confidence

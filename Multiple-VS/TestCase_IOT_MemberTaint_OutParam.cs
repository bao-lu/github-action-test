using System;
using System.IO;

/**
 * TestCase_IOT_MemberTaint
 * <p/>
 * The results of assessing this file should be two input/output stack traces with main as the root.  The first trace
 * uses a member variable of a class to transmit the taint directly. The test passes the taint though a set method and
 * gets it back using a get method.  These differ from the TestCase_IOT_MemberTaint in that they transmit the taint
 * through an output parameter.
 * <p/>
 * Complexity: Hard
 */
public class TestCase_IOT_MemberTaint_OutParam {
    public class TaintedStruct {
        public string m_tainted = null;

        public TaintedStruct() {
        }

        public TaintedStruct(string tainted) {
            m_tainted = tainted;
        }

        public string getTainted() {
            return m_tainted;
        }

        public void setTainted(string tainted) {
            m_tainted = tainted;
        }
    }

    public static void main(string[] args) {
        try {
            // first case
            TestCase_IOT_MemberTaint_OutParam testCase = new TestCase_IOT_MemberTaint_OutParam();
            TaintedStruct struct1 = new TaintedStruct();
            testCase.getTaintedStructSource1(args[0], struct1);
            testCase.writeToVulnerableSink1(args[0], struct1);

            // second case
            TaintedStruct struct2 = new TaintedStruct();
            testCase.getTaintedStructSource2(args[0], struct2);
            testCase.writeToVulnerableSink2(args[0], struct2);
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

    public void getTaintedStructSource1(string file, TaintedStruct struct1) {
        struct1.m_tainted = getVulnerableSource(file);
    }

    public void writeToVulnerableSink1(string file, TaintedStruct struct1) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
        writer.Write(struct1.m_tainted);
    }

    public void getTaintedStructSource2(string file, TaintedStruct struct1) {
        struct1.setTainted(getVulnerableSource(file));
    }

    public void writeToVulnerableSink2(string file, TaintedStruct struct1) {
		FileStream fos = new FileStream(file, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(struct1.getTainted());
		writer.Close();
    }
}

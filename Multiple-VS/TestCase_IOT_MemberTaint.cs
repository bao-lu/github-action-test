using System;
using System.IO;

/**
 * TestCase_IOT_MemberTaint
 * <p/>
 * The results of assessing this file should be three input/output stack traces with main as the root.  The first trace
 * uses a member variable of a class to transmit the taint directly. The second test passes the taint through the
 * constructor and gets it back using a get method. The test passes the taint though a set method and gets it back using
 * a get method.
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_MemberTaint {
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
            TestCase_IOT_MemberTaint testCase = new TestCase_IOT_MemberTaint();
            TaintedStruct struct1 = new TaintedStruct();
            struct1.m_tainted = testCase.getVulnerableSource(args[0]);
            testCase.writeToVulnerableSink1(struct1.m_tainted);

            // second case
            TaintedStruct struct2 = new TaintedStruct(testCase.getVulnerableSource(args[0]));
            testCase.writeToVulnerableSink2(struct2.getTainted());

            // third case
            TaintedStruct struct3 = new TaintedStruct();
            struct3.setTainted(testCase.getVulnerableSource(args[0]));
            testCase.writeToVulnerableSink3(struct3.getTainted());
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

    public void writeToVulnerableSink1(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
		writer.Close();
	}

    public void writeToVulnerableSink2(string str) {
        FileStream fos = new FileStream(str, FileMode.OpenOrCreate);
		StreamWriter writer = new StreamWriter(fos);
        writer.Write(str.ToCharArray());
    }

    public void writeToVulnerableSink3(string str) {
        FileStream fos = new FileStream(str, FileMode.OpenOrCreate);
		StreamWriter writer = new StreamWriter(fos);
        writer.Write(str);
    }
}

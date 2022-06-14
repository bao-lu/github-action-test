using System;
using System.IO;

/**
 * TestCase_Lost_IOT_MemberTaint
 * <p/>
 * The results of assessing this file should be three input/output stack traces with main as the root.  All three traces
 * will be reported on the source, not the sink, because the trace is lost. See TestCase_IOT_MemberTaint for more
 * information on the expected results.
 * <p/>
 * Complexity: Hard
 */
public class TestCase_IOT_Lost_MemberTaint {
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
            string file = args[0];
            string key = args[1];

            // first case
            TestCase_IOT_Lost_MemberTaint testCase = new TestCase_IOT_Lost_MemberTaint();
            TaintedStruct taintStruct = new TaintedStruct();
            taintStruct.m_tainted = testCase.getVulnerableSource(file);
            testCase.writeToUnknownSink1(key, taintStruct.m_tainted);

            // second case
            TaintedStruct struct2 = new TaintedStruct(testCase.getVulnerableSource(args[0]));
            testCase.writeToUnknownSink2(struct2.getTainted());

            // third case
            TaintedStruct struct3 = new TaintedStruct();
            struct3.setTainted(testCase.getVulnerableSource(args[0]));
            testCase.writeToUnknownSink3(key, struct3.getTainted());
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

    public void writeToUnknownSink1(string key, string str) {
		System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(key);
		writer.AddResource(key, str);
	}

    public void writeToUnknownSink2(string str) {
		System.Windows.Forms.MessageBox.Show(str);
    }

    public void writeToUnknownSink3(string pattern, string str) {
		System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(str);
		writer.AddResource(pattern, str);
    }
}

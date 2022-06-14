using System.IO;

/**
 * TestCase_IOT_XFile_Part2
 * <p/>
 * Part of TestCase_IOT_XFile_Part1
 * <p/>
 * Complexity: Moderate
 */
public class TestCase_IOT_XFile_Part2 {
    public void writeToVulnerableSink(string str) {
		FileStream fos = new FileStream(str, FileMode.CreateNew);
		StreamWriter writer = new StreamWriter(fos);
		writer.Write(str);
    }
}

/**
 * TestCase_IOT_Lost_XFile_Part2
 * <p/>
 * Part of TestCase_Lost_IOT_XFile_Part1
 * <p/>
 * Complexity: Moderate Hard
 */
public class TestCase_IOT_Lost_XFile_Part2 {
    public void writeToUnknownSink(string key, string str) {
		System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(key);
		writer.AddResource(key, str);
    }
}

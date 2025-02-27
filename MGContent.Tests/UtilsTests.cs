using MGContent;

namespace MGContent.Tests
{
	public class UtilsTests
	{
		[Fact]
		public void MatchPathPatternTest()
		{
			Assert.True(Utils.MatchPathPattern("C:/aded/ade.txt", "*.txt"));
			Assert.True(Utils.MatchPathPattern("C:/aded/ade.txt", "aded/*.txt"));
			Assert.True(Utils.MatchPathPattern("C:\\aded\\ade.txt", "aded/*.txt"));
			Assert.True(Utils.MatchPathPattern("C:/aded/ade.txt", "*"));
			Assert.True(Utils.MatchPathPattern("C:\\aded\\ade.txt", "*"));
			Assert.True(Utils.MatchPathPattern("C:\\aded/ade.txt", "*"));

			Assert.False(Utils.MatchPathPattern("C:/aded/ade.txt", "*.png"));
			Assert.False(Utils.MatchPathPattern("C:/aded/ade.txt", "adeb/*.txt"));
		}
	}
}
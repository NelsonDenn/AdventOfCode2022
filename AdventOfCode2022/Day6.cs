using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day6
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data);
			var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(List<char> datastream)
		{
			return GetUniqueCharactersIndex(datastream, 14);
			// Actual: 3153
		}

		private static int RunPart1(List<char> datastream)
		{
			return GetUniqueCharactersIndex(datastream, 4);
			// Actual: 1542
		}

		//private static int RunPart1(List<char> datastream)
		//{
		//	int currentIndex = 1; // One-based index
		//	var queue = new Queue<char>();

		//	foreach (var character in datastream)
		//	{
		//		queue.Enqueue(character);

		//		if (queue.Count == 4)
		//		{
		//			var firstChar = queue.ElementAt(0);
		//			var SecondChar = queue.ElementAt(1);
		//			var ThirdChar = queue.ElementAt(2);
		//			var FourthChar = queue.ElementAt(3);

		//			if (firstChar != SecondChar &&
		//				firstChar != ThirdChar &&
		//				firstChar != FourthChar &&
		//				SecondChar != ThirdChar &&
		//				SecondChar != FourthChar &&
		//				ThirdChar != FourthChar)
		//			{
		//				break;
		//			}

		//			_ = queue.Dequeue();
		//		}

		//		currentIndex++;
		//	}

		//	return currentIndex;
		//	// Actual: 1542
		//}

		private static int GetUniqueCharactersIndex(List<char> datastream, int numUniqueCharacters)
		{
			int currentIndex = 1; // One-based index
			var queue = new Queue<char>();

			foreach (var character in datastream)
			{
				queue.Enqueue(character);

				if (queue.Count == numUniqueCharacters)
				{
					bool areCharactersUnique = true;
					var characterSet = new HashSet<char>();

					foreach (var queueChar in queue)
					{
						if (characterSet.Contains(queueChar))
						{
							// This character already appeared. Move on to the next set of characters.
							areCharactersUnique = false;
							break;
						}
						else
						{
							characterSet.Add(queueChar);
						}
					}

					if (areCharactersUnique)
					{
						// This is the first set of x unique characters. End the loop and return the current index.
						break;
					}

					_ = queue.Dequeue();
				}

				currentIndex++;
			}

			return currentIndex;
		}

		private static List<char> GetTestData()
		{
			string data = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"; // 7
															//string data = "bvwbjplbgvbhsrlpgdmjqwftvncz"; // 5
															//string data = "nppdvjthqldpwncqszvftbrmjlhg"; // 6
															//string data = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"; // 10
															//string data = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"; // 11

			return ParseData(data);
		}

		private static List<char> GetData()
		{
			string data = "czfztznzpnpqpfftcftcchfhjjsqsvvffhwfhhqnhqqmlldpdcdnnfvvmpmzpmmrsrwssqbsbsmmdjdssfqfllclcdczdccppvzvtzznpnfpnnljnnpcphpjprrdhddsgdssbqqghqqmgqgccjffdtdcdmdjdcctltdddvhdhfhnnrffwjfjpjnpnpddhbbrrflrldlqlrlqqwssqsrswrrrmcmrcrfrwfwpfpdfppslsjsjhhjjqhqwwzzhttcvvzrrrnlrlmljlrjrsshwwndnnzwzpzcpcllfrrsjsnjncjcvcllrhrbhhdsdqdpqpffmppdzztggvpvnvwvpvdvqddznzrzppznnpnhhwjjrpjpvvqvpvrvfvjvzjvzjzqzlqqwsqqrsrprqpqmmbhbffbnnlzzhszhznnhzhmzzfhflfccvvbnbtnnlcnndjjcvjjshhssfjfcjjpzpqpmmdqmqmmbbldlwwvdwvddznncwcqwcwdwbdwwgzzzqvqqlqlttbppgtptlldldblbbjcbcttbpbvvhlhggghvvqsshjhjjzqzwwcggsfgsgjjgbjgbbgllhhmqmggmsggzszvvzggdngnznlzlrlprpjpgjgvgppfhhbqqcddvvbhbqqmlmsssbbszzfdzzznssjllwlplttdgtdttsmspmpvpgpspqpdqdppfvpvvmggrwwgswswmmmnppmttqppvttrvrllcmctcqqlvvpwwghhlmhmwhmmpfmfvfwwbgbqqdbqblqqclljcljccnhhghbgbrrnrppchhmpmdmfmvffvqqtnqqfccgcfgcgwgcwggvgzvvbwbgbjbwbcbgbzztbbsbdbvddzrdrjjlqqlslrssjddhbddzbzvbvhhrjhhrtrltljljwljjrnjjjlpjphjphjhjvjgjbbwfwcctgtmtmdttjhthphhchfcfnnqlqhqvvjcclczlzwlzlhzlzdlzlbzbvbmbcmcjcncwncntndtntbbgqqbrbsrssrnnrcrmrlmmrqqddhzdhhnqqvccbmbpbwpbwbrwwwgdgnnhzzhphbhwhwzwhhvhqqnmqnmnjmnjjjjztzvzvbzbbvcvggcdchhhhbfbttmmtsmsgmmqvqdvdrvrqrdqqrhqqpsqqfnntsnnslsggcqqrnrmrfffmgmsmgggrdgdrgddzfzqfqfcqfcfrfwrrrgglwlmlmlgmlmrlljzlzglzztllgnndhdthtwwdrrjnncssdsnszsggdhdpprnnqzzltzlzznlzlflgfgbfggqtggfbbccchdcdrrnwnbnhbnbjjpzjpzzwjwcwggfqfccdzzfddmbddgbgrgllnttrwrjwrrbsrsbbwqwmmlggzqgzqzrqzzffmcmssgjjjcdcssndsdfsfddcffbrrcvccwhhzzlglhhsccvdvjjdldndrrvlrrbwrrbqqcbcjcjpjggngvnndtdvttlrlmmcbcggbtbhthjttdggtvtmmlgmlgmmdwdndmmldlttjlmbtrwfnvmnzggqfqwnppbrnjmpjfppwltpqfstgnnjwmbffgfbjmfhqngnswftgrqvpljsmghszclhhprscrgtmsgbwnbrfhwdsbjrdlhdcqfthwfggpzhjrvmprcjjgrlbmdqczmsltnpnlpldvnsjbdjzbrglpsmbwdhhphcdmjqmssdlgjtmphhjvpjqcdvmdtszldnjtbcffhwrbqpsrsmmsqsbbrnhsrvfhhcmwgcpsfhrfcpvldrwstqtgrcgrqttvdnstlbdblnbtcgdgpbcjdlvwpplsslqdsbpncbwzcmlglbhnrtbwvnbfsgrdlpzszhzmzqtztzgjjjszmjgggdnslqqqrrlwcghpvzwpndbjbhhhpbbtvjvjvjhntqggtmqwdnrtswcchsqpdwnvmfqrgpvbtjpttfhmlgtnsgnpcrhbswfblwrbflqlgtmvzvwprtfntshjcpzshnlnqthvqgpzffsbdbvbhpghwjhqpjwjfbsrzgttsnrpmfrvlwrjrfplrtzpntjbvhdjjfjjzrrvrsrctrtldrsgcgvntgvmtrnqbtmpzvlbwnddbnldbhfcpncqclzqgmbjcnnfdfsmdjdgtmtzbpwqrhrlggrfjhvrrqpjptqdzpwffwnfwmfhfhmcpljmhttbltgltqjhtvbdzgwrppzjwhctnncqthnlrhzgtnffpwnnqqgddjtgtczfmwvbccgvmdfvggdghmssjlflptzrpdncbjcsdlbqjfzmnzdggwdczghfpldsbnvzdjzsbsdnmwptgngmnbqmwbrzqgnqbmfvbzzmsvlswbtvfllqzsjdlvzjmbmlbnljwwjsqzqvfwhzmfdgttfqsdttnjmvspsgvpzwczpvdbvhjjrttghjddrgffqdsmzspnssztjjmwtsrtlrmbqhfbvpzsmnqthssbqrjhvqllrbbpjjllszmnjvzmfsbjtvvvgzrvjwldlbzjdbrsvvgmmjrgsbvtvthjbcqlzqhjqfvbzzvlpmrlmphftwbsvvwnwqfwvsnlvlcdgqjspprmwptcjhbjplzrqlhmvzmbfvwwvgvtqlvgfvzqdprfcjqsnndwqmbhlvfvhdhghtsrhbcrrshlsdtlbwntbcvvjhlcscfcnhbdnngtsccrnfbbqbfggvwnmlfhmqbdqpnzvzcvngcctbrshsdvhstcnwvjgwchzdndgbgjnddrmfgftvvjtdnqhfjmpjcjjtmgztvnwnrjjgmcgcqclnwvclngrtjggrprmfpqcsmdtbswhwrgwgfflqldtpnfdhdltrnhdvqqdsshcrnncdpvbhvlcbjlznjjzsdcjjpqprnzpmvqvrfnhdvhgrsmlcgtbsmrgqqtmblqmqcnmnmbcmmjvnptzbbqmcpwbflhccpfrjvfzmcsttfjtjgrnjbtwbncrsjjgjwdpmvfpqmzqwgjvgvfnpbhgzlzhfsnwmjjjsgcjjcwcfbzrmfzwdmhffqnzscbnfmpnbdbccrsjgqgvftszdbqbstwtwmwfzbtmvdmwvshlcpqnsqfwqlfclmjdjvgszsdblwvnhtwtrjcwsmpgflhdqmndrjvqlhqclmwhscnznmhjtqjlnglpbmgvptjnrpnjlqslssfnbdwlrwrdrtfspsjwzvwzfjpflqgmfnbvzqglrrccsnqtchnhlnrwlwqmqdvwsjmbjbvczvqcgfrrtftcgqsnvzhjlshnlvqmvrjljpsvmdzwqgltmccnplbmplhpvvflhmgfzmhczpwffbzhzhhggzgwtpmlzgrbpfgcnmfdbgcwrbgdpzpwmtwgmjtpmqzzgclmcpjgzdrwqfdwqzzqszrwmbwcqzcjdphqtwlbqmnddsdthgjrlshhmzgrqlhghrbqgzppqbdmmlnthhhmtdcdnbqfvbwdrdgbbpbhtrflwmhwjmzdtwmzhjndbrvbtbqnzbsrrbfdrrdnjltdtsjnpddmqzzbzblzvwctpdhfqgqntczwlgqbvvmhjblztlpptqjscztlrvrglrbsbfdjdfbwjltjcdgmqzgtjqjfgjnnpqgfhsrcggrszhbgfhtjmlgmmvshcpjwmcwcvwhnbjzrzfwtnlsnmdgfpbhvgfrhmhmsmtrnblsmnjclqzfgttrdmvpsvlcvnhmgmltfhffzllfrmdqdpzdmjdmsnmpwbwvrdvwvrcdsfwftfbfvmzjttrqgqlqqdtmcpdpfqfsclcmcdzhjwqrbhmzjjsvpbjzzgfwbjbqmzmtrnflwrqgnbgqnpntqmlngmgcrflgvhpznvrfwzlwrswbdsldfdbhvwvcbwhmpcslhmmbjdzvmgrjwzqqzlrrgddpssqcshzrsvrrzzcbsgvfmrlcgjrcpphctdwtjbmrlnrmrbgsqbjnmnsbjfhszcgdqfflcwvrvnwtvcnhcffvphwmzqpwnwncrbnzrnvjzsrnlzlfdbgztrhvlvpczzqnwsvwvnhgnclhndjsgqdgznjzrtmslmgnjzpj";

			return ParseData(data);
		}

		private static List<char> ParseData(string data)
		{
			return data.ToCharArray().ToList();
		}
	}
}

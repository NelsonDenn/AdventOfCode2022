using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day3
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data);
			var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(List<Rucksack> rucksacks)
		{
			int groupCount = 1;
			var groupCommonItems = new List<char>();
			var groupBadges = new List<char>();

			foreach (var rucksack in rucksacks)
			{
				// Consider all items within the rucksack
				var allRucksackItems = rucksack.CompartmentOneItems.Union(rucksack.CompartmentTwoItems);

				// For the first rucksack in the group, add all items to the common items list
				if (groupCount == 1)
				{
					groupCommonItems = allRucksackItems.ToList();
				}
				else
				{
					// For rucksacks two and three, find the items in common with the other rucksacks
					groupCommonItems = groupCommonItems
						.Intersect(allRucksackItems)
						.ToList();
				}

				// Add the group badge and start a new group
				if (groupCount == 3)
				{
					groupBadges.AddRange(groupCommonItems); // Should be just one item

					groupCount = 1;
					groupCommonItems = new List<char>();
				}
				else
				{
					// Increment the group count
					groupCount++;
				}
			}

			return GetSumOfItemPriorities(groupBadges);
			// Test: 70
			// Actual: 2488
		}

		private static int RunPart1(List<Rucksack> rucksacks)
		{
			var allCommonItems = new List<char>();

			// For each rucksack, find the items that are in both compartments
			foreach (var rucksack in rucksacks)
			{
				var commonItems = rucksack.CompartmentOneItems.Intersect(rucksack.CompartmentTwoItems);
				allCommonItems.AddRange(commonItems);
			}

			return GetSumOfItemPriorities(allCommonItems);
			// Test: 157
			// Actual: 7863
		}

		private static int GetSumOfItemPriorities(List<char> items)
		{
			// Convert each item type to a priority
			// - Lowercase item types a through z have priorities 1 through 26.
			// - Uppercase item types A through Z have priorities 27 through 52.
			var priorities = new List<int>();
			foreach (var item in items)
			{
				int priority = char.IsLower(item)
					? item - 'a' + 1
					: item - 'A' + 27;
				priorities.Add(priority);
			}

			return priorities.Sum();
		}

		private static List<Rucksack> GetTestData()
		{
			string data =
@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

			return ParseData(data);
		}

		private static List<Rucksack> GetData()
		{
			//var data = new List<int>
			//{
			//	1,4,3,3,1,3,1,1,1,2,1,1,1,4,4,1,5,5,3,1,3,5,2,1,5,2,4,1,4,5,4,1,5,1,5,5,1,1,1,4,1,5,1,1,1,1,1,4,1,2,5,1,4,1,2,1,1,5,1,1,1,1,4,1,5,1,1,2,1,4,5,1,2,1,2,2,1,1,1,1,1,5,5,3,1,1,1,1,1,4,2,4,1,2,1,4,2,3,1,4,5,3,3,2,1,1,5,4,1,1,1,2,1,1,5,4,5,1,3,1,1,1,1,1,1,2,1,3,1,2,1,1,1,1,1,1,1,2,1,1,1,1,2,1,1,1,1,1,1,4,5,1,3,1,4,4,2,3,4,1,1,1,5,1,1,1,4,1,5,4,3,1,5,1,1,1,1,1,5,4,1,1,1,4,3,1,3,3,1,3,2,1,1,3,1,1,4,5,1,1,1,1,1,3,1,4,1,3,1,5,4,5,1,1,5,1,1,4,1,1,1,3,1,1,4,2,3,1,1,1,1,2,4,1,1,1,1,1,2,3,1,5,5,1,4,1,1,1,1,3,3,1,4,1,2,1,3,1,1,1,3,2,2,1,5,1,1,3,2,1,1,5,1,1,1,1,1,1,1,1,1,1,2,5,1,1,1,1,3,1,1,1,1,1,1,1,1,5,5,1
			//};

			string data =
@"PPZTzDhJPLqPhqDTqrwQZZWbmCBMJMcsNmCBFWmMcsNb
vplSlfdfGvfRRGsgNcMglsFWMWMC
jtjvFHdjjwqrwqwL
NSffhsNSjfLjfstsjtjNNjjqMqnpggHngqgHGHCgClGbCzCC
dDPZZDZFdwFWwFZFWZRTFDwGzCMlgnpgCpnzglClHMbg
DTPFZQRcdTVNhbjVbcLc
JZLDcSZSpHHrrLrJcpzBRrhlzgRTmTmvBRmm
qQsQMCbMQWqCVVvmTRhTTRhCRhTg
svbGWPqGPNLJSpZnZpnN
wLtPGCLwfWLflCPtPfLLTSbHMbSgMdtvDHghhHvdgZ
nNsFznJcJqzFFszFqrNnRzdbZDDbRMbMdRHbMdgHvZSd
VczNnjsrFrjcNprqVwTPfjGllWPQBBWlgB
nnGtjFFjFTTTGtBGmWBTWffLcMJMQlzjQPCPcChCQDJzDJJd
SSggbHVbZRgZsHZRHdVhzCcJhzhMzJhQPQ
rHsNSsSZqqrNgpLLWmCfFGqGCBWm
ZnCtCCVZmVBCQBWQnWQNCQMcLrMMgMLqLSwMSSDwjcBD
hTbGJGfTbrSfScmjwj
bbGGlTdlJTdGlFpdFvJdsbmdZWPWtnVCHQvtVHzvtHWtCtVN
pNpCNFMNFhhwDgRVdSVqwgrdmJ
nvHbPZtTHWbntTmdTRrqVRrdmz
vHWPBWvntbWnHLHZLqWtBCjjhBNhCjGjjNjDNChlFC
CnFbFzpzJbsCRpbRpbnPCnJLTtwQtjdtcttHHHDtDPjQwTHB
qGrflmrNgvvmGqcdwrtWQHwTBHQWtj
mVMvSclGqvNVMMNVsZsnJJRJsbzpSJpJ
cJTcRllRldjZlFcbcFJrrvqCCVTNNVWSPpQNmpQqCPVC
wGLBfLzgBfzHGGGnLDGDGgwHqqHmVSWqpQpCpQRWVWVNpS
DRshRBLMhZlFZMJvlJ
SdGbmRGddMcfbWWSptssDHssGDNsjCCC
glPLTzczrCpNNsHTst
gqcZJPrBlhJgPndMVJbWMVfnWV
qNbmLmndBQqjsCPLZsLPZz
pwfhfCvJvvTMGzSjzPSPjcZp
VvvJVMCrvTRwgvwWvqNmqblNHtBWqQWlql
WNJmddmpFmMMrnlFddlWTHCHBRcnCBTRzTDRTwTz
qffLvLLvbqhqPbjbqRGPSqVtPDTTwTwTDzCBCccQczssCwcc
tfhGLhqthZVhbfpFRJMMMrJrZpmZ
VVgSmdqFpMddqSfpfVVWQvzTPvTWPrpsQPQQJv
ZCnRCDwRWCPrTrsW
HwRNLLsnHRNjtRSqNqMmfqVVMbqg
slqwzGvWqMsvbmTzTCBhhBhgcgjbCPCchc
tJVJSZStQdMQSdntJHjFNFPCNpjFCPcFFdhB
QDrRVZSnrQDVVRRtRHHWGmzTDDqMqMfzwswWsl
rFBrJFcrWHzCLFHqSg
PdVjfjlGPRzRGtGLRC
TQPMpMVPDDPfPTMMPpTWWrhbcbTcWbzzcsmTmb
ZDQDZDJNqqNbwQPgtlGntHlVGlPPrf
vhmChcgvMCdvzCvvHfdntBHGBldrHBVG
cCpTCLvmjhpjzSTTLSpwbDqjJQFZgNDwJssFDw
LfMFLwMwdrFmWBJD
tVlHqqVTHRtmQggrjQqDJg
VDntHnDGRntHPbLPPLLZhcsLPLww
FBLddLctDQcbCLltbdCRdLQVNVDjnPHVnsjnPqVSHNNVTP
vGmwrZZWJpfWfmvZgZJjSTPqsTrVPTrHTssNPP
wNZJffhNWmhvMhgwMZpvNJtbQRtQQLdFhbQFClLBCBlc
npvSWJBCDDBBDSvCZSpJdsTZsRhTdgMgPdhqHHqR
bjtwqLrtmfmtLVjVLQHHHdgwTTHMssMTGHhH
VtmVVNtqlllpJvnnnS
nCqrnLSSGnpjBjBGbcbPbB
vfdVdtdgMMrFgHfHPcBcPBjwQDjFbwDB
zgHWMmgHmWfWvVvRRzLCSCJsZNpJZSsrnssW
bBjWlfrrnClSssMMFmVVhMjgMpLM
dRDqDdzQrDdhqMMPtVLgFL
THDRwHDNDdQdcDvTcZbBGBGrZZnZcrlb
jgSVPVsVmshhsCQm
vmFtcDBfDFLrvTFZvLFvWzWhHwCWHnwHnCQCcwnq
FmDpFBmZZFrDbDfDtmLNgMPNGdPjRdPlPPpMVN
VVJGdSHZnnHdgFntcschhccvvPvtstPq
mLNjNQFBpPlPvNqs
LMTMMRDwwMMSGZzRnnGbzF
JFFfVrvVmHfGmHFvmrSQBQlSJLlShLlgBqwJ
DMCdpCbtgbcCCNpbCCPgRqdldBRQRBRwLsBSLhQs
cgbcPbpcWDWjNCZDWWZttDDGrHzznHzjGzmHnVjVvvVnrz
rtGTmSTGNtvvgfNGSbfwWWvJqwcDwwJPWcwWqD
lZhdHzFhLZhdBcWsWsWmwPcFqW
BhhjLzhZCCdhgCTtCSMmMrbt
FrzSRNrWNFdNhcRDDdrFWCVVZZZmjJbJSPlllgllVbgT
nQGGHqvHMVVpMLGffqtwLMtwlmPbTbjZPJmllLJBmZmZlZbP
VMvvpvQttHqnsvhhzhdrcdWNchsW
BzRTBbWVQNdngtDFVprDFrpF
vhfhSJvbhwSpDDFZHfMpHp
JhmvJsLLJLJqmsJLbsGGjvNRzzBcTBNlRNmnRTQdzzzn
vpCLrTcpRmncrncLcnccvLLNWVsRbhbtsQbJbVQWtWlWbW
PfFfdjdSjPffMFsQbNhlFssFNQ
zBDsjgfZHczLHTHC
BnvpJnVgPWJzczpnvnWVWRGTrRTGmmBhRmBmThrmrf
dNlwjLNLlbLSjLQVdLdjjSTRHmRmTTNmmRRtfTTfhThs
FSSSqbVDQZzzPPPFZc
fTTrrBqwfDTWfTDrRNrnRjgPSpJPnnmp
PvHPbsvZlMtbbvbCLLMHtHZZjtgJRjSnJSpSpjRgRjggSRmn
VLHbCbVPLZvlvMhHCHlPHbLCqQQfdQTBddTWhDTBchQzQwBW
ZBHHfHWLfLqjfLjHZBSDwHDWhvpFCQqNpvVNVNQCFPJvPQCF
zMrtclbdvFPSpdFp
rGMMnnGgsbzblRnlSrzSgRRDwLWjLjTTDBZmfLwZmDBf
rfJVfnztTfZFMfZq
cRGcdddPRbHvHCRHRmShqFrPSTmTPjSjZT
NvRHGGdNLrNJsDtL
dgggppRqnlnjbbjRwzmtHb
ZTPhrVvMZhrVQPZNCMZQjjbFtjmswwFtzVmLGbmL
rZhPCPPcNPNTMfvZPCvhMPSBpgSqSqfpDgJJggdwWJgg
TsgFbTQSZZsSJFThhggQFshpMSzRRRDPwwzPwDftRDrczLww
GCmnjCNjmlVdHNfdGNjMwPrrLwLcMcrcRHzMHM
nmmBnmlWlVWvjnNlpfFQbZsFpQhgJFpB
jfpdTTqqJpDfQrscgsDh
mNFmHHtVsVQrsllG
CmFtZPHNzzpBspzpLBqw
RsgJsjsZbTjmZZMMJPtCSPPDhCSrDhrjhC
lBwzHLQddZlLQnChGdtDhrnqhq
fLHpNQQpwvHllQVQHNNfHpzZMFWbccmTcbJMcFsWTRVbsTWF
DhHFMRDDmLmshTmSCpSWZVNHtCCNnW
fJJPBvlvlBflQQfQtNWtQzpSWZNtCZpN
vPJbdvBfqhqhShTFMs
lttWShphLtWWGppCQLlwZTHZHmfjjvwvHFmw
MFNssMMDVzrrnTmvJTHwJmZmZr
MsBznNznRgzzncFBLQQGGBBtdhdGpWPp
JFmvMWBmBlbBCZrZrH
DjRRjgffgjqwsDqrcHNNbCZbCbbN
sSffwRVjjDVzfjSjswDSQLdPrvGvFMmMJMMJmnWzGL
RbvwgbTVgzGTrhvWDmNDGJfCDffMmNBD
PjSlqldccqFLSqQLCpJfpWDCcCJZBpMD
jlHdPlqqSnjHggWgwrhzRz
WsJnWnmCJpTnLWmJLCSDVVmhNjRbrDRgrgZRhrjrhhgdZN
QlFfQBqlBwBqBffMFPsbgdgwsrZjrPNrNs
MvvvlHBcfBtvffGBcMqqqLpmLsJtpSLSCWCVCnnTzJ
QSRRwSWPhWhwwHbtsNGZNRNZTgRcmc
nfvDCrnnDvJJDDVMLNgtsZmZsVtZGgqmGp
nCngDlFfvvJLnCJJLMFMClHBBzQzwzbhlQHHPllbBhdb
qNFzGFFFnGGDJnzSdFdzjdvmCvzPzzRmgCmMmtgvcmtV
sQlhpLpWQQZpQZpQrBlLsLLCRggtMgVbbtggBmVVmbmBVm
ppQHRflRSGTdHSGN
BgTnWbbwPgPWgPGfGlWfdFFFlsSF
QvDNDZRMjCNZcHZZDQFShGFpBhdRsGpLsFRd
rrqvNcHjBcJgrTzbPVVz
pCjdsSdCljdBlpJpdmjHFHmDfTDTwgfgcw
WQrWnGzhNZzWfcnHfBwnvwHc
RLGNGQhWZhMrZNtBpsLPsVJbPLVb
TRDDSzNhHNHfSppCCMZsMMssdgZvfZ
GbGlctGqrBqVtrtLGtmqTtssPdMZcCZMnMvCZgdddgsP
BQqbVVVqLtBqWbTlmLBhQjFhJDRpHhzpJSJDNF
FTHTnPSmFqSPCTVDhZRCZDhjDRsV
blbLcBgMgMlrcLMbzrlBLZjRVsZZfNVfVfJsbNqDDh
tLtLQgzWMzSnHmPSWdqW
DnwmFcpmpGqFwCwmfCDMZsNVVMdlVrsNsczrrl
ghtBPJbHtWPbJPdBSPMzsrTTzZMTTMSNzrVZ
bPdjjhtPPJjjBQJJdbhRgJhLCnfLGGmLnGLpLfDRGGfpqG
HzzZZjCLjjZCmVQppssbGpmcTGgp
nMnJPqJlMPqBJllSJvlMSDGGBcgBcsfBhTpsFhgTTfFQ
drSRqnSqJDcSJrMJnRvCHtWZtjZZrHtVjrwZLj
mDJjmQggstPvDWJgQWJgPPmNFFLZpphZNlNpZLhSLvLZNf
rzzfTczCzCdBwddcCCHFpHFMNHZSHMFrHNFp
bcfzdGnCtVsVtsWn
CMfCfGfwbMMLdTMH
lqzDgLWqQWhQNWnlQqHTFFFFchbddTRbSddd
WnzDNZmPZmgglNCrvjrtrLffrJZJ
BllCMzjjlBHCCllHvljCMhbrQbTVTrvprTqrFWFFvF
ZNwfNLRnfwtRtNwqThVFpwWVJrbh
LnRnpNRSLcpZZNtmZmjgGgGMPBHCmMzsmsHM
MRMPpwWTLMMMMLLRMPbvfQZCnqQnqvWZWGqGDQ
gJslgmlmSZqssGGbnV
mJFSmmjcdmgJhdgrdjcdjwbRwpMwpHBRHrztwtRMRL
CccctqnQVvQcrsFwznmnrBrs
LLPThTSjPdJjLTtMBrsDmbbsBwbswGMF
jLSLWPLlTlgSlgSghdvtQZvqvWRcCpHVCqWp
DmggMZCDbdNrttnbPn
cclqSqGjSBwLqswNrFNzffFfNPsCsd
jGpLBcwlQBSpLjSQLvjRTZHTCRRDggRTHhmvmV
LWzrnwZFnzQlWQZndWFNvHBNDVBBNjplNjjVvV
RCSmPSCgsgTmCRqnsTCHvjJJpvBBSVSMpVjVMJ
sfgsRGnRTghQWWbLwfhF
JnJlTnDnwtWHMdJt
phNPDPVvPcPPvMRqVZRWMqRRWM
fGGScQDfssQzLbgT
rLVVdSSvVzQSRnzSRRnBQMJqDTjMsMsMqqPPhP
wmbWWFGVlGfptbhshsTmhqDPJJMm
CbWtWlCNpglgbtCWtFHdvNHVZnZZVnNVHHcc
qtRszqSZVnTCVwwLJpcgppLHqp
MPvBGNnDWMGMPDvGBPvMBFvpppjHwHpJJJJJFjjwLcjLJg
hBfPNvGvhDnvMPdslCZCtVsRRCSZff
HgvgvLNDcCcNBPDDvNNBzLNBnnhQqwRnRLQhWRhhWssqsnZZ
FlfprJzttrrTlGpbJVQhRwwbnbsRWVnZss
FGmdJrFdJddmjFfTttpfftlDvHSDgCDHgvSgHPcBzMHSzj
dgHhCJbBbwgNcVzlvzvzQVzH
RZPjRRRDPnjrjSnfFrDsnvzWVlzLfGzvBVWccclcGl
nDDZPjjRnZmFstnZBrsZgtCgbtMdqbJdNqMdgdwg
LvdrGnDBGBGrvrGLJtdrmmcfClqllsqlmjsLmcQC
VpbwbwTSVVwpzZMRSVmsjDqCclclNZCjNjQq
PpzWwRMwTwFpwtJBHnhtdnDvvP
GCJSClmwzmSgrpPpSpgcrS
bjLpsspHMHvBTsDLjTDtbTBFRFrQQQPcgQgcFcFPcFcnPs
pBTBvqBHbDjHfJqlGlzWJGllWC
GgbGNjQGzzQvFcFfRFmFJbff
pHDSLLCqpqppPZVSCSDdncRFwfmZcRWRJfncmRJn
LDTTLSSDSMQgvNTmzz
bLjgLVlJnjSJjgLgLjqqMcMfddftcCCnCccfQfsC
FRRmphNRmmdwPHpBPswDftDzMCfMcfWMMC
FrvdvrPPFHhNRPvFRmFZVbggqbGjJglJbjjTrVjJ
cSpdczpfRQQGNGGqvGHv
wPwFPhCFWbbmChwWmhFPsWrsDVwqVqZqvVVVvzGHDZqHqtZz
gbPznCbPmmWrWbWhJRpLnLRnTcScRSSLBd
pfwbrjTbDczbzbPcwTQbvWdWWTdddSldVHWSgHgn
mQmFQRFssJJJJthtJmBJmCHMdlWdnlltnHSVlSlgtnvM
BJBhRhRqRLRJNhJhBDwjzPPPcNQZrZrzDr
HTHwQJzwLZNcTwJtFRFHDtFvRFRFdr
jlMnMlbqqpMBvsWBVGRRGdSVgrDthFDddh
qvMjjMMpbPWqmMjnlpplpnWscNwLCQPzQLfzTzcZJfLQLPJz
gCTHTVdPdPvFfdCCSScZSZncMMCGGM
LNqLlssmWlLqzBRzWBwWBRRZdmJZcSSQDncnJtQcGDDDMd
jszNLwNWRRrzNbhbvHpdTjdvfH
NgDWhQmhfFlWFWlzDfhpBwfPBBdwwBJRBPqJJB
HHMCcHtGcMMbSGjScMGLSsRnqdJJlqqpCPJBRpBRdp
trMtLVtjjVtjttGctrLLTlZmgFzZTZzZNQmzZDNFhgFD
MBMtFzBnzSJJttSZBNNZfTsPqTqqqNZNPc
LlQDQLVCwCDbfRCmRGVjCwLLcqslNcNPgqgTPTsqNNsvqHqs
RfffRmdjnnzpndtr
nzpnDRDVwRRjVTSlTtlSSCQLDC
vgZHFBbbfCbvFbrFSSTqqcGtGTtBGQqS
HggMfPfbFZbPvNvgZZdHsnJjCzphzVwJjsmpmjMw
FgvgrgDpRDGTTWVDVd
zmbnNhHHQHshhhcbnHnnbBTMBMLLWLLGMqWLfzLLTV
mnnQHjcHHJmnNcHnnhmvZpRrCVZVZFrRJZVPFr
ZhpVhCSHbsCPbnmF
GBWGGrftTJlrfttQqsVPbPnGjDszFVncsG
qfMlQJWBrNWQfftqrQqBhRvdVVSNVSpZdvSZwZRS
mfzRQqMrtCwLMMZM
sbVbWgFdJCldtNRPNZ
FpggcgsjJccJVJJJcbbWWGrHnfmBnrHzfnfzvqrQmpfR
rWvbrfLnHHnzlFrFWWzJfRQQTwbwRQmQQCgQTwQBwg
MPsqcZpZdpjGMptpqCSsSwRNjQTBhgwhmgwgmmwgBm
SZqqqCDGMtpMFFDvFHfJlvDJ
RZnrZLZbLjRZwdlrnbLClCHMNCTlMMWBPPTHlT
SDgDzQDcfzJBNPssWCwSMN
fpQJwmmQmjhjGRbr
jQzqvLzvMTTQMMSS
VJlSrnBRZbbJbVnWWBlVWRCSNTMppMBMmmNPHMcNBmTMGmHp
CtDZJllRClbFzLtszhqFqS
VwcwzBdmMzJSdMBzzGndGSmlbqbgZgfblTJrTbrqZbTlTb
tRDvCvCRDCDWtPjCHjvDWTgZLfNqclZTTrNfZtLbLc
DDvPhhRRvjFjHPRjvVdMwhnpwBSGcwSGcc
LRtLJSNLdNLjNLRRNdRzVmVrggWmmmpVmpVvBBWFvm
wqfZnsMnhCnlfGPCCqwphJpWrFHBWHrvTrTHTH
ZsbGsflwGtjJbzLLjt
MjdFCjzsQFJQjzbmWHHLPPTfvsLfPZ
cNgpBgplzVvPTvmLPTVm
qNlqpcNtBzjQQCQqJjwJ
cczNGcgNhztlGPCCLDpBSpfVDpCJpSpBWs
nFqnwrRQrHnFHTMnZqQwMnmFsVsfbSSSJBSsfVVDfWVBWbVm
RRFZFMHjrQMjrHZqRjLdchdGtGjzDLgtzN
tzFmNfFNmFclrffhNtNlDWRSdFWnJCnRvRjvJvjD
LMgZgpZLMpPPPVPMMPbjbndWJSnWbWHWSCRbJD
BLwMpgMPgwQpTgTTpMVZMTftcGhhhtrhhcGGzQhsllCN
DWtHFWDHtwsWFHWDWwQblVgnllRBnjvBlVjRVbll
TzChCMGfNhZSLhLhzRRVvvjmczRgvRmv
pZSGpNJhCNGLSNLfMGJfJJQFrDDQDqHHgHpsWPFDttQW
qqccVQZWBVfjzzPVDMJtDtNccCgmtCCm
lvBRHBRGRDDCRmnmbD
SLFpGTSsTSsLFsHGvLvdGvdVwZBzZqqzjdQdZjVfrqwW
dLLwHLTWwjMLRZHCHZglDvjgvvNNDssSsmmN
nPfJPFbnhMJQfnpJGQJfQBBVssglsmsVVmBDsSqDgvNlgv
FbMrPPhrtGbJpnhJpJFFbbpJcwtdZzHZZwHCZdwctzTLcHTw
hCfzfLVbShRwRlBjdzmz
JZhrMMhZgvHFHJnHGgWdpmpnmlpRwBndRBwcBj
rMHFhgHZqtGvrWsbPfPNSbstTffN
dSwQQdSlHtlVQtqqrMZbFBRnBfZRMbDwMw
GTpcgLcvWvGvJzGpZGJpWpfmMWPBmDPRFPMFMmBfbFFW
hpcGhTCsGspLchGGzcpvZNStQHSSlNjjtlttlQsq
nJlhHlLlJGlRnMSsMLsnsSMwFHgwrTBFFwjBjppgpqfwqf
QmzzmbzNcZQQzDVZDcZQNDtzRqRBFTqTjjwpjBBNRrpBwfww
mvQPbQtttVVDzvzmRWCWLhSSslvWLGlW
nSpdSDPpRdrNBhMBMSJJ
bzCTcwzbczCZswMssczmCCgcqBmrQrrmBmqJBVBVNtrVttrq
cvzscgZzwTCsWsZvwTfzljDHnflMGMMndHFDdRPp
sDDnzLVnsMtjnDgfSSbfBjggbqBC
rlrlcppWcGdPrZNccJrSBSmCHqbSqCbSCgCHPq
NWcZgpFFgFpDFnDvtMDFzz
ZpczTjpZcnncHFDn
hSqqJWWWRRhlnFDVSFnQCB
qhNMWfqfMhJJRLRvppDzNZTpPTvwwD
rbSDCwPWwPVcSHcwHspNpNsddszmwGtswN
vBBfgQvBMvMvjLLlllqtBTNNTPPzTztTsRNPTt
vvQnjlMjjjqJqQLSrcDcPWbcHbDrDn
BphwqbwNsjsNsBdDjjhGpsGWzMMTWRTCflfDWSSzfzSlWf
cFnrrHrnZVZQQvFFVvZhRzTCWlSRMSWhZMWt
HvJVvggHvVrmQLqmNqLBbhsssw
NcBqSjGGBjNbZcHwhRVHPwPwPlZR
TvdtdtvFgHwWgdhn
tMJFTvtvFtrLJMHBqNNBzjrQcjqNmB
RrQfLfQvMFdQvLLQVfFcnbSbnwVSnbDslsbSDzDD
PHLjmHqqZCjPglsbwJbqnDbbJl
LNHLhjBHHGRfphphrtfh
FcdMchqcgdchqcfcNWWghNrRrVRjGHFjDJmGFDrRVGJD
zQtvBvtbQzpSzBptzbBznnwZJRDRVmVnGRGrDrHjZGrr
HLspTpQLBqWqLqcNgM
rnqbSSQhsshFqQQnsPSnhbnrzHzzHLNvLHCLFCvVJTlNLJTz
fcffljGBwgmwwftzNdCCdvLvtHNLTH
BjWgMGjjWZMwjjgwgMWpwPlnnqSnqQhnrbPMRDsqQs
hRJhFdNJcrbqbbMF
qZlTTgTvlHnqvllPssQrgQcQwscrQMsf
CClpSnTllZnZCDLHnlNzGSRmRthVmmzqJzhd
DLzSMtDLtzmmlDlMlMDbcrcTDqFvVvVqqTbD
dnZshHpZRChgnszRwRZCpTqqTcwVqFcrVqcBVPqVTr
snRRRdJsZgphCWlWtmJSjWWzjt";

			return ParseData(data);
		}

		private static List<Rucksack> ParseData(string data)
		{
			var rows = data.Split("\r\n");

			var rucksacks = new List<Rucksack>();

			foreach (string row in rows)
			{
				var characters = row.ToCharArray();
				var rucksack = new Rucksack
				{
					CompartmentOneItems = characters.Take(characters.Length / 2).ToList(),
					CompartmentTwoItems = characters.Skip(characters.Length / 2).ToList()
				};
				rucksacks.Add(rucksack);
			}

			return rucksacks;
		}

		class Rucksack
		{
			public List<char> CompartmentOneItems;
			public List<char> CompartmentTwoItems;
		}
	}
}

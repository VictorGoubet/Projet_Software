using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Solution.Problem2;
using System.Linq;

namespace TestSolution

{
    [TestClass]
    public class Test_Probleme2
    {

        [TestMethod]
        public void test_wordCounting()
        {
            Dictionary<string, string> input_Text = new Dictionary<string, string>
            {
                ["firstDocument"] = "chien chat",
                ["secondDocument"] = "chat licorne"
            };

            #region Reduce function
            KeyValuePair<string, int> Reduce_Text(string key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new KeyValuePair<string, int>(key, sum);
            }
            #endregion

            #region Map function
            IList<KeyValuePair<string, int>> Map_Text(string key, string value)
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                foreach (var word in value.Split(' '))
                {
                    result.Add(new KeyValuePair<string, int>(word, 1));
                }
                return result;
            }
            #endregion

            MapReduce<string, string, string, int, string, int> mapreduce_text = new MapReduce<string, string, string, int, string, int>(Map_Text, Reduce_Text);
            IEnumerable<KeyValuePair<string, int>> res_text = mapreduce_text.get_result(input_Text);

            Dictionary<string, int> theorycal_res = new Dictionary<string, int> {["chien"] = 1, ["chat"] = 2, ["licorne"] = 1};
            
            Assert.IsTrue(Enumerable.SequenceEqual(theorycal_res, res_text));
        }

        [TestMethod]
        public void test_columnsSum()
        {
            Dictionary<string, int[,]> input_mat = new Dictionary<string, int[,]>
            {
                ["firstmat"] = new int[,] { { 1, 2, 3 }, { 5, 9, 8 } },
                ["secondMat"] = new int[,] { { 8, 6, 3 }, { 10, 5, 3 } }

            };

            #region Map function
            IList<KeyValuePair<int, int>> Map_mat(string key, int[,] value)
            {

                IList<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();
                for (int j = 0; j < value.GetLength(1); j++)
                {
                    int sum = 0;
                    for (int i = 0; i < value.GetLength(0); i++)
                    {
                        sum += value[i, j];
                    }
                    res.Add(new KeyValuePair<int, int>(j, sum));
                }
                return res;
            }
            #endregion

            #region Reduce function
            KeyValuePair<int, int> Reduce_mat(int key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new KeyValuePair<int, int>(key, sum);
            }
            #endregion

            MapReduce<string, int[,], int, int, int, int> mapreduce_mat = new MapReduce<string, int[,], int, int, int, int>(Map_mat, Reduce_mat);
            IEnumerable<KeyValuePair<int, int>> res_mat = mapreduce_mat.get_result(input_mat);

            Dictionary<int, int> theorycal_res = new Dictionary<int, int> { [0] = 24, [1] = 22, [2] = 17 };
            Assert.IsTrue(Enumerable.SequenceEqual(theorycal_res, res_mat));
        }
    }
}

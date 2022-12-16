namespace BasicTest;

[TestClass]
public class PermutationTest
{
    [TestMethod]
    public void TestPwdDecryptByPermutation()
    {
        Permutation.PwdDecryptByPermutation("abb", "", "abcde".ToCharArray());
        Assert.IsTrue(true);
    }
}

namespace Basic
{
    public static class Permutation
    {
        /**
            根据password的长度，穷举所有可能的字母组合。（遍历树）
        **/
        public static void PwdDecryptByPermutation2(string password)
        {
            
        }

        /**Lesson07
            @Desc:
                To decrypt 4 digits character password. The character is in the range of [a, e]
                exp: f(password.len) = f(password.len-1) + compare
                condition: password[0] == Map[i]
                finish:password.length ==0
            @Param:
                password - user's password
                result - save decoded characters
                map - characters range
            @return:
                void
        **/
        public static void PwdDecryptByPermutation(string password, string result, char[] map)
        {

            if (password.Length == 0)
            {
                Console.WriteLine(result);
                return;
            }

            var chr = password.Substring(0, 1);
            for (int i = 0; i < map.Length; i++)
            {
                if (chr == map[i].ToString())
                {
                    result += chr;
                    password = password.Substring(1);
                }
                else
                    continue;

                PwdDecryptByPermutation(password, result, map);
            }
        }
    }
}
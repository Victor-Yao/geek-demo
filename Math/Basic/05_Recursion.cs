namespace Basic
{
    public static class Recursion
    {
        /**
           @Desc:
               Recurse to find get all combinations of an integer that is divisible.
               exp:        div / k
               condition:  div % k == 0
               finish:     div / k == 1
           @Param:
               div - divisor
               list - save results
           @return:
               void
       **/
        public static void GetDivCombinations(int div, List<int> list)
        {
            if (div == 1) //finish
            {
                if (list.Count == 1)//divied by itslef
                    list.Add(1);

                Console.WriteLine($"solution: {string.Join(" ", list.ToArray())}");
                return;
            }

            for (int i = 1; i < div + 1; i++)
            {
                if (div % i == 0)
                {
                    if (list.Contains(1) && i == 1)//remove redundancy 1; avoid to infinite loop
                        continue;

                    var newList = new List<int>(list);
                    newList.Add(i);
                    GetDivCombinations(div / i, newList); //recursive expression
                }
                else
                    continue;
            }

        }
    }
}
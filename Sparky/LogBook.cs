namespace Sparky
{
    public interface ILogBook
    {
        void Message(string message);
        bool LogToDb(string message);
        bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);

        string MessageWithReturnStr(string message);

        bool LogWithOutputResult(string str, out string outputStr);
    }

    public class LogBook : ILogBook
    {
        public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
        {
            if(balanceAfterWithdrawal >= 0)
            {
                Console.WriteLine("Success");
                return true;
            }

            Console.WriteLine("Failure");
            return false;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithOutputResult(string str, out string outputStr)
        {
            outputStr = "Hello " + str;
            return true;
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public string MessageWithReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    //public class LogFakker : ILogBook
    //{
    //    public void Message(string message)
    //    {
    //    }
    //}
}

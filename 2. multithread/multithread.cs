// The Send method is executed simultaneously in X threads. 
// Is everything correct in this method, and if not, how can the errors be fixed?

public struct Account
{
    public int UserId;
    public string UserName;
    public double Amount;
}

public async Task Send(Account a, Account b, double amount)
{
    lock (a)
    {
        lock (b)
        {
            if (a.Amount - amount >= 0)
            {
                a.Amount -= amount;
                b.Amount += amount;
            }
        }
    }
}
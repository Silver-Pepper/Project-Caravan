using System.Collections;
using System.Collections.Generic;

/// <summary>
/// store informations of deposits and loans
/// </summary>
public class DepositAndLoan
{
    //just a number that can be used to assign an unique id for a member of the class
    public static int IdCount { get; set; } = 1;

    //an unique identifier of member in the class
    public  int Uid { get; private set;}
    public float Amount { get; set; }
    public int DepositOrLoanMonth { get; set; }
    public GameDateTime StartDateTime { get; set; }
    public float InterestRate { get; set; }
    public GameDateTime NextDateTimeForUpdate { get; set; }
    public GameDateTime MaturityOrOverdueTime { get; set; }

    public DepositAndLoan() 
    {
        Uid = IdCount;
        IdCount++;
    }

}

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// store information about the bank, name, interest rate etc. 
/// </summary>
public class Bank
{
    public string BankName { get; set; }

    //deposit

    public float DepositInterestRate1 { get; set; } = 1.05f;

    public float DepositInterestRate2 { get; set; } = 1.07f;

    public int LeastMonthForDepositInterestRate2 { get; set; } = 12;

    //loan 
    public float LoanInterestRate1 { get; set; } = 1.08f;

    public float LoanInterestRate2 { get; set; } = 1.06f;

    public float LeastMoneyForLoanInterestRate2 { get; set; } = 1000f;

    //overdue loan
    public float OverdueRate1  { get; set; } = 1.10f;

    public float OverdueRate2 { get; set; } = 1.25f;

    public float OverdueRate3 { get; set; } = 1.50f;

    public int LeastMonthForOverdueRate2 { get; set; } = 1;

    public int LeastMonthForOverdueRate3 { get; set; } = 2;
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// store informations of the bank account of an entity
/// </summary>
public class BankAccount
{
    //Data
    public Bank Bank { get; set; }
    public float DemandDeposit { get; set; }
    public List<DepositAndLoan> TermDepositList { get; set; }
    public List<DepositAndLoan> LoanList { get; set; }
    public List<DepositAndLoan> OverdueLoanList { get; set; }

    /// <summary>
    /// Call this method daily to update account information
    /// </summary>
    public async static void AccountDailyUpdateAsync(BankAccount bankAccount)
    {
        //每日0时检查 定存 贷 逾期贷的list 如果到一个月就进行一次结算
        //先月结 再变动
        await Task.Run(() =>
        {
            bankAccount.DepositAndLoanMonthlyUpdate(bankAccount.TermDepositList);
            bankAccount.DepositAndLoanMonthlyUpdate(bankAccount.LoanList);
            bankAccount.OverdueLoanMonthlyCalculation(bankAccount.OverdueLoanList);
        });

        bankAccount.CheckTermDeposit(bankAccount);
        bankAccount.CheckForOverdue(bankAccount.LoanList, bankAccount.OverdueLoanList);

    }

    public void DepositAndLoanMonthlyUpdate(List<DepositAndLoan> list)
    {
        if (list == null)
        {
            return;
        }

        foreach (var depositOrLoan in list)
        {
            GameDateTime updateTime = depositOrLoan.NextDateTimeForUpdate;

            if (GameDateTime.Compare(updateTime, GameTime.Now) >= 0)
            {
                depositOrLoan.Amount *= depositOrLoan.InterestRate;
                depositOrLoan.NextDateTimeForUpdate = GameDateTime.TimeLapseMonth(1, updateTime);
            }
        }
    }


    public void OverdueLoanMonthlyCalculation(List<DepositAndLoan> overdueLoanList)
    {
        if (overdueLoanList == null)
        {
            return;
        }

        foreach (var overdueLoan in overdueLoanList)
        {
            float rate = overdueLoan.InterestRate;
            //calculate new loan amount
            GameDateTime updateTime = overdueLoan.NextDateTimeForUpdate;

            if (GameDateTime.Compare(updateTime, GameTime.Now) >= 0)
            {
                overdueLoan.Amount *= rate;
                overdueLoan.NextDateTimeForUpdate = GameDateTime.TimeLapseMonth(1, updateTime);
            }

            //update overdue rate 
            if (rate == Bank.OverdueRate1)
            {
                GameDateTime rate2Time =
                GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(
                overdueLoan.DepositOrLoanMonth + Bank.LeastMonthForOverdueRate2, overdueLoan.StartDateTime));

                if (GameDateTime.Compare(rate2Time, GameTime.Now) >= 0)
                {
                    overdueLoan.InterestRate = Bank.OverdueRate2;
                }

            }

            else if (rate == Bank.OverdueRate2)
            {
                GameDateTime rate3Time =
                GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(
                overdueLoan.DepositOrLoanMonth + Bank.LeastMonthForOverdueRate3, overdueLoan.StartDateTime));

                if (GameDateTime.Compare(rate3Time, GameTime.Now) >= 0)
                {
                    overdueLoan.InterestRate = Bank.OverdueRate3;
                }

            }

        }
    }

    /// <summary>
    /// check if the loan is overdue
    /// </summary>
    /// <param name="loanList">loan list</param>
    /// <param name="overdueLoanList">overdue loan list</param>
    public void CheckForOverdue(List<DepositAndLoan> loanList, List<DepositAndLoan> overdueLoanList)
    {
        if (loanList == null)
        {
            return;
        }

        List<int> overdueLoanUidList = new List<int>();

        foreach (var loan in loanList)
        {
            GameDateTime overedueTime =
            GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(loan.DepositOrLoanMonth, loan.StartDateTime));

            if (GameDateTime.Compare(GameTime.Now, overedueTime) <= 0)
            {
                loan.InterestRate = Bank.OverdueRate1;
                overdueLoanList.Add(loan);
                overdueLoanUidList.Add(loan.Uid);
            }

        }

        //remove the overdue loan from loan list
        foreach (int uid in overdueLoanUidList)
        {
            loanList.Remove(loanList.FirstOrDefault(overdueLoan => overdueLoan.Uid == uid));
        }

    }

    /// <summary>
    /// check if the term deposit reach maturity, if mature, it will be added to the demand deposit
    /// </summary>
    /// <param name="termDepositList">list of term deposit</param>
    /// <param name="demandDeposit">demand deposit in bank</param>
    public void CheckTermDeposit(BankAccount bankAccount)
    {
        if (bankAccount.TermDepositList == null)
        {
            return;
        }
        List<int> matureDepositUidList = new List<int>();

        foreach (var termDeposit in bankAccount.TermDepositList)
        {
            if (GameDateTime.Compare(GameTime.Now, termDeposit.MaturityOrOverdueTime) <= 0)
            {
                bankAccount.DemandDeposit += termDeposit.Amount;
                matureDepositUidList.Add(termDeposit.Uid);
            }

        }

        //remove the mature deposit from deposit list
        foreach (int uid in matureDepositUidList)
        {
            bankAccount.TermDepositList.Remove(bankAccount.TermDepositList.FirstOrDefault(matureDeposit => matureDeposit.Uid == uid));
        }
    }

    /// <summary>
    /// rewrite this method when working on bank interaction
    /// </summary>
    /// <param name="loan"></param>
    /// <param name="amount"></param>
    public void Repayment(DepositAndLoan loan, float amount)
    {
        loan.Amount -= amount;

        if (loan.Amount <= 0)
        {
            this.LoanList.Remove(this.LoanList.FirstOrDefault(Ln => Ln.Uid == loan.Uid));
        }

    }

    /// <summary>
    /// rewrite this method when working on bank interaction
    /// </summary>
    /// <param name="loan"></param>
    /// <param name="amount"></param>
    public void Withdrawal(BankAccount bankAccount, float amount)
    {
        if (bankAccount.DemandDeposit <= amount)
        {
            return;
        }
        bankAccount.DemandDeposit -= amount;
    }

    /// <summary>
    /// Get the total money of the bank account
    /// </summary>
    /// <param name="bankAccount"></param>
    /// <returns>total money</returns>
    public float TotalMoneyInBank(BankAccount bankAccount)
    {
        //initialize variable
        float MoneyInBank = DemandDeposit;

        foreach (var termDeposit in bankAccount.TermDepositList)
        {
            MoneyInBank += termDeposit.Amount;
        }

        foreach (var loan in bankAccount.LoanList)
        {
            MoneyInBank += loan.Amount;
        }

        foreach (var overdueLoan in bankAccount.OverdueLoanList)
        {
            MoneyInBank += overdueLoan.Amount;
        }

        return MoneyInBank;
    }

    /// <summary>
    /// make a deposit, this method does not used the demand deposit of the account holder
    /// </summary>
    /// <param name="depositAmount">deposit amount</param>
    /// <param name="depositMonth">amount of time deposited</param>
    /// <param name="depositStartDateTime">deposit start time</param>
    public void Deposit(float depositAmount, int depositMonth, GameDateTime depositStartDateTime, Bank bank)
    {
        float depositInterestRate;
        GameDateTime updateDateTime = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(1, depositStartDateTime));
        GameDateTime maturityTime = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(depositMonth, depositStartDateTime));

        if (depositMonth >= bank.LeastMonthForDepositInterestRate2)
        {
            depositInterestRate = bank.DepositInterestRate2;
        }

        else
        {
            depositInterestRate = bank.DepositInterestRate1;
        }

        TermDepositList.Add(new DepositAndLoan()
        {
            Amount = depositAmount,
            DepositOrLoanMonth = depositMonth,
            StartDateTime = depositStartDateTime,
            InterestRate = depositInterestRate,
            NextDateTimeForUpdate = updateDateTime,
            MaturityOrOverdueTime = maturityTime,
        }); ;
    }

    /// <summary>
    /// make a loan
    /// </summary>
    /// <param name="loanAmount">loan amount</param>
    /// <param name="loanMonth">amount of time loaned</param>
    /// <param name="loanStartDateTime">start date of the loan</param>
    public void Loan(float loanAmount, int loanMonth, GameDateTime loanStartDateTime, Bank bank)
    {
        float loanInterestRate;
        GameDateTime updateDateTime = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(1, loanStartDateTime));
        GameDateTime overdueTime = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseMonth(loanMonth, loanStartDateTime));

        if (loanAmount >= bank.LeastMoneyForLoanInterestRate2)
        {
            loanInterestRate = bank.LoanInterestRate2;
        }

        else
        {
            loanInterestRate = bank.LoanInterestRate1;
        }

        LoanList.Add(new DepositAndLoan()
        {
            Amount = -loanAmount,
            DepositOrLoanMonth = loanMonth,
            StartDateTime = loanStartDateTime,
            InterestRate = loanInterestRate,
            NextDateTimeForUpdate = updateDateTime,
            MaturityOrOverdueTime = overdueTime
        });
    }

}

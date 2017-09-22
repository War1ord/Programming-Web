use BudgetManager
go
--use master
--ALTER DATABASE BudgetManager 
--SET SINGLE_USER WITH ROLLBACK IMMEDIATE 
--drop database BudgetManager
go
select * from Imports where Id = 56 --truncate table Imports
select * from BankTransactions where importid = 56 --truncate table BankTransactions
select cast(errorxml as xml) errorxml, * from dbo.Errors as e
	where cast(e.ErrorDate as date) = cast(getdate() as date) -- Today
	order by created desc -- truncate table Imports

select * from BankTransactions where BankTransactionType = 2 and Amount > 10 order by TransactionDate, TransactionSequence
select * from BankTransactions where BankTransactionType = 1 order by TransactionDate, TransactionSequence
select sum(amount) from BankTransactions where BankTransactionType = 2 and Amount > 10

select t.Description, t.TransactionDate, t.Amount, t.Balance from BankTransactions t group by t.Description, t.TransactionDate, t.Amount, t.Balance having count(id)>1

select * from dbo.BankTransactions as bt
inner join (select t.Description, t.TransactionDate, t.Amount, t.Balance from BankTransactions t 
			group by t.Description, t.TransactionDate, t.Amount, t.Balance
			having count(id)>1 
) as data on bt.Amount = data.Amount and bt.Balance = data.Balance and bt.Description = data.Description and bt.TransactionDate = data.TransactionDate
order by bt.TransactionDate, bt.Amount

select * from dbo.__MigrationHistory

----------------------------------------------------------------------------------------------------------------------------------------------------------
-- Bank Trasactions Selects
select * from dbo.BankTransactions	-- truncate table dbo.BankTransactions
select * from dbo.Imports			-- delete from dbo.Imports

----------------------------------------------------------------------------------------------------------------------------------------------------------
-- Budget Selects
select * from dbo.BudgetTemplateItems as bi -- delete from dbo.BudgetItems
select * from dbo.BudgetRowItems as bi -- truncate table dbo.BudgetRowItems

----------------------------------------------------------------------------------------------------------------------------------------------------------
-- User Select
select * from dbo.Users as u

----------------------------------------------------------------------------------------------------------------------------------------------------------
-- rules 
select * from dbo.BankTransactions as bt left join dbo.BankTransactionGroups as btg on bt.TypeGroupId = btg.Id
select * from dbo.BankTransactionRules as btr

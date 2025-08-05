use luckys
go

-- get brands and the current products that are offerred for the brand even if they are not assigned a category where price is greater then 75. Calculate the total value of inventory per item.
select BrandName, Description, Quantity, Price, CategoryName, CategoryDescription, Quantity * Price as TotalValue
from Brand br
inner join Products p on br.BrandId = p.BrandID
left join Categories cat on p.CategoryID = cat.CategoryID
where price > 75
order by BrandName


-- Total  the quantity of items in inventory by brand and category
select BrandName, CategoryName, sum(Quantity) TotalItems
from Brand br
inner join Products p on br.BrandId = p.BrandID
left join Categories cat on p.CategoryID = cat.CategoryID
group by br.BrandName, CategoryName
order by BrandName

-- Calculate total sales per item.
WITH TotalSales AS (	
	select ProductID, sum(Quantity) as TotalSales
	from CustomerOrderItems
	group by ProductID

)
select p.ProductName,
		isnull(totalSales, 0) as 'Total Sales',
		CASE
			WHEN isnull(totalSales, 0) >= 50 THEN 'High Sales'
			WHEN isnull(totalSales, 0) >= 25 THEN 'Medium Sales'
			ELSE 'Low Sales'
		END as 'Sales Rating'
FROM Products p
left join TotalSales on p.ProductID = TotalSales.ProductID
order by [Sales Rating] desc


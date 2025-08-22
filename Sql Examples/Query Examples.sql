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

-- categories that have more then 15 products in inventory
select CategoryName, sum(Quantity)
from products prod
inner join Categories cat on Prod.CategoryID = cat.CategoryID
group by CategoryName
having sum(Quantity) > 15

--  roll up to show subCategory totals and Category totals
select CategoryName, SubCategoryName, sum(Quantity) Quantity
from products prod
inner join Categories cat on prod.CategoryID = cat.CategoryID
inner join SubCategories sub on sub.SubCategoryID = prod.SubCategoryID
group by rollup(CategoryName,SubCategoryName);

-- validate grand total in rollup
select sum(quantity) as TotalQuantity
from Products

-- check that every products category has a name in the Categories table
select *
from Products
where CategoryID not in (select CategoryID from Categories) 


select CategoryName, SubCategoryName, sum(Quantity) Quantity
from products prod
inner join Categories cat on prod.CategoryID = cat.CategoryID
inner join SubCategories sub on sub.SubCategoryID = prod.SubCategoryID
group by cube(CategoryName,SubCategoryName);
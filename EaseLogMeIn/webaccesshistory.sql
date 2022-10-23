
SELECT * FROM WebsiteAccessLog L
JOIN Employee E ON E.UniqueId = L.UserId
JOIN Website W ON W.UniqueId = L.WebsiteId
where l.WebsiteId ='8bba4b9f-aed4-4a04-9538-5ffd13af46fb'
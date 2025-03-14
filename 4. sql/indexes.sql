-- You have a service for tourists with a huge database of tours that you sold to customers. 
-- Once you realized that your DB slowed down and found that those queries are running very slow:

SELECT
    [DateStart],
    [DateEnd]
FROM
    [Tours]
WHERE
    [DateStart] < SYSUTCDATETIME()
        AND
    [CityId] = 2345
        AND
    [Status] = 'A'
    
-- To keep simplicity suppose that there are the only slow queries and you should optimize only them. 
-- You also found that there are completely no indexes for table tours. 
-- What indexes should you create to get the fastest performance from such queries?
SELECT  *
FROM	family;


SELECT '--------';


SELECT  ID, FAMILY_NAME
FROM	family;



SELECT '--------';


SELECT  ID, FAMILY_NAME, FATHER_NAME, MOTHER_NAME
FROM	family
WHERE	ID = 2;



SELECT '--------';


SELECT  ID, FAMILY_NAME, FATHER_NAME, MOTHER_NAME
FROM	family
WHERE	FAMILY_NAME  LIKE   '%mit%';


